using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace spaceshooter
{
    //approve plz
    public class MenuScreen : Screen
    {
        Button startButton;
        Button playerButton;
        Button exitButton;
        Button settingsButton;

        int playerAmount;
        
        SpriteFont font;
        SpriteFont bigFont;

        ContentManager _content;

        Sprite finger;
        Sprite megusta;

        public override void Reset(GraphicsDevice GraphicsDevice, ContentManager Content)
        {
            base.Reset(graphicsDevice, Content);
            _content = Content;
            playerAmount = 1;

            Texture2D pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData<Color>(new Color[] { Color.White });

            font = Content.Load<SpriteFont>("font");
            bigFont = Content.Load<SpriteFont>("bigfont");

            Color buttonColor = Color.Lerp(Color.Green, Color.Transparent, .5f);
            Color buttonHoverColor = Color.Lerp(Color.Green, Color.Transparent, .25f);

            megusta = new Sprite(Content.Load<Texture2D>("meguseta"), Vector2.Zero, Color.White);
            finger = new Sprite(Content.Load<Texture2D>("LEL"), Vector2.Zero, Color.White);
            finger.Origin = new Vector2(0, 0);
            sprites.Add(finger);

            startButton = new Button(pixel, new Vector2(Settings.WindowWidth / 2 - 300, Settings.WindowHeight / 2 - 200), new Vector2(600, 150), "Start", font, Color.Green, Color.Green, Color.White, buttonColor, buttonHoverColor, Color.Green);
            playerButton = new Button(pixel, new Vector2(Settings.WindowWidth / 2 - 300, Settings.WindowHeight / 2), new Vector2(600, 150), "1 Player", font, Color.Green, Color.Green, Color.White, buttonColor, buttonHoverColor, Color.Green);
            exitButton = new Button(pixel, new Vector2(Settings.WindowWidth / 2 - 300, Settings.WindowHeight / 2 + 200), new Vector2(600, 150), "Exit", font, Color.Green, Color.Green, Color.White, buttonColor, buttonHoverColor, Color.Green);
            settingsButton = new Button(pixel, new Vector2(Settings.WindowWidth - 250, Settings.WindowHeight - 120), new Vector2(225, 100), "Settings", font, Color.Green, Color.Green, Color.White, buttonColor, buttonHoverColor, Color.Green);

            sprites.Add(startButton);
            sprites.Add(playerButton);
            sprites.Add(exitButton);
            sprites.Add(settingsButton);
        }

        public MenuScreen(GraphicsDevice GraphicsDevice, ContentManager Content)
            : base(GraphicsDevice, Content)
        {
            base.Reset(graphicsDevice, Content);
            _content = Content;
            playerAmount = 1;

            Texture2D pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData<Color>(new Color[] { Color.White });

            font = Content.Load<SpriteFont>("font");
            bigFont = Content.Load<SpriteFont>("bigfont");

            Color buttonColor = Color.Lerp(Color.Green, Color.Transparent, .5f);
            Color buttonHoverColor = Color.Lerp(Color.Green, Color.Transparent, .25f);
            
            megusta = new Sprite(Content.Load<Texture2D>("meguseta"), Vector2.Zero, Color.White);
            finger = new Sprite(Content.Load<Texture2D>("LEL"), Vector2.Zero, Color.White);
            finger.Origin = new Vector2(0, 0);
            megusta.Origin = new Vector2(0, 0);

            startButton = new Button(pixel, new Vector2(Settings.WindowWidth / 2 - 300, Settings.WindowHeight / 2 - 200), new Vector2(600, 150), "Start", font, Color.Green, Color.Green, Color.White, buttonColor, buttonHoverColor, Color.Green);
            playerButton = new Button(pixel, new Vector2(Settings.WindowWidth / 2 - 300, Settings.WindowHeight / 2), new Vector2(600, 150), "1 Player", font, Color.Green, Color.Green, Color.White, buttonColor, buttonHoverColor, Color.Green);
            exitButton = new Button(pixel, new Vector2(Settings.WindowWidth / 2 - 300, Settings.WindowHeight / 2 + 200), new Vector2(600, 150), "Exit", font, Color.Green, Color.Green, Color.White, buttonColor, buttonHoverColor, Color.Green);
            settingsButton = new Button(pixel, new Vector2(Settings.WindowWidth - 250, Settings.WindowHeight - 120), new Vector2(225, 100), "Settings", font, Color.Green, Color.Green, Color.White, buttonColor, buttonHoverColor, Color.Green);

            sprites.Add(startButton);
            sprites.Add(playerButton);
            sprites.Add(exitButton);
            sprites.Add(settingsButton);
            sprites.Add(finger);
            sprites.Add(megusta);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            finger.X = InputManager.MouseState.X;
            finger.Y = InputManager.MouseState.Y;

            megusta.X = InputManager.MouseState.X;
            megusta.Y = InputManager.MouseState.Y;
            

            Mouse.SetPosition((int)((float)InputManager.MouseState.X + InputManager.GamePad[PlayerIndex.One].ThumbSticks.Left.X * 30), (int)((float)InputManager.MouseState.Y - InputManager.GamePad[PlayerIndex.One].ThumbSticks.Left.Y * 30));
            base.Update(gameTime);
            if (startButton.IsReleased)
            {
                Settings.Players = playerAmount;
                ScreenManager.Set("Game");
                ScreenManager.Reset("Game", graphicsDevice, _content);
            }
            else if (playerButton.IsReleased)
            {
                if (playerAmount < 4)
                {
                    playerAmount++;
                    playerButton.Text = string.Format("{0} Players", playerAmount);
                }
                else
                {
                    playerAmount = 1;
                    playerButton.Text = "1 Player";
                }
            }
            else if (exitButton.IsReleased)
            {
                Settings.ExitGame();
            }
            else if (settingsButton.IsReleased)
            {
                ScreenManager.Set("Settings");
                ScreenManager.Reset("Settings", graphicsDevice, _content);
            }
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(bigFont, "Space Shooter", new Vector2(Settings.WindowWidth / 2 - bigFont.MeasureString("Space Shoter").X / 2, bigFont.MeasureString("Space Shooter").Y / 2 - 25), Color.Green);
            base.Draw(spriteBatch);
        }
    }
}
