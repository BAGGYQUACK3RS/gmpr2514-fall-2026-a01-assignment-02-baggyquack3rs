using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Assignment2;

public class BlockerActive
{

    private const int _WidthAndHeight = 30;
    private Texture2D _texture;
    private Vector2 _position;
    
    internal void Initialize()
    {
        
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
        
    }
}

