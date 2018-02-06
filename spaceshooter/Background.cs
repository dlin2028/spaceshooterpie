using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace spaceshooter
{
    class Background : Sprite
    {
        private Vector2 _speed;

        public Vector2 Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        private Vector2 startPosition;

        public Vector2 StartPosition
        {
            get { return startPosition; }
            set { startPosition = value; }
        }

        private Vector2 endPosition;

        public Vector2 EndPosition
        {
            get { return endPosition; }
            set { endPosition = value; }
        }

        bool _isleft = false;
        bool _isup = false;
        
        public Background(Texture2D image, Vector2 startPosition, Vector2 endPosition, Vector2 speed, Color color, bool isleft, bool isup)
            : base(image, startPosition, color)
        {
            origin = Vector2.Zero;
            this.startPosition = startPosition;
            this.endPosition = endPosition;
            _isleft = isleft;
            _isup = isup;
            if (endPosition.X > startPosition.X)
            {
                _isleft = false;
            }
            if (endPosition.Y > startPosition.Y)
            {
                _isup = false;
            }
            _speed = speed;
        }

        public override void Update(GameTime gameTime)
        {
            if (_isup && _position.Y < endPosition.Y ||
                !_isup && _position.Y > endPosition.Y)
            {
                _position.Y = startPosition.Y;
            }
            if (_isleft && _position.X < endPosition.X ||
                !_isleft && _position.X > endPosition.X)
            {
                _position.X = startPosition.X;
            }
            _position += _speed;
            base.Update(gameTime);
        }
      
    }
}
