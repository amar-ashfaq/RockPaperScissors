using System;
using System.Collections.Generic;
using System.Linq;

namespace RockPaperScissors
{
    public class Game
    {
        private static readonly Random random = new Random();
        private readonly Dictionary<string, int> legalMoves = new Dictionary<string, int>
        {
            ["rock"] = 0,
            ["paper"] = 0,
            ["scissors"] = 0
        };

        private int round = 0;
        private int playerPoints = 0;
        private int computerPoints = 0;
        private int turnsTakenToWin = 0;
        private readonly string nl = Environment.NewLine;

        public void Start()
        {
            Console.WriteLine("Welcome to Rock, Paper, Scissors!" + nl);

            while (true)
            {
                round++;
                Console.WriteLine("Round #" + round);
                Console.WriteLine("Enter your move (Rock, Paper, or Scissors) or 'Q' to quit:");
                string playerMove = Console.ReadLine().Trim().ToLower();
                Console.WriteLine();

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

                string computerMove = ComputersMove();
                Console.WriteLine("Computer played " + computerMove);

                string result = GetResult(playerMove, computerMove);
                Console.WriteLine("Result: " + result);

                
                Console.WriteLine($"Points: Player: {playerPoints} | Computer: {computerPoints}{nl}");
            }

            EndGameResult();
            Console.WriteLine($"Thank you for playing!{nl}");
        }

        private bool IsValidMove(string move)
        {
            if (legalMoves.ContainsKey(move))
            {
                RecordMoveFrequency(move);
                return true;
            }

            return false;
        }

        private void RecordMoveFrequency(string move)
        {
            legalMoves[move] += 1;
        }

        private string ComputersMove()
        {
            string[] moves = { "rock", "paper", "scissors" };
            int index = random.Next(moves.Length);
            RecordMoveFrequency(moves[index]);
            return moves[index];
        }

        private string GetResult(string playerMove, string computerMove)
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

        private void PrintTurnsTakenToWin()
        {
            Console.WriteLine($"Turns taken to win: {turnsTakenToWin}");
            turnsTakenToWin = 0;
        }

        private string DetermineWinner()
        {
            if (round == 1)
                return "No rounds were played";

            if (playerPoints > computerPoints)
                return "Player";
            else if (playerPoints < computerPoints)
                return "Computer";

            return "No winner. It's a tie!";
        }

        private string DetermineMostFrequentMove()
        {
            if (round == 1)
                return "-";

            string mostFrequentMove = legalMoves.OrderByDescending(m => m.Value).First().Key;
            return mostFrequentMove;
        }

        private void EndGameResult()
        {
            string endResult = $"-----END RESULTS-----{nl}" +
                               $"Total rounds played: {round - 1}{nl}" +
                               $"Overall Winner: {DetermineWinner()}{nl}" +
                               $"Most frequent move played across all rounds: {DetermineMostFrequentMove()}";

            Console.WriteLine(endResult);
        }
    }
}
