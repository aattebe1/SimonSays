//=======================================================================================
//  ECET 3710 - Simon Says
//
//  Simon Says Class {SimonSays.cs}
//  by Austin Atteberry, Reza Kamarjian, and Mark Brown
//
//  This program interfaces three LEDs and one switch to the Raspberry Pi. The first time
//  the switch is pressed it will show the first LED pattern for the sequence. The second
//  button push will show the first LED pattern and then the second LED pattern. This
//  continues until the end of the sequence of 10 is reached. Then, the program exits.
//
//=======================================================================================

using System;
using WiringPi;

public class SimonSays
{
	//-----------------------------------------------------------------------------------
	//  Main method - The entry point for the program
	//       params:  String array
	//       return:  none
	//-----------------------------------------------------------------------------------
	public static void Main(String[] args)
	{
		/* Create an LEDGame object */
        LEDGame newGame = new LEDGame(10, 1);

        /* Set cheatmode status */
        if (args[0].ToLowerInvariant() == "cheatmode")
        {
            newGame.CheatMode(true);
        }
        else
        {
            newGame.CheatMode(false);
        }

        for (int i = 0; i < newGame.Length(); i++)
        {
            /* Display the LED pattern */
            newGame.LEDPattern();

            /* Check input, exit if incorrect */
            if (!newGame.CheckInput())
            {
                break;
            }
        }
	}
}