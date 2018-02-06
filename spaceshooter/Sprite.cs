using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace spaceshooter
{
    //Approval By Kevin ✔
    public class Sprite
    {
        protected Rectangle _hitbox;

        protected Texture2D _image;
        protected Vector2 _position;
        protected Rectangle _sourceRectangle;
        protected Color _color;
        protected float _rotation;
        protected Vector2 origin;
        protected Vector2 _scale;
        protected SpriteEffects _spriteEffects;
        protected float _layerDepth;
        public Rectangle Hitbox
        {
            get
            {
                return _hitbox;
            }
        }
        public Texture2D Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
            }
        }
        public float X
        {
            get
            {
                return _position.X;
            }
            set
            {
                _position.X = value;
            }
        }
        public float Y
        {
            get
            {
                return _position.Y;
            }
            set
            {
                _position.Y = value;
            }
        }
        public int Height
        {
            get
            {
                return _hitbox.Width;
            }
        }
        public int Width
        {
            get
            {
                return _hitbox.Height;
            }

        }
        public Rectangle SourceRectangle
        {
            get { return _sourceRectangle; }
            set { _sourceRectangle = value; }
        }
        public Color Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
            }
        }
        public float Rotation
        {
            get
            {
                return _rotation;
            }
            set
            {
                _rotation = value;
            }
        }
        public Vector2 Origin
        {
            get
            {
                return origin;
            }
            set
            {
                origin = value;
            }
        }
        public Vector2 Scale
        {
            get
            {
                return _scale;
            }
            set
            {
                _scale = value;
            }
        }
        public SpriteEffects SpriteEffects
        {
            get { return _spriteEffects; }
            set { _spriteEffects = value; }
        }
        public float LayerDepth
        {
            get { return _layerDepth; }
            set { _layerDepth = value; }
        }


        public Sprite(Texture2D image, Vector2 position, Color color)
            :this(image, position, color, Vector2.One)
        {
        }
        public Sprite(Texture2D image, Vector2 position, Color color, Vector2 scale)
        {
            _image = image;
            _position = position;
            _color = color;
            _scale = scale;
            _sourceRectangle = new Rectangle(0, 0, _image.Width, _image.Height);
            origin = new Vector2(_sourceRectangle.Width / 2, _sourceRectangle.Height / 2);
            _hitbox = new Rectangle((int)(_position.X - origin.X * _scale.X), (int)(_position.Y - origin.Y * _scale.Y), (int)(_sourceRectangle.Width * _scale.X), (int)(_sourceRectangle.Height * _scale.Y));
        }

        public virtual void Update(GameTime gameTime)
        {
            _hitbox = new Rectangle((int)(_position.X - origin.X * _scale.X), (int)(_position.Y - origin.Y * _scale.Y), (int)(_sourceRectangle.Width * _scale.X), (int)(_sourceRectangle.Height * _scale.Y));
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_image, _position, _sourceRectangle, _color, _rotation, origin, _scale, _spriteEffects, _layerDepth);
        }

        public virtual void DrawHitBox(SpriteBatch spriteBatch, Texture2D image)
        {
            spriteBatch.Draw(image, _hitbox, _color);
        }
    }
}


