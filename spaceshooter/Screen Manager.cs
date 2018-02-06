using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace spaceshooter
{
    public static class ScreenManager
    {
        static Dictionary<string, Screen> screens = new Dictionary<string,Screen>();
        public static Screen CurrentScreen;

        

        public static void Add(string name, Screen screen)
        {
            screens.Add(name, screen);
        }
        public static void RemoveAll()
        {
            screens = new Dictionary<string,Screen>();
        }

        public static void Set(string name)
        {
            CurrentScreen = screens[name];
        }

        public static void Reset(string name, GraphicsDevice graphicsDevice, ContentManager content)
        {
            screens[name].Reset(graphicsDevice, content);
        }

    public static void Update(GameTime gameTime)
        {
            if (CurrentScreen != null)
            {
                CurrentScreen.Update(gameTime);
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            if (CurrentScreen != null)
            {
                CurrentScreen.Draw(spriteBatch);
            }
        }
    }
}
