/*********************************************************************************************************************
 * HackerRank question: Flipping the Matrix
 
 * Sean invented a game involving a 2n x 2n matrix where each cell of the matrix contains an integer. He can reverse any of its rows or columns any number of times, 
 * and the goal of the game is to maximize the sum of the elements in the n x n submatrix located in the upper-left corner of the 2n x 2n  matrix (i.e., its upper-left quadrant).
 * 
 * Given the initial configurations for q matrices, help Sean reverse the rows and columns of each matrix in the best possible way so that the 
 * sum of the elements in the matrix's upper-left quadrant is maximal. For each matrix, print the maximized sum on a new line.
 * 
 * *******************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;

namespace HackerRank_FlippingtheMatrix
{
    class Program
    {
        static void Main(String[] args)
        {
            int q = Convert.ToInt32(Console.ReadLine());

            for (int a0 = 0; a0 < q; a0++)  
            {
                int n = Convert.ToInt32(Console.ReadLine());
                int[][] matrix = new int[2 * n][];

                for (int matrix_i = 0; matrix_i < 2 * n; matrix_i++)
                {
                    string[] matrix_temp = Console.ReadLine().Split(' ');
                    matrix[matrix_i] = Array.ConvertAll(matrix_temp, Int32.Parse);
                }

                int result = flippingMatrix(matrix, n);
                Console.WriteLine(result);
            }
        }

        /// <summary>
        /// Perform reverse operations on a given matrix to find the largest sum possible
        /// from all the values at the upper left quadrant.
        /// </summary>
        /// <param name="matrix">2-D array matrix.</param>
        /// <param name="n">Size of submatrix.</param>
        /// <returns>Maximum sum.</returns>
        static int flippingMatrix(int[][] matrix, int n)
        {
            int[] arrayStorage = new int[4];
            List<int> result = new List<int>();

            // There would only be 3 other points that can be flipped
            // into a specific point in the upper-left quadrant. So for
            // each point in the upper-left quadrant, we have to compare 4
            // points
            for (int x = 0; x < n; x++)
                for (int y = 0; y < n; y++)
                {
                    arrayStorage[0] = matrix[x][y];
                    arrayStorage[1] = matrix[2 * n - 1 - x][y];
                    arrayStorage[2] = matrix[x][2 * n - 1 - y];
                    arrayStorage[3] = matrix[2 * n - 1 - x][2 * n - 1 - y];

                    result.Add(GetMax(arrayStorage));
                }

            return result.Sum();
        }

        /// <summary>
        /// Find the maximum value between 4 different values that can occupy
        /// a position in the upper left quadrant.
        /// </summary>
        /// <param name="array">Array containing a list of integers.</param>
        /// <returns>Maximum value.</returns>
        static int GetMax(int[] array)
        {
            int max = array[0];

            for (int x = 1; x < array.Length; x++)
            {
                if (array[x] > max)
                    max = array[x];
            }

            return max;
        }
    }
}
