using Microsoft.Xna.Framework;
using Nez;
using Nez.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickBreaker.Scenes
{
    public class LevelSelect : Scene
    {
        public const int SCREEN_SPACE_RENDER_LAYER = 999;
        public UICanvas canvas;

        private Table _table;
        List<Button> _buttons;

        private float _currentRowWidth;

        public override void initialize()
        {
            base.initialize();
            this.addRenderer(new DefaultRenderer());            

            this.addRenderer(new ScreenSpaceRenderer(100, SCREEN_SPACE_RENDER_LAYER));

            this.canvas = this.createEntity("UI").addComponent(new UICanvas());
            this.canvas.isFullScreen = true;
            this.canvas.renderLayer = SCREEN_SPACE_RENDER_LAYER;

            this.SetupUI();
        }

        private void SetupUI()
        {
            

            this._table = this.canvas.stage.addElement(new Table());
            this._table.setFillParent(true).left().top();

            this.AddLevelSelectButton("Level 1", new Level01());
            this.AddLevelSelectButton("Level 2", new Level02());
            //this.AddLevelSelectButton("Level 3", new Level03());
            //this.AddLevelSelectButton("Level 4", new Level04());
            //this.AddLevelSelectButton("Level 5", new Level05());
            //this.AddLevelSelectButton("Level 6", new Level06());
            //this.AddLevelSelectButton("Level 7", new Level07());
            //this.AddLevelSelectButton("Level 8", new Level08());
            //this.AddLevelSelectButton("Level 9", new Level09());
            //this.AddLevelSelectButton("Level 10", new Level10());
            //this.AddLevelSelectButton("Level 11", new Level11());
            //this.AddLevelSelectButton("Level 12", new Level12());
            //this.AddLevelSelectButton("Level 13", new Level13());
            //this.AddLevelSelectButton("Level 14", new Level14());
            //this.AddLevelSelectButton("Level 15", new Level15());
            //this.AddLevelSelectButton("Level 16", new Level16());
            //this.AddLevelSelectButton("Level 17", new Level17());
            //this.AddLevelSelectButton("Level 18", new Level18());
        }

        private void TransitionToLevel(LevelBase level)
        {
            Core.startSceneTransition(new WindTransition(() => level));
        }

        private void AddLevelSelectButton(string text, LevelBase level)
        {
            float minHeight = Core.graphicsDevice.Viewport.Height / 12;
            float minWidth = Core.graphicsDevice.Viewport.Width / 6;
            float margin = 10f;

            var buttonStyle = new TextButtonStyle(new PrimitiveDrawable(Color.Black, 10f), new PrimitiveDrawable(Color.Yellow), new PrimitiveDrawable(Color.DarkSlateBlue))
            {
                downFontColor = Color.Black
            };

            this._table.add(new TextButton(text, buttonStyle))
                .setFillY()
                .setMinHeight(minHeight)
                .setMinWidth(minWidth)
                .getElement<Button>()
                .onClicked += (obj) => { this.TransitionToLevel(level); };

            this._currentRowWidth += minWidth;

            if (this._currentRowWidth >= Core.graphicsDevice.Viewport.Width - margin)
            {
                this._table.row();
                this._currentRowWidth = 0f;
            }
        }

    }
}
