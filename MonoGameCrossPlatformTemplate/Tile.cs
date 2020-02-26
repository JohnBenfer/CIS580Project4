using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CIS580Project4
{
    public class Tile
    {
        Texture2D currentTexture;
        Game1 game;
        double X;
        double Y;
        int width;
        int height;
        Vector2 origin;
        public Hitbox hitbox;

        public Tile(Game1 game, double X, double Y)
        {
            this.game = game;
            LoadContent();
            
            this.X = X;
            this.Y = Y;
            width = game.TILE_WIDTH;
            height = game.TILE_HEIGHT;
            origin = new Vector2(width / 2, height / 2);
            hitbox = new Hitbox(height, width, (int)X, (int)Y);

        }

        public void Update()
        {


        }

        public void Draw(SpriteBatch spriteBatch, double ViewportX, double ViewportY)
        {
            spriteBatch.Draw(currentTexture, new Rectangle((int)(X + ViewportX), (int)(Y - ViewportY), width, height), null, Color.White, 0f, origin, SpriteEffects.None, 0);
        }

        public void LoadContent()
        {
            currentTexture = game.Content.Load<Texture2D>("Tile");
        }

    }
}
