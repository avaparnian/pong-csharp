using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Pong;

public class Player1 : Paddle {
    public Player1(Game1 game) : base(game) {
        _paddleColor = new Color(0, 80, 255);
        _paddleX = _paddleWidth*2;
        _paddleY = game.ScreenHeight/2 - _paddleHeight/2;
    }

    protected override void Input() {
        KeyboardState keyState = Keyboard.GetState();

        _upPressed = keyState.IsKeyDown(Keys.W);
        _downPressed = keyState.IsKeyDown(Keys.S);
    }
}