using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickBreaker
{
    public class PaddleController : Component, IUpdatable
    {
        public float speed = 450f;
        public bool ballAttached = true;

        private BallController ballController;

        public override void onAddedToEntity()
        {
            base.onAddedToEntity();
            this.ballController = Core.scene.findEntity("ball").getComponent<BallController>();
        }
        public void update()
        {
            if (ballAttached && Input.isKeyPressed(Keys.Space))
            {
                float xDir = Nez.Random.nextFloat();
                xDir = Nez.Random.choose(xDir, xDir * -1);

                Vector2 dir = new Vector2(xDir, -1);
                this.ballController.SetMoveDir(dir);
                ballAttached = false;
            }

            var moveDir = Vector2.Zero;

            if (Input.isKeyDown(Keys.Left))
            {
                moveDir.X = -1f;
            }
            else if (Input.isKeyDown(Keys.Right))
            {
                moveDir.X = 1f;
            }
            
            
            var newPosition = entity.position + (moveDir * speed * Time.deltaTime);
            float halfWidth = entity.getComponent<Sprite>().width / 2;

            //adjust position to keep paddle in bounds
            newPosition.X = Mathf.clamp(newPosition.X, halfWidth, Core.graphicsDevice.Viewport.Width - halfWidth);

            entity.position = newPosition;
        }
    }
}
