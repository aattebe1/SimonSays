//=======================================================================================
//  ECET 3710 - Sequence
//
//  Sequence Class {Sequence.cs}
//  by Austin Atteberry
//
//  This class defines a "Simon Says" type of sequence of patterns that increases in
//  length with each iteration.
//
//=======================================================================================

public class Sequence {
	
	/* Private Data Fields */
	private System.Collections.Generic.List<byte[]> patterns;
	private int currentIndex, popIndex;
	private readonly int lastIndex;
	
	
	//-----------------------------------------------------------------------------------
	//  Pattern method - The constructor method for the class
	//       params:  int
	//       return:  none
	//-----------------------------------------------------------------------------------
	public Sequence(int arrayLength)
	{
		this.currentIndex = 0;
		this.popIndex = arrayLength - 1;
		this.lastIndex = arrayLength - 1;
		this.patterns = new System.Collections.Generic.List<byte[]>();
		CreateList(ref arrayLength);
	}


	//-----------------------------------------------------------------------------------
	//  CreateList method - Creates a list of arrays
	//       params:  int reference
	//       return:  none
	//-----------------------------------------------------------------------------------
	private void CreateList(ref int arrayLength)
	{
		/* Create a Random object */
		System.Random generator = new System.Random();

		/* Create arrays and add them to patterns */
		for (int i = 0; i < arrayLength; i++)
		{
			byte[] temp = new byte[i + 1]; // Creates byte array of i+1 length
			
			if (i > 0)
			{
				for (int j = 0; j < i; j++)
				{
					/* Copy the elements from the previous array to temp */
					System.Array.Copy(this.patterns[i - 1], temp, i);
				}
			}
			
			/* Generate a random value from 1 to 3 and assign it to the last array element */
			temp[i] = (byte)(generator.Next(3) + 1);
			
			/* Add the array to patterns */
			patterns.Add(temp);
		}
	}
	
	
	//-----------------------------------------------------------------------------------
	//  getSequence method - Returns the array at the specified index of patterns
	//       params:  int
	//       return:  byte array
	//-----------------------------------------------------------------------------------
	public byte[] getSequence(int index)
	{
		/* Return the array */
		return this.patterns[index];
	}
	
	
	//-----------------------------------------------------------------------------------
	//  Next method - Returns the array at the current index of patterns
	//       params:  none
	//       return:  byte array
	//-----------------------------------------------------------------------------------
	public byte[] Next()
	{
		/* Go to next index */
		currentIndex++;
		
		/* Return the array */
		return this.patterns[this.currentIndex - 1];
	}
	
	
	//-----------------------------------------------------------------------------------
	//  Previous method - Returns the array at the previous index of patterns
	//       params:  none
	//       return:  byte array
	//-----------------------------------------------------------------------------------
	public byte[] Previous()
	{
		/* Return the array */
		return this.patterns[this.currentIndex - 1];
	}
	
	
	//-----------------------------------------------------------------------------------
	//  Pop method - Returns the array at the pop index of patterns and decreases
	//               popIndex
	//       params:  none
	//       return:  byte array
	//-----------------------------------------------------------------------------------
	public byte[] Pop()
	{
		/* Decrease popIndex */
		popIndex--;
		
		/* Return the array */
		return this.patterns[this.popIndex + 1];
	}
	
	
	//-----------------------------------------------------------------------------------
	//  Peek method - Returns the array at the pop index of patterns without decreasing
	//                popIndex
	//       params:  none
	//       return:  byte array
	//-----------------------------------------------------------------------------------
	public byte[] Peek()
	{
		/* Return the array */
		return this.patterns[this.popIndex];
	}
	
	
	//-----------------------------------------------------------------------------------
	//  Get Current Index method - Returns the current index
	//       params:  none
	//       return:  int
	//-----------------------------------------------------------------------------------
	public int getCurrentIndex()
	{
		/* Return the current index */
		return this.currentIndex;
	}
	
	
	//-----------------------------------------------------------------------------------
	//  Get Pop Index method - Returns the pop index
	//       params:  none
	//       return:  int
	//-----------------------------------------------------------------------------------
	public int getPopIndex()
	{
		/* Return the pop index */
		return this.popIndex;
	}
	
	
	//-----------------------------------------------------------------------------------
	//  Length method - Returns the list length
	//       params:  none
	//       return:  int
	//-----------------------------------------------------------------------------------
	public int Length()
	{
		/* Return the list length */
		return lastIndex + 1;
	}
}