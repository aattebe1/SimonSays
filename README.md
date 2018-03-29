Dependencies:

	Mono (https://www.mono-project.com/download/stable/#download-lin-raspbian) (Install mono-complete package)
	Wiring Pi Interface library (http://wiringpi.com/download-and-install/)
	RPi.cs (http://stippens.com/teach/materials/embedded/rPi/exampleCode/Csharp/RPi.cs)


Instructions

	1. Place RPi.cs in the source directory.
	2. Compile using the command 'mcs SimonSays.cs RPi.cs Sequence.cs LEDGame.cs'.


Game Mode Descriptions

	• One-button mode displays the LED pattern for the current round, then requires the user to press button 1 before continuing to the next round.
	• Three-button mode works using the traditional Simon gameplay functionality.


Notes:

	• The program must be run with root privileges.
	• Only one-button mode functionality is complete at this time. One-button mode has been tested on the Raspberry Pi. Three-button mode will be added soon.

