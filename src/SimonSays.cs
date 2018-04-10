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
	/* Private Constants */
	private const int SWITCH_1 = 0x0D; // PIN33
	private const int SWITCH_2 = 0x13; // PIN35
	private const int SWITCH_3 = 0x10; // PIN36
	private const int LED_1 = 0x1A;    // PIN37
	private const int LED_2 = 0x14;    // PIN38
	private const int LED_3 = 0x15;    // PIN40
	private const int SPEAKER = 0x0C;  // PIN32
	private const int OFF = 0x01;      // 3.3V
	private const int ON = 0x00;       // 0V
	
	
	//-----------------------------------------------------------------------------------
	//  Main method - The entry point for the program
	//       params:  String array
	//       return:  none
	//-----------------------------------------------------------------------------------
	public static void Main(String[] args)
	{
		/* Save the number of the last switch pressed */
		byte switchPressed = 0;
		
		/* Variable declaration section */
		bool correctInput = false;
		
		/* Create an LEDGame object */
		LEDGame newGame = new LEDGame(10, 1,
			new int[]
			{
				SWITCH_1,
				SWITCH_2,
				SWITCH_3,
				LED_1,
				LED_2,
				LED_3,
				SPEAKER,
				OFF,
				ON
			});
		
		/* Set cheatmode status */
		try
		{
			if (args[0].ToLowerInvariant() == "cheatmode")
			{
				newGame.CheatMode(true);
			}
		}
		catch (IndexOutOfRangeException)
		{
			newGame.CheatMode(false);
		}
		
		for (int i = 0; i < newGame.Length(); i++)
		{
			/* Reset input checking variable */
			correctInput = false;
			
			/* Display the LED pattern */
			newGame.LEDPattern(i);
			
			/* Check input */
			newGame.CheckInput(i, ref correctInput, ref switchPressed);
		}
	}
}
