using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Assignment2;

public class BlockerActive
{
    private const int _width = 12, _height = 108;
    private int _speed = 60;
    private Rectangle _playAreaBoundingBox;

    private Texture2D _texture;
    private Vector2 _position;

    internal Rectangle BoundingBox
    {
        get { return new Rectangle((int)_position.X, (int)_position.Y, _width, _height);}
    }

    internal void Initialize(Vector2 startingPos, Rectangle playAreaBoundingBox)
    {
        _playAreaBoundingBox = playAreaBoundingBox;
        _position = startingPos;
    }

    internal void Update(GameTime gameTime)
    {
        float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;

        if (_position.Y <= _playAreaBoundingBox.Top || _position.Y + _height >= _playAreaBoundingBox.Bottom)
        {
            _speed = -_speed;
        }

        _position.Y += _speed * dt;
    }

    internal void LoadContent(ContentManager content)
    {
        _texture = content.Load<Texture2D>("Paddle"); 
    }

    internal void Draw(SpriteBatch spriteBatch)
    {
        Rectangle rectangle = new Rectangle((int) _position.X, (int)_position.Y, _width, _height);
        spriteBatch.Draw(_texture, rectangle, Color.White); 
    }
}

