using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Assignment2;

public class Ball
{
    private const int _WidthAndHeight = 21, _Speed = 240, _CollisionTimerIntervalMillis = 400;
    private int _collisionTimerMillis;

    private Texture2D _texture;
    private Vector2 _position, _velocity;   
    private Rectangle _playAreaBoundingBox;

    internal Rectangle BoundingBox
    {
        get { return new Rectangle((int)_position.X, (int)_position.Y, _WidthAndHeight, _WidthAndHeight);}
    }

    internal void Initialize(Vector2 startingPos, Rectangle playAreaBoundingBox)
    {
        _playAreaBoundingBox = playAreaBoundingBox;
        _position = startingPos;
        _velocity = new Vector2(-_Speed, -_Speed); //up and to the left
    }

    internal void LoadContent(ContentManager content)
    {
        _texture = content.Load<Texture2D>("Ball"); 
    }

    internal void Update(GameTime gameTime)
    {
        _collisionTimerMillis += gameTime.ElapsedGameTime.Milliseconds;

        float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;
        _position += _velocity * dt;

        if (_position.X <= _playAreaBoundingBox.Left || (_position.X + _WidthAndHeight) >= _playAreaBoundingBox.Right)
        {
            _velocity.X *= -1;
            _position = new Vector2(375,225);
        }

        if (_position.Y <= _playAreaBoundingBox.Top || (_position.Y + _WidthAndHeight) >= _playAreaBoundingBox.Bottom)
        {
            _velocity.Y *= -1;
        }
    }

    internal void Draw(SpriteBatch spriteBatch)
    {
        Rectangle rectangle = new Rectangle((int) _position.X, (int)_position.Y, _WidthAndHeight, _WidthAndHeight);
        spriteBatch.Draw(_texture, rectangle, Color.White); 
    }

    internal bool ProcessCollision(Rectangle otherBoundingBox)
    {
        bool didCollide = false;

        if(_collisionTimerMillis >= _CollisionTimerIntervalMillis &&
            BoundingBox.Intersects(otherBoundingBox))
        {
            _collisionTimerMillis = 0;
            // collision!
            didCollide = true;
            // intersection represents the rectangle created by the overlapping two rectangles
            Rectangle intersection = Rectangle.Intersect(BoundingBox, otherBoundingBox);
            if(intersection.Width > intersection.Height)
                // horizontal rectangle, so it's a top or bottom collision
                _velocity.Y *= -1;
            else
                //vertical rectangle, so it's a side collision
                _velocity.X *= -1;

        }

        return didCollide;
    }
}

