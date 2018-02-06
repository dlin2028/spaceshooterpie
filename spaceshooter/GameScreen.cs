using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace spaceshooter
{
    class GameScreen : Screen
    {
        int friendlyScore;
        int fps;
        int frames;

        ContentManager _content;
        //load your button here
        Button resumeButton;
        Button restartButton;
        Button mainMenuButton;

        int totalscore;

        bool pause = false;

        Vector2 size;
        string words;

        List<Ship> spaceships;
        List<Enemy> enemys;
        List<Projectile> projectiles;
        List<Explosion> explosions;
        List<ARROW> arrows;
        List<Speedometer> speedometers;

        TimeSpan FPSTotalTime;

        TimeSpan projectileWaitTime;
        TimeSpan laserWaitTime;

        TimeSpan nextWaveWaitTime;
        TimeSpan nextWaveTimePassed;

        SoundEffect shotSound;
        SoundEffect missileLaunch;
        SoundEffect laserShot;
        SoundEffect ExplosionSound;

        Sprite speedneedle;
        Sprite deathsign;

        MouseClicker mouseClicker;
        Sprite finger;

        Sprite RIP;
        List<Sprite> RIPs;

        SpriteFont bigfont;
        SpriteFont font;
        SpriteFont smallfont;

        Powerup medkit;

        Texture2D explosionTexture;
        Texture2D ufoTexture;
        Texture2D laserTexture;
        Texture2D fingerTexture;

        bool anyAlive = true;
        bool addNextWave;
        int wave;

        Texture2D pixel;
        public override void Reset(GraphicsDevice GraphicsDevice, ContentManager Content)
        {
            sprites = new List<Sprite>();

            _content = Content;

            speedometers = new List<Speedometer>();

            FPSTotalTime = new TimeSpan(0, 0, 1);

            explosionTexture = Content.Load<Texture2D>("explosion");
            laserTexture = Content.Load<Texture2D>("Red Lazr");
            ufoTexture = Content.Load<Texture2D>("ufo");
            fingerTexture = Content.Load<Texture2D>("LEL");

            nextWaveTimePassed = new TimeSpan();
            nextWaveWaitTime = new TimeSpan(0, 0, 10);
            pixel = new Texture2D(graphicsDevice, 1, 1);
            pixel.SetData<Color>(new Color[] { Color.White });
            wave = 0;
            addNextWave = true;

            font = Content.Load<SpriteFont>("font");
            smallfont = Content.Load<SpriteFont>("smallfont");
            bigfont = Content.Load<SpriteFont>("bigfont");

            Color buttonColor = Color.Lerp(Color.Green, Color.Transparent, .5f);
            Color buttonHoverColor = Color.Lerp(Color.Green, Color.Transparent, .25f);
            resumeButton = new Button(pixel, new Vector2(Settings.WindowWidth / 2 - 300, Settings.WindowHeight / 2 - 200), new Vector2(600, 150), "Resume", font, Color.Green, Color.Green, Color.White, buttonColor, buttonHoverColor, Color.Green);
            restartButton = new Button(pixel, new Vector2(Settings.WindowWidth / 2 - 300, Settings.WindowHeight / 2), new Vector2(600, 150), "Restart", font, Color.Green, Color.Green, Color.White, buttonColor, buttonHoverColor, Color.Green);
            mainMenuButton = new Button(pixel, new Vector2(Settings.WindowWidth / 2 - 300, Settings.WindowHeight / 2 + 200), new Vector2(600, 150), "Main Menu", font, Color.Green, Color.Green, Color.White, buttonColor, buttonHoverColor, Color.Green);

            medkit = new Powerup(Content.Load<Texture2D>("MEDIKITTTTTTTTTTTTT!"), new Vector2(-100, -100), Color.White, new Vector2(0, 5));

            speedneedle = new Sprite(Content.Load<Texture2D>("Green Dot"), new Vector2(1730, 954), Color.White);
            speedneedle.Rotation = MathHelper.ToRadians(0);
            RIP = new Sprite(Content.Load<Texture2D>("RIP IN RIP"), new Vector2(0, 0), Color.White);

            mouseClicker = new MouseClicker(Content.Load<Texture2D>("meguseta"), new Vector2(InputManager.MouseState.X, InputManager.MouseState.Y));
            deathsign = new Sprite(Content.Load<Texture2D>("download"), new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), Color.White);
            finger = new Sprite(Content.Load<Texture2D>("LEL"), Vector2.Zero, Color.White);
            mouseClicker.Origin = new Vector2(0, 0);
            finger.Origin = new Vector2(0, 0);
            sprites.Add(finger);
            sprites.Add(mouseClicker);

            RIPs = new List<Sprite>();
            explosions = new List<Explosion>();
            spaceships = new List<Ship>();
            projectiles = new List<Projectile>();
            arrows = new List<ARROW>();
            enemys = new List<Enemy>();
            projectileWaitTime = new TimeSpan(0, 0, 1);
            laserWaitTime = new TimeSpan(0, 0, 0, 0, 250);

            ExplosionSound = Content.Load<SoundEffect>("BOOMEFFECT");
            missileLaunch = Content.Load<SoundEffect>("Nuke Launch");
            laserShot = Content.Load<SoundEffect>("Laser Blast");
            shotSound = Content.Load<SoundEffect>("shot");
            #region Create Ships and Arrows
            for (int i = 0; i < Settings.Players; i++)
            {
                spaceships.Add(new Ship(Content.Load<Texture2D>("Spaceship2"), new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height - 100), Color.White, 2000, Team.Friend, 100, (PlayerIndex)i, Content));
                arrows.Add(new ARROW(new Vector2(0, 0), Content.Load<Texture2D>("Yellow_Arrow_Right"), Color.White));
                speedometers.Add(new Speedometer(smallfont, Content.Load<Texture2D>("BALLER"), new Vector2(Settings.WindowWidth, Settings.WindowHeight), Color.Green));
                speedometers[i].Scale = new Vector2(0.75f, 0.75f);
                speedometers[i].X -= speedometers[i].Width * i;
                speedometers[i].X -= speedometers[i].Width / 2;
                speedometers[i].Y -= speedometers[i].Height / 2;
                speedometers[i].MaxSpeed = 900;
                if (i == 0)
                {
                    spaceships[i].Color = Color.White;
                    spaceships[i].upkey = Keys.Up;
                    spaceships[i].downkey = Keys.Down;
                    spaceships[i].leftkey = Keys.Left;
                    spaceships[i].rightkey = Keys.Right;
                    spaceships[i].shotgunkey = Keys.RightShift;
                    spaceships[i].laserkey = Keys.RightControl;
                    spaceships[i].nukekey = Keys.Enter;
                }

                if (i == 1)
                {
                    spaceships[i].Color = Color.Red;
                    spaceships[i].upkey = Keys.W;
                    spaceships[i].downkey = Keys.S;
                    spaceships[i].leftkey = Keys.A;
                    spaceships[i].rightkey = Keys.D;
                    spaceships[i].nukekey = Keys.Tab;
                    spaceships[i].shotgunkey = Keys.LeftShift;
                    spaceships[i].laserkey = Keys.LeftControl;
                }
                if (i == 2)
                {
                    spaceships[i].Color = Color.Green;
                    spaceships[i].upkey = Keys.Y;
                    spaceships[i].downkey = Keys.H;
                    spaceships[i].leftkey = Keys.G;
                    spaceships[i].rightkey = Keys.J;
                    spaceships[i].laserkey = Keys.X;
                    spaceships[i].shotgunkey = Keys.C;
                    spaceships[i].nukekey = Keys.V;
                }
                if (i == 3)
                {
                    spaceships[i].Color = Color.Blue;
                    spaceships[i].upkey = Keys.P;
                    spaceships[i].downkey = Keys.OemSemicolon;
                    spaceships[i].leftkey = Keys.L;
                    spaceships[i].rightkey = Keys.OemQuotes;
                    spaceships[i].laserkey = Keys.N;
                    spaceships[i].shotgunkey = Keys.M;
                    spaceships[i].nukekey = Keys.OemComma;
                }
            }
            #endregion
        }

        public GameScreen(GraphicsDevice GraphicsDevice, ContentManager Content)
            : base(GraphicsDevice, Content)
        {
            //stuf in reset void
        }

        public override void Update(GameTime gameTime)
        {
            FPSTotalTime += gameTime.ElapsedGameTime;
            if (FPSTotalTime >= TimeSpan.FromSeconds(1))
            {
                // framespersecond = fps
                FPSTotalTime = TimeSpan.Zero;
                fps = frames;
                frames = 0;
            }

            finger.X = InputManager.MouseState.X;
            finger.Y = InputManager.MouseState.Y;
            mouseClicker.Update(gameTime);
            if (pause == false)
            {

                #region Mouse Shooting
                if (InputManager.MouseState.LeftButton == ButtonState.Pressed)
                {
                    if (InputManager.LastMouseState.LeftButton == ButtonState.Released)
                    {
                        projectiles.Add(new Projectile(fingerTexture, new Vector2(finger.X, finger.Y), Color.White, 5, Team.Friendly, new Vector2(-10, -10), 0f));
                        projectiles[projectiles.Count - 1].Origin = new Vector2(0, 0);
                    }
                    else
                    {
                        finger.X = -100;
                    }
                }
                #endregion
                #region Projectile Update
                foreach (Projectile projectile in projectiles)
                {
                    if (projectile.Team == Team.Friend || projectile.Team == Team.Friendly)
                    {
                        foreach (Enemy enemy in enemys)
                        {
                            if (projectile.Hitbox.Intersects(enemy.Hitbox) && enemy.Alive)
                            {
                                projectile.Remove = true;
                                if (projectile.Team == Team.Friend)
                                {
                                    #region Add Score

                                    if (projectile.Owner == PlayerIndex.One)
                                    {
                                        if (enemy.Health - projectile.Damage > 0)
                                        {
                                            spaceships[0].score += projectile.Damage;
                                        }
                                        else
                                        {
                                            spaceships[0].score += enemy.Health;
                                            enemy.KilledBy = PlayerIndex.One;
                                        }
                                    }
                                    else if (projectile.Owner == PlayerIndex.Two)
                                    {
                                        if (enemy.Health - projectile.Damage > 0)
                                        {
                                            spaceships[1].score += projectile.Damage;
                                        }
                                        else
                                        {
                                            spaceships[1].score += enemy.Health;
                                            enemy.KilledBy = PlayerIndex.Two;
                                        }
                                    }
                                    else if (projectile.Owner == PlayerIndex.Three)
                                    {
                                        if (enemy.Health - projectile.Damage > 0)
                                        {
                                            spaceships[2].score += projectile.Damage;
                                        }
                                        else
                                        {
                                            spaceships[2].score += enemy.Health;
                                            enemy.KilledBy = PlayerIndex.Three;
                                        }
                                    }
                                    else
                                    {
                                        if (enemy.Health - projectile.Damage > 0)
                                        {
                                            spaceships[3].score += projectile.Damage;
                                        }
                                        else
                                        {
                                            spaceships[3].score += enemy.Health;
                                            enemy.KilledBy = PlayerIndex.Four;
                                        }
                                    }

                                    #endregion
                                }
                                else
                                {
                                    friendlyScore += projectile.Damage;
                                }
                                enemy.Health -= projectile.Damage;
                                if (projectile.Explosive)
                                {
                                    explosions.Add(new Explosion(explosionTexture, new Vector2(projectile.X, projectile.Y), projectile.Color, 250, 50, projectile.Team));
                                    ExplosionSound.Play();
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (Ship ship in spaceships)
                        {
                            if (projectile.Hitbox.Intersects(ship.Hitbox) && ship.Alive)
                            {
                                projectile.Remove = true;
                                ship.Health -= projectile.Damage;
                            }
                        }
                    }
                    projectile.Update(gameTime);
                }
                #endregion
                #region Explosion update

                foreach (Explosion explosion in explosions)
                {
                    explosion.Update(gameTime);
                    foreach (Ship ship in spaceships)
                    {
                        if (explosion.Hitbox.Intersects(ship.Hitbox) && ship.Alive)
                        {
                            if (ship.ExplosionHit)
                            {
                                ship.ExplosionHit = false;
                            }
                            else
                            {
                                ship.Health -= 10;
                            }
                        }
                    }
                    foreach (Enemy enemy in enemys)
                    {
                        if (explosion.Hitbox.Intersects(enemy.Hitbox))
                        {
                            if (enemy.ExplosionHit)
                            {
                                enemy.ExplosionHit = false;
                            }

                            if (explosion.Team == Team.Friend)
                            {
                                #region Add Score
                                if (explosion.Player == PlayerIndex.One)
                                {
                                    if (enemy.Health - 10 > 0)
                                    {
                                        spaceships[0].score += 10;
                                    }
                                    else
                                    {
                                        spaceships[0].score += enemy.Health;
                                        enemy.KilledBy = PlayerIndex.One;
                                    }
                                }
                                else if (explosion.Player == PlayerIndex.Two)
                                {
                                    if (enemy.Health - 10 > 0)
                                    {
                                        spaceships[1].score += 10;
                                    }
                                    else
                                    {
                                        spaceships[1].score += enemy.Health;
                                        enemy.KilledBy = PlayerIndex.Two;
                                    }
                                }
                                else if (explosion.Player == PlayerIndex.Three)
                                {
                                    if (enemy.Health - 10 > 0)
                                    {
                                        spaceships[2].score += 10;
                                    }
                                    else
                                    {
                                        spaceships[2].score += enemy.Health;
                                        enemy.KilledBy = PlayerIndex.Three;
                                    }
                                }
                                else if (explosion.Player == PlayerIndex.Four)
                                {
                                    if (enemy.Health - 10 > 0)
                                    {
                                        spaceships[3].score += 10;
                                    }
                                    else
                                    {
                                        spaceships[3].score += enemy.Health;
                                        enemy.KilledBy = PlayerIndex.Four;
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                if (enemy.Health - 10 > 0)
                                {
                                    friendlyScore += 10;
                                }
                                else
                                {
                                    friendlyScore += enemy.Health;
                                }
                            }
                        }
                    }
                }
                #endregion
                #region Other Intersection
                foreach (Ship ship in spaceships)
                {
                    foreach (Enemy enemy in enemys)
                    {
                        if (ship.Hitbox.Intersects(enemy.Hitbox) && ship.Alive)
                        {
                            ship.Health -= enemy.Health;
                            enemy.Health = 0;
                            ship.ExplosionHit = true;
                        }
                    }
                }
                #endregion
                #region Remove ded stuff

                    for (int i = 0; i < enemys.Count; i++)
                    {
                    if (enemys[i].Alive == false)
                    {
                        if (enemys[i].KilledByPlayer)
                        {
                            explosions.Add(new Explosion(explosionTexture, new Vector2(enemys[i].X, enemys[i].Y), enemys[i].Color, 250, 50, enemys[i].KilledBy, Team.Friend));
                        }
                        else
                        {
                            explosions.Add(new Explosion(explosionTexture, new Vector2(enemys[i].X, enemys[i].Y), enemys[i].Color, 250, 50, Team.None));
                        }
                            
                            ExplosionSound.Play();
                            enemys.RemoveAt(i);
                        }

                    }
                

                for (int i = 0; i < explosions.Count; i++)
                {
                    if (explosions[i].Exploding == false)
                    {
                        explosions.RemoveAt(i);
                    }
                }
                for (int i = 0; i < projectiles.Count; i++)
                {
                    if (projectiles[i].Remove)
                    {
                        projectiles.RemoveAt(i);
                    }
                }
                #endregion
                #region Medkit
                foreach (Ship ship in spaceships)
                {
                    if (ship.Hitbox.Intersects(medkit.Hitbox))
                    {
                        medkit.Reset();
                        for (int i = 0; i < spaceships.Count; i++)
                        {
                            if (spaceships[i].Alive)
                            { 
                            if (spaceships[i].Health >= 75)
                            {
                                spaceships[i].Health = 100;
                            }
                            else
                            {
                                spaceships[i].Health += 25;
                            }
                            }
                        }
                        break;
                    }
                }
                medkit.Update(gameTime);
                #endregion
                #region Waves
                if (nextWaveTimePassed >= nextWaveWaitTime || enemys.Count <= 0)
                {
                    for (int i = 0; i < RIPs.Count; i++)
                    {
                        //RIPs.RemoveAt(i);
                    }
                    nextWaveTimePassed = new TimeSpan();
                    addNextWave = true;
                }
                nextWaveTimePassed += gameTime.ElapsedGameTime;
                if (addNextWave)
                {

                    wave++;
                    for (int i = 0; i < wave; i++)
                    {
                        enemys.Add(new Enemy(new Vector2(100 * Util.GlobalRandom.Next(0, Settings.WindowWidth / 100), 100 * Util.GlobalRandom.Next(0, Settings.WindowHeight / 100) / 3), new Vector2(Util.GlobalRandom.Next(5, 15), 0), 20, new TimeSpan(0, 0, 0), ufoTexture, Color.White));
                    }
                    addNextWave = false;
                }
                #endregion
                for (int i = 0; i < spaceships.Count; i++)
                {
                    if (spaceships[i].Alive)
                    {
                        spaceships[i].ShipUpdate(gameTime, projectileWaitTime, laserWaitTime, projectiles);
                    }
                    arrows[i].Update(new Vector2(spaceships[i].X, spaceships[i].Y));
                    speedometers[i].CurrentSpeed = (int)spaceships[i].CurrentSpeed + 300;
                    speedometers[i].Update(gameTime);
                }
                foreach (Enemy enemy in enemys)
                {
                    enemy.Update(gameTime, laserTexture, projectiles, laserShot);
                }

                speedneedle.Origin = new Vector2(speedneedle.Width / 2, 0);
                anyAlive = false;
                for (int i = 0; i < spaceships.Count; i++)
                {
                    if (spaceships[i].Alive)
                    {
                        anyAlive = true;
                    }
                }
                if (anyAlive == false)
                {
                    Mouse.SetPosition((int)((float)InputManager.MouseState.X + InputManager.GamePad[PlayerIndex.One].ThumbSticks.Left.X * 30), (int)((float)InputManager.MouseState.Y - InputManager.GamePad[PlayerIndex.One].ThumbSticks.Left.Y * 30));
                    if (InputManager.Keys.IsKeyDown(Keys.Space) && InputManager.LastKeys.IsKeyUp(Keys.Space) || InputManager.GamePad[PlayerIndex.One].Buttons.Start == ButtonState.Pressed && anyAlive == false)
                    {
                        Reset(Settings.Graphics.GraphicsDevice, _content);
                    }
                }
                else
                {
                    if (InputManager.Keys.IsKeyDown(Keys.Back) && InputManager.LastKeys.IsKeyUp(Keys.Back) || InputManager.GamePad[PlayerIndex.One].Buttons.Start == ButtonState.Pressed)
                    {
                        pause = true;
                    }
                }
                base.Update(gameTime);
            }
            else
            {
                Mouse.SetPosition((int)((float)InputManager.MouseState.X + InputManager.GamePad[PlayerIndex.One].ThumbSticks.Left.X * 30), (int)((float)InputManager.MouseState.Y - InputManager.GamePad[PlayerIndex.One].ThumbSticks.Left.Y * 30));

                resumeButton.Update(gameTime);
                restartButton.Update(gameTime);
                mainMenuButton.Update(gameTime);
                if (resumeButton.IsReleased)
                {
                    pause = false;
                }
                else if (restartButton.IsReleased)
                {
                    Reset(Settings.Graphics.GraphicsDevice, _content);
                    pause = false;
                }
                else if (mainMenuButton.IsReleased)
                {
                    ScreenManager.Set("Menu");
                    pause = false;
                }
                else if (InputManager.Keys.IsKeyDown(Keys.Back) && InputManager.LastKeys.IsKeyUp(Keys.Back))
                {
                    pause = false;
                }
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            frames++;
            base.Draw(spriteBatch);

            if (anyAlive == false)
            {
                deathsign.Draw(spriteBatch);
                spriteBatch.DrawString(font, "Press Spacebar or Start button to Restart", new Vector2(Settings.WindowWidth / 2 - font.MeasureString("Press Spacebar or Start button to Restart").X / 2, Settings.WindowHeight - 200), Color.Green);
            }
            else
            {
                medkit.Draw(spriteBatch);
            }
            foreach (Explosion explosion in explosions)
            {
                explosion.Draw(spriteBatch);
            }
            foreach (Enemy enemy in enemys)
            {
                enemy.Draw(spriteBatch);
            }
            foreach (Projectile proj in projectiles)
            {
                proj.Draw(spriteBatch);
            }
            for (int i = 0; i < spaceships.Count; i++)
            {
                spaceships[i].Draw(spriteBatch);
                speedometers[i].Draw(spriteBatch);
                if (spaceships[i].Offscreen && spaceships[i].Alive)
                {
                    arrows[i].Draw(spriteBatch);
                }
            }

            #region GUI

            //Health
            spriteBatch.Draw(pixel, new Rectangle(10, 10, 250, 55 * (spaceships.Count + 1)), Color.Lerp(Color.Green, Color.Transparent, .5f));

            spriteBatch.DrawString(font, "Health", new Vector2(130, 10) - new Vector2(font.MeasureString("Health").X / 2, 0), Color.Green);
            for (int i = 0; i < Settings.Players; i++)
            {
                words = string.Format("P{0}: {1}", i + 1, spaceships[i].Health);
                size = font.MeasureString(words);
                spriteBatch.DrawString(font, words, new Vector2(130, 20 + size.Y * (i + 1)) - new Vector2(size.X / 2, 0), Color.Green);
            }


            //Score
            totalscore = 0;
            foreach (Ship ship in spaceships)
            {
                totalscore += ship.score;
            }
            totalscore += friendlyScore;
            spriteBatch.Draw(pixel, new Rectangle((int)Settings.WindowWidth - 210, 10, 200, 55 * (spaceships.Count + 2)), Color.Lerp(Color.Green, Color.Transparent, .5f));
            spriteBatch.DrawString(font, "Score", new Vector2((int)Settings.WindowWidth - 110, 10) - new Vector2(font.MeasureString("Score").X / 2, 0), Color.Green);

            words = string.Format("Total: {0}", totalscore);
            size = font.MeasureString(words);
            spriteBatch.DrawString(font, words, new Vector2((int)Settings.WindowWidth - 110, 20 + size.Y) - new Vector2(size.X / 2, 0), Color.Green);

            for (int i = 0; i < Settings.Players; i++)
            {
                words = string.Format("P{0}: {1}", i + 1, spaceships[i].score);
                size = font.MeasureString(words);
                spriteBatch.DrawString(font, words, new Vector2((int)Settings.WindowWidth - 110, 20 + size.Y * (i + 2)) - new Vector2(size.X / 2, 0), Color.Green);
            }

            //Border
            spriteBatch.Draw(pixel, new Rectangle(0, 0, 10, (int)Settings.WindowHeight), Color.Lerp(Color.Green, Color.Transparent, .5f));
            spriteBatch.Draw(pixel, new Rectangle((int)Settings.WindowWidth - 10, 0, 10, (int)Settings.WindowHeight), Color.Lerp(Color.Green, Color.Transparent, .5f));
            spriteBatch.Draw(pixel, new Rectangle(10, 0, (int)Settings.WindowWidth - 20, 10), Color.Lerp(Color.Green, Color.Transparent, .5f));
            spriteBatch.Draw(pixel, new Rectangle(10, (int)Settings.WindowHeight - 10, (int)Settings.ScreenWidth - 20, 10), Color.Lerp(Color.Green, Color.Transparent, .5f));
            //G0551J
            //Next Wave
            spriteBatch.Draw(pixel, new Rectangle(10, (int)Settings.WindowHeight - 125, 350, 115), Color.Lerp(Color.Green, Color.Transparent, 0.5f));
            spriteBatch.DrawString(font, "Next Wave In:", new Vector2(180, (int)Settings.WindowHeight - 125) - new Vector2(font.MeasureString("Next Wave In:").X / 2, 0), Color.Green);
            spriteBatch.DrawString(font, string.Format("{0}", 10 - (int)nextWaveTimePassed.TotalSeconds), new Vector2(180, (int)Settings.WindowHeight - 125) - new Vector2(font.MeasureString(string.Format("{0}", 10 - (int)nextWaveTimePassed.TotalSeconds)).X / 2, -font.MeasureString(string.Format("{0}", 10 - (int)nextWaveTimePassed.TotalSeconds)).Y), Color.Green);
            #endregion
            if (pause)
            {
                spriteBatch.DrawString(bigfont, "Pause Menu", new Vector2(Settings.WindowWidth / 2 - bigfont.MeasureString("Pause Menu").X / 2, bigfont.MeasureString("Pause Menu").Y / 2), Color.Green);
                mainMenuButton.Draw(spriteBatch);
                restartButton.Draw(spriteBatch);
                resumeButton.Draw(spriteBatch);
            }

            spriteBatch.DrawString(font, string.Format("FPS: {0}", fps), new Vector2((float)Settings.WindowWidth / 2 - (float)font.MeasureString(string.Format("FPS {0}", fps)).X, font.MeasureString(string.Format("FPS {0}", fps)).Y), Color.Green);


        }
    }
}
