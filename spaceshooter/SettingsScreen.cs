using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.Threading;
using Microsoft.Xna.Framework.Input;

namespace spaceshooter
{
    //approve plz
    public class SettingsScreen : Screen
    {
        ContentManager _content;

        Sprite Finger;

        SpriteFont font;
        SpriteFont bigFont;

        Button volumeButton;
        Button resolutionButton;
        Button confirmButton;

        int volume;
        int width;
        int height;
        int displayres;

        public override void Reset(GraphicsDevice GraphicsDevice, ContentManager Content)
        {
            sprites = new List<Sprite>();

            _content = Content;

            Finger = new Sprite(Content.Load<Texture2D>("LEL"), Vector2.Zero, Color.White);
            Finger.Origin = new Vector2(0, 0);
            font = Content.Load<SpriteFont>("font");
            bigFont = Content.Load<SpriteFont>("bigfont");

            volume = 100;
            displayres = 1;
            width = 1280;
            height = 720;

            Texture2D pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData<Color>(new Color[] { Color.White });
            Color buttonColor = Color.Lerp(Color.Green, Color.Transparent, .5f);
            Color buttonHoverColor = Color.Lerp(Color.Green, Color.Transparent, .25f);
            volumeButton = new Button(pixel, new Vector2(Settings.WindowWidth / 2 - 300, Settings.WindowHeight / 2 - 200), new Vector2(600, 150), string.Format("Volume: {0}", volume), font, Color.Green, Color.Green, Color.White, buttonColor, buttonHoverColor, Color.Green);
            resolutionButton = new Button(pixel, new Vector2(Settings.WindowWidth / 2 - 300, Settings.WindowHeight / 2), new Vector2(600, 150), string.Format("Resolution: {0}x{1}", width, height), font, Color.Green, Color.Green, Color.White, buttonColor, buttonHoverColor, Color.Green);
            confirmButton = new Button(pixel, new Vector2(Settings.WindowWidth / 2 - 300, Settings.WindowHeight / 2 + 200), new Vector2(600, 150), "Confirm", font, Color.Green, Color.Green, Color.White, buttonColor, buttonHoverColor, Color.Green);

            sprites.Add(volumeButton);
            sprites.Add(resolutionButton);
            sprites.Add(confirmButton);
        }

        public SettingsScreen(GraphicsDevice GraphicsDevice, ContentManager Content)
            :base(GraphicsDevice, Content)
        {
            _content = Content;

            Finger = new Sprite(Content.Load<Texture2D>("LEL"), Vector2.Zero, Color.White);
            Finger.Origin = new Vector2(0, 0);
            font = Content.Load<SpriteFont>("font");
            bigFont = Content.Load<SpriteFont>("bigfont");

            volume = 100;
            displayres = 1;
            width = 1280;
            height = 720;

            Texture2D pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData<Color>(new Color[] { Color.White });
            Color buttonColor = Color.Lerp(Color.Green, Color.Transparent, .5f);
            Color buttonHoverColor = Color.Lerp(Color.Green, Color.Transparent, .25f);
            volumeButton = new Button(pixel, new Vector2(Settings.WindowWidth / 2 - 300, Settings.WindowHeight / 2 - 200), new Vector2(600, 150), string.Format("Volume: {0}", volume), font, Color.Green, Color.Green, Color.White, buttonColor, buttonHoverColor, Color.Green);
            resolutionButton = new Button(pixel, new Vector2(Settings.WindowWidth / 2 - 300, Settings.WindowHeight / 2), new Vector2(600, 150), string.Format("Resolution: {0}x{1}", width, height), font, Color.Green, Color.Green, Color.White, buttonColor, buttonHoverColor, Color.Green);
            confirmButton = new Button(pixel, new Vector2(Settings.WindowWidth / 2 - 300, Settings.WindowHeight / 2 + 200), new Vector2(600, 150), "Confirm", font, Color.Green, Color.Green, Color.White, buttonColor, buttonHoverColor, Color.Green);

            sprites.Add(volumeButton);
            sprites.Add(resolutionButton);
            sprites.Add(confirmButton);

        }

        public override void Update(GameTime gameTime)
        {
            
            Mouse.SetPosition((int)((float)InputManager.MouseState.X + InputManager.GamePad[PlayerIndex.One].ThumbSticks.Left.X * 30), (int)((float)InputManager.MouseState.Y - InputManager.GamePad[PlayerIndex.One].ThumbSticks.Left.Y * 30));

            Finger.X = InputManager.MouseState.X;
            Finger.Y = InputManager.MouseState.Y;

            if (volumeButton.IsReleased)
            {
                if (volume < 100)
                {
                    volume += 10;
                }
                else
                {
                    volume = 0;
                }
                volumeButton.Text = string.Format("Volume: {0}", volume);
            }
            if(resolutionButton.IsReleased)
            {
                #region Resolution
                if (displayres < 5)
                {
                    displayres++;
                }
                else
                {
                    displayres = 0;
                }
                if (displayres == 0)
                {
                    width = 1280;
                    height = 720;
                }
                else if (displayres == 1)
                {
                    width = 1366;
                    height = 768;
                }
                else if (displayres == 2)
                {
                    width = 1600;
                    height = 900;
                }
                else if (displayres == 3)
                {
                    width = 1920;
                    height = 1080;
                }
                else if(displayres == 4)
                {
                    width = 2048;
                    height = 1152;
                }
                #endregion
                resolutionButton.Text = string.Format("Resolution: {0}x{1}", width, height);
                if(displayres == 5)
                {
                    resolutionButton.Text = string.Format("Full Screen", width, height);
                }
            }
            if (confirmButton.IsReleased)
            {
                Settings.Volume = volume;
                if (displayres != 5)
                { 
                Settings.WindowWidth = width;
                Settings.WindowHeight = height;
                Settings.Graphics.IsFullScreen = false;
                }
                else
                {
                Settings.Graphics.IsFullScreen = true;
                }
                Settings.Graphics.ApplyChanges();
                ScreenManager.Reset("Menu", Settings.Graphics.GraphicsDevice, _content);
                ScreenManager.Set("Menu");
            }
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(bigFont, "Settings", new Vector2(Settings.WindowWidth / 2 - bigFont.MeasureString("Settings").X / 2, bigFont.MeasureString("Settings").Y / 2 - 25), Color.Green);
            base.Draw(spriteBatch);

            Finger.Draw(spriteBatch);
        }
    }
}
