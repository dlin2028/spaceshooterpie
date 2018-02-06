using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace spaceshooter
{
    public static class Shake
    {
        static TimeSpan[] elapsedTime = new TimeSpan[] { TimeSpan.Zero, TimeSpan.Zero, TimeSpan.Zero, TimeSpan.Zero };
        static TimeSpan[] length = new TimeSpan[4] { TimeSpan.Zero, TimeSpan.Zero, TimeSpan.Zero, TimeSpan.Zero };
        static float[] left = new float[4] { 0, 0, 0, 0 };
        static float[] right = new float[4] { 0, 0, 0, 0 };

        public static void SetVibration(PlayerIndex player, float newleft, float newright, TimeSpan newlength)
        {
            elapsedTime[(int)player] = TimeSpan.Zero;
            length[(int)player] = newlength;
            left[(int)player] = newleft;
            right[(int)player] = newright;
        }

        public static void Update(GameTime gameTime)
        {
            for (int i = 0; i < length.Length; i++)
            {
                elapsedTime[i] += gameTime.ElapsedGameTime;
                if (elapsedTime[i] < length[i])
                {
                    GamePad.SetVibration((PlayerIndex)i, left[i], right[i]);
                }
                else
                {
                    GamePad.SetVibration((PlayerIndex)i, 0, 0);
                }
                
            }
        }
    }
}
