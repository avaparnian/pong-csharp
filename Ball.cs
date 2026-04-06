using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong;

public class Ball {
    private Game1 _game;
    private Random _random = new();
    
    private int _ballSize = 40;
    public int BallX, BallY;
    private int _ballStartingX, _ballStartingY;
    private int _speed = 7, _speedX, _speedY;

    private Rectangle _hitbox;
    private bool _isColliding;

    private int[] _direction;
    
    private Texture2D _ballTexture;

    
    public int SetTimer = 0;

    public Ball(Game1 game) {
        _game = game;

        _ballStartingX = _game.ScreenWidth / 2 - _ballSize / 2;
        _ballStartingY = _game.ScreenHeight / 2 - _ballSize / 2;

        _hitbox = new Rectangle(BallX, BallY, _ballSize, _ballSize);
        
        _direction = [-_speed, _speed];
        
        SetBall();
    }

    public void Load() {
        _ballTexture = Texture2D.FromStream(_game.GraphicsDevice, File.OpenRead("res/images/ball.png"));
    }

    public void Update() {
        if (SetTimer < 60 * 3) {
            SetTimer++;
        }
        else {
            CheckCollision();
            BallMovement(); 
            _hitbox = new Rectangle(BallX, BallY, _ballSize, _ballSize);
        }
    }

    private void SetBall() {
        BallX = _ballStartingX;
        BallY = _ballStartingY;
        
        _speedX = _direction[_random.Next(0, 2)];
        _speedY = _direction[_random.Next(0, 2)];
    }

    private void BallMovement() {
        BallX += _speedX;
        BallY += _speedY;
        
        if (BallY <= 0 || BallY + _ballSize >= _game.ScreenHeight) {
            _speedY *= -1;
        }
        if (BallX <= 0) {
            _game.Player2Score++;
            SetBall();
            SetTimer = 0;
        }
        if (BallX + _ballSize >= _game.ScreenWidth) {
            _game.Player1Score++;
            SetBall();
            SetTimer = 0;
        }
    }

    private void CheckCollision() {
        Rectangle player1Hitbox = _game.Player1.Hitbox;
        Rectangle player2Hitbox = _game.Player2.Hitbox;

        if (!_isColliding && (_hitbox.Intersects(player1Hitbox) || _hitbox.Intersects(player2Hitbox))) {
            _isColliding = true;
            if (_hitbox.Intersects(player1Hitbox)) {
                CheckWhereHit(player1Hitbox);
            }
            else {
                CheckWhereHit(player2Hitbox);
            }
            _speedX *= -1;
        }

        if (!_hitbox.Intersects(player1Hitbox) && !_hitbox.Intersects(player2Hitbox)) {
            _isColliding = false;
        }
    }

    private void CheckWhereHit(Rectangle paddle) {
        int increment = 3;
        int ballCentre = BallY + _ballSize / 2;

        // RESET
        if (_speedY > 0) {
            _speedY = _speed;
        }
        else {
            _speedY = -_speed;
        }
        if (_speedX > 0) {
            _speedX = _speed;
        }
        else {
            _speedX = -_speed;
        }
        
        // TOP HIT
        if (ballCentre < paddle.Y + paddle.Height / 3) {
            _speedY = -_speed - increment;
        }
        
        // CENTRE HIT
        else if (ballCentre > paddle.Y + paddle.Height / 3 && ballCentre < paddle.Y + paddle.Height / 3 *2 ) {
            if (_speedX > 0) {
                _speedX = _speed + increment;
            }
            else {
                _speedX = -_speed + -increment;
            }
        }
        
        // BOTTOM HIT
        else if (ballCentre > paddle.Y + paddle.Height / 3 *2 ) {
            _speedY = _speed + increment;
        }
    }

    public void Draw(SpriteBatch sprite) {
        if (SetTimer >= _game.FPS * 3) {
            sprite.Draw(_ballTexture, new Rectangle(BallX,BallY, _ballSize, _ballSize), Color.White);
        }
    }
}