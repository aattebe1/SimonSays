//=======================================================================================
//  ECET 3710 - LEDGame
//
//  LED Game Class {LEDGame.cs}
//  by Austin Atteberry
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
	private byte switchPressed;            // Number of pressed switch
	
	
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
		SetDefaults();
		
		/* Initialize GPIO */
		InitializeIO();
		
		/* Set number of switches */
		switches = 3;
		
		/* Initialize data fields */
		switchPressed = 0;
	}
	
	
	//-----------------------------------------------------------------------------------
	//  LED Game method - The overloaded constructor method for the class. Instantiates
	//                    the parent class with a specified value. Sets the number of
	//                    switches to the specified value.
	//       params:  int, byte
	//       return:  none
	//-----------------------------------------------------------------------------------
	public LEDGame(int arrayLength, byte numSwitches)
		: base(arrayLength)
	{
		/* Assign switch and LED variables */
		SetDefaults();
		
		/* Initialize GPIO */
		InitializeIO();
		
		/* Set number of switches */
		if (numSwitches < 2)
		{
			switches = 1;
		}
		else
		{
			switches = 3;
		}
		
		/* Initialize data fields */
		switchPressed = 0;
	}
	
	
	//-----------------------------------------------------------------------------------
	//  LED Game method - The overloaded constructor method for the class. Instantiates
	//                    the parent class with a specified value. Sets the number of
	//                    switches to the specified value. Assigns pins and on/off values
	//                    to the specified values.
	//       params:  int, byte, int array
	//       return:  none
	//-----------------------------------------------------------------------------------
	public LEDGame(int arrayLength, byte numSwitches, int[] pinSettings)
		: base(arrayLength)
	{
		/* Assign switch and LED variables */
		SetPins(pinSettings);
		
		/* Initialize GPIO */
		InitializeIO();
		
		/* Set number of switches */
		if (numSwitches < 2)
		{
			switches = 1;
		}
		else
		{
			switches = 3;
		}
		
		/* Initialize data fields */
		switchPressed = 0;
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
		WiringPi.GPIO.pinMode(switch1, WiringPi.GPIO.INPUT);
		WiringPi.GPIO.pinMode(switch2, WiringPi.GPIO.INPUT);
		WiringPi.GPIO.pinMode(switch3, WiringPi.GPIO.INPUT);
		WiringPi.GPIO.pinMode(led1, WiringPi.GPIO.OUTPUT);
		WiringPi.GPIO.pinMode(led2, WiringPi.GPIO.OUTPUT);
		WiringPi.GPIO.pinMode(led3, WiringPi.GPIO.OUTPUT);
		SoftwareTones.Tones.SoftToneCreate(speaker);
		
		/* Initialize pins to off state */
		WiringPi.GPIO.digitalWrite(led1, off);
		WiringPi.GPIO.digitalWrite(led2, off);
		WiringPi.GPIO.digitalWrite(led3, off);
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
			if (round != 0)
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
				WiringPi.GPIO.digitalWrite(led1, on); // Turn on LED
			}
			else if (base.getSequence(round)[i] == 2)
			{
				WiringPi.GPIO.digitalWrite(led2, on); // Turn on LED
			}
			else
			{
				WiringPi.GPIO.digitalWrite(led3, on); // Turn on LED
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
			WiringPi.GPIO.digitalWrite(led1, off);
			WiringPi.GPIO.digitalWrite(led2, off);
			WiringPi.GPIO.digitalWrite(led3, off);
			
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
			SoftwareTones.Tones.SoftToneWrite(speaker, CNATURAL);
		}
		else if (sequence == 2)
		{
			SoftwareTones.Tones.SoftToneWrite(speaker, DSHARP);
		}
		else
		{
			SoftwareTones.Tones.SoftToneWrite(speaker, GSHARP);
		}
	}
	
	
	//-----------------------------------------------------------------------------------
	//  Stop Note method - Turns off the speaker (stops playing note)
	//       params:  none
	//       return:  none
	//-----------------------------------------------------------------------------------
	private void StopNote()
	{
		SoftwareTones.Tones.SoftToneWrite(speaker, 0);
	}
	
	
	//-----------------------------------------------------------------------------------
	//  Check Input method - Checks the input from the switch(es)
	//       params:  int
	//       return:  boolean
	//-----------------------------------------------------------------------------------
	public bool CheckInput(int round)
	{	
		for (int i = 0; i < base.getSequence(round).Length; i++)
		{	
			/* Wait for switch push */
			do
			{
				/* Create debounce variable */
				int debounce = 0;
				
				/* Reset switchPressed */
				switchPressed = 0;
				
				/* Check switch 1 */
				while (WiringPi.GPIO.digitalRead(switch1) == on)
				{
					/* Increase debounce */
					debounce++;
					
					/* Check debounce */
					if ((debounce >= 700) && (switchPressed == 0))
					{
						/* Set switchPressed */
						switchPressed = 1;

						/* Turn on LED */
						WiringPi.GPIO.digitalWrite(led1, on);
						
						/* Play note */
						PlayNote(1);
					}
				}
				
				/* Check switch 2 */
				while (WiringPi.GPIO.digitalRead(switch2) == on)
				{
					/* Increase debounce */
					debounce++;
					
					/* Check debounce */
					if ((debounce >= 700) && (switchPressed == 0))
					{
						/* Turn on LED */
						WiringPi.GPIO.digitalWrite(led2, on);
						
						/* Play note */
						PlayNote(2);
						
						/* Set switchPressed */
						switchPressed = 2;
					}
				}
				
				/* Check switch 3 */
				while (WiringPi.GPIO.digitalRead(switch3) == on)
				{
					/* Increase debounce */
					debounce++;
					
					/* Check debounce */
					if ((debounce >= 700) && (switchPressed == 0))
					{
						/* Turn on LED */
						WiringPi.GPIO.digitalWrite(led3, on);
						
						/* Play note */
						PlayNote(3);
						
						/* Set switchPressed */
						switchPressed = 3;
					}
				}
				
				/* Stop Note */
				StopNote();
				
				/* Turn off LEDs */
				WiringPi.GPIO.digitalWrite(led1, off);
				WiringPi.GPIO.digitalWrite(led2, off);
				WiringPi.GPIO.digitalWrite(led3, off);
				
				if (switchPressed == 1)
				{
					if (base.getSequence(round)[i] != 1)
					{
						System.Console.WriteLine("Wrong Input, Expecting Switch {0}, Switch {1} Pressed", base.getSequence(round)[i], switchPressed);
						return false; // Wrong input
					}
				}
				else if (switchPressed == 2)
				{
					if (base.getSequence(round)[i] != 2)
					{
						System.Console.WriteLine("Wrong Input, Expecting Switch {0}, Switch {1} Pressed", base.getSequence(round)[i], switchPressed);
						return false; // Wrong input
					}
				}
				else if (switchPressed == 3)
				{
					if (base.getSequence(round)[i] != 3)
					{
						System.Console.WriteLine("Wrong Input, Expecting Switch {0}, Switch {1} Pressed", base.getSequence(round)[i], switchPressed);
						return false; // Wrong input
					}
				}
			} while (switchPressed == 0);
		}

		return true;
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
		System.Console.ForegroundColor = System.ConsoleColor.Cyan;                  // Change text color
		
		/* Display the number of switches */
		if (switches == 1)
		{
			System.Console.Write("\n\n1 Switch Mode\n");
		}
		else
		{
			System.Console.Write("\n\n{0} Switches Mode\n", switches);
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
						System.Console.ForegroundColor = System.ConsoleColor.Red;   // Set to LED color
						System.Console.Write("Red ");
					}
					else if (base.getSequence(i)[j] == 2)
					{
						System.Console.ForegroundColor = System.ConsoleColor.Green; // Set to LED color
						System.Console.Write("Green ");
					}
					else
					{
						System.Console.ForegroundColor = System.ConsoleColor.Blue; // Set to LED color
						System.Console.Write("Blue ");
					}
				}
				
				/* Go to next line */
				System.Console.Write("\n");
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
		switch1 = 0x0D;
		switch2 = 0x13;
		switch3 = 0x10;
		led1 = 0x1A;
		led2 = 0x14;
		led3 = 0x15;
		speaker = 0x0C;
		off = 0x01;
		on = 0x00;
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
			switch1 = pinSettings[0];
			switch2 = pinSettings[1];
			switch3 = pinSettings[2];
			led1 = pinSettings[3];
			led2 = pinSettings[4];
			led3 = pinSettings[5];
			speaker = pinSettings[6];
			off = pinSettings[7];
			on = pinSettings[8];
		}
		catch (System.IndexOutOfRangeException)
		{
			switch1 = 0x0D;
			switch2 = 0x13;
			switch3 = 0x10;
			led1 = 0x1A;
			led2 = 0x14;
			led3 = 0x15;
			speaker = 0x0C;
			off = 0x01;
			on = 0x00;
		}
	}
	
	
	//-----------------------------------------------------------------------------------
	//  Loss method - Displays loss pattern and sounds
	//       params:  none
	//       return:  none
	//-----------------------------------------------------------------------------------
	public void Loss()
	{
		for (int i = 0; i < 10; i++)
		{	
			/* Turn on LEDs */
			WiringPi.GPIO.digitalWrite(led1, on);
			WiringPi.GPIO.digitalWrite(led2, on);
			WiringPi.GPIO.digitalWrite(led3, on);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(500);
			}
			catch (System.OverflowException)
			{}
			
			/* Turn off LEDs */
			WiringPi.GPIO.digitalWrite(led1, off);
			WiringPi.GPIO.digitalWrite(led2, off);
			WiringPi.GPIO.digitalWrite(led3, off);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(500);
			}
			catch (System.OverflowException)
			{}
		}
	}
	
	
	//-----------------------------------------------------------------------------------
	//  Win method - Displays win pattern and sounds
	//       params:  none
	//       return:  none
	//-----------------------------------------------------------------------------------
	public void Win()
	{
		for (int i = 0; i < 10; i++)
		{
			/* Turn on LEDs */
			WiringPi.GPIO.digitalWrite(led1, on);
			WiringPi.GPIO.digitalWrite(led2, off);
			WiringPi.GPIO.digitalWrite(led3, off);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(500);
			}
			catch (System.OverflowException)
			{}
			
			/* Turn off LEDs */
			WiringPi.GPIO.digitalWrite(led1, off);
			WiringPi.GPIO.digitalWrite(led2, on);
			WiringPi.GPIO.digitalWrite(led3, on);
			
			/* Delay */
			try
			{
				WiringPi.Timing.delay(500);
			}
			catch (System.OverflowException)
			{}
		}
		
		/* Turn off LEDs */
		WiringPi.GPIO.digitalWrite(led1, off);
		WiringPi.GPIO.digitalWrite(led2, off);
		WiringPi.GPIO.digitalWrite(led3, off);
	}
}
