using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace spaceshooter
{
    //approve plz
    public class Ship : Sprite
    {
        public int score;
        int bulletsShot;

        TimeSpan offScreenTime = new TimeSpan(0, 0, 5);
        TimeSpan offScreenPassed = new TimeSpan();
        TimeSpan FramesTimePassed = new TimeSpan();
        TimeSpan FramesWaitTime;
        int frame;
        List<Rectangle> frames;
        
        

        bool shipmoved;
        protected PlayerIndex _player;
        protected int _health = 100;
        protected Team _team;
        protected float _speed;
        private float _currentSpeed;
        public float CurrentSpeed
        {
            get { return _currentSpeed; }
            set { _currentSpeed = value; }
        }


        public Keys upkey = Keys.Up;
        public Keys downkey = Keys.Down;
        public Keys leftkey = Keys.Left;
        public Keys rightkey = Keys.Right;
        public Keys laserkey = Keys.RightControl;
        public Keys nukekey = Keys.Enter;
        public Keys shotgunkey = Keys.RightShift;
        protected TimeSpan LaserTimePassed = new TimeSpan();
        protected TimeSpan ProjectileTimePassed = new TimeSpan();

        Texture2D _laserimage;
        Texture2D _bulletimage;
        Texture2D _nukeimage;
        Texture2D _deathimage;

        SoundEffect laserShot;
        SoundEffect missilelaunch;
        SoundEffect shotSound;

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

        public PlayerIndex Player
        {
            get
            {
                return _player;
            }
        }

        public bool Alive
        {
            get
            {
                return _health > 0;
            }
        }

        private bool _offscreen;
        public bool Offscreen
        {
            get { return _offscreen; }
            set { _offscreen = value; }
        }

        private bool _explosionHit;
        public bool ExplosionHit
        {
            get { return _explosionHit; }
            set { _explosionHit = value; }
        }

        public float Speed
        {
            get
            {
                return _speed * .5f;
            }
            set
            {
                _speed = value * 2;
            }
        }
        
        Vector2 direction;

        public Ship(Texture2D image, Vector2 position, Color color, float speed, Team team, int health, PlayerIndex player, ContentManager content)
            : base(image, position, color)
        {
            _player = player;
            _speed = speed;
            _team = team;
            _health = health;
            image = _image;

            shotSound = content.Load<SoundEffect>("shot");
            _laserimage = content.Load<Texture2D>("Red Lazr");
            _bulletimage = content.Load<Texture2D>("BUWWETzerzez");
            _nukeimage = content.Load<Texture2D>("Dood");
            missilelaunch = content.Load<SoundEffect>("Nuke Launch");
            laserShot = content.Load<SoundEffect>("Laser Blast");
            
            _deathimage = content.Load<Texture2D>("RIP IN RIP");
            direction = new Vector2(0);
            _explosionHit = false;

            origin = new Vector2(_sourceRectangle.Width / 2, _sourceRectangle.Height / 2);
            frames = new List<Rectangle>();
            FramesTimePassed = new TimeSpan();
            FramesWaitTime = new TimeSpan(0,0,0,0, 50);


            for (int i = 0; i < 4; i++)
            {
                frames.Add(new Rectangle(52*i,0,52,88));
            }
        }

        
        public void ShipUpdate(GameTime gameTime, TimeSpan projectileWaitTime, TimeSpan laserWaitTime, List<Projectile> projectiles)
        {
            GamePadState state = InputManager.GamePad[_player];
            GamePadState laststate = InputManager.LastGamePad[_player];
            if (_position.X > Settings.WindowWidth || _position.X < 0)
            {
                _offscreen = true;
            }
            else if (_position.Y > Settings.WindowHeight || _position.Y < 0)
            {
                _offscreen = true;
            }
            else
            {
                _offscreen = false;
            }
            #region offscreen
            if (offScreenTime <= offScreenPassed)
            {
                _health = 0;
            }
            if (_offscreen)
            {
                offScreenPassed += gameTime.ElapsedGameTime;
            }
            else
            {
                offScreenPassed = TimeSpan.Zero;
            }
            #endregion
            if(Alive)
            {
                #region XboxMovement
                if (state.ThumbSticks.Left.Y > 0)//InputManager.Keys.IsKeyDown(upkey))
                {
                    direction.Y -= 0.3f * state.ThumbSticks.Left.Y;
                    GamePad.SetVibration(_player, 0, 0.5f);
                    shipmoved = true;
                }
                if (state.ThumbSticks.Left.Y < 0)//InputManager.Keys.IsKeyDown(downkey))
                {
                    direction.Y += -0.3f * state.ThumbSticks.Left.Y;
                    GamePad.SetVibration(_player, 0, 0.5f);
                    shipmoved = true;
                }
                if (state.ThumbSticks.Left.X > 0)//InputManager.Keys.IsKeyDown(rightkey))
                {
                    direction.X -= -state.ThumbSticks.Left.X;
                    _rotation += 0.03f * state.ThumbSticks.Left.X;
                    direction.Y += 0.08f;
                    GamePad.SetVibration(_player, 0, 0.5f);
                    shipmoved = true;
                }
                if (state.ThumbSticks.Left.X < 0)//InputManager.Keys.IsKeyDown(leftkey))
                {
                    direction.X += state.ThumbSticks.Left.X;
                    _rotation -= -0.03f * state.ThumbSticks.Left.X;
                    direction.Y += 0.08f;
                    GamePad.SetVibration(_player, 0, 0.5f);
                    shipmoved = true;

                }
                #endregion
            #region KeyboardMovement
                if (InputManager.Keys.IsKeyDown(upkey))
                {
                    direction.Y -= 0.3f;
                    shipmoved = true;
                }
                if (InputManager.Keys.IsKeyDown(downkey))
                {
                    direction.Y += 0.3f;
                    shipmoved = true;
                }
                if (InputManager.Keys.IsKeyDown(rightkey))
                {
                    direction.X++;
                    _rotation += 0.03f;
                    direction.Y += 0.08f;
                }
                if (InputManager.Keys.IsKeyDown(leftkey))
                {
                    direction.X --;
                    _rotation -= 0.03f;
                    direction.Y += 0.08f;
                    shipmoved = true;
                }
                #endregion
            }
                if (shipmoved == false)
                {
                    GamePad.SetVibration(_player, 0, 0);
                }
                shipmoved = false;

                Vector2.Clamp(direction, new Vector2(-.5f, -.5f), new Vector2(.5f, .5f));
                _position.X = _position.X + direction.X * Speed * gameTime.ElapsedGameTime.Milliseconds / 10000 * Settings.WindowWidth / 1920;
                _position.Y = _position.Y + direction.Y * Speed * gameTime.ElapsedGameTime.Milliseconds / 10000 * Settings.WindowHeight / 1080;
                CurrentSpeed = (-direction.Y * Speed + Math.Abs(direction.X * Speed/4)) * gameTime.ElapsedGameTime.Milliseconds / 1000;
                if (((state.Buttons.LeftShoulder == ButtonState.Pressed) || (state.Triggers.Left > 0.5f) || (InputManager.Keys.IsKeyDown(nukekey)) || (InputManager.Keys.IsKeyDown(shotgunkey))) && ProjectileTimePassed >= projectileWaitTime && Alive)
                {
                    if (state.Buttons.LeftShoulder == ButtonState.Pressed || InputManager.Keys.IsKeyDown(nukekey))
                    {
                        if (InputManager.Keys.IsKeyDown(Keys.Q))
                        {

                        }
                        else
                        {
                            ProjectileTimePassed = TimeSpan.Zero;
                        }
                        projectiles.Add(new Projectile(_nukeimage, new Vector2(_position.X, _position.Y), _color, 25, Team.Friend, new Vector2(0, 0), _rotation, _player, true));
                        projectiles[projectiles.Count - 1].Speed = _rotation.RadianToVector();//setting the speed based on the rotation of the ship
                        projectiles[projectiles.Count - 1].Speed.Normalize();//making sure it's not crazy fast by making speed (1,1)
                        projectiles[projectiles.Count - 1].SpeedX *= 15;
                        projectiles[projectiles.Count - 1].SpeedY *= 15;
                        missilelaunch.Play();
                    }
                    else
                    {
                        float randomnumber;
                        if (InputManager.Keys.IsKeyDown(Keys.Q))
                        {
                            bulletsShot = 100;
                            randomnumber = (float)((Util.GlobalRandom.NextDouble() * 4) - 2);
                            shotSound.Play();
                        }
                        else
                        {
                            shotSound.Play();
                            bulletsShot = 10;
                            ProjectileTimePassed = TimeSpan.Zero;
                            randomnumber = (float)((Util.GlobalRandom.NextDouble() * 0.5f) - 0.25f);
                        }
                        for (int i = 0; i < bulletsShot - 1; i++)
                        {
                            if (InputManager.Keys.IsKeyDown(Keys.Q))
                            {
                                randomnumber = (float)((Util.GlobalRandom.NextDouble() * 8) - 4);

                            }
                            else
                            {
                                ProjectileTimePassed = TimeSpan.Zero;
                                randomnumber = (float)((Util.GlobalRandom.NextDouble() * 0.5f) - 0.25f);
                            }
                            float rotation = _rotation + randomnumber;
                            projectiles.Add(new Projectile(_bulletimage, new Vector2(_position.X, _position.Y), _color, 5, Team.Friend, new Vector2(0, 0), _rotation, _player, false));
                            projectiles[projectiles.Count - 1].Speed = rotation.RadianToVector();//setting the speed based on the rotation of the ship
                            projectiles[projectiles.Count - 1].Speed.Normalize();//making sure it's not crazy fast by making speed (1,1)
                            projectiles[projectiles.Count - 1].SpeedX *= 10;
                            projectiles[projectiles.Count - 1].SpeedY *= 10;
                        }
                    }
                }
                ProjectileTimePassed += gameTime.ElapsedGameTime;
                LaserTimePassed += gameTime.ElapsedGameTime;


                if ((InputManager.Keys.IsKeyDown(laserkey) ||state.Triggers.Right >= 0.5f) && LaserTimePassed >= laserWaitTime && Alive)
                {
                    LaserTimePassed = TimeSpan.Zero;
                    projectiles.Add(new Projectile(_laserimage, new Vector2(_position.X, _position.Y), _color, 5, Team.Friend, new Vector2(0, 0), _rotation, _player, false));
                    projectiles[projectiles.Count - 1].Speed = _rotation.RadianToVector();//setting the speed based on the rotation of the ship
                    projectiles[projectiles.Count - 1].Speed.Normalize();//making sure it's not crazy fast by making speed (1,1)
                    projectiles[projectiles.Count - 1].SpeedX *= 15;
                    projectiles[projectiles.Count - 1].SpeedY *= 15;
                    laserShot.Play();
                }
                base.Update(gameTime);

            if (FramesWaitTime < FramesTimePassed)
            {
                FramesTimePassed = new TimeSpan();
                if (frame == 3)
                {
                    frame = 0;
                }
                frame++;
            }
            FramesTimePassed += gameTime.ElapsedGameTime;
            _sourceRectangle = frames[frame];
            origin = new Vector2(_sourceRectangle.Width / 2, _sourceRectangle.Height / 2);
        }
        

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Alive)
            {
                base.Draw(spriteBatch);
            }
            else
            {
                _color = Color.White;
                spriteBatch.Draw(_deathimage, _position, _hitbox, _color, _rotation, origin, _scale, _spriteEffects, _layerDepth);
            }
        }
    }
}

