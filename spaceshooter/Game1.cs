using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Xml;
using System.Threading;

namespace spaceshooter
{
    //approve plz
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        Background background;
        Background background2;
        SpriteBatch spriteBatch;

        public Game1() :
            base()
        {
            Settings.Init(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Settings.WindowWidth = GraphicsDevice.DisplayMode.Width;
            Settings.WindowHeight = GraphicsDevice.DisplayMode.Height;
            #region ScreenManager
            ScreenManager.Add("Begin", new BeginScreen(GraphicsDevice, Content));
            ScreenManager.Add("Game", new GameScreen(GraphicsDevice, Content));
            ScreenManager.Add("Menu", new MenuScreen(GraphicsDevice, Content));
            ScreenManager.Add("Settings", new SettingsScreen(GraphicsDevice, Content));
            #endregion
            ScreenManager.Set("Begin");

            Texture2D backimage = Content.Load<Texture2D>("PIEEEEEEEEEEEEEEEEEEEEEEEEEEZ");
            background = new Background(backimage, new Vector2(0, -backimage.Height), new Vector2(0, GraphicsDevice.Viewport.Height), new Vector2(0, 2), Color.White, false, false);
            background2 = new Background(backimage, new Vector2(0, -backimage.Height), new Vector2(0, GraphicsDevice.Viewport.Height), new Vector2(0, 2), Color.White, false, false);
            background.Y = 0;
        }
        protected override void UnloadContent()
        {
        }
        protected override void Update(GameTime gameTime)
        {

            InputManager.Update();
            ScreenManager.Update(gameTime);

            background.Update(gameTime);
            background2.Update(gameTime);


            if (InputManager.Keys.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            background.Draw(spriteBatch);
            background2.Draw(spriteBatch);

            ScreenManager.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
