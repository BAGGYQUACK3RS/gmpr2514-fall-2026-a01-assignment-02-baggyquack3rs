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
    private Paddle _paddleLeft, _paddleRight;
    private Brick _brick;

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
        _paddleLeft = new Paddle();
        _paddleRight = new Paddle();
        _brick = new Brick();

        Vector2 _startingPosBall = new Vector2(_WindowWidth/2, _WindowHeight/2);
        Vector2 _startingPosPaddleLeft = new Vector2(10, _WindowHeight / 2 - 54);
        Vector2 _startingPosPaddleRight = new Vector2(_WindowWidth - 22, _WindowHeight / 2 - 54);

        _ball.Initialize(_startingPosBall, PlayAreaBoundingBox);
        _paddleLeft.Initialize(_startingPosPaddleLeft, PlayAreaBoundingBox);
        _paddleRight.Initialize(_startingPosPaddleRight, PlayAreaBoundingBox);
        _brick.Initialize(new Vector2(500, 300));
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _backgroundTexture = Content.Load<Texture2D>("Court");
        
        _ball.LoadContent(Content);
        _brick.LoadContent(Content);
        _paddleLeft.LoadContent(Content);
        _paddleRight.LoadContent(Content);
    }

    protected override void Update(GameTime gameTime) 
    {
       float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;

        _ball.Update(gameTime);
        _brick.Update(gameTime);
        _paddleLeft.Update(gameTime);
        _paddleRight.Update(gameTime);

        KeyboardState kbState = Keyboard.GetState();

        #region leftpaddle
        if(kbState.IsKeyDown(Keys.W))
            _paddleLeft.Direction = new Vector2(0, -1);
        else if(kbState.IsKeyDown(Keys.S))
            _paddleLeft.Direction = new Vector2(0, 1);
        else
            _paddleLeft.Direction = Vector2.Zero;

        if(kbState.IsKeyDown(Keys.LeftShift))
            _paddleLeft.Speed = 2;
        else
            _paddleLeft.Speed = 1;
        #endregion

        #region rightpaddle
        if(kbState.IsKeyDown(Keys.Up))
            _paddleRight.Direction = new Vector2(0, -1);
        else if(kbState.IsKeyDown(Keys.Down))
            _paddleRight.Direction = new Vector2(0, 1);
        else
            _paddleRight.Direction = Vector2.Zero;
        
        if(kbState.IsKeyDown(Keys.RightShift))
            _paddleRight.Speed = 2;
        else
            _paddleRight.Speed = 1;

        if(_ball.ProcessCollision(_paddleRight.BoundingBox) || _ball.ProcessCollision(_paddleLeft.BoundingBox) || _ball.ProcessCollision(_brick.BoundingBox))
        {
            ;
        }

        if (_brick.ProcessCollision(_ball.BoundingBox))
        {
            ;
        }

        #endregion

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        _spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, _WindowWidth, _WindowHeight), Color.White);
        _ball.Draw(_spriteBatch);
        _paddleLeft.Draw(_spriteBatch);
        _paddleRight.Draw(_spriteBatch);
        _brick.Draw(_spriteBatch);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}