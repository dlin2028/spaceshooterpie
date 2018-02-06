using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace spaceshooter
{
    //approve plz
    //1/2 approval by kevin
    public class Explosion : Sprite
    {
        
        protected Team _team;
        protected PlayerIndex _player;
        protected bool _exploding = true;
        protected int _maxSize;
        protected int _explosionRate;
        protected int CurrentSize;

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
        public int MaxSize
        {
            get { return _maxSize; }
            set { _maxSize = value; }
        }
        public int ExplosionRate
        {
            get { return _explosionRate; }
            set { _explosionRate = value; }
        }
        public bool Exploding
        {
            get { return _exploding; }
            set { _exploding = value; }
        }
        public PlayerIndex Player
        {
            get { return _player; }
            set { _player = value; }
        }

        public Explosion(Texture2D image, Vector2 position, Color color, int maxSize, int explosionRate, PlayerIndex player, Team team)
            : base(image, position, color, new Vector2(0.00000001f))

        {
            _team = team;
            _player = player;
            _maxSize = maxSize;
            _explosionRate = explosionRate;
            CurrentSize = 0;
        }

        public Explosion(Texture2D image, Vector2 position, Color color, int maxSize, int explosionRate, Team team)
            : base(image, position, color, new Vector2(0.00000001f))
        {
            _team = team;
            _player = PlayerIndex.One;
            _maxSize = maxSize;
            _explosionRate = explosionRate;
            CurrentSize = 0;
        }

        public override void Update(GameTime gameTime)
        {
            
            CurrentSize += CurrentSize + _explosionRate > _maxSize ? 0 : _explosionRate; //if CurrentSize + _rate > max then 0 else explosion rate
            _scale = new Vector2(CurrentSize / (float)_image.Width);
            if (CurrentSize >= MaxSize)
            {                
                _exploding = false;
            }
            base.Update(gameTime);
        }
    }
}