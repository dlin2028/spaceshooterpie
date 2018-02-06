using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace spaceshooter
{
    class BeginScreen : Screen
    {
        TimeSpan WaitTime;
        TimeSpan TimePassed;

        public Sprite creator;
        public BeginScreen(GraphicsDevice GraphicsDevice, ContentManager Content)
            : base(GraphicsDevice, Content)
        {
            WaitTime = new TimeSpan(0, 0, 1);
            TimePassed = new TimeSpan();

            creator = new Sprite(Content.Load<Texture2D>("Cweatowr"), new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), Color.White);
            sprites.Add(creator);
        }
        public override void Update(GameTime gameTime)
        {
            if (TimePassed > WaitTime)
            {
                ScreenManager.Set("Menu");
            }
            TimePassed += gameTime.ElapsedGameTime;
            base.Update(gameTime);
        }
    }
}
