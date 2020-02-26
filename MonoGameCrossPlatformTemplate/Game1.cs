using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;

namespace CIS580Project4
{

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        Texture2D backgroundSky;
        Texture2D backgroundCity;
        Texture2D background;
        double backgroundCityX = 0;
        public int SCREEN_WIDTH = 1790;
        //public int SCREEN_HEIGHT = 1020;
        public int SCREEN_HEIGHT = 800;
        public int GAME_WIDTH;
        public int GAME_HEIGHT;
        public double ViewportX;
        public double ViewportY;
        public int TILE_WIDTH;
        public int TILE_HEIGHT;
        List<Tile> tiles = new List<Tile>();
        int tileCount;
        Matrix transformMatrix;
        
        public Game1()
        {
            TILE_WIDTH = SCREEN_WIDTH / 25;
            TILE_HEIGHT = TILE_WIDTH;
            GAME_WIDTH = SCREEN_WIDTH * 3;
            GAME_HEIGHT = SCREEN_HEIGHT * 2;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            tileCount = GAME_WIDTH/TILE_WIDTH;
            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            graphics.ApplyChanges();
            transformMatrix = new Matrix();
            
            ViewportX = 0;
            ViewportY = 0;
            player = new Player(this);
            for(int i = 0; i <= tileCount; i++)
            {
                tiles.Add(new Tile(this, i*TILE_WIDTH, SCREEN_HEIGHT - TILE_HEIGHT + 10));
            }
            tiles.Add(new Tile(this, 0, SCREEN_HEIGHT - (TILE_HEIGHT * 2) + 10));
            tiles.Add(new Tile(this, tileCount * TILE_WIDTH, SCREEN_HEIGHT - (TILE_HEIGHT * 2) + 10));

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            backgroundSky = Content.Load<Texture2D>("BackgroundSky");
            backgroundCity = Content.Load<Texture2D>("BackgroundCity");
            background = Content.Load<Texture2D>("Background");
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update();


            foreach (Tile tile in tiles)
            {
                if(PlayerHittingTopOfTile(tile))
                {
                    player.playerSpeedY = 0;
                    player.location = PlayerLocation.Ground;
                    Console.WriteLine("Here");
                }
                if(PlayerHittingBottomOfTile(tile))
                {
                    player.playerSpeedY = 0;
                }
                if(PlayerHittingSideOfTile(tile))
                {
                    player.playerSpeedX = 0;
                }
            }



            // translates the view matrix
            transformMatrix = Matrix.CreateTranslation(new Vector3((float)ViewportX, (float)ViewportY, 0));

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, transformMatrix);
            /*
            spriteBatch.Draw(backgroundSky, new Rectangle(new Point(0, 0), new Point(SCREEN_WIDTH, SCREEN_HEIGHT)), Color.White);
            spriteBatch.Draw(backgroundCity, new Rectangle(new Point((int)ViewportX - 100, (int)ViewportY + 0 - 100), new Point(SCREEN_WIDTH + 100, SCREEN_HEIGHT + 100)), Color.White);
            spriteBatch.Draw(backgroundCity, new Rectangle(new Point((int)ViewportX - 100 + SCREEN_WIDTH, (int)ViewportY + 0 - 100), new Point(SCREEN_WIDTH + 100, SCREEN_HEIGHT + 100)), Color.White);
            */
            for (int i = 0; i < (GAME_WIDTH/SCREEN_WIDTH); i++)
            {
                //spriteBatch.Draw(background, new Rectangle(new Point((int)ViewportX + (i*SCREEN_WIDTH), (int)ViewportY), new Point(SCREEN_WIDTH, SCREEN_HEIGHT)), Color.White);
                for (int j = 0; j < (GAME_HEIGHT / SCREEN_HEIGHT); j++)
                {
                    //spriteBatch.Draw(background, new Rectangle(new Point((int)ViewportX + (i * SCREEN_WIDTH), (int)ViewportY + (j*SCREEN_HEIGHT)), new Point(SCREEN_WIDTH, SCREEN_HEIGHT)), Color.White);
                    spriteBatch.Draw(background, new Rectangle(new Point((int)0 + (i * SCREEN_WIDTH), (int)0 + (j * SCREEN_HEIGHT)), new Point(SCREEN_WIDTH, SCREEN_HEIGHT)), Color.White);
                }
            }
            
            player.Draw(spriteBatch);
            foreach(Tile t in tiles)
            {
                t.Draw(spriteBatch, 0, 0);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }


        private bool PlayerHittingTopOfTile(Tile tile)
        {
            if((player.hitbox.box.Y + player.hitbox.box.Height >= tile.hitbox.box.Y &&
                player.hitbox.box.Y + player.hitbox.box.Height < tile.hitbox.box.Y + tile.hitbox.box.Height) &&
                (player.hitbox.box.X < tile.hitbox.box.X + tile.hitbox.box.Width && player.hitbox.box.X > tile.hitbox.box.X) ||  // player left is to the left of tile right and right of tile left
                (player.hitbox.box.X + player.hitbox.box.Width > tile.hitbox.box.X && player.hitbox.box.X + player.hitbox.box.Width < tile.hitbox.box.X + tile.hitbox.box.Width))
            {
                return true;
            }
            return false;
        }

        private bool PlayerHittingBottomOfTile(Tile tile)
        {
            if ((player.hitbox.box.Y <= tile.hitbox.box.Y + tile.hitbox.box.Height && // player top is above tile bottom
                player.hitbox.box.Y + player.hitbox.box.Height > tile.hitbox.box.Y + tile.hitbox.box.Height) && // player bottom is below tile bottom
                (player.hitbox.box.X < tile.hitbox.box.X + tile.hitbox.box.Width && player.hitbox.box.X > tile.hitbox.box.X) ||  // player left is to the left of tile right and right of tile left
                (player.hitbox.box.X + player.hitbox.box.Width > tile.hitbox.box.X && player.hitbox.box.X + player.hitbox.box.Width < tile.hitbox.box.X + tile.hitbox.box.Width)) // player right is right of tile left and left of tile right
            {
                return true;
            }

            return false;
        }

        private bool PlayerHittingSideOfTile(Tile tile)
        {
            if((player.hitbox.box.X + player.hitbox.box.Width > tile.hitbox.box.X &&
                player.hitbox.box.X < tile.hitbox.box.X) &&
                (player.hitbox.box.Y > tile.hitbox.box.Y && player.hitbox.box.Y < tile.hitbox.box.Y + tile.hitbox.box.Height ||
                player.hitbox.box.Y + player.hitbox.box.Height < tile.hitbox.box.Y + tile.hitbox.box.Height && player.hitbox.box.Y + player.hitbox.box.Height > tile.hitbox.box.Y))
            {
                return true; // player hitting left side of tile
            }

            if ((player.hitbox.box.X + player.hitbox.box.Width > tile.hitbox.box.X + tile.hitbox.box.Width &&
                player.hitbox.box.X < tile.hitbox.box.X + tile.hitbox.box.Width) &&
                (player.hitbox.box.Y > tile.hitbox.box.Y && player.hitbox.box.Y < tile.hitbox.box.Y + tile.hitbox.box.Height ||
                player.hitbox.box.Y + player.hitbox.box.Height < tile.hitbox.box.Y + tile.hitbox.box.Height && player.hitbox.box.Y + player.hitbox.box.Height > tile.hitbox.box.Y))
            {
                return true; // player hitting right side of tile
            }

            return false;
        }

    }
}
