using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickBreaker.Scenes
{
    public abstract class LevelBase : Scene
    {
        protected Entity paddle;
        protected Entity ball;

        private bool levelEnded;
        private int bricksAdded = 0;

        public override void initialize()
        {
            base.initialize();
            this.AddPaddleAndBallToScene(this);
            this.addRenderer(new DefaultRenderer());
        }

        public override void update()
        {
            if (this.levelEnded)
            {
                return;
            }

            base.update();            

            if (this.findEntitiesWithTag(Game1.BRICK_TAG).Count == 0)
            {
                this.levelEnded = true;
                Debug.log("you win!");
                this.destroyAllEntities();
                Core.startSceneTransition(new FadeTransition(() => new LevelSelect()));
            }
        }

        private void AddPaddleAndBallToScene(Scene scene)
        {
            var paddleTexture = Core.content.Load<Texture2D>("images/paddle");
            var ballTexture = Core.content.Load<Texture2D>("images/ball");

            this.ball = scene.createEntity("ball");
            this.ball.addComponent(new Sprite(ballTexture));
            this.ball.addComponent<CircleCollider>();
            this.ball.addComponent<BallController>();

            this.paddle = scene.createEntity("paddle");
            this.paddle.addComponent(new Sprite(paddleTexture));
            this.paddle.setScale(new Vector2(1, 0.75f));
            this.paddle.addComponent<PaddleController>();
            this.paddle.addComponent<BoxCollider>();
            this.paddle.position = new Vector2(Core.graphicsDevice.Viewport.Width / 2f, (float)(Core.graphicsDevice.Viewport.Height * 0.85));

            float ballHeight = this.ball.getComponent<Sprite>().height;
            float paddleWidth = this.paddle.getComponent<Sprite>().width;
            float paddleHeight = this.paddle.getComponent<Sprite>().height;

            this.ball.position = new Vector2(this.paddle.position.X, this.paddle.position.Y - paddleHeight + (ballHeight / 2));
        }

        protected void AddBrick(float x, float y)
        {
            var brickTexture = this.content.Load<Texture2D>("images/brick");

            var brick = this.createEntity("brick-" + bricksAdded);
            brick.addComponent(new Sprite(brickTexture));
            brick.getComponent<Sprite>().color = GetRandomColor();
            brick.addComponent<BoxCollider>();
            brick.setTag(Game1.BRICK_TAG);
            brick.position = new Vector2(x, y);

            bricksAdded++;
        }

        private Color GetRandomColor()
        {
            Color color;

            int result = Nez.Random.nextInt(5);

            switch (result)
            {
                case 0:
                    color = Color.Green;
                    break;
                case 1:
                    color = Color.Yellow;
                    break;
                case 2:
                    color = Color.Red;
                    break;
                case 3:
                    color = Color.Purple;
                    break;
                case 4:
                    color = Color.Blue;
                    break;
                default:
                    color = Color.Black;
                    break;
            }

            return color;
        }
    }
}
