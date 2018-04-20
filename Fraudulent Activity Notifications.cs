/*********************************************************************************************************************
 * HackerRank question: Fraudulent Activity Notifications (https://www.hackerrank.com/challenges/fraudulent-activity-notifications/problem)
 *
 * HackerLand National Bank has a simple policy for warning clients about possible fraudulent account activity. If the amount spent by a client on a 
 * particular day is greater than or equal to 2x the client's median spending for the last d days, they send the client a notification about potential fraud. 
 * The bank doesn't send the client any notifications until they have at least d prior days of transaction data.

 * Given the value of  and a client's total daily expenditures for a period of n days, find and print the number of times the client will receive a notification over all n 
 * days.

 * Note: The median of a list of numbers can be found by arranging all the numbers from smallest to greatest. If there is an odd number of numbers, the middle one is picked. 
 * If there is an even number of numbers, median is then defined to be the average of the two middle values. (Wikipedia)
 * 
 * *******************************************************************************************************************/

using System;
using System.Collections.Generic;

namespace HackerRank_FraudulentActivityNotifications
{
    class Program
    {
        const int maxValue = 201; // Maximum expenditure
        
        /// <summary>
        /// Main entry point.
        /// </summary>
        /// <param name="args">Arguments.</param>
        static void Main(string[] args)
        {
            string[] tokens_n = Console.ReadLine().Split(' ');
            int n = Convert.ToInt32(tokens_n[0]);
            int d = Convert.ToInt32(tokens_n[1]);
            string[] expenditure_temp = Console.ReadLine().Split(' ');
            int[] expenditure = Array.ConvertAll(expenditure_temp, Int32.Parse);
            int result = activityNotifications(expenditure, d);
            Console.WriteLine(result);

            Console.ReadLine();
        }

        /// <summary>
        /// Check the number of notification received.
        /// </summary>
        /// <param name="expenditure">The list of expenditures.</param>
        /// <param name="d">The number of days with spending required for notification.</param>
        /// <returns>Number of notification.</returns>
        static int activityNotifications(int[] expenditure, int d)
        {
            // Will not receive notification
            if (expenditure.Length <= d)
            {
                return 0;
            }

            int notificationCount = 0; // Number of notification
            int[] priorSpending = new int[maxValue]; // Array to hold the prior spending (d days)
            Queue<int> localqueue = new Queue<int>(); // Queue which holds the dynamic spending

            // Load all the expenditure
            for (int j = 0; j < d; j++)
            {
                priorSpending[expenditure[j]]++;
                localqueue.Enqueue(expenditure[j]);
            }

            for (int i = d; i < expenditure.Length; i++)
            {
                // Do not run this on the first interation
                if (i != d)
                {
                    // Since we are constantly changing the previous spending,
                    // Queue is used to monitor which spending we should remove,
                    // and which spending should be added

                    var value = localqueue.Dequeue();
                    priorSpending[value]--;

                    priorSpending[expenditure[i - 1]]++;
                    localqueue.Enqueue(expenditure[i - 1]);
                }

                // Check whether notification is received
                if (expenditure[i] >= 2 * getMedian(priorSpending, d))
                {
                    notificationCount++;
                }

            }

            return notificationCount;
        }

        /// <summary>
        /// Get the median value of the prior spendings.
        /// </summary>
        /// <param name="priorSpending">n-days of prior spendings.</param>
        /// <param name="d">The number of days with spending required for notification.</param>
        /// <returns>Median value.</returns>
        static double getMedian(int[] priorSpending, int d)
        {
            int sum = 0;
            int[] priorSpendingHist = new int[maxValue]; // Modified prior spending to find median

            for (int j = 0; j < maxValue; j++)
            {
                sum += priorSpending[j];
                priorSpendingHist[j] = sum;
            }

            double midPoint = 0;

            // Get the mid point
            if (d % 2 != 0)
            {
                midPoint = findMedianValue(priorSpendingHist, d / 2);
            }
            else
            {
                midPoint = ((double)(findMedianValue(priorSpendingHist, d / 2 - 1) + findMedianValue(priorSpendingHist, d / 2)) / 2);
            }

            return midPoint;
        }

        /// <summary>
        /// Find the value of the median.
        /// </summary>
        /// <param name="modifiedPriorSpending">Prior spending array that basically is a sum of the current and all previous spendings in the array, for each position.</param>
        /// <param name="medianPost">The location of the median.</param>
        /// <returns>Median value.</returns>
        static int findMedianValue(int[] modifiedPriorSpending, int medianPost)
        {
            for(int i = 0; i < modifiedPriorSpending.Length; i++)
            {
                if(modifiedPriorSpending[i] > medianPost)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
