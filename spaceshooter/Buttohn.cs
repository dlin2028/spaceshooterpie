using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace spaceshooter
{
    class Buttohn
    {
        private Vector2 _position;
        private Texture2D _image;
        private Color _color = Color.White;
        private Rectangle _hitbox;
        private bool click = false;
        public Buttohn(Vector2 position, Texture2D image)
        {
            position = _position;
            image = _image;
            _hitbox = new Rectangle((int)position.X, (int)position.Y, image.Width, image.Height);
        }
        public bool Click
        {
            get
            {
                return click;
            }
        }
        public void Update(MouseState mowze)
        {
            if (mowze.X > _position.X && mowze.X < _position.X + _image.Width && mowze.Y > _position.X && mowze.Y < _position.Y + _image.Width && mowze.LeftButton == ButtonState.Pressed)
            {
                click = true;
            }

            //Checks if the rectangle contains the mouse pointer. yay =D
            //if(_hitbox.Contains(new Point(mowze.X, mowze.Y))
        }
        public void Draw(SpriteBatch Spritebatch)
        {
            Spritebatch.Draw(_image, _position, _color);
        }
    }
}
