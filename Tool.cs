using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Pong;

public static class Tool {

    public static Texture2D CreateTexture(GraphicsDevice graphicsDevice, Color color ) {
        Texture2D texture = new Texture2D(graphicsDevice, 1, 1);
        texture.SetData([color]);

        return texture;
    }
    
}