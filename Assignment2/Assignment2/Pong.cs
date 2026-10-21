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
    private Brick _brick1, _brick2, _brick3, _brick4;

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
        #region ball
        _ball = new Ball();

        _ball.Initialize(new Vector2(_WindowWidth/2, _WindowHeight/2), PlayAreaBoundingBox);
        #endregion
        #region paddles
        _paddleLeft = new Paddle();
        _paddleRight = new Paddle();

        _paddleLeft.Initialize(new Vector2(10, _WindowHeight / 2 - 54), PlayAreaBoundingBox);
        _paddleRight.Initialize(new Vector2(_WindowWidth - 22, _WindowHeight / 2 - 54), PlayAreaBoundingBox);
        #endregion
        #region bricks
        _brick1 = new Brick();
        _brick2 = new Brick();
        _brick3 = new Brick();
        _brick4 = new Brick();

        _brick1.Initialize(new Vector2(50, 50));
        _brick2.Initialize(new Vector2(50, 370));
        _brick3.Initialize(new Vector2(670, 50));
        _brick4.Initialize(new Vector2(670, 370));
        #endregion
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _backgroundTexture = Content.Load<Texture2D>("Court");
        
        _ball.LoadContent(Content);
        _paddleLeft.LoadContent(Content);
        _paddleRight.LoadContent(Content);

        _brick1.LoadContent(Content);
        _brick2.LoadContent(Content);
        _brick3.LoadContent(Content);
        _brick4.LoadContent(Content);

    }

    protected override void Update(GameTime gameTime) 
    {
       float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;

        _ball.Update(gameTime);
        _paddleLeft.Update(gameTime);
        _paddleRight.Update(gameTime);

        _brick1.Update(gameTime);
        _brick2.Update(gameTime);
        _brick3.Update(gameTime);
        _brick4.Update(gameTime);


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

        if(_ball.ProcessCollision(_paddleRight.BoundingBox) || _ball.ProcessCollision(_paddleLeft.BoundingBox) || 
        _ball.ProcessCollision(_brick1.BoundingBox) || _ball.ProcessCollision(_brick2.BoundingBox) || _ball.ProcessCollision(_brick3.BoundingBox) || _ball.ProcessCollision(_brick4.BoundingBox))
        {
            ;
        }

        if (_brick1.ProcessCollision(_ball.BoundingBox) || _brick2.ProcessCollision(_ball.BoundingBox) || _brick3.ProcessCollision(_ball.BoundingBox) || _brick4.ProcessCollision(_ball.BoundingBox))
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

        _brick1.Draw(_spriteBatch);
        _brick2.Draw(_spriteBatch);
        _brick3.Draw(_spriteBatch);
        _brick4.Draw(_spriteBatch);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}