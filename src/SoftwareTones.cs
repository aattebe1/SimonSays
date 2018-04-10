/************************************************************************************************
 * This wrapper class was written by Austin Atteberry for the software tone functions of Gordon *
 * Hendersons WiringPi C library                                                                *
 *                                                                                              *
 * I take no credit for the underlying functionality that this wrapper provides.                *
 * Authored: 05/04/2018                                                                         *
 ***********************************************************************************************/

 using System;
 using System.Runtime.InteropServices;

 namespace SoftwareTones
 {
	/// <summary>
	/// Provides access to the Software Tone Library
	/// </summary>
	public class Tones
	{
		[DllImport("libwiringPi.so", EntryPoint = "softToneCreate")]
		public static extern int SoftToneCreate(int pin);

		[DllImport("libwiringPi.so", EntryPoint = "softToneWrite")]
		public static extern int SoftToneWrite(int pin, int freq);

		[DllImport("libwiringPi.so", EntryPoint = "softToneStop")]
		public static extern void SoftToneStop(int pin);
	}
 }