using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong;

public class Board {
    private Game1 _game;
    
    private Texture2D _lightGrass, _darkGrass, _line;

    private Color _darkGrassColor = new Color(3, 166, 74);
    private Color _lightGrassColor = new Color(85, 179, 80);
    private Color _lineColor = new Color(253, 251, 212);

    public Board(Game1 game) {
        _game = game;
    }
    
    public void Load() {
        _darkGrass = Tool.CreateTexture(_game.GraphicsDevice, _darkGrassColor);
        _lightGrass = Tool.CreateTexture(_game.GraphicsDevice, _lightGrassColor);
        _line = Tool.CreateTexture(_game.GraphicsDevice, _lineColor);
    }

    public void Draw(SpriteBatch sprite) {
        DrawGrass(sprite);
        DrawLines(sprite);
    }

    private void DrawLines(SpriteBatch sprite) {
        int padding = _game.ScreenHeight / 10;
        int lineWidth = 2;
        int halfSegment = _game.ScreenWidth / 2 - padding;
        
        // Outer square
        sprite.Draw(_line, new Rectangle(padding, padding, _game.ScreenWidth - padding * 2, lineWidth), Color.White);
        sprite.Draw(_line, new Rectangle(padding, _game.ScreenHeight - padding, _game.ScreenWidth - padding * 2, lineWidth), Color.White);
        sprite.Draw(_line, new Rectangle(padding, padding, lineWidth, _game.ScreenHeight - padding * 2), Color.White);
        sprite.Draw(_line, new Rectangle(_game.ScreenWidth - padding, padding, lineWidth, _game.ScreenHeight - padding * 2), Color.White);
        
        // Inner horizontal line
        sprite.Draw(_line, new Rectangle(padding, padding * 2, halfSegment * 2, lineWidth), Color.White);
        sprite.Draw(_line, new Rectangle(padding, _game.ScreenHeight - padding * 2, halfSegment * 2, lineWidth), Color.White);
        
        // Inner vertical line
        sprite.Draw(_line,
            new Rectangle(padding + halfSegment / 2, padding * 2, lineWidth, _game.ScreenHeight - padding * 4), Color.White);
        sprite.Draw(_line,
            new Rectangle(padding + halfSegment + halfSegment / 2, padding * 2, lineWidth, _game.ScreenHeight - padding * 4), Color.White);
        
        // Horizontal half line
        sprite.Draw(_line,
            new Rectangle(padding + halfSegment / 2, _game.ScreenHeight / 2 - lineWidth, halfSegment, lineWidth), Color.White);
        
        // Vertical half line
        int netWidth = 8;
        sprite.Draw(_line, new Rectangle(_game.ScreenWidth / 2 - netWidth / 2, 0, netWidth, _game.ScreenHeight), Color.White);
    }

    private void DrawGrass(SpriteBatch sprite) {
        int yPos = 0;
        int inc = _game.ScreenHeight / 8;
        bool isDark = true;

        for (int i = 0; i < 8; i++) {
            if (isDark) {
                sprite.Draw(_darkGrass, new Rectangle(0, yPos, _game.ScreenWidth, inc), Color.White);
                isDark = false;
            }
            else {
                sprite.Draw(_lightGrass, new Rectangle(0, yPos, _game.ScreenWidth, inc), Color.White);
                isDark = true;
            }
            yPos += inc;
        }
    }
}