using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
        double backgroundCityX = 0;
        public int SCREEN_WIDTH = 1790;
        public int SCREEN_HEIGHT = 1020;
        public double ViewportX;
        public double ViewportY;

        public Game1()
        {
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

            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            graphics.ApplyChanges();

            ViewportX = 0;
            ViewportY = 0;
            player = new Player();

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
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update();
            backgroundCityX -= 0.7;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundSky, new Rectangle(new Point(0, 0), new Point(SCREEN_WIDTH, SCREEN_HEIGHT)), Color.White);
            spriteBatch.Draw(backgroundCity, new Rectangle(new Point((int)ViewportX - 100, (int)ViewportY + 0 - 100), new Point(SCREEN_WIDTH + 100, SCREEN_HEIGHT + 100)), Color.White);
            spriteBatch.Draw(backgroundCity, new Rectangle(new Point((int)ViewportX - 100 + SCREEN_WIDTH, (int)ViewportY + 0 - 100), new Point(SCREEN_WIDTH + 100, SCREEN_HEIGHT + 100)), Color.White);
            player.Draw();

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
