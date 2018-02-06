using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace spaceshooter
{
    class Powerup : Sprite
    {
        protected Vector2 _speed;

        private int randomnumber;
        private int chance = 100;
        
        public float SpeedY
        {
            get
            {
                return _speed.Y;
            }
            set
            {
                _speed.Y = value;
            }
        }
        public float SpeedX
        {
            get
            {
                return _speed.X;
            }
            set
            {
                _speed.X = value;
            }
        }

        private bool falling;
        public bool Falling
        {
            get { return falling; }
            set { falling = value; }
        }

        public Powerup(Texture2D image, Vector2 position, Color color, Vector2 speed)
            : base(image, position, color)
        {
            _speed = speed;
        }

        public override void Update(GameTime gameTime)
        {
            #region Generation
            if (falling == false)
            {
                randomnumber = Util.GlobalRandom.Next(0, chance);
                if (randomnumber == 0)
                {
                    falling = true;
                    chance = 100;
                    _position.X = Util.GlobalRandom.Next(Width, Settings.WindowWidth);
                }
                else
                {
                    chance--;
                }
            }
            #endregion

            #region Falling
            if (falling)
            {
                _position += _speed;
                if (_position.Y > Settings.WindowHeight + Height)
                {
                    falling = false;
                    _position.Y = -_image.Height;
                }
            }
            #endregion

            base.Update(gameTime);
        }
        public void Reset()
        {
            falling = false;
            _position.Y = -_image.Height;
        }
    }
}
