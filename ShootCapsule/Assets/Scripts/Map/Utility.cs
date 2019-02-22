using System.Collections;

//FISHER-YATES ALGORITHM
//random shuffle between series of array except for the index 0, and it starts from index n-1 all the time

//static calss cannot inherit from any class or cannot be inherited by other calss.
public static class Utility
{
    public static T[] ShuffleArray<T>(T[] array, int seed)
    {
        
        //seed: is the initilization number either start of the array or end of the array`  

        //Represents a pseudo-random number generator
        //the system.random have different methods to determine the randomness   
        
        System.Random prng = new System.Random(seed);
     
        for (int i = 0; i<=array.Length - 1; i++)
        {
            //next() allows for the random number to be picked from the range
            int randomIndex = prng.Next(i, array.Length);
            
            T tempStore = array[randomIndex];
            array[randomIndex] = array[i];
            array[i] = tempStore;
        
        }
        
        return array;
    }
}
