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
	/* Class Constants */
	const int SWITCH_A = 6;
	const int RED = 13;
	const int GREEN = 19;
	const int YELLOW = 26;
	const int OFF = 1;
	const int ON = 0;
	
	
	//-----------------------------------------------------------------------------------
	//  Main method - The entry point for the program
	//       params:  String array
	//       return:  none
	//-----------------------------------------------------------------------------------
	public static void Main(String[] args)
	{
		/* Initialize Pins */
		Initialize();
		
		/* Play Game */
		if ((args.Length == 1) && (args[0].ToLowerInvariant() == "cheatmode"))
		{
			PlayGame(true);
		}
		else
		{
			PlayGame(false);
		}
	}
	
	
	//-----------------------------------------------------------------------------------
	//  Initialize method - Initializes the pins 
	//       params:  none
	//       return:  none
	//-----------------------------------------------------------------------------------
	private static void Initialize()
	{
		/* Initialize pins */
		Init.WiringPiSetupGpio();
		GPIO.pinMode(SWITCH_A, GPIO.INPUT);
		GPIO.pinMode(RED, GPIO.OUTPUT);
		GPIO.pinMode(GREEN, GPIO.OUTPUT);
		GPIO.pinMode(YELLOW, GPIO.OUTPUT);
	}
	
	
	//-----------------------------------------------------------------------------------
	//  Play Game method - The code the controls the gameplay characteristics
	//       params:  boolean
	//       return:  none
	//-----------------------------------------------------------------------------------
	private static void PlayGame(bool cheatMode)
	{
		/* Create Sequence Object */
		Sequence seqGame = new Sequence(10);
		
		/* Cheatmode on */
		if (cheatMode)
		{
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.Write("\n\nCheat Mode Activated!\n\n");
			
			for (int i = 0; i < seqGame.Length(); i++)
			{
				for (int j = 0; j < seqGame.getSequence(i).Length; j++)
				{
					/* Print the sequences to the console */
					if (seqGame.getSequence(i)[j] == 1)
					{
						Console.ForegroundColor = ConsoleColor.Yellow;
						Console.Write("{0} ", seqGame.getSequence(i)[j]);
					}
					else if (seqGame.getSequence(i)[j] == 2)
					{
						Console.ForegroundColor = ConsoleColor.Green;
						Console.Write("{0} ", seqGame.getSequence(i)[j]);
					}
					else
					{
						Console.ForegroundColor = ConsoleColor.Red;
						Console.Write("{0} ", seqGame.getSequence(i)[j]);
					}
				}
				
				Console.Write("\n");
			}
			
			Console.ResetColor();
		}
		
		for (int i = 0; i < seqGame.Length(); i++)
		{
			for (int j = 0; j < seqGame.getSequence(i).Length; j++)
			{
				/* Turn on LED */
				if (seqGame.getSequence(i)[j] == 1)
				{
					GPIO.digitalWrite(YELLOW, 0);
				}
				else if (seqGame.getSequence(i)[j] == 2)
				{
					GPIO.digitalWrite(GREEN, 0);
				}
				else
				{
					GPIO.digitalWrite(RED, 0);
				}
				
				/* Delay */
				try
				{
					Timing.delay((uint)(Math.Abs(10 - i) * 200));
				}
				catch (OverflowException)
				{}
				
				GPIO.digitalWrite(YELLOW, 1);
				GPIO.digitalWrite(GREEN, 1);
				GPIO.digitalWrite(RED, 1);
			}
			
			/* Wait for switch push */
			while(GPIO.digitalRead(SWITCH_A) == 1)
			{
				/* Delay */
				Timing.delay(10);
			}
		}
	}
}