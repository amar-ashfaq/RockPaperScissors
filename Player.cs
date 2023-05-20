using System;

namespace RockPaperScissors
{
    public class Player
    {
        /// <summary>
        /// Prompts the player to enter a move and returns the player's move.
        /// </summary>
        /// <returns>The player's move as a string.</returns>
        public string GetMove()
        {
            Console.WriteLine("Enter your move (Rock, Paper, or Scissors) or 'Q' to quit:");
            string playerMove = Console.ReadLine().Trim().ToLower();
            Console.WriteLine();

            return playerMove;
        }
    }
}
