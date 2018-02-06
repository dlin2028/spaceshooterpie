using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace spaceshooter
{
    public static class Settings
    {
        //Approval By Kevin ✔
        private static Game Game;
        private static GraphicsDeviceManager graphics;
        public static GraphicsDeviceManager Graphics
        {
            get
            {
                return graphics;
            }
        }

        public static int WindowWidth
        {
            get
            {
                return graphics.PreferredBackBufferWidth;
            }
            set
            {
                graphics.PreferredBackBufferWidth = value;
                graphics.ApplyChanges();
            }
        }

        public static int WindowHeight
        {
            get
            {
                return graphics.PreferredBackBufferHeight;
            }
            set
            {
                graphics.PreferredBackBufferHeight = value;
                graphics.ApplyChanges();
            }
        }

        public static int ScreenWidth { get { return graphics.GraphicsDevice.Viewport.Width; } }

        public static int ScreenHeight { get { return graphics.GraphicsDevice.Viewport.Height; } }

        private static int volume;
        public static int Volume
        {
            get
            {
                return volume;
            }
            set
            {
                volume = value;
                SoundEffect.MasterVolume = value / 100f;
            }
        }

        public static void Init(Game game)
        {
            Game = game;
            graphics = new GraphicsDeviceManager(game);
        }

        private static int _players = 1;
        public static int Players
        {
            get { return _players; }
            set { _players = value; }
        }


        public static void ExitGame()
        {
            graphics.GraphicsDevice.Dispose();
            Game.Exit();
        }
    }
}
