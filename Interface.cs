using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong;
using FontStashSharp;

public class Interface {
    private Game1 _game;

    private FontSystem _thamireFontSubSystem, _thamireFontTitleSystem;
    private SpriteFontBase _thamireFontSub, _thamireFontTitle;

    private Color _orangeColor = new Color(255, 148, 0);
    private Color _greenColor = new Color(137,249,79);
    private Color _skyColor = new Color(135, 206, 250);

    private Vector2 _player1ScoreSize;

    private Vector2 _beginCountdownSize;
    private int _beginCountdownSecond = 1;

    private Texture2D _menuBackgroundTexture;
    
    private Texture2D _CPUButtonTexture, _playersButtonTexture;
    private Rectangle _CPUButtonRect, _playersButtonRect;
    private string _CPUButtonText = "CPU", _playersButtonText = "2 Player";
    private Vector2 _CPUButtonTextSize, _playersButtonTextSize;
    private int _buttonSize = 256;
    
    private string _title = "Tennis Pong";
    private Vector2 _titleSize;

    public Interface(Game1 game) {
        _game = game;

        int buttonGap = 200;
        _playersButtonRect = new Rectangle(_game.ScreenWidth / 2 - _buttonSize - buttonGap / 2, 270, _buttonSize, _buttonSize);
        _CPUButtonRect = new Rectangle(_game.ScreenWidth / 2 + buttonGap / 2, 270, _buttonSize, _buttonSize);
    }

    public void Load() {
        _thamireFontSubSystem = new FontSystem(new FontSystemSettings { PremultiplyAlpha = false });
        _thamireFontSubSystem.AddFont(File.ReadAllBytes("../../../res/fonts/thamire.otf"));
        _thamireFontSub = _thamireFontSubSystem.GetFont(80);

        _thamireFontTitleSystem = new FontSystem(new FontSystemSettings { PremultiplyAlpha = false });
        _thamireFontTitleSystem.AddFont(File.ReadAllBytes("../../../res/fonts/thamire.otf"));
        _thamireFontTitle = _thamireFontTitleSystem.GetFont(256);

        _menuBackgroundTexture = Tool.CreateTexture(_game.GraphicsDevice, _skyColor);
        _CPUButtonTexture = Texture2D.FromStream(_game.GraphicsDevice, File.OpenRead(Path.GetFullPath("../../../res/images/CPU.png")));
        _playersButtonTexture = Texture2D.FromStream(_game.GraphicsDevice, File.OpenRead(Path.GetFullPath("../../../res/images/players.png")));

        _titleSize = _thamireFontTitle.MeasureString(_title);
        _CPUButtonTextSize = _thamireFontSub.MeasureString(_CPUButtonText);
        _playersButtonTextSize = _thamireFontSub.MeasureString(_playersButtonText);
    }

    public void Update() {
        if (_game.CurrentState == GameState.Playing) {
            PlayingUpdate();
        }
        if (_game.CurrentState == GameState.Menu) {
            MenuUpdate();
        }
    }

    private void PlayingUpdate() {
        _player1ScoreSize = _thamireFontSub.MeasureString(_game.Player1Score.ToString());

        if (_game.Ball.SetTimer < 160) {
            if (_game.Ball.SetTimer < _game.FPS) {
                _beginCountdownSecond = 3;
            }
            else if (_game.Ball.SetTimer < _game.FPS * 2) {
                _beginCountdownSecond = 2;
            }
            else if (_game.Ball.SetTimer < _game.FPS * 3) {
                _beginCountdownSecond = 1;
            }
            _beginCountdownSize = _thamireFontTitle.MeasureString(_beginCountdownSecond.ToString());
        }
    }

    private void MenuUpdate() {
        MouseState mouseState = Mouse.GetState();
        Point mousePos = new Point(mouseState.X, mouseState.Y);

        if (_playersButtonRect.Contains(mousePos) && mouseState.LeftButton == ButtonState.Pressed) {
            _game.Player2.IsCPU = false;
            _game.CurrentState = GameState.Playing;
        }
        if (_CPUButtonRect.Contains(mousePos) && mouseState.LeftButton == ButtonState.Pressed) {
            _game.Player2.IsCPU = true;
            _game.CurrentState = GameState.Playing;
        }
    }

    public void Draw(SpriteBatch sprite) {
        if (_game.CurrentState == GameState.Playing) {
            PlayingDraw(sprite);
        }
        if (_game.CurrentState == GameState.Menu) {
            MenuDraw(sprite);
        }
    }

    public void PlayingDraw(SpriteBatch sprite) {
        int padding = 30;
        
        sprite.DrawString(_thamireFontSub, _game.Player1Score.ToString(), new Vector2(_game.ScreenWidth/2 - _player1ScoreSize.X - padding, 0), Color.White);
        sprite.DrawString(_thamireFontSub, _game.Player2Score.ToString(), new Vector2(_game.ScreenWidth/2 + padding - 7, 0), Color.White);

        if (_game.Ball.SetTimer < _game.FPS * 3) {
            sprite.DrawString(_thamireFontTitle, _beginCountdownSecond.ToString(),
                new Vector2(_game.ScreenWidth / 2 - _beginCountdownSize.X/2, _game.ScreenHeight / 2 - _beginCountdownSize.Y / 2 - padding),
                _orangeColor);
        }
    }

    public void MenuDraw(SpriteBatch sprite) {
        sprite.Draw(_menuBackgroundTexture, new Rectangle(0, 0, _game.ScreenWidth, _game.ScreenHeight), Color.White);
        sprite.DrawString(_thamireFontTitle, _title, new Vector2(_game.ScreenWidth / 2 - _titleSize.X / 2, 10), _greenColor);
        
        sprite.Draw(_playersButtonTexture, _playersButtonRect, Color.White);
        sprite.Draw(_CPUButtonTexture, _CPUButtonRect, Color.White);

        sprite.DrawString(_thamireFontSub, _playersButtonText, new Vector2(_playersButtonRect.X + _buttonSize / 2 - _playersButtonTextSize.X / 2, _playersButtonRect.Y + _buttonSize), Color.White);
        sprite.DrawString(_thamireFontSub, _CPUButtonText, new Vector2(_CPUButtonRect.X + _buttonSize / 2 - _CPUButtonTextSize.X / 2, _CPUButtonRect.Y + _buttonSize), Color.White);
    }
}