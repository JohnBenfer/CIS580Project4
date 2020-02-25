using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CIS580Project4
{
    public enum PlayerState
    {
        Moving,
        Shooting,
        Standing
    }


    public enum PlayerLocation
    {
        Ground,
        Air
    }

    public class Player
    {
        Texture2D standingRight;
        Texture2D standingLeft;
        Texture2D[] jumpingRight = new Texture2D[4];
        Texture2D[] jumpingLeft = new Texture2D[4];
        Texture2D[] runningRight = new Texture2D[4];
        Texture2D[] runningLeft = new Texture2D[4];

        bool jumping;
        double playerSpeedX;
        double playerSpeedY;
        PlayerState action;
        PlayerLocation location;
        double MAX_JUMP_HEIGHT;
        double MAX_X_SPEED;
        double MAX_Y_SPEED;
        double playerX;
        double playerY;
        Game1 game;

        public Player(Game1 game)
        {
            LoadContent();

            MAX_JUMP_HEIGHT = 100;
            MAX_X_SPEED = 10;
            MAX_Y_SPEED = 15;
            playerSpeedX = 0;
            playerSpeedY = 0;
            jumping = false;
            this.game = game;

        }

        public void Update()
		{
            var keyboard = Keyboard.GetState();

            switch (action)
			{
                case PlayerState.Moving:
					if(keyboard.IsKeyDown(Keys.Right))
                    {
                        if (playerSpeedX < MAX_X_SPEED)
                        {
                            if(playerSpeedX < 0)
                            {
                                playerSpeedX += 3;
                            } else
                            {
                                playerSpeedX += 0.6;
                            }
                            
                        }
                        playerX += playerSpeedX;
                        game.ViewportX -= playerSpeedX;
                    }
                    if (keyboard.IsKeyDown(Keys.Left))
                    {
                        if (playerSpeedX > MAX_X_SPEED * -1)
                        {
                            if (playerSpeedX > 0)
                            {
                                playerSpeedX -= 3;
                            }
                            else
                            {
                                playerSpeedX -= 0.6;
                            }
                        }
                        playerX += playerSpeedX;
                    }
                    break;
                case PlayerState.Shooting:

                    break;
                case PlayerState.Standing:
                    break;
                
			} // end of state switch

            

            if(keyboard.IsKeyDown(Keys.Space))
            {
                action = PlayerState.Moving;
            }
            if(keyboard.IsKeyDown(Keys.Right))
            {
                action = PlayerState.Moving;
            }
            if (keyboard.IsKeyDown(Keys.Left))
            {
                action = PlayerState.Moving;
            }


        } // end of update method

        public void Draw()
		{

		}

        public void LoadContent()
		{
            standingLeft = game.Content.Load<Texture2D>("StandinLeft");
		}

    }
}
