//=======================================================================================
//  ECET 3710 - LEDGame
//
//  LED Game Class {LEDGame.cs}
//  by Austin Atteberry, Reza Kamarjian, and Mark Brown
//
//  This class extends the Sequence class. It provides the methods control the game.
//
//=======================================================================================

public class LEDGame : Sequence
{
	/* Constants */
	private const int GSHARP = 208;
	private const int CNATURAL = 262;
	private const int DSHARP = 311;
	
	
	/* Private Data Fields */
	private int switch1, switch2, switch3; // Switch Pins
	private int led1, led2, led3;          // LED pins
	private int speaker;                   // Speaker pin
	private int on, off;                   // ON/OFF Values
	private byte switches;                 // Number of switches
	
	
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
		/* Assign switch and LED variables */
		this.SetDefaults();
		
		/* Initialize GPIO */
		InitializeIO();
		
		/* Set number of switches */
		this.switches = 3;
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
		/* Assign switch and LED variables */
		this.SetDefaults();
		
		/* Initialize GPIO */
		InitializeIO();
		
		/* Set number of switches */
		if (switches < 2)
		{
			this.switches = 1;
		}
		else
		{
			this.switches = 3;
		}
	}
	
	
	//-----------------------------------------------------------------------------------
	//  LED Game method - The overloaded constructor method for the class. Instantiates
	//                    the parent class with a specified value. Sets the number of
	//                    switches to the specified value. Assigns pins and on/off values
	//                    to the specified values.
	//       params:  int, byte, int array
	//       return:  none
	//-----------------------------------------------------------------------------------
	public LEDGame(int arrayLength, byte switches, int[] pinSettings)
		: base(arrayLength)
	{
		/* Assign switch and LED variables */
		this.SetPins(pinSettings);
		
		/* Initialize GPIO */
		InitializeIO();
		
		/* Set number of switches */
		if (switches < 2)
		{
			this.switches = 1;
		}
		else
		{
			this.switches = 3;
		}
	}
	
	
	//-----------------------------------------------------------------------------------
	//  Initialize IO method - Initializes the GPIO pins
	//       params:  none
	//       return:  none
	//-----------------------------------------------------------------------------------
	private void InitializeIO()
	{
		/* Run setup */
		WiringPi.Init.WiringPiSetupGpio();
		
		/* Setup pins */
		WiringPi.GPIO.pinMode(this.switch1, WiringPi.GPIO.INPUT);
		WiringPi.GPIO.pinMode(this.switch2, WiringPi.GPIO.INPUT);
		WiringPi.GPIO.pinMode(this.switch3, WiringPi.GPIO.INPUT);
		WiringPi.GPIO.pinMode(this.led1, WiringPi.GPIO.OUTPUT);
		WiringPi.GPIO.pinMode(this.led2, WiringPi.GPIO.OUTPUT);
		WiringPi.GPIO.pinMode(this.led3, WiringPi.GPIO.OUTPUT);
		SoftwareTones.Tones.SoftToneCreate(this.speaker);
		
		/* Initialize pins to off state */
		WiringPi.GPIO.digitalWrite(this.led1, this.off);
		WiringPi.GPIO.digitalWrite(this.led2, this.off);
		WiringPi.GPIO.digitalWrite(this.led3, this.off);
	}
	
	
	//-----------------------------------------------------------------------------------
	//  LED Pattern method - Controls the current LED pattern for the sequence.
	//       params:  int
	//       return:  none
	//-----------------------------------------------------------------------------------
	public void LEDPattern(int round)
	{
		/* Loop through current pattern */
		for (int i = 0; i < base.getSequence(round).Length; i++)
		{
			/* Check for first round */
			if (round != 1)
			{
				/* Delay if not first round */
				try
				{
					WiringPi.Timing.delay(500);
				}
				catch (System.OverflowException)
				{}
			}
			
			/* Determine which LED is active */
			if (base.getSequence(round)[i] == 1)
			{
				WiringPi.GPIO.digitalWrite(this.led1, this.on); // Turn on LED
			}
			else if (base.getSequence(round)[i] == 2)
			{
				WiringPi.GPIO.digitalWrite(this.led2, this.on);  // Turn on LED
			}
			else
			{
				WiringPi.GPIO.digitalWrite(this.led3, this.on);    // Turn on LED
			}
			
			/* Play note */
			PlayNote(base.getSequence(round)[i]);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay((uint)(System.Math.Abs(10 - round) * 100));
			}
			catch (System.OverflowException)
			{}
			
			/* Stop note */
			StopNote();
			
			/* Turn off LEDs */
			WiringPi.GPIO.digitalWrite(this.led1, this.off);
			WiringPi.GPIO.digitalWrite(this.led2, this.off);
			WiringPi.GPIO.digitalWrite(this.led3, this.off);
			
			/* Check for end of pattern */
			if ((i + 1) < base.getSequence(round).Length)
			{
				/* Delay if not end of pattern */
				try
				{
					WiringPi.Timing.delay((uint)(System.Math.Abs(10 - round) * 100));
				}
				catch (System.OverflowException)
				{}
			}
		}
	}
	
	
	//-----------------------------------------------------------------------------------
	//  Play Note method - Plays the note that corresponds to the active LED
	//       params:  byte
	//       return:  none
	//-----------------------------------------------------------------------------------
	private void PlayNote(byte sequence)
	{
		/* Determine which note to play */
		if (sequence == 1)
		{
			SoftwareTones.Tones.SoftToneWrite(this.speaker, CNATURAL);
		}
		else if (sequence == 2)
		{
			SoftwareTones.Tones.SoftToneWrite(this.speaker, DSHARP);
		}
		else
		{
			SoftwareTones.Tones.SoftToneWrite(this.speaker, GSHARP);
		}
	}
	
	
	//-----------------------------------------------------------------------------------
	//  Stop Note method - Turns off the speaker (stops playing note)
	//       params:  none
	//       return:  none
	//-----------------------------------------------------------------------------------
	private void StopNote()
	{
		SoftwareTones.Tones.SoftToneWrite(this.speaker, 0);
	}
	
	
	//-----------------------------------------------------------------------------------
	//  Check Input method - Checks the input from the switch(es)
	//       params:  int, boolean pointer, byte pointer
	//       return:  none
	//-----------------------------------------------------------------------------------
	public void CheckInput(int round, ref bool correctInput, ref byte switchPressed)
	{
		byte[] currentRound = base.getSequence(round);
		
		switch (this.switches)
		{
			case 1:
			
				while (!correctInput)
				{
					/* Reset the number of the pressed switch */
					switchPressed = 0;
					
					/* Wait for switch push */
					while (switchPressed == 0)
					{
						/* Check for switch press */
						if (WiringPi.GPIO.digitalRead(this.switch1) == this.on)
						{
							switchPressed = 1;   // Switch 1 pressed
							correctInput = true; // Correct input received
						}
					}
				}
				
				break;
			
			default:
				
			break;
		}
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
	
	
	//-----------------------------------------------------------------------------------
	//  Set Defaults method - Sets the default pin numbers if to use if no pin numbers
	//                        were specified by the user
	//       params:  none
	//       return:  none
	//-----------------------------------------------------------------------------------
	private void SetDefaults()
	{
		this.switch1 = 0x0D;
		this.switch2 = 0x13;
		this.switch3 = 0x10;
		this.led1 = 0x1A;
		this.led2 = 0x14;
		this.led3 = 0x15;
		this.speaker = 0x0C;
		this.off = 0x01;
		this.on = 0x00;
	}
	
	
	//-----------------------------------------------------------------------------------
	//  Set Pins method - Sets the default pin numbers if to use if no pin numbers
	//                        were specified by the user
	//       params:  int array
	//       return:  none
	//-----------------------------------------------------------------------------------
	private void SetPins(int[] pinSettings)
	{
		/* Assign pin numbers */
		try
		{
			this.switch1 = pinSettings[0];
			this.switch2 = pinSettings[1];
			this.switch3 = pinSettings[2];
			this.led1 = pinSettings[3];
			this.led2 = pinSettings[4];
			this.led3 = pinSettings[5];
			this.speaker = pinSettings[6];
			this.off = pinSettings[7];
			this.on = pinSettings[8];
		}
		catch (System.IndexOutOfRangeException)
		{
			this.switch1 = 0x0D;
			this.switch2 = 0x13;
			this.switch3 = 0x10;
			this.led1 = 0x1A;
			this.led2 = 0x14;
			this.led3 = 0x15;
			this.speaker = 0x0C;
			this.off = 0x01;
			this.on = 0x00;
		}
	}
}
