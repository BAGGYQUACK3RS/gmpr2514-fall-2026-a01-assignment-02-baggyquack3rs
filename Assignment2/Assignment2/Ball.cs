using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Assignment2;

public class Ball
{
    private const int _widthAndHeight = 21, _speed = 240;

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
        _texture = content.Load<Texture2D>("Ball"); 
    }

    internal void Update(GameTime gameTime)
    {
        float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;

        _position += _velocity * dt;

        if (_position.X <= _playAreaBoundingBox.Left || (_position.X + _widthAndHeight) >= _playAreaBoundingBox.Right)
        {
            _velocity.X *= -1;
        }
        if (_position.Y <= _playAreaBoundingBox.Top || (_position.Y + _widthAndHeight) >= _playAreaBoundingBox.Bottom)
        {
            _velocity.Y *= -1;
        }
    }

    internal void Draw(SpriteBatch spriteBatch)
    {
        Rectangle rectangle = new Rectangle((int) _position.X, (int)_position.Y, _widthAndHeight, _widthAndHeight);
        spriteBatch.Draw(_texture, rectangle, Color.White); 
    }
}

