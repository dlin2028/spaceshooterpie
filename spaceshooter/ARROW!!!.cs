using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace spaceshooter
{
    class ARROW : Sprite
    {
        public ARROW(Vector2 position, Texture2D image, Color color)
            :base(image, position, color)
        {
            _position = position;
            _image = image;
            _color = color;
        }

        public void Update(Vector2 spaceship)
        {
            //Top
            if (spaceship.Y <= 0)
            {
                _position.Y = Width;
                _rotation = (float)(3 * Math.PI) / 2;
                _position.X = spaceship.X;
            }
            //Left
            if (spaceship.X <= 0)
            {
                _position.Y = spaceship.Y;
                _rotation = (float)Math.PI;
                _position.X = Width;
            }
            //Right
            if (spaceship.X >= Settings.WindowWidth)
            {
                _position.Y = spaceship.Y;
                _rotation = (float)Math.PI * 2;
                _position.X = Settings.WindowWidth - Width;
            }
            //Bottom
            if (spaceship.Y >= Settings.WindowHeight)
            {
                _position.X = spaceship.X;
                _rotation = (float)Math.PI / 2;
                _position.Y = Settings.WindowHeight - Width;
            }
            //Top Left
            if (spaceship.Y <= 0 && spaceship.X <= 0)
            {
                _position.X = 100;
                _position.Y = 100;
                _rotation = ((float)Math.PI * 5) / 4;
            }
            //Top Right
            if (spaceship.Y <= 0 && spaceship.X >= Settings.WindowWidth)
            {
                _position.X = Settings.WindowWidth - 100;
                _position.Y = 100;
                _rotation = ((float)Math.PI * 7) / 4;
            }
            //Bottom Left
            if (spaceship.Y >= Settings.WindowHeight && spaceship.X <= 0)
            {
                _position.X = 100;
                _position.Y = Settings.WindowHeight - 100;
                _rotation = ((float)Math.PI * 3) / 4;
            }
            //Bottom Right
            if (spaceship.Y >= Settings.WindowHeight && spaceship.X >= Settings.WindowWidth)
            {
                _position.X = Settings.WindowWidth - 100;
                _position.Y = Settings.WindowHeight - 100;
                _rotation = ((float)Math.PI) / 4;
            }
        }
    }
}


