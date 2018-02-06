using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace spaceshooter
{
    //1/2 approval by kevin
    public class Projectile : Sprite
    {
        private bool _remove;

        public bool Remove
        {
            get { return _remove; }
            set { _remove = value; }
        }

        protected PlayerIndex _owner;

        protected bool _explosive;

        public bool Explosive
        {
            get { return _explosive; }
            set { _explosive = value; }
        }
        
        public int Damage;

        protected Team _team;
        public Team Team
        {
            get
            {
                return _team;
            }
            set
            {
                _team = value;
            }
        }

        protected Vector2 _speed;
        public Vector2 Speed
        {
            get
            {
                return _speed;
            }
            set
            {
                _speed = value;
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
        public PlayerIndex Owner
        {
            get
            {
                return _owner;
            }
        }

        //Player Shooting
        public Projectile(Texture2D image, Vector2 position, Color color, int damage, Team team, Vector2 speed, float rotation, PlayerIndex owner, bool explosive)
            : base(image, position, color)
        {
            _explosive = explosive;
            Damage = damage;
            _team = team;
            _speed = speed;
            _rotation = rotation;
            _owner = owner;
        }
        //Enemy Shooting
        public Projectile(Texture2D image, Vector2 position, Color color, int damage, Team team, Vector2 speed, float rotation)
            : this(image, position, color, damage, team, speed, rotation, PlayerIndex.One, false)
        {}

        public override void Update(GameTime gameTime)
        {
            _position.X = _position.X + _speed.X;
            _position.Y = _position.Y + _speed.Y;

            if (_position.X < -100 || _position.X > Settings.WindowWidth + 100 || _position.Y < -100 || _position.Y > Settings.WindowHeight + 100)
            {
                _remove = true;
            }
            base.Update(gameTime);
        }
    }
}
