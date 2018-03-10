/*********************************************************************************************************************
 * HackerRank question: Almost Sorted
 * Given an array with  elements, can you sort this array in ascending order using only one of the following operations?
 * 
 * 1. Swap two elements.
 * 2. Reverse one sub-segment.
 * 
 * *******************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;

namespace HackerRank_AlmostSorted
{
    class Program
    {
        static void Main(string[] args)
        {
            int arraySize = int.Parse(Console.ReadLine());
            int[] testSample = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

            if (arraySize == 2)
            // Check if the items are already sorted, if not, sort them
            { 
                if (testSample[1] > testSample[0])
                {
                    Console.WriteLine("yes");
                }
                else
                {
                    Console.WriteLine("yes");
                    Console.WriteLine("swap 1 2");
                }
            }
            else
            {
                // Get the number of items that is out-of-sort

                List<int> dipList = new List<int>();

                for (int item = 1; item < arraySize; item++)
                {
                    if (testSample[item] < testSample[item - 1])
                    {
                        dipList.Add(item - 1);
                    }
                }

                if (dipList.Count == 0)
                // Array is already sorted
                {
                    Console.WriteLine("yes");
                }
                else if (dipList.Count == 1)
                // Since there is only one location that is out-of-sort
                // Swap the incorrect item with its adjacent item to check
                // if the array is sortable
                {
                    int temp = testSample[dipList[0]];
                    testSample[dipList[0]] = testSample[dipList[0] + 1];
                    testSample[dipList[0] + 1] = temp;

                    if(checkSort(arraySize, testSample))
                    {
                        Console.WriteLine("yes");
                        Console.WriteLine($"swap {dipList[0] + 1} {dipList[0] + 2}");
                    }
                    else
                    {
                        Console.WriteLine("no");
                    }
                }
                else if (dipList.Count == 2)
                // There are two locations which are out-of-sort.
                // However, since we can only make a single swap,
                // we need to check whether the array is sortable
                // when the two items which their locations are incorrect
                // are swapped
                {
                    int temp = testSample[dipList[0]];
                    testSample[dipList[0]] = testSample[dipList[1] + 1];
                    testSample[dipList[1] + 1] = temp;

                    if (checkSort(arraySize, testSample))
                    {
                        Console.WriteLine("yes");
                        Console.WriteLine($"swap {dipList[0] + 1} {dipList[1] + 2}");
                    }
                    else
                    {
                        Console.WriteLine("no");
                    }
                }
                else
                {
                    // If the number of locations where the items in the array are out-of-sort 
                    // is more than 2, we could only consider applying reverse method. To implement
                    // reverse method, the out-of-sort location must be sequential

                    if(IsSequential(dipList))
                    {
                        // Make a clone of the array
                        int[] reference = (int[])testSample.Clone();

                        int offset = dipList.Count;
                        int initialpost = dipList[0];

                        // Reverse the items in the area which contains out-of-sort
                        // items
                        for(int y = 0; y <= dipList.Count; y ++)
                        {
                            if (y == dipList.Count)
                            {
                                testSample[initialpost + offset] = reference[dipList[y - 1] + 1];
                            }
                            else
                            {
                                testSample[initialpost + offset] = reference[dipList[y]];
                                offset--;
                            }
                        }

                        if(checkSort(arraySize, testSample))
                        {
                            Console.WriteLine("yes");
                            Console.WriteLine($"reverse {dipList[0] + 1} {dipList[dipList.Count - 1] + 2}");
                        }
                        else
                        {
                            Console.WriteLine("no");
                        }
                    }
                    else
                    {
                        Console.WriteLine("no");
                    }
                }
            }

            Console.ReadLine();
        }

        /// <summary>
        /// Check whether a list is sequential or not.
        /// </summary>
        /// <param name="a">List of integers.</param>
        /// <returns>true: sequential, false: otherwise.</returns>
        public static bool IsSequential(List<int> a)
        {
            return Enumerable.Range(1, a.Count - 1).All(i => a[i] - 1 == a[i - 1]);
        }

        /// <summary>
        /// Check whether an array is sorted.
        /// </summary>
        /// <param name="count">Number of items in an array.</param>
        /// <param name="arraySample">Array sample.</param>
        /// <returns>true: sorted, false: otherwise.</returns>
        static bool checkSort(int count, int[] arraySample)
        {
            bool isSorted = true;

            for (int item = 1; item < count; item++)
            {
                if (arraySample[item] < arraySample[item - 1])
                {
                    isSorted = false;
                    break;
                }
            }

            return isSorted;
        }
    }
}
