/* Question could be found at https://practice.geeksforgeeks.org/problems/champagne-overflow/0 */

using System;

namespace WaterOverflow
{
    class Program
    {
        const int MAX_AMT = 1000;
        const int GLASS_CAPACITY = 1;

        /// <summary>
        /// Main entry point.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int numTestCase = int.Parse(Console.ReadLine());

            for(int j = 0; j < numTestCase; j++)
            {
                int[] testParams = new int[3];

                testParams[0] = int.Parse(Console.ReadLine()); // Amount of water poured
                testParams[1] = int.Parse(Console.ReadLine()); // Glass row
                testParams[2] = int.Parse(Console.ReadLine()); // Glass index for a given row

                Console.WriteLine(FindTheWaterContent(GLASS_CAPACITY, testParams[0], testParams[1], testParams[2]).ToString("F6"));
            }

            Console.ReadLine();
        }

        /// <summary>
        /// Find the amount of water in a glass on a specific row.
        /// </summary>
        /// <param name="C">Capacity for each glass.</param>
        /// <param name="N">Amount of water to be poured.</param>
        /// <param name="row">Row number of a specific glass. (Assume first row is 1)</param>
        /// <param name="glass_idx">Glass index of a specific row. (Assume first index starts from 1)</param>
        /// <returns>Amount of water in a specific glass.</returns>
        static double FindTheWaterContent(double C, double N, int row, int glass_idx)
        {
            double[,] glasses = new double[MAX_AMT, MAX_AMT];
            bool isWaterRemain = true;
            double nGlassAmt = 0;

            // Set the amount of water at the top-most glass
            glasses[0, 0] = N;

            // Keep track of the level of glasses, starting from
            // the first level
            int levelCount = 0;

            while (isWaterRemain)
            {
                isWaterRemain = false;

                // For each glass at that level
                for (int x = 0; x <= levelCount; x++)
                {
                    // If a glass has more water than it can store,
                    // pour them to the glasses below
                    if (glasses[levelCount, x] > C)
                    {
                        // Get the extra amount of water after filling the current glass
                        double extraWater = glasses[levelCount, x] - C;
                        // The current glass should be at the full capacity
                        glasses[levelCount, x] = C; 

                        // Fill up the 2 glasses below the current glass
                        glasses[levelCount + 1, x] += extraWater/2;
                        glasses[levelCount + 1, x + 1] += extraWater/2;

                        isWaterRemain = true; // Continue the loop as there is still water
                    }
                }

                levelCount++;

                // Check if we have arrived on the specific glass
                if ((levelCount - 1) == (row - 1))
                {
                    nGlassAmt = glasses[levelCount - 1, glass_idx - 1];
                    break;
                }
            }

            return Math.Round(nGlassAmt, 6);
        }
    }
}
