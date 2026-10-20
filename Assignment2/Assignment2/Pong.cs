using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Assignment2;

public class Pong : Game
{
    private const int _WindowWidth = 750, _WindowHeight = 450, 
    _PlayAreaEdgeLineWidth = 12;

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Ball _ball;
    private Paddle _paddle;

    private Texture2D _backgroundTexture;

    private Rectangle PlayAreaBoundingBox
    {
        get
        {   return new Rectangle(
            0, 
            _PlayAreaEdgeLineWidth, 
            _WindowWidth, 
            _WindowHeight - (_PlayAreaEdgeLineWidth * 2));
        }
    }  

    public Pong()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = _WindowWidth;
        _graphics.PreferredBackBufferHeight = _WindowHeight;
        _graphics.ApplyChanges();

        _ball = new Ball();
        _paddle = new Paddle();
        Vector2 _startingPosBall = new Vector2(_WindowWidth/2, _WindowHeight/2);
        Vector2 _startingPosPaddle = new Vector2(10, _WindowHeight / 2 - 54);

        _ball.Initialize(_startingPosBall, PlayAreaBoundingBox);
        _paddle.Initialize(_startingPosPaddle, PlayAreaBoundingBox);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _backgroundTexture = Content.Load<Texture2D>("Court");
        _ball.LoadContent(Content);
        _paddle.LoadContent(Content);
    }

    protected override void Update(GameTime gameTime) 
    {
       float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;
       KeyboardState kbState = Keyboard.GetState();

        _ball.Update(gameTime);
        _paddle.Update(gameTime, kbState);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        _spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, _WindowWidth, _WindowHeight), Color.White);
        _ball.Draw(_spriteBatch);
        _paddle.Draw(_spriteBatch);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}