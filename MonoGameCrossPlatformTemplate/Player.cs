using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CIS580Project4
{



    public enum PlayerLocation
    {
        Ground,
        Air
    }

    public enum PlayerState
    {
        Jumping,
        Crouching,
        Standing
    }

    public enum PlayerDirection
    {
        Right,
        Left
    }

    public class Player
    {
        Texture2D currentTexture;
        Texture2D standingRight;
        Texture2D standingLeft;
        Texture2D[] jumpingRight = new Texture2D[4];
        Texture2D[] jumpingLeft = new Texture2D[4];
        Texture2D[] runningRight = new Texture2D[4];
        Texture2D[] runningLeft = new Texture2D[4];

        public Hitbox hitbox;
        
        bool moving;
        double playerSpeedX;
        double playerSpeedY;
        PlayerState action;
        PlayerLocation location;
        PlayerDirection direction;
        double MAX_JUMP_HEIGHT;
        double MAX_X_SPEED;
        double MAX_Y_SPEED;
        double playerAccelerationX;
        double playerAccelerationY;
        double X;
        double Y;
        Game1 game;
        int width;
        int height;
        Vector2 origin;

        public Player(Game1 game)
        {
            this.game = game;
            LoadContent();
            currentTexture = standingRight;
            direction = PlayerDirection.Right;
            MAX_JUMP_HEIGHT = 100;
            MAX_X_SPEED = 14;
            MAX_Y_SPEED = 20;
            playerSpeedX = 0;
            playerSpeedY = 0;
            playerAccelerationX = 1;
            playerAccelerationY = 5;
            moving = false;
            
            width = 200;
            height = 250;
            X = game.SCREEN_WIDTH / 2;
            Y = game.SCREEN_HEIGHT - game.TILE_HEIGHT - (height/2) + 10;
            origin = new Vector2(standingRight.Width / 2, standingRight.Height / 2);
            hitbox = new Hitbox(width, height, (int)X, (int)Y);

        }

        public void Update()
		{
            var keyboard = Keyboard.GetState();

            switch (action)
			{
                case PlayerState.Jumping:
					
                    if(location.Equals(PlayerLocation.Ground))
                    {
                        Jump();
                    }
                    break;
                case PlayerState.Crouching:

                    break;
                case PlayerState.Standing:
                    break;
                
			} // end of state switch


            

            if(keyboard.IsKeyDown(Keys.W))
            {
                moving = true;
            }
            if(keyboard.IsKeyDown(Keys.D))
            {
                moving = true;
                direction = PlayerDirection.Right;
                if (X < game.GAME_WIDTH)
                {
                    MoveRight();
                }
            }
            if (keyboard.IsKeyDown(Keys.A))
            {
                moving = true;
                direction = PlayerDirection.Left;
                if (X > 0)
                {
                    MoveLeft();
                }

            }
            if(keyboard.IsKeyDown(Keys.Space))
            {
                Shoot();
            }


        } // end of update method

        public void Draw(SpriteBatch spriteBatch)
		{
            spriteBatch.Draw(currentTexture, new Rectangle((int)X, (int)Y, width, height), null, Color.White, 0f, origin, SpriteEffects.None, 0);
        }

        private void Shoot()
        {

        }

        private void MoveRight()
        {
            if (playerSpeedX < MAX_X_SPEED)
            {
                if (playerSpeedX < 0)
                {
                    playerSpeedX = 0;
                }
                else
                {
                    playerSpeedX += playerAccelerationX;
                }

            }
            
            game.ViewportX -= playerSpeedX;
        }

        private void MoveLeft()
        {
            if (playerSpeedX > MAX_X_SPEED * -1)
            {
                if (playerSpeedX > 0)
                {
                    playerSpeedX = 0;
                }
                else
                {
                    playerSpeedX -= playerAccelerationX;
                }
            }
            //X += playerSpeedX;
            game.ViewportX -= playerSpeedX;
        }

        private void Jump()
        {

        }

        public void LoadContent()
		{

            standingLeft = game.Content.Load<Texture2D>("StandingLeft");
            standingRight = game.Content.Load<Texture2D>("StandingRight");
            runningLeft[0] = game.Content.Load<Texture2D>("RunningLeft1");
            runningLeft[1] = game.Content.Load<Texture2D>("RunningLeft2");
            runningLeft[2] = game.Content.Load<Texture2D>("RunningLeft3");
            runningLeft[3] = game.Content.Load<Texture2D>("RunningLeft4");
            runningRight[0] = game.Content.Load<Texture2D>("RunningRight1");
            runningRight[1] = game.Content.Load<Texture2D>("RunningRight2");
            runningRight[2] = game.Content.Load<Texture2D>("RunningRight3");
            runningRight[3] = game.Content.Load<Texture2D>("RunningRight4");
            jumpingLeft[0] = game.Content.Load<Texture2D>("JumpingLeft1");
            jumpingLeft[1] = game.Content.Load<Texture2D>("JumpingLeft2");
            jumpingLeft[2] = game.Content.Load<Texture2D>("JumpingLeft3");
            jumpingLeft[3] = game.Content.Load<Texture2D>("JumpingLeft4");
            jumpingRight[0] = game.Content.Load<Texture2D>("JumpingRight1");
            jumpingRight[1] = game.Content.Load<Texture2D>("JumpingRight2");
            jumpingRight[2] = game.Content.Load<Texture2D>("JumpingRight3");
            jumpingRight[3] = game.Content.Load<Texture2D>("JumpingRight4");
        }

    }
}
