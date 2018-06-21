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
    public class Level01 : LevelBase
    {
        public override void initialize()
        {
            base.initialize();

            int width = 32;
            int height = 16;

            int columns = (int)(Core.graphicsDevice.Viewport.Width * 0.95) / width;
            int rows = (int)(Core.graphicsDevice.Viewport.Height * 0.8) / height;

            int bricksToAdd = columns;
            int added = 0;

            for (int i = 1; i <= rows; i++)
            {
                for (int j = 1; j <= columns; j++)
                {
                    if (added < bricksToAdd)
                    {
                        this.AddBrick(j * width, i * height);
                        added++;
                    }
                }

                bricksToAdd -= 2;
                added = 0;
            }
        }
    }
}
