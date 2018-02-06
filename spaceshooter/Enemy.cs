using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace spaceshooter
{
    public class Enemy : Sprite
    {
        //approve plz
        protected bool killedByPlayer = false;
        protected PlayerIndex _killedBy;
        protected Vector2 _speed;
        protected int _health;
        protected TimeSpan _firerate;
        protected bool explosionHit;
        protected TimeSpan FramesWaitTime;
        protected TimeSpan FramesTimePassed;
        protected List<Rectangle> frames;
        protected int frame = 0;

        public bool KilledByPlayer
        {
            get { return killedByPlayer; }
            set { killedByPlayer = value; }
        }
        public PlayerIndex KilledBy
        {
            get { return _killedBy; }
            set { _killedBy = value;
                killedByPlayer = true;}
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
        public int Health
        {
            get
            {
                return _health;
            }
            set
            {
                _health = value;
            }
        }
        public bool Alive
        {
            get
            {
                return _health > 0;
            }
        }
        public bool ExplosionHit
        {
            get { return explosionHit; }
            set { explosionHit = value; }
        }


        public Enemy(Vector2 position, Vector2 speed, int health, TimeSpan firerate, Texture2D image, Color color)
            : base(image, position, color)
        {
            explosionHit = false;
            _position = position;
            _speed = speed;
            _health = health;
            _firerate = firerate;
            _image = image;
            

            FramesTimePassed = new TimeSpan();
            FramesWaitTime = new TimeSpan(0, 0, 0, 0, 250);
            frames = new List<Rectangle>();
            for (int i = 0; i <= 1; i++)
            {
                frames.Add(new Rectangle(75*i,0,75,37));
            }
        }
        
        public void Update(GameTime gameTime, Texture2D lazor, List<Projectile> lasers, SoundEffect laser)
        {
            if (Alive)
            {
                if (X < 0)
                {
                    _speed.X *= -1;
                }
                else if (X + Width >= Settings.WindowWidth)
                {
                    _speed.X *= -1;
                }
                _firerate += gameTime.ElapsedGameTime;
                if (_firerate > new TimeSpan(0, 0, 0, 0, 500))
                {
                    _firerate = TimeSpan.Zero;
                    laser.Play();
                    lasers.Add(new Projectile(lazor, new Vector2(_position.X + _sourceRectangle.Width / 2, _position.Y + _sourceRectangle.Height), Color.White, 5, Team.Enemy, new Vector2(0, 15), 0f));
                }
                if (FramesTimePassed >= FramesWaitTime)
                {
                    FramesTimePassed = new TimeSpan();
                    if (frame < 1)
                    {
                        frame++;
                    }
                    else
                    {
                        frame = 0;
                    }
                }
                FramesTimePassed += gameTime.ElapsedGameTime;
                _sourceRectangle = frames[frame];
                origin = new Vector2(0, 0);

                _position.X += _speed.X;
                _position.Y += _speed.Y;
            }
            else
            {

            }
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Alive)
            {
                spriteBatch.Draw(_image, _position, frames[frame], _color);
            }
        }
    }
}
