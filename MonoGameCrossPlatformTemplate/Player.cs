using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CIS580Project4
{
    public enum PlayerState
    {
        Moving,
        Jumping,
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
        double playerSpeedX;
        double playerSpeedY;
        PlayerState action;
        PlayerLocation location;
        
        double playerX;
        double playerY;

        public Player()
        {
            LoadContent();




        }

        public void Update()
		{

            switch (action)
			{
                case PlayerState.Moving:
					
                    break;
                case PlayerState.Jumping:
                    if(location == PlayerLocation.Air)
                    {

                    }
                    break;
                case PlayerState.Shooting:

                    break;
                case PlayerState.Standing:
                    break;
                
			} // end of state switch

            var keyboard = Keyboard.GetState();

            if(keyboard.IsKeyDown(Keys.Space))
            {
                action = PlayerState.Jumping;
            }


		} // end of update method

        public void Draw()
		{

		}

        public void LoadContent()
		{

		}

    }
}
