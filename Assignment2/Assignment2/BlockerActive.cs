using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Assignment2;

public class BlockerActive
{
private const int _WidthAndHeight = 30, _CollisionTimerIntervalMillis = 400;
    private int _collisionTimerMillis;
    private bool debounce = true;
    private bool didCollide;

    private Texture2D _texture;
    private Vector2 _position;

    internal Rectangle BoundingBox
    {
        get { return new Rectangle((int)_position.X, (int)_position.Y, _WidthAndHeight, _WidthAndHeight);}
    }


    internal void Initialize(Vector2 startingPos)
    {
        _position = startingPos;
    }

    internal void Update(GameTime gameTime)
    {
        _collisionTimerMillis += gameTime.ElapsedGameTime.Milliseconds;
    }

    internal void LoadContent(ContentManager content)
    {
        _texture = content.Load<Texture2D>("Paddle"); 
    }

    internal void Draw(SpriteBatch spriteBatch)
    {
        if (didCollide == false && debounce == true)
        {
            Rectangle rectangle = new Rectangle((int) _position.X, (int)_position.Y, _WidthAndHeight, _WidthAndHeight);
            spriteBatch.Draw(_texture, rectangle, Color.White); 
        } else
        {
            // out of sight out of mind
            _position = new Vector2(-100, -100);
            debounce = false;
        }
    }

    internal bool ProcessCollision(Rectangle otherBoundingBox)
    {
        didCollide = false;

        if(_collisionTimerMillis >= _CollisionTimerIntervalMillis &&
            BoundingBox.Intersects(otherBoundingBox))
        {
            _collisionTimerMillis = 0;
            didCollide = true;           
        }

        return didCollide;
    }
}

