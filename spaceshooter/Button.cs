using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace spaceshooter
{
    //Approval By Kevin ✔
    class Button : Sprite
    {
        private bool _isHovered;
        public bool IsHovered
        {
            get { return _isHovered; }
            set { _isHovered = value; }
        }

        private bool _isClicked;
        public bool IsClicked
        {
            get { return _isClicked; }
            set { _isClicked = value; }
        }

        private bool _isReleased;
        public bool IsReleased
        {
            get { return _isReleased; }
            set { _isReleased = value; }
        }


        private string _text;
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        private SpriteFont _font;
        public SpriteFont Font
        {
            get { return _font; }
            set { _font = value; }
        }

        private Color _originalColor;
        public Color OrigionalColor
        {
            get
            {
                return _originalColor;
            }
            set
            {
                _originalColor = value;
            }
        }

        private Color _hoverColor;
        public Color HoverColor
        {
            get
            {
                return _hoverColor;
            }
            set
            {
                _hoverColor = value;
            }
        }

        private Color _clickedColor;
        public Color ClickedColor
        {
            get
            {
                return _clickedColor;
            }
            set
            {
                _clickedColor = value;
            }
        }

        private Color _textColor;
        public Color TextColor
        {
            get
            {
                return _textColor;
            }
            set
            {
                _textColor = value;
            }
        }

        private Color _originalTextColor;
        public Color OrigionalTextColor
        {
            get
            {
                return _originalTextColor;
            }
            set
            {
                _originalTextColor = value;
            }
        }

        private Color _textHoverColor;
        public Color TextHoverColor
        {
            get
            {
                return _textHoverColor;
            }
            set
            {
                _textHoverColor = value;
            }
        }

        private Color _textClickedColor;
        public Color TextClickedColor
        {
            get
            {
                return _textClickedColor;
            }
            set
            {
                _textClickedColor = value;
            }
        }

        public Button(Texture2D texture, Vector2 position, Vector2 size, string text, SpriteFont font, Color textColor, Color textHoverColor, Color textClickedColor, Color originalColor,
                      Color hoverColor, Color clickedColor)
            : base(texture, position, originalColor)
        {
            _scale = size;
            _text = text;
            _font = font;

            _color = originalColor;
            _originalColor = originalColor;
            _hoverColor = hoverColor;
            _clickedColor = clickedColor;

            _textColor = textColor;
            _originalTextColor = textColor;
            _textHoverColor = textHoverColor;
            _textClickedColor = textClickedColor;
        }

        public override void Update(GameTime gameTime)
        {
            _isHovered = false;
            _isClicked = false;
            _isReleased = false;
            if (_hitbox.Contains(InputManager.MouseState.X, InputManager.MouseState.Y))
            {
                _isHovered = true;
                _color = _hoverColor;
                _textColor = _textHoverColor;
                if (InputManager.MouseState.LeftButton == ButtonState.Pressed || InputManager.GamePad[PlayerIndex.One].Buttons.A == ButtonState.Pressed)
                {
                    _isClicked = true;
                    _color = _clickedColor;
                    _textColor = _textClickedColor;
                }
                else if (InputManager.LastMouseState.LeftButton == ButtonState.Pressed || InputManager.LastGamePad[PlayerIndex.One].Buttons.A == ButtonState.Pressed)
                {
                    _isReleased = true;
                }
            }
            else
            {
                _color = _originalColor;
                _textColor = _originalTextColor;
            }
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.DrawString(_font, _text, _position - _font.MeasureString(_text) / 2 - origin + new Vector2(_hitbox.Width,
                _hitbox.Height) / 2, _textColor);
        }

    }
}
