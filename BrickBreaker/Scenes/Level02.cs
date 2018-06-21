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
    public class Level02 : LevelBase
    {
        public override void initialize()
        {
            base.initialize();
            var brickTexture = this.content.Load<Texture2D>("images/brick");
            var entityTwo = this.createEntity("entity-two");
            entityTwo.addComponent(new Sprite(brickTexture));
            entityTwo.getComponent<Sprite>().color = Color.ForestGreen;
            entityTwo.addComponent<BoxCollider>();
            entityTwo.setTag(Game1.BRICK_TAG);
            entityTwo.transform.position = new Vector2(200, 100);
        }
    }
}
