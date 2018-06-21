using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickBreaker
{
    public class BallController : Component, IUpdatable
    {
        public float speed = 300f;
        public float speedIncreasePerPaddleHit = 25f;
        public float maxSpeed = 500f;

        private Vector2 _moveDir = Vector2.Zero;
        private PaddleController _paddleController;
        private Entity _paddle;
        private bool _gameOver = false;

        public override void onAddedToEntity()
        {
            base.onAddedToEntity();
            _paddle = Core.scene.findEntity("paddle");
            _paddleController = _paddle.getComponent<PaddleController>();
        }
        public void update()
        {
            if (_gameOver)
            {
                return;
            }

            if (_paddleController.ballAttached)
            {
                this.UpdateAttachedPosition();
                return;
            }

            var newPosition = entity.position;
            if (_moveDir != Vector2.Zero)
            {
                if (_moveDir.Y >= -0.15f && _moveDir.Y <= 0.15f)
                {
                    Debug.log("vertical direction too shallow, adjusting ...");
                    _moveDir.Y = Nez.Random.choose(-0.5f, 0.5f);
                }

                var moveDelta = _moveDir * speed * Time.deltaTime;

                CollisionResult paddleCollisionResult;
                CollisionResult brickCollisionResult;

                if (entity.getComponent<Collider>().collidesWith(_paddleController.getComponent<Collider>(), moveDelta, out paddleCollisionResult))
                {
                    //moveDir.X can be any value between -1 and 1 with 0 at the center of the paddle.
                    var distanceFromCenter = paddleCollisionResult.point.X - _paddle.position.X;
                    _moveDir.X = (distanceFromCenter * 2) / _paddle.getComponent<Sprite>().width;

                    _moveDir.Y = -1f;

                    if (speed <= maxSpeed)
                    {
                        speed += this.speedIncreasePerPaddleHit;
                    }
                }
                else if (entity.getComponent<Collider>().collidesWithAny(out brickCollisionResult))
                {
                    if (brickCollisionResult.collider.entity.tag == Game1.BRICK_TAG)
                    {
                        _moveDir = Vector2.Reflect(_moveDir, brickCollisionResult.normal);
                        brickCollisionResult.collider.entity.destroy();
                    }
                }

                newPosition += moveDelta - paddleCollisionResult.minimumTranslationVector;

                newPosition.X = Mathf.clamp((int)newPosition.X, 0, Core.graphicsDevice.Viewport.Width);
                newPosition.Y = Mathf.clamp((int)newPosition.Y, 0, Core.graphicsDevice.Viewport.Height);

                entity.position = newPosition;

                if (entity.position.Y >= Core.graphicsDevice.Viewport.Height)
                {
                    Debug.log("you died");
                    _gameOver = true;
                    Core.startSceneTransition(new WindTransition(() => new Scenes.LevelSelect()));
                }

                if (entity.position.Y <= 0)
                {
                    _moveDir.Y = 1f;
                }

                if (entity.position.X <= 0 || entity.position.X >= Core.graphicsDevice.Viewport.Width)
                {
                    _moveDir.X *= -1f;
                }
            }
            entity.position = newPosition;
        }

        public void SetMoveDir(Vector2 direction)
        {
            direction.Normalize();
            _moveDir = direction;
        }

        public void UpdateAttachedPosition()
        {
            if (!_paddleController.ballAttached)
            {
                return;
            }

            float ballHeight = this.getComponent<Sprite>().height;
            float paddleWidth = _paddle.getComponent<Sprite>().width;
            float paddleHeight = _paddle.getComponent<Sprite>().height;

            this.entity.position = new Vector2(_paddle.position.X, _paddle.position.Y - paddleHeight + (ballHeight / 2));
        }
    }
}
