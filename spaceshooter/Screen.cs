using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace spaceshooter
{
    public abstract class Screen
    {
        public List<Sprite> sprites;
        protected GraphicsDevice graphicsDevice;

        public Screen(GraphicsDevice GraphicsDevice, ContentManager Content)
        {
            sprites = new List<Sprite>();
            this.graphicsDevice = GraphicsDevice;
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (Sprite sprite in sprites)
            {
                sprite.Update(gameTime);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (Sprite sprite in sprites)
            {
                sprite.Draw(spriteBatch);
            }
        }

        public virtual void Reset(GraphicsDevice GraphicsDevice, ContentManager Content)
        {
            sprites = new List<Sprite>();
            this.graphicsDevice = GraphicsDevice;
        }
    }
}
