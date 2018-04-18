//=======================================================================================
//  ECET 3710 - Simon Says
//
//  Simon Says Class {SimonSays.cs}
//  by Austin Atteberry
//
//  This program interfaces three LEDs and one switch to the Raspberry Pi. The first time
//  the switch is pressed it will show the first LED pattern for the sequence. The second
//  button push will show the first LED pattern and then the second LED pattern. This
//  continues until the end of the sequence of 10 is reached. Then, the program exits.
//
//=======================================================================================

using System;
using System.Threading;
using WiringPi;
using SoftwareTones;

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
	
	/* Private Data Fields */
	private static bool gameOver;
	
	
	//-----------------------------------------------------------------------------------
	//  Main method - The entry point for the program
	//       params:  String array
	//       return:  none
	//-----------------------------------------------------------------------------------
	public static void Main(String[] args)
	{
		/* Initialize data field */
		gameOver = false;
		
		/* Create threads */
		Thread[] song = new Thread[2];

		/* Initialize threads */
		song[0] = new Thread(LosingSong);
		song[1] = new Thread(WinningSong);
		
		/* Variable declaration section */
		bool correctInput = false;
		
		/* Create an LEDGame object */
		LEDGame newGame = new LEDGame(10, 3,
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
		
		/* Loop through game rounds */
		for (int i = 0; i < newGame.Length(); i++)
		{
			/* Reset input checking variable */
			//correctInput = false;
			
			/* Display the LED pattern */
			newGame.LEDPattern(i);
			
			/* Check input */
			correctInput = newGame.CheckInput(i);
			
			/* Check for correct input */
			if (!correctInput)
			{
				break; // Exit loop on incorrect input
			}
		}
		
		/* Check for successful game completion */
		if (!correctInput)
		{
			/* Start loss song */
			song[0].Start();
			
			/* Call loss pattern method */
			newGame.Loss();
			
			/* Stop loss song */
			try
			{
				gameOver = true;
				song[0].Join();
			}
			catch (ThreadStateException)
			{}
		}
		else
		{
			/* Start win song */
			song[1].Start();
			
			/* Call win pattern method */
			newGame.Win();
			
			/* Stop win song */
			try
			{
				gameOver = true;
				song[1].Join();
			}
			catch (ThreadStateException)
			{}
		}
	}
	
	
	//-----------------------------------------------------------------------------------
	//  Winning Song method - Plays song if user wins
	//       params:  none
	//       return:  none
	//-----------------------------------------------------------------------------------
	private static void WinningSong()
	{
		while (!gameOver)
		{
			/* Play note */
			Tones.SoftToneWrite(SPEAKER, 370);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(91);
			}
			catch (System.OverflowException)
			{}
			
			/* Rest */
			Tones.SoftToneWrite(SPEAKER, 0);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(91);
			}
			catch (System.OverflowException)
			{}
			
			/* Play note */
			Tones.SoftToneWrite(SPEAKER, 370);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(91);
			}
			catch (System.OverflowException)
			{}
			
			/* Rest */
			Tones.SoftToneWrite(SPEAKER, 0);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(91);
			}
			catch (System.OverflowException)
			{}
			
			/* Play note */
			Tones.SoftToneWrite(SPEAKER, 294);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(91);
			}
			catch (System.OverflowException)
			{}
			
			/* Rest */
			Tones.SoftToneWrite(SPEAKER, 0);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(91);
			}
			catch (System.OverflowException)
			{}
			
			/* Play note */
			Tones.SoftToneWrite(SPEAKER, 247);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(91);
			}
			catch (System.OverflowException)
			{}
			
			/* Rest */
			Tones.SoftToneWrite(SPEAKER, 0);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(273);
			}
			catch (System.OverflowException)
			{}
			
			/* Play note */
			Tones.SoftToneWrite(SPEAKER, 247);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(91);
			}
			catch (System.OverflowException)
			{}
			
			/* Rest */
			Tones.SoftToneWrite(SPEAKER, 0);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(273);
			}
			catch (System.OverflowException)
			{}
			
			/* Play note */
			Tones.SoftToneWrite(SPEAKER, 330);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(91);
			}
			catch (System.OverflowException)
			{}
			
			/* Rest */
			Tones.SoftToneWrite(SPEAKER, 0);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(273);
			}
			catch (System.OverflowException)
			{}
			
			/* Play note */
			Tones.SoftToneWrite(SPEAKER, 330);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(91);
			}
			catch (System.OverflowException)
			{}
			
			/* Rest */
			Tones.SoftToneWrite(SPEAKER, 0);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(273);
			}
			catch (System.OverflowException)
			{}
			
			/* Play note */
			Tones.SoftToneWrite(SPEAKER, 330);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(91);
			}
			catch (System.OverflowException)
			{}
			
			/* Rest */
			Tones.SoftToneWrite(SPEAKER, 0);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(91);
			}
			catch (System.OverflowException)
			{}
			
			/* Play note */
			Tones.SoftToneWrite(SPEAKER, 415);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(91);
			}
			catch (System.OverflowException)
			{}
			
			/* Rest */
			Tones.SoftToneWrite(SPEAKER, 0);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(91);
			}
			catch (System.OverflowException)
			{}
			
			/* Play note */
			Tones.SoftToneWrite(SPEAKER, 415);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(91);
			}
			catch (System.OverflowException)
			{}
			
			/* Rest */
			Tones.SoftToneWrite(SPEAKER, 0);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(91);
			}
			catch (System.OverflowException)
			{}
			
			/* Play note */
			Tones.SoftToneWrite(SPEAKER, 440);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(91);
			}
			catch (System.OverflowException)
			{}
			
			/* Rest */
			Tones.SoftToneWrite(SPEAKER, 0);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(91);
			}
			catch (System.OverflowException)
			{}
			
			/* Play note */
			Tones.SoftToneWrite(SPEAKER, 494);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(91);
			}
			catch (System.OverflowException)
			{}
			
			/* Rest */
			Tones.SoftToneWrite(SPEAKER, 0);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(91);
			}
			catch (System.OverflowException)
			{}
			
			/* Play note */
			Tones.SoftToneWrite(SPEAKER, 440);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(91);
			}
			catch (System.OverflowException)
			{}
			
			/* Rest */
			Tones.SoftToneWrite(SPEAKER, 0);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(91);
			}
			catch (System.OverflowException)
			{}
			
			/* Play note */
			Tones.SoftToneWrite(SPEAKER, 440);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(91);
			}
			catch (System.OverflowException)
			{}
			
			/* Rest */
			Tones.SoftToneWrite(SPEAKER, 0);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(91);
			}
			catch (System.OverflowException)
			{}
			
			/* Play note */
			Tones.SoftToneWrite(SPEAKER, 440);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(91);
			}
			catch (System.OverflowException)
			{}
			
			/* Rest */
			Tones.SoftToneWrite(SPEAKER, 0);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(91);
			}
			catch (System.OverflowException)
			{}
			
			/* Play note */
			Tones.SoftToneWrite(SPEAKER, 330);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(91);
			}
			catch (System.OverflowException)
			{}
			
			/* Rest */
			Tones.SoftToneWrite(SPEAKER, 0);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(273);
			}
			catch (System.OverflowException)
			{}
			
			/* Play note */
			Tones.SoftToneWrite(SPEAKER, 294);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(91);
			}
			catch (System.OverflowException)
			{}
			
			/* Rest */
			Tones.SoftToneWrite(SPEAKER, 0);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(273);
			}
			catch (System.OverflowException)
			{}
			
			/* Play note */
			Tones.SoftToneWrite(SPEAKER, 370);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(91);
			}
			catch (System.OverflowException)
			{}
			
			/* Rest */
			Tones.SoftToneWrite(SPEAKER, 0);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(273);
			}
			catch (System.OverflowException)
			{}
			
			/* Play note */
			Tones.SoftToneWrite(SPEAKER, 370);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(91);
			}
			catch (System.OverflowException)
			{}
			
			/* Rest */
			Tones.SoftToneWrite(SPEAKER, 0);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(273);
			}
			catch (System.OverflowException)
			{}
			
			/* Play note */
			Tones.SoftToneWrite(SPEAKER, 370);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(91);
			}
			catch (System.OverflowException)
			{}
			
			/* Rest */
			Tones.SoftToneWrite(SPEAKER, 0);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(91);
			}
			catch (System.OverflowException)
			{}
			
			/* Play note */
			Tones.SoftToneWrite(SPEAKER, 330);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(91);
			}
			catch (System.OverflowException)
			{}
			
			/* Rest */
			Tones.SoftToneWrite(SPEAKER, 0);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(91);
			}
			catch (System.OverflowException)
			{}
			
			/* Play note */
			Tones.SoftToneWrite(SPEAKER, 330);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(91);
			}
			catch (System.OverflowException)
			{}
			
			/* Rest */
			Tones.SoftToneWrite(SPEAKER, 0);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(91);
			}
			catch (System.OverflowException)
			{}
			
			/* Play note */
			Tones.SoftToneWrite(SPEAKER, 370);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(91);
			}
			catch (System.OverflowException)
			{}
			
			/* Rest */
			Tones.SoftToneWrite(SPEAKER, 0);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(91);
			}
			catch (System.OverflowException)
			{}
			
			/* Play note */
			Tones.SoftToneWrite(SPEAKER, 330);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(91);
			}
			catch (System.OverflowException)
			{}
			
			/* Rest */
			Tones.SoftToneWrite(SPEAKER, 0);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(91);
			}
			catch (System.OverflowException)
			{}
		}
	}
	
	
	//-----------------------------------------------------------------------------------
	//  Losing Song method - Plays song if user loses
	//       params:  none
	//       return:  none
	//-----------------------------------------------------------------------------------
	private static void LosingSong()
	{
		while (!gameOver)
		{
			/* Play 7/8 of first measure */
			for (int i = 1; i < 5; i++)
			{
				/* Play 8th note */
				Tones.SoftToneWrite(SPEAKER, 392);
				
				/* Delay */
				try
				{
					WiringPi.Timing.delay(120);
				}
				catch (System.OverflowException)
				{}
				
				/* Rest for 8th note */
				if (i != 4)
				{
					Tones.SoftToneWrite(SPEAKER, 0);
					
					try
					{
						WiringPi.Timing.delay(120);
					}
					catch (System.OverflowException)
					{}
				}
			}
			
			/* Play dotted 16th note */
			Tones.SoftToneWrite(SPEAKER, 440);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(90);
			}
			catch (System.OverflowException)
			{}
		
			/* Brief pause to end of measure */
			Tones.SoftToneWrite(SPEAKER, 0);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(30);
			}
			catch (System.OverflowException)
			{}
			
			/* Play 7/8 of second measure */
			for (int i = 1; i < 5; i++)
			{
				/* Play 8th note */
				Tones.SoftToneWrite(SPEAKER, 370);
				
				/* Delay */
				try
				{
					WiringPi.Timing.delay(120);
				}
				catch (System.OverflowException)
				{}
				
				/* Rest for 8th note */
				if (i != 4)
				{
					Tones.SoftToneWrite(SPEAKER, 0);
					
					try
					{
						WiringPi.Timing.delay(120);
					}
					catch (System.OverflowException)
					{}
				}
			}
			
			/* Play dotted 16th note */
			Tones.SoftToneWrite(SPEAKER, 330);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(90);
			}
			catch (System.OverflowException)
			{}
		
			/* Brief pause to end of measure */
			Tones.SoftToneWrite(SPEAKER, 0);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(30);
			}
			catch (System.OverflowException)
			{}
			
			/* Play 7/8 of third measure */
			for (int i = 1; i < 5; i++)
			{
				/* Play 8th note */
				Tones.SoftToneWrite(SPEAKER, 247);
				
				/* Delay */
				try
				{
					WiringPi.Timing.delay(120);
				}
				catch (System.OverflowException)
				{}
				
				/* Rest for 8th note */
				if (i != 4)
				{
					Tones.SoftToneWrite(SPEAKER, 0);
					
					try
					{
						WiringPi.Timing.delay(120);
					}
					catch (System.OverflowException)
					{}
				}
			}
			
			/* Play dotted 16th note */
			Tones.SoftToneWrite(SPEAKER, 277);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(90);
			}
			catch (System.OverflowException)
			{}
		
			/* Brief pause to end of measure */
			Tones.SoftToneWrite(SPEAKER, 0);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(30);
			}
			catch (System.OverflowException)
			{}
			
			/* Play 7/8 of fourth measure */
			for (int i = 1; i < 5; i++)
			{
				/* Play 8th note */
				Tones.SoftToneWrite(SPEAKER, 233);
				
				/* Delay */
				try
				{
					WiringPi.Timing.delay(120);
				}
				catch (System.OverflowException)
				{}
				
				/* Rest for 8th note */
				if (i != 4)
				{
					Tones.SoftToneWrite(SPEAKER, 0);
					
					try
					{
						WiringPi.Timing.delay(120);
					}
					catch (System.OverflowException)
					{}
				}
			}
			
			/* Play dotted 16th note */
			Tones.SoftToneWrite(SPEAKER, 277);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(90);
			}
			catch (System.OverflowException)
			{}
		
			/* Brief pause to end of measure */
			Tones.SoftToneWrite(SPEAKER, 0);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(30);
			}
			catch (System.OverflowException)
			{}
		}
	}
}
