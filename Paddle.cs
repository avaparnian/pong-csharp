using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong;

public abstract class Paddle {
    protected Game1 _game;

    protected Texture2D _paddleTexture;
    protected Color _paddleColor;
    protected int _paddleX, _paddleY, _paddleWidth, _paddleHeight;
    
    public Rectangle Hitbox;

    protected int _speed = 6;
    
    protected bool _upPressed, _downPressed;

    protected Paddle(Game1 game) {
        _game = game;
        
        _paddleWidth = game.ScreenWidth / 85;
        _paddleHeight = game.ScreenHeight / 6;

        Hitbox = new Rectangle(_paddleX, _paddleY, _paddleWidth, _paddleHeight);
    }

    public void Load() {
        _paddleTexture = Tool.CreateTexture(_game.GraphicsDevice, _paddleColor);
    }

    public void Update() {
        Input();
        Movement();
        Hitbox = new Rectangle(_paddleX, _paddleY, _paddleWidth, _paddleHeight);
    }

    protected abstract void Input();

    private void Movement() {
        if (_downPressed && _paddleY + _paddleHeight < _game.ScreenHeight) {
            _paddleY += _speed;
        }

        if (_upPressed && _paddleY > 0) {
            _paddleY -= _speed;
        }
    }
    
    public void Draw(SpriteBatch sprite) {
        sprite.Draw(_paddleTexture, new Rectangle(_paddleX, _paddleY, _paddleWidth, _paddleHeight), Color.White);
    }
}