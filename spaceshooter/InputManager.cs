using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace spaceshooter
{
    public static class InputManager
    {
        //Approval By Kevin ✔
        public static MouseState MouseState { get; set; }
        public static MouseState LastMouseState { get; private set; }
        public static KeyboardState Keys { get; private set; }
        public static KeyboardState LastKeys { get; private set; }

        static Dictionary<PlayerIndex, GamePadState> gamePad = new Dictionary<PlayerIndex, GamePadState>()
        {
            { PlayerIndex.One, new GamePadState() },
            { PlayerIndex.Two, new GamePadState() },
            { PlayerIndex.Three, new GamePadState() },
            { PlayerIndex.Four, new GamePadState() }
        };

        static Dictionary<PlayerIndex, GamePadState> lastGamePad = new Dictionary<PlayerIndex, GamePadState>()
        {
            { PlayerIndex.One, new GamePadState() },
            { PlayerIndex.Two, new GamePadState() },
            { PlayerIndex.Three, new GamePadState() },
            { PlayerIndex.Four, new GamePadState() }
        };

        public static Dictionary<PlayerIndex, GamePadState> GamePad { get { return gamePad; } }
        public static Dictionary<PlayerIndex, GamePadState> LastGamePad { get { return lastGamePad; } }


        public static void Update()
        {
            LastMouseState = MouseState;
            MouseState = Mouse.GetState();


            LastKeys = Keys;
            Keys = Keyboard.GetState();

            for (int i = 0; i < Settings.Players; i++)
            {
                lastGamePad[(PlayerIndex)i] = gamePad[(PlayerIndex)i];
                gamePad[(PlayerIndex)i] = Microsoft.Xna.Framework.Input.GamePad.GetState((PlayerIndex)i);
            }
            
        }
    }
}
