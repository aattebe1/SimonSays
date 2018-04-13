GMCS = mcs

CFLAGS = 

default: SimonSays.exe

SimonSays.exe: SimonSays.cs RPi.cs SoftwareTones.cs Sequence.cs LEDGame.cs
	$(GMCS) $(CFLAGS) SimonSays.cs RPi.cs SoftwareTones.cs Sequence.cs LEDGame.cs

clean:
	$(RM) *.exe