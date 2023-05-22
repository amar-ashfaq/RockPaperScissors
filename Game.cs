using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("RockPaperScissorsTest")]
namespace RockPaperScissors
{
    public class Game
    {
        // Dictionary to track the frequency of legal moves
        internal Dictionary<string, int> legalMoves = new Dictionary<string, int>
        {
            ["rock"] = 0,
            ["paper"] = 0,
            ["scissors"] = 0
        };

        private int turnsTakenToWin = 0;
        internal int round = 0;       
        internal int playerPoints = 0;
        internal int computerPoints = 0;
        
        private readonly string nl = Environment.NewLine;

        private readonly Player player;
        private readonly Computer computer;

        public Game()
        {
            player = new Player();
            computer = new Computer();
        }

        /// <summary>
        /// Starts the Rock, Paper, Scissors game.
        /// </summary>
        internal void Start()
        {
            Console.WriteLine("Welcome to Rock, Paper, Scissors!" + nl);

            while (true)
            {
                round++;
                Console.WriteLine("Round #" + round);
                string playerMove = player.GetMove();

                if (playerMove == "q")
                {
                    break; // end game
                }

                if (!IsValidMove(playerMove))
                {
                    Console.WriteLine("Invalid move! Please try again");
                    round--;
                    continue;
                }
                else
                {
                    Console.WriteLine("Player played " + playerMove);
                }

                string computerMove = computer.GetMove();
                RecordMoveFrequency(computerMove);
                Console.WriteLine("Computer played " + computerMove);

                string result = GetResult(playerMove, computerMove);
                Console.WriteLine("Result: " + result);

                
                Console.WriteLine($"Points: Player: {playerPoints} | Computer: {computerPoints}{nl}");
            }

            EndGameResult();
            Console.WriteLine($"{nl}Thank you for playing!{nl}");
        }

        /// <summary>
        /// Checks if a move is valid.
        /// </summary>
        /// <param name="move">The move to check.</param>
        /// <returns>True if the move is valid, false otherwise.</returns>
        internal bool IsValidMove(string move)
        {
            if (legalMoves.ContainsKey(move))
            {
                RecordMoveFrequency(move);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Records the frequency of a move.
        /// </summary>
        /// <param name="move">The move to record.</param>
        internal void RecordMoveFrequency(string move)
        {
            legalMoves[move] += 1;
        }

        /// <summary>
        /// Determines the result of a round.
        /// </summary>
        /// <param name="playerMove">The player's move.</param>
        /// <param name="computerMove">The computer's move.</param>
        /// <returns>The result of the round.</returns>
        internal string GetResult(string playerMove, string computerMove)
        {
            turnsTakenToWin++;

            if (playerMove == computerMove)          
                return "It's a tie. No points awarded";
            
            bool playerWins = (playerMove == "rock" && computerMove == "scissors") ||
                              (playerMove == "paper" && computerMove == "rock") ||
                              (playerMove == "scissors" && computerMove == "paper");

            UpdatePointsAndPrintTurnsTaken(playerWins);

            return playerWins ? "You win this round!" : "Computer wins this round!";
        }

        /// <summary>
        /// Updates the points and prints the number of turns taken to win.
        /// </summary>
        /// <param name="playerWins">Indicates if the player wins the round.</param>
        private void UpdatePointsAndPrintTurnsTaken(bool playerWins)
        {
            if (playerWins)
            {
                playerPoints++;
            }
            else
            {
                computerPoints++;
            }

            PrintTurnsTakenToWin();
        }

        /// <summary>
        /// Prints the number of turns taken to win and resets the counter.
        /// </summary>
        private void PrintTurnsTakenToWin()
        {
            Console.WriteLine($"Turns taken to win: {turnsTakenToWin}");
            turnsTakenToWin = 0;
        }

        /// <summary>
        /// Determines the overall winner of the game.
        /// </summary>
        /// <returns>The overall winner of the game.</returns>
        internal string DetermineWinner()
        {
            if (round == 1)
                return "No rounds were played";

            if (playerPoints > computerPoints)
                return "Player";
            else if (playerPoints < computerPoints)
                return "Computer";

            return "No winner. It's a tie!";
        }

        /// <summary>
        /// Determines the most frequent move played across all rounds.
        /// </summary>
        /// <returns>The most frequent move played.</returns>
        private string DetermineMostFrequentMove()
        {
            if (round == 1)
                return "No moves were played";

            string mostFrequentMove = legalMoves.OrderByDescending(m => m.Value).First().Key;
            return mostFrequentMove;
        }

        /// <summary>
        /// Prints the end game results.
        /// </summary>
        internal void EndGameResult()
        {
            string endResult = $"-----END RESULTS-----{nl}" +
                               $"Total rounds played: {round - 1}{nl}" +
                               $"Overall Winner: {DetermineWinner()}{nl}" +
                               $"Most frequent move played across all rounds: {DetermineMostFrequentMove()}";

            Console.WriteLine(endResult);
        }
    }
}
