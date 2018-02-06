using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace spaceshooter
{
    //Approval By Kevin ✔
    public class Speedometer : Sprite
    {
        protected Texture2D pixel;
        protected float rotation;

        protected List<Vector2> labelPositions = new List<Vector2>();
        protected SpriteFont font;
        protected int currentSpeed;
        protected int maxSpeed;
        
        public int CurrentSpeed
        {
            get { return currentSpeed; }
            set { currentSpeed = (int)MathHelper.Clamp(value, 0, maxSpeed);
            rotation = currentSpeed / (float)maxSpeed * (float)Math.PI * 1.5f;
            }
        }
        public int MaxSpeed
        {
            get { return maxSpeed; }
            set { maxSpeed = value; }
        }

        public Speedometer(SpriteFont font, Texture2D image, Vector2 position, Color color)
            : base(image, position, color)
        {
            this.font = font;
            origin = new Vector2(image.Width / 2, image.Height / 2);
            pixel = new Texture2D(image.GraphicsDevice, 1, 1);
            pixel.SetData<Color>(new Color[] { Color.White });
            double endpoint = Math.PI * 1.5;
            double startpoint = Math.PI * 1.5;
            double interval = (endpoint + startpoint) / 8 / 2;
            double i;
            for (i = startpoint; i < startpoint + endpoint; i += interval)
            {
                float x = (float)Math.Cos(i);
                float y = (float)Math.Sin(i);
                labelPositions.Add(new Vector2(x, y));
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            
            int speed = 0;
            int counter = 0;
            float speedincrease = maxSpeed / 8f;
            foreach (Vector2 positions in labelPositions)
            {
                counter++;
                spriteBatch.DrawString(font, speed.ToString(), _position - positions * Image.Width / 2.5f*Scale - font.MeasureString(speed.ToString())/2, _color);
                speed = (int)(counter * speedincrease);
            }

            spriteBatch.Draw(pixel, _position, null, _color, rotation, new Vector2(.5f, 0f), new Vector2(4, Image.Width / 3f*Scale.Y), SpriteEffects.None, 0);
        }
    }
}
