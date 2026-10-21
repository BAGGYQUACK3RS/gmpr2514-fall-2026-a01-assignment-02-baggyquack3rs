using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Assignment2;

public class BlockerStationary
{
    private const int _WidthAndHeight = 20;

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
        
    }

    internal void LoadContent(ContentManager content)
    {
        _texture = content.Load<Texture2D>("Paddle"); 
    }

    internal void Draw(SpriteBatch spriteBatch)
    {
        Rectangle rectangle = new Rectangle((int) _position.X, (int)_position.Y, _WidthAndHeight, _WidthAndHeight);
        spriteBatch.Draw(_texture, rectangle, Color.White); 
    }
}

