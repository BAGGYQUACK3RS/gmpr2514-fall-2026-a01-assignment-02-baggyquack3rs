using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Assignment2;

public class Paddle
{
    private const int _width = 12, _height = 108, _baseSpeed = 120;
    private int _speed = 120;

    private Texture2D _texture;
    private Vector2 _position, _velocity;   
    private Rectangle _playAreaBoundingBox;

    internal Vector2 Direction
    {
        set{ _velocity = value * _speed;}
    }

    internal int Speed
    {
        set{_speed = _baseSpeed * value;}
    }

    internal Rectangle BoundingBox
    {
        get { return new Rectangle((int)_position.X, (int)_position.Y, _width, _height);}
    }

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

    internal void Update(GameTime gameTime)
    {
        float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;
        _position += _velocity * dt;

        if (_position.Y <= _playAreaBoundingBox.Top) {
            _position.Y = _playAreaBoundingBox.Top;
        } else if(_position.Y + _height >= _playAreaBoundingBox.Bottom)
        {
            _position.Y = _playAreaBoundingBox.Bottom - _height;
        }
    }

    internal void Draw(SpriteBatch spriteBatch)
    {
         spriteBatch.Draw(_texture, BoundingBox, Color.White);
    }
}

