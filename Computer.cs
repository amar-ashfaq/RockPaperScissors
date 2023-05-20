using System;

namespace RockPaperScissors
{
    public class Computer
    {
        private static readonly Random random = new Random();

        /// <summary>
        /// Generates a random move for the computer.
        /// </summary>
        /// <returns>The computer's randomly generated move as a string.</returns>
        public string GetMove()
        {
            string[] moves = { "rock", "paper", "scissors" };
            int index = random.Next(moves.Length);
            return moves[index];
        }
    }
}
