using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Assignment2;

public class Paddle
{
    private const int _width = 12, _height = 108, _speed = 240;

    private Texture2D _texture;
    private Vector2 _position, _velocity;   
    private Rectangle _playAreaBoundingBox;

    internal void Initialize(Vector2 startingPos, Rectangle playAreaBoundingBox)
    {
        _playAreaBoundingBox = playAreaBoundingBox;
        _position = startingPos;
        _velocity = new Vector2(-_speed, -_speed); //up and to the left
    }

    internal void LoadContent(ContentManager content)
    {
        _texture = content.Load<Texture2D>("Paddle"); 
    }

    internal void Update(GameTime gameTime, KeyboardState kbState)
    {
        float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;

        if(kbState.IsKeyDown(Keys.Up)) {
            _velocity = new Vector2(0, -_speed);
            _position += _velocity * dt;
        }

        if(kbState.IsKeyDown(Keys.Down)) {
            _velocity = new Vector2(0, _speed);
            _position += _velocity * dt;
        }

        if (_position.Y <= _playAreaBoundingBox.Top) {
            _position.Y = _playAreaBoundingBox.Top;
        } else if(_position.Y + _height >= _playAreaBoundingBox.Bottom)
        {
            _position.Y = _playAreaBoundingBox.Bottom - _height;
        }
    }

    internal void Draw(SpriteBatch spriteBatch)
    {
        Rectangle paddleRectangle = new Rectangle((int) _position.X, (int) _position.Y, _width, _height);
        spriteBatch.Draw(_texture, paddleRectangle, Color.White);
    }
}

