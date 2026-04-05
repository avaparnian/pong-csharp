using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Pong;

public class Player2 : Paddle {
    public bool IsCPU;
    
    public Player2(Game1 game) : base(game) {
        _paddleColor = new Color(255, 0, 0);
        _paddleX = _game.ScreenWidth - _paddleWidth - _paddleWidth * 2;
        _paddleY = game.ScreenHeight / 2 - _paddleHeight / 2;
    }

    protected override void Input() {
        if (IsCPU) {
            CPUInput();
        }
        else {
            KeyInput();
        }
    }

    private void CPUInput() {
        int paddleCentre = _paddleY + _paddleHeight / 2;
        int spaceFromBall = paddleCentre - _game.Ball.BallY;
        int safeZone = 5;

        if (spaceFromBall > safeZone) {
            _upPressed = true;
            _downPressed = false;
        }
        else if (spaceFromBall < -safeZone) {
            _upPressed = false;
            _downPressed = true;
        }
        else {
            _upPressed = false;
            _downPressed = false;
        }
    }

    private void KeyInput() {
        KeyboardState keyState = Keyboard.GetState();

        _upPressed = keyState.IsKeyDown(Keys.Up);
        _downPressed = keyState.IsKeyDown(Keys.Down);
    }
}