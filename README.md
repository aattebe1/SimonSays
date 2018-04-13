Dependencies:

	Mono (https://www.mono-project.com/download/stable/#download-lin-raspbian) (Install mono-complete package)
	Wiring Pi Interface library (http://wiringpi.com/download-and-install/)
	RPi.cs (http://stippens.com/teach/materials/embedded/rPi/exampleCode/Csharp/RPi.cs)


Instructions

	1. Place RPi.cs in the source directory as the other files.
	2. Use the Makefile OR Compile using the command 'mcs SimonSays.cs RPi.cs SoftwareTones.cs Sequence.cs LEDGame.cs'.


Game Mode Descriptions

	• One-button mode displays the LED pattern for the current round, then requires the user to press button 1 before continuing to the next round.
	• Three-button mode works using the traditional Simon gameplay functionality.


Notes:

	• The program must be run with root privileges.
	• Three-button mode is the current default. To use one-button mode, change line 43 of SimonSays.cs to 'LEDGame newGame = new LEDGame(10, 1,'.
	• All code has been tested and is working, however, I assume no responsibility for any damage to your equipment. USE AT YOUR OWN RISK!


Pin Numbering Explanation:

	The pin numbering is based on the Raspberry Pi 3 Version B+ 40 pin GPIO port. See the pin numbering file for a diagram of the pin assignments used in this program. To determine your Pi's specific pin numbers, use the command 'gpio readall'. The numbers listed in the BCM column are used for assigning pins in the program.

