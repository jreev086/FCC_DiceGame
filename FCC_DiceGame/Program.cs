using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FCC_DiceGame
{
    internal class Program
    {
        private static Random rand = new Random();

        static void Main(string[] args)
        {
            var diceCount = 6;
            var simulationCount = 10000;

            RunSimulation(diceCount, simulationCount);
        }

        /// <summary>
        /// Method to run the simulation
        /// </summary>
        /// <param name="diceCount">Total Dice Count</param>
        /// <param name="simulationCount">Total simulation count</param>
        private static void RunSimulation(int diceCount, int simulationCount)
        {
            var totalList = new Dictionary<int, int>();

            var timer = Stopwatch.StartNew();

            for (int i = 0; i < simulationCount; i++)
            {
                var total = PlayGame(diceCount);

                totalList[total] = totalList.ContainsKey(total) ? totalList[total] + 1 : 1;
            }
            timer.Stop();

            Console.WriteLine($"Number of simulations was {simulationCount} using {diceCount} dice.");
            foreach (var (key, value) in totalList)
            {
                Console.WriteLine($"Total {key} occurs {value / (double)simulationCount:P2} occurred {value} times.");
            }
            Console.WriteLine($"Total simulation took {timer.Elapsed.TotalSeconds:F2} seconds.");
        }

        /// <summary>
        /// Method to play 1 simulated game
        /// </summary>
        /// <param name="numOfDice">Number of dice to use</param>
        /// <returns>The total after playing the game</returns>
        private static int PlayGame(int numOfDice)
        {
            int total = 0;
            int remainingDice = numOfDice;

            while (remainingDice > 0)
            {           
                var diceValues = RollDice(remainingDice);
                int minValue = 0;

                if(diceValues.Any())
                {
                    minValue = diceValues.Min();
                    diceValues.Remove(minValue);
                    remainingDice = diceValues.Count;
                    total += minValue;
                }            
                else
                {
                    break;
                }
            }

            return total;
        }

        /// <summary>
        /// Method to roll all the dice and to return the list of values, removing 3
        /// </summary>
        /// <param name="diceToRoll">Number of dice left to roll</param>
        /// <returns>List containing dice values</returns>
        private static List<int> RollDice(int diceToRoll) 
        {
            var listOfDice = new List<int>();

            for (int i = 0; i < diceToRoll; i++)
            {
                var diceVal = rand.Next(1, 6);

                if (diceVal != 3)
                {
                    listOfDice.Add(diceVal);
                }
            }

            return listOfDice;
        }
    }
}