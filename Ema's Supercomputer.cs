/*********************************************************************************************************************
 * HackerRank question: Ema's Supercomputer
 * 
 * Given a grid of size N x M, each cell in the grid is either good or bad.
 * 
 * A valid plus is defined here as the crossing of two segments (horizontal and vertical) of equal lengths. These lengths 
 * must be odd, and the middle cell of its horizontal segment must cross the middle cell of its vertical segment.
 * 
 * Find the 2 valid pluses that can be drawn on good cells in the grid, and print an integer denoting the maximum product of their areas.
 * 
 * Note: The two pluses cannot overlap, and the product of their areas should be maximal.
 * 
 * *******************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQTesting
{
    public class Program
    {
        /// <summary>
        /// Get all the possible pluses and perform the operation to get the maximum product
        /// of 2 non-overlapping pluses.
        /// </summary>
        /// <param name="grid">The grid in array format.</param>
        /// <param name="height">Height of the grid.</param>
        /// <param name="width">Width of the grid.</param>
        /// <returns></returns>
        static int twoPluses(string[] grid, int height, int width)
        {
            int gridCounter = 0;
            // List of possible pluses
            List<List<int>> plusList = new List<List<int>>();

            // Row loop
            for (int i = 0; i < height; i++)
            {
                // Define the boundaries for the current row
                int leftBoundary = i * width;
                int rightBoundary = leftBoundary + width - 1;

                // Column loop
                for (int j = 0; j < width; j++)
                {
                    //Plus that can be formed on the current coordinate
                    List<int> currentPlus = new List<int>();

                    if (!grid[gridCounter].Equals("B"))
                    {
                        int leftRight = 1;
                        int topBottom = width;

                        while (true)
                        {
                            // Check left
                            int left = gridCounter - leftRight;
                            if (!(left >= leftBoundary && grid[left].Equals("G")))
                                break;

                            // Check right
                            int right = gridCounter + leftRight;
                            if (!(right <= rightBoundary && grid[right].Equals("G")))
                                break;

                            // Check bottom
                            int bottom = gridCounter + topBottom;
                            if (!(bottom <= grid.Length - 1 && grid[bottom].Equals("G")))
                                break;

                            // Check top
                            int top = gridCounter - topBottom;
                            if (!(top >= 0 && grid[top].Equals("G")))
                                break;

                            if(!currentPlus.Contains(gridCounter))
                                currentPlus.Add(gridCounter);

                            currentPlus.Add(left);
                            currentPlus.Add(right);
                            currentPlus.Add(top);
                            currentPlus.Add(bottom);

                            plusList.Add(new List<int>(currentPlus));

                            leftRight += 1;
                            topBottom += width;
                        }
                    }

                    if (!(gridCounter + 1 > rightBoundary))
                        gridCounter++;
                }

                gridCounter++;
            }

            return getNonIntersectPluses(plusList);
        }

        /// <summary>
        /// Find the maximum product of 2 non-overlapping pluses.
        /// </summary>
        /// <param name="plusList">A list of possible pluses.</param>
        /// <returns>Maximum product.</returns>
        static int getNonIntersectPluses(List<List<int>> plusList)
        {
            int plusOne = 1;
            int plusTwo = 1;
            List<int> scoreList = new List<int>();

            var sortedList = plusList.OrderByDescending(x => x.Count).ToList();

            for(int i = 0; i < sortedList.Count - 1; i++)
            {
                for(int j = i+1; j < sortedList.Count; j++)
                {
                    // Check if there is overlapping
                    var intersection = sortedList[i].Intersect(sortedList[j]).ToList();

                    if (intersection.Count == 0)
                    {
                        plusOne = sortedList[i].Count();
                        plusTwo = sortedList[j].Count();

                        scoreList.Add(plusOne * plusTwo);
                    }
                }
            }

            // There are no non-overlapping pluses
            if(scoreList.Count < 1)
            {
                // Get the largest plus as plus 1, and 1 for plus 2
                // I am assuming we could always pluses of value 1
                // as the worst case scenario
                if (sortedList.Count > 0)
                {
                    plusOne = sortedList[0].Count();
                    plusTwo = 1;
                }

                return plusOne * plusTwo;
            }
            else
            {
                return scoreList.OrderByDescending(x => x).ToList()[0];
            }
        }

        static void Main(String[] args)
        {
            string[] tokens_n = Console.ReadLine().Split(' ');
            int n = Convert.ToInt32(tokens_n[0]);
            int m = Convert.ToInt32(tokens_n[1]);

            string[] grid = new string[n * m];

            int j = 0;

            for (int grid_i = 0; grid_i < n; grid_i++)
            {
                foreach(char a in Console.ReadLine())
                {
                    grid[j] = a.ToString();
                    j++;
                }
            }

            int result = twoPluses(grid, n, m);
            Console.WriteLine(result);
        }
    }
}
