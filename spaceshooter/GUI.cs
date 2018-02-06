/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace SpaceShooterDavidLinPie
{
    public class GUI
    {
        private int _settingsWidth;
        public int SettingsWidth
        {
            get { return _settingsWidth; }
            set { _settingsWidth = value; }
        }

        private int _settingsHeight;
        public int SettingsHeight
        {
            get { return _settingsHeight; }
            set { _settingsHeight = value; }
        }

        private int _settingsVolume;
        public int SettingsVolume
        {
            get { return _settingsVolume; }
            set { _settingsVolume = value; }
        }

        private bool _applySettings;
        public bool ApplySettings
        {
            get { return _applySettings; }
            set { _applySettings = value; }
        }

        private bool _restart = false;
        public bool Restart
        {
            get
            {
                return _restart;
            }
            set
            {
                _restart = value;
            }
        }

        private bool _exit;
        public bool Exit
        {
            get
            {
                return _exit;
            }
            set
            {
                _exit = value;
            }
        }

        private int totalScore;
        protected int cursorscore = 0;
        protected Texture2D pixel;
        protected SpriteFont bigfont;
        protected SpriteFont font;
        protected SpriteFont smallFont;
        protected Color color;
        protected int playernumber;

        protected Vector2 screenSize;
        public int CursorScore
        {
            get { return cursorscore; }
            set { cursorscore = value; }
        }
        public Dictionary<PlayerIndex, int> PlayerHealth;
        public Dictionary<PlayerIndex, int> PlayerScore;
        public TimeSpan timeLeft;
        public Dictionary<PlayerIndex, Speedometer> PlayerSpeed;




        public GUI(ContentManager Content, GraphicsDevice graphicsDevice, Color color, int playernumber)
        {
            screenSize = new Vector2(graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height);

            font = Content.Load<SpriteFont>("font");
            smallFont = Content.Load<SpriteFont>("smallFont");
            bigfont = Content.Load<SpriteFont>("bigfont");

            this.color = color;
            this.playernumber = playernumber;

            pixel = new Texture2D(graphicsDevice, 1, 1);
            pixel.SetData<Color>(new Color[] { Color.White });

            PlayerHealth = new Dictionary<PlayerIndex, int>();
            PlayerScore = new Dictionary<PlayerIndex, int>();
            PlayerSpeed = new Dictionary<PlayerIndex, Speedometer>();

            _settingsWidth = (int)screenSize.X;
            _settingsHeight = (int)screenSize.Y;
            _settingsVolume = 100;
            _applySettings = false;
            _exit = false;

            for (int i = 0; i < playernumber; i++)
            {
                PlayerHealth.Add((PlayerIndex)i, 100);
                PlayerScore.Add((PlayerIndex)i, 0);
                PlayerSpeed.Add((PlayerIndex)i, new Speedometer(smallFont, Content.Load<Texture2D>("BALLER"), screenSize - new Vector2(200 * (playernumber - i) - 50, 125), color) { Scale = new Vector2(.75f) });
            }
        }

        public void DrawGame(SpriteBatch spriteBatch)
        {
            string words;
            Vector2 size;
            //Border
            spriteBatch.Draw(pixel, new Rectangle(0, 0, 10, (int)screenSize.Y), Color.Lerp(color, Color.Transparent, .5f));
            spriteBatch.Draw(pixel, new Rectangle((int)screenSize.X - 10, 0, 10, (int)screenSize.Y), Color.Lerp(color, Color.Transparent, .5f));
            spriteBatch.Draw(pixel, new Rectangle(10, 0, (int)screenSize.X - 20, 10), Color.Lerp(color, Color.Transparent, .5f));
            spriteBatch.Draw(pixel, new Rectangle(10, (int)screenSize.Y - 10, (int)screenSize.X - 20, 10), Color.Lerp(color, Color.Transparent, .5f));

            //Health
            spriteBatch.Draw(pixel, new Rectangle(10, 10, 250, 55 * (playernumber + 1)), Color.Lerp(color, Color.Transparent, .5f));

            spriteBatch.DrawString(font, "Health", new Vector2(130, 10) - new Vector2(font.MeasureString("Health").X / 2, 0), color);
            for (int i = 0; i < playernumber; i++)
            {
                words = string.Format("P{0}: {1}", i + 1, PlayerHealth[(PlayerIndex)i]);
                size = font.MeasureString(words);
                spriteBatch.DrawString(font, words, new Vector2(130, 20 + size.Y * (i + 1)) - new Vector2(size.X / 2, 0), color);
            }

            //Score
            totalScore = 0;
            for (int i = 0; i < playernumber; i++)
            {
                totalScore += PlayerScore[(PlayerIndex)i];
            }
            totalScore += cursorscore;

            spriteBatch.Draw(pixel, new Rectangle((int)screenSize.X - 210, 10, 200, 55 * (playernumber + 2)), Color.Lerp(color, Color.Transparent, .5f));
            spriteBatch.DrawString(font, "Score", new Vector2((int)screenSize.X - 110, 10) - new Vector2(font.MeasureString("Score").X / 2, 0), color);

            words = string.Format("Total: {0}", totalScore);
            size = font.MeasureString(words);
            spriteBatch.DrawString(font, words, new Vector2((int)screenSize.X - 110, 20 + size.Y) - new Vector2(size.X / 2, 0), color);
            for (int i = 0; i < playernumber; i++)
            {
                words = string.Format("P{0}: {1}", i + 1, PlayerScore[(PlayerIndex)i]);
                size = font.MeasureString(words);
                spriteBatch.DrawString(font, words, new Vector2((int)screenSize.X - 110, 20 + size.Y * (i + 2)) - new Vector2(size.X / 2, 0), color);
            }

            //Next Wave
            spriteBatch.Draw(pixel, new Rectangle(10, (int)screenSize.Y - 125, 350, 115), Color.Lerp(color, Color.Transparent, 0.5f));
            spriteBatch.DrawString(font, "Next Wave In:", new Vector2(180, (int)screenSize.Y - 125) - new Vector2(font.MeasureString("Next Wave In:").X / 2, 0), color);
            spriteBatch.DrawString(font, string.Format("{0}", (int)timeLeft.TotalSeconds), new Vector2(180, (int)screenSize.Y - 125) - new Vector2(font.MeasureString(string.Format("{0}", (int)timeLeft.TotalSeconds)).X / 2, -font.MeasureString(string.Format("{0}", (int)timeLeft.TotalSeconds)).Y), color);

            for (int i = 0; i < playernumber; i++)
            {
                PlayerSpeed[(PlayerIndex)i].Draw(spriteBatch);
            }
        }
        public void DrawPause(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(pixel, new Rectangle((int)screenSize.X / 2 - 300, (int)screenSize.Y / 2 - 200, 600, 150), Color.Lerp(color, Color.Transparent, .5f));
            spriteBatch.Draw(pixel, new Rectangle((int)screenSize.X / 2 - 300, (int)screenSize.Y / 2, 600, 150), Color.Lerp(color, Color.Transparent, .5f));
            spriteBatch.Draw(pixel, new Rectangle((int)screenSize.X / 2 - 300, (int)screenSize.Y / 2 + 200, 600, 150), Color.Lerp(color, Color.Transparent, .5f));
            spriteBatch.DrawString(font, "Resume", new Vector2(screenSize.X / 2 - font.MeasureString("Resume").X / 2, screenSize.Y / 2 - 150), Color.Green);
            spriteBatch.DrawString(font, "Restart", new Vector2(screenSize.X / 2 - font.MeasureString("Restart").X / 2, screenSize.Y / 2 + font.MeasureString("Restart").Y), Color.Green);
            spriteBatch.DrawString(font, "Main Menu", new Vector2(screenSize.X / 2 - font.MeasureString("Main Menu").X / 2, screenSize.Y / 2 + font.MeasureString("Main Menu").Y + 200), Color.Green);
            spriteBatch.DrawString(bigfont, "Pause Menu", new Vector2(screenSize.X / 2 - bigfont.MeasureString("Pause Menu").X / 2, bigfont.MeasureString("Pause Menu").Y), Color.Green);
        }
        public void DrawSettings(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(bigfont, "Settings", new Vector2(screenSize.X / 2 - bigfont.MeasureString("Settings").X / 2, bigfont.MeasureString("Settings").Y / 2 + 25), Color.Lerp(color, Color.Transparent, .5f));
            spriteBatch.Draw(pixel, new Rectangle((int)screenSize.X / 2 - 300, (int)screenSize.Y / 2 - 200, 600, 150), Color.Lerp(color, Color.Transparent, .5f));
            spriteBatch.Draw(pixel, new Rectangle((int)screenSize.X / 2 - 300, (int)screenSize.Y / 2, 600, 150), Color.Lerp(color, Color.Transparent, .5f));
            spriteBatch.Draw(pixel, new Rectangle((int)screenSize.X / 2 - 300, (int)screenSize.Y / 2 + 200, 600, 150), Color.Lerp(color, Color.Transparent, .5f));
            spriteBatch.DrawString(font, string.Format("Volume : {0}", _settingsVolume), new Vector2(screenSize.X / 2 - font.MeasureString(string.Format("Volume : {0}", _settingsVolume)).X / 2, screenSize.Y / 2 - 150), Color.Green);
            spriteBatch.DrawString(font, string.Format("Screen Size : {0} x {1}", _settingsWidth, _settingsHeight), new Vector2(screenSize.X / 2 - font.MeasureString(string.Format("Screen Size : {0} x {1}", _settingsWidth, _settingsHeight)).X / 2, screenSize.Y / 2 + font.MeasureString(string.Format("Screen Size : {0} x {1}", _settingsWidth, _settingsHeight)).Y), Color.Green);
            spriteBatch.DrawString(font, "Confirm", new Vector2(screenSize.X / 2 - font.MeasureString("Confirm").X / 2, screenSize.Y / 2 + font.MeasureString("Confirm").Y + 200), Color.Green);

        }



        int displayres;
        public void UpdateSettings(MouseState mouse, MouseState lastmouse, ref Screens screen)
        {
            if (new Rectangle((int)screenSize.X / 2 - 300, (int)screenSize.Y / 2 - 200, 600, 150).Contains((int)mouse.X, (int)mouse.Y) && mouse.LeftButton != ButtonState.Released && lastmouse.LeftButton != ButtonState.Pressed)
            {
                if (_settingsVolume < 100)
                {
                    _settingsVolume += 10;
                }
                else
                {
                    _settingsVolume = 0;
                }
            }
            else if (new Rectangle((int)screenSize.X / 2 - 300, (int)screenSize.Y / 2, 600, 150).Contains((int)mouse.X, (int)mouse.Y) && mouse.LeftButton != ButtonState.Released && lastmouse.LeftButton != ButtonState.Pressed)
            {
                if (displayres < 4)
                {
                    displayres++;
                }
                else
                {
                    displayres = 0;
                }
                if (displayres == 0)
                {
                    _settingsHeight = 720;
                    _settingsWidth = 1280;
                }
                else if (displayres == 1)
                {
                    _settingsHeight = 720;
                    _settingsWidth = 1280;
                    
                }
                else if (displayres == 2)
                {
                    _settingsHeight = 900;
                    _settingsWidth = 1600;
                }
                else if (displayres == 3)
                {
                    _settingsHeight = 1080;
                    _settingsWidth = 1920;
                }
                else
                {
                    _settingsHeight = 1152;
                    _settingsWidth = 2048;
                }

            }
            else if (new Rectangle((int)screenSize.X / 2 - 300, (int)screenSize.Y / 2 + 200, 600, 150).Contains((int)mouse.X, (int)mouse.Y) && mouse.LeftButton != ButtonState.Released && lastmouse.LeftButton != ButtonState.Pressed)
            {
                _applySettings = true;
            }
        }

        public void UpdateScreenSize(Viewport viewport)
        {
            screenSize.X = viewport.Width;
            screenSize.Y = viewport.Height;
        }

    }
}
 * */
//Old 4 box settings
//spriteBatch.Draw(pixel, new Rectangle((int)screenSize.X / 2 - 300, (int)screenSize.Y / 2 - 300, 600, 150), Color.Lerp(color, Color.Transparent, .5f));
//spriteBatch.Draw(pixel, new Rectangle((int)screenSize.X / 2 - 300, (int)screenSize.Y / 2 - 100, 600, 150), Color.Lerp(color, Color.Transparent, .5f));
//spriteBatch.Draw(pixel, new Rectangle((int)screenSize.X / 2 - 300, (int)screenSize.Y / 2 + 100, 600, 150), Color.Lerp(color, Color.Transparent, .5f));
//spriteBatch.Draw(pixel, new Rectangle((int)screenSize.X / 2 - 300, (int)screenSize.Y / 2 + 300, 600, 150), Color.Lerp(color, Color.Transparent, .5f));
//spriteBatch.DrawString(font, string.Format("Volume : {0}", volume), new Vector2(screenSize.X / 2 - (font.MeasureString(string.Format("Volume: {0}", volume)).X/2), screenSize.Y / 2 - 250), Color.Green);