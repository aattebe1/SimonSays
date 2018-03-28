//=======================================================================================
//  ECET 3710 - LEDGame
//
//  LED Game Class {LEDGame.cs}
//  by Austin Atteberry, Reza Kamarjian, and Mark Brown
//
//  This class extends the Sequence class. It provides the methods control the game.
//
//=======================================================================================

public class LEDGame : Sequence{
	
	/* Private Constants */
	private const int SWITCH_1 = 0x0D; // PIN33
	private const int SWITCH_2 = 0x13; // PIN35
	private const int SWITCH_3 = 0x10; // PIN36
	private const int LED_1 = 0x1A;    // PIN37
	private const int LED_2 = 0x14;    // PIN38
	private const int LED_3 = 0x15;    // PIN40
	private const int OFF = 0x01;      // 5V
	private const int ON = 0x00;       // 0V
	
	/* Private Data Fields */
	private int round;     // Current round
	private byte switches; // Number of switches used in the game
	
	
	//-----------------------------------------------------------------------------------
	//  LED Game method - The default constructor method for the class. Instantiates the
	//                    parent class with a value of 10 and sets the game to be played
	//                    with 3 switches.
	//       params:  none
	//       return:  none
	//-----------------------------------------------------------------------------------
	public LEDGame()
		: base(10)
	{
		/* Initialize GPIO */
		InitializeIO();
		
		/* Assign switches & start round at 0 */
		this.switches = 3;
		this.round = 0;
	}
	
	
	//-----------------------------------------------------------------------------------
	//  LED Game method - The overloaded constructor method for the class. Instantiates
	//                    the parent class with a specified value. Sets the number of
	//                    switches to the specified value.
	//       params:  int, byte
	//       return:  none
	//-----------------------------------------------------------------------------------
	public LEDGame(int arrayLength, byte switches)
		: base(arrayLength)
	{
		/* Initialize GPIO */
		InitializeIO();
		
		/* Assign switches, default to 1 or 3 if number outside of range is entered */
		if (switches < 2)
		{
			this.switches = 1;
		}
		else if (switches == 2)
		{
			this.switches = 2;
		}
		else
		{
			this.switches = 3;
		}
		
		/* Start round at 0 */
		this.round = 0;
	}
	
	
	//-----------------------------------------------------------------------------------
	//  Initialize IO method - Initializes the GPIO pins
	//       params:  none
	//       return:  none
	//-----------------------------------------------------------------------------------
	private void InitializeIO()
	{
		WiringPi.Init.WiringPiSetupGpio();
		WiringPi.GPIO.pinMode(SWITCH_1, WiringPi.GPIO.INPUT);
		WiringPi.GPIO.pinMode(SWITCH_2, WiringPi.GPIO.INPUT);
		WiringPi.GPIO.pinMode(SWITCH_3, WiringPi.GPIO.INPUT);
		WiringPi.GPIO.pinMode(LED_1, WiringPi.GPIO.OUTPUT);
		WiringPi.GPIO.pinMode(LED_2, WiringPi.GPIO.OUTPUT);
		WiringPi.GPIO.pinMode(LED_3, WiringPi.GPIO.OUTPUT);
	}
	
	
	//-----------------------------------------------------------------------------------
	//  LED Pattern method - Controls the current LED pattern for the sequence.
	//       params:  none
	//       return:  none
	//-----------------------------------------------------------------------------------
	public void LEDPattern()
	{
		/* Loop through current pattern */
		for (int i = 0; i < base.getSequence(this.round).Length; i++)
		{
			/* Determine which LED is active */
			if (base.getSequence(this.round)[i] == 1)
			{
				WiringPi.GPIO.digitalWrite(LED_1, ON); // Turn on LED
			}
			else if (base.getSequence(this.round)[i] == 2)
			{
				WiringPi.GPIO.digitalWrite(LED_2, ON);  // Turn on LED
			}
			else
			{
				WiringPi.GPIO.digitalWrite(LED_3, ON);    // Turn on LED
			}
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay((uint)(System.Math.Abs(10 - i) * 200));
			}
			catch (System.OverflowException)
			{}
			
			/* Turn off LEDs */
			WiringPi.GPIO.digitalWrite(LED_1, OFF);
			WiringPi.GPIO.digitalWrite(LED_1, OFF);
			WiringPi.GPIO.digitalWrite(LED_1, OFF);
		}
	}
	
	
	//-----------------------------------------------------------------------------------
	//  Check Input method - Checks the input from the switch(es)
	//       params:  none
	//       return:  boolean
	//-----------------------------------------------------------------------------------
	public bool CheckInput()
	{
		bool correctInput = true;
		byte switchPressed = 0;
		byte[] currentRound = base.getSequence(this.round);
		
		/* Loop through current pattern */
		for (int i = 0; i < currentRound.Length; i++)
		{
			/* Wait for switch push */
			while(switchPressed == 0)
			{
				if (WiringPi.GPIO.digitalRead(SWITCH_1) == ON)
				{
					switchPressed = 1; // Switch 1 pressed
				}
				else if (WiringPi.GPIO.digitalRead(SWITCH_2) == ON)
				{
					switchPressed = 2; // Switch 2 pressed
				}
				else if (WiringPi.GPIO.digitalRead(SWITCH_3) == ON)
				{
					switchPressed = 3; // Switch 3 pressed
				}
			}
			
			/* Determine the number of switches */
			if (this.switches == 1)
			{
				break; // Any switch is correct, exit loop
			}
			else if (this.switches == 2)
			{
				/* Determine if switch is correct (any switch is correct for 3rd LED) */
				if ((currentRound[i] == 1) && (switchPressed != 1))
				{
					correctInput = false; // Wrong switch
					break;                // Exit loop
				}
				else if ((currentRound[i] == 2) && (switchPressed != 2))
				{
					correctInput = false; // Wrong switch
					break;                // Exit loop
				}
			}
			else
			{
				/* Determine if switch is correct */
				if ((currentRound[i] == 1) && (switchPressed != 1))
				{
					correctInput = false; // Wrong switch
					break;                // Exit loop
				}
				else if ((currentRound[i] == 2) && (switchPressed != 2))
				{
					correctInput = false; // Wrong switch
					break;                // Exit loop
				}
				else if ((currentRound[i] == 3) && (switchPressed != 3))
				{
					correctInput = false; // Wrong switch
					break;                // Exit loop
				}
			}
		}
		
		/* Go to next round */
		this.round++;
		
		/* Return true if the correct input was entered, false otherwise */
		return correctInput;
	}
	
	//-----------------------------------------------------------------------------------
	//  Cheat Mode method - Displays a message stating that the cheatmode is activated,
	//                      then displays the patterns that will be presented in the
	//                      game.
	//       params:  boolean
	//       return:  none
	//-----------------------------------------------------------------------------------
	public void CheatMode(bool onOff)
	{
		System.Console.ForegroundColor = System.ConsoleColor.Cyan;                      // Change text color
		
		/* Display the number of switches */
		if (this.switches == 1)
		{
			System.Console.Write("\n\n1 Switch Mode\n");
		}
		else
		{
			System.Console.Write("\n\n{0} Switches Mode\n", this.switches);
		}
		
		/* Determine cheatmode status */
		if (onOff)
		{
			System.Console.Write("Cheat Mode Activated!\n\n");
			
			for (int i = 0; i < base.Length(); i++)
			{
				for (int j = 0; j < base.getSequence(i).Length; j++)
				{
					/* Print the sequences to the console */
					if (base.getSequence(i)[j] == 1)
					{
						System.Console.ForegroundColor = System.ConsoleColor.Yellow;    // Set to LED color
						System.Console.Write("{0} ", base.getSequence(i)[j]);
					}
					else if (base.getSequence(i)[j] == 2)
					{
						System.Console.ForegroundColor = System.ConsoleColor.Green;     // Set to LED color
						System.Console.Write("{0} ", base.getSequence(i)[j]);
					}
					else
					{
						System.Console.ForegroundColor = System.ConsoleColor.Red;       // Set to LED color
						System.Console.Write("{0} ", base.getSequence(i)[j]);
					}
					
					System.Console.Write("\n");                                         // Go to next line
				}
			}
		}
		else
		{
			System.Console.Write("Normal Mode Activated!\n\n");
		}
		
		System.Console.ResetColor();
	}
}