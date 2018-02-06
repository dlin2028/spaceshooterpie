using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace spaceshooter
{
    //approve plz
    public static class Util
    {
        public static Random GlobalRandom = new Random();

        public static Vector2 RadianToVector(this float rotation)
        {
            rotation -= (float)Math.PI / 2;
            Vector2 answer = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));
            return answer;
        }
    }
}
