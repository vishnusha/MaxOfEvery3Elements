using System;
using System.Collections.Generic;
using System.Linq;

namespace MaxOfEvery3Elements
{
    
    class Program
    {
        static void Main()
        {
            // Step 1: Get the array from user input
            Console.WriteLine("Enter array elements separated by commas (e.g., 2,3,4,7,8,0,9,5):");
            string input = Console.ReadLine();
            int[] arr = input.Split(',').Where(s => !string.IsNullOrWhiteSpace(s)).Select(int.Parse).ToArray();


            // Step 2: Get window size
            Console.WriteLine("Enter window size (e.g., 3):");
            int windowSize = int.Parse(Console.ReadLine());

            // Step 3: Get mode
            Console.WriteLine("Enter mode ('naive' or 'optimized'):");
            string mode = Console.ReadLine().Trim().ToLower();

            // Step 4: Call the function
            int[] result = GetSlidingWindowMax(arr, windowSize, mode);

            // Output the result
            Console.WriteLine("Result: " + string.Join(",", result));
        }

        // Wrapper function
        static int[] GetSlidingWindowMax(int[] arr, int windowSize, string mode)
        {
            if (mode == "optimized")
            {
                return SlidingWindowMaxOptimized(arr, windowSize);
            }
            else
            {
                return SlidingWindowMaxNaive(arr, windowSize);
            }
        }

        // Naive method
        static int[] SlidingWindowMaxNaive(int[] arr, int windowSize)
        {
            if (arr.Length < windowSize)
                return new int[0];

            int[] result = new int[arr.Length - windowSize + 1];

            for (int i = 0; i <= arr.Length - windowSize; i++)
            {
                int max = arr[i];
                for (int j = 1; j < windowSize; j++)
                {
                    if (arr[i + j] > max)
                    {
                        max = arr[i + j];
                    }
                }
                result[i] = max;
            }

            return result;
        }

        // Optimized method using deque
        static int[] SlidingWindowMaxOptimized(int[] arr, int k)
        {
            if (arr == null || arr.Length == 0 || k <= 0)
                return new int[0];

            List<int> result = new List<int>();
            LinkedList<int> deque = new LinkedList<int>(); // stores indices

            for (int i = 0; i < arr.Length; i++)
            {
                // Remove indices out of the current window
                while (deque.Count > 0 && deque.First.Value < i - k + 1)
                    deque.RemoveFirst();

                // Remove smaller values from the back
                while (deque.Count > 0 && arr[deque.Last.Value] < arr[i])
                    deque.RemoveLast();

                deque.AddLast(i);

                if (i >= k - 1)
                    result.Add(arr[deque.First.Value]);
            }

            return result.ToArray();
        }
    }

}
