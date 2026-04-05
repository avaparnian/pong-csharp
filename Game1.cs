using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong;

public enum GameState {
    Menu,
    Playing,
    Paused
}

public class Game1 : Game {
    
    public int ScreenWidth = 1280;
    public int ScreenHeight = 720;

    public int FPS = 60;

    public int Player1Score = 0, Player2Score = 0;
    
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public GameState CurrentState;

    private Board _board;
    public Player1 Player1; 
    public Player2 Player2;
    public Ball Ball;
    private Interface _interface;
    
    public Game1() {
        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferWidth = ScreenWidth;
        _graphics.PreferredBackBufferHeight = ScreenHeight;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize() {
        CurrentState = GameState.Menu;
        _board = new Board(this);
        Player1 = new Player1(this);
        Player2 = new Player2(this);
        Ball = new Ball(this);
        _interface = new Interface(this);
        base.Initialize();
    }

    protected override void LoadContent() {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        
        _board.Load();
        Player1.Load();
        Player2.Load();
        Ball.Load();
        _interface.Load();
    }

    protected override void Update(GameTime gameTime) {
        if (CurrentState == GameState.Playing) {
            Player1.Update();
            Player2.Update();
            Ball.Update();
        }
        _interface.Update();
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime) {
        GraphicsDevice.Clear(Color.Black);
        _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);

        if (CurrentState == GameState.Playing) {
            _board.Draw(_spriteBatch);
            Player1.Draw(_spriteBatch);
            Player2.Draw(_spriteBatch);
            Ball.Draw(_spriteBatch); 
        }
        _interface.Draw(_spriteBatch);
        
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
