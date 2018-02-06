using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace spaceshooter
{
    public class MouseClicker : Sprite
    {
        //approve plz
        protected Vector2 lastclick;

        private bool clicked;
        public bool Clicked
        {
            get { return clicked; }
            set { clicked = value; }
        }

        private bool _canShoot;

        public bool CanShoot
        {
            get { return _canShoot; }
            set { _canShoot = value; }
        }

        private bool ControllerClicking;

        public bool _controllerClicking
        {
            get { return ControllerClicking; }
            set { ControllerClicking = value; }
        }


        public MouseClicker(Texture2D image, Vector2 position)
            : base(image, position, Color.White)
        {

        }

        public override void Update(GameTime gameTime)
        {
            _position = new Vector2(InputManager.MouseState.X, InputManager.MouseState.Y);
           if ((InputManager.GamePad[PlayerIndex.One].Buttons.A == ButtonState.Pressed && _controllerClicking)|| InputManager.MouseState.LeftButton == ButtonState.Pressed)
           {
               clicked = true;
           }
           else
           {
               clicked = false;
           }
           base.Update(gameTime);
        }
    }
}
