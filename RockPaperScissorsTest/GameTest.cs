using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RockPaperScissors;
using System;
using System.Collections.Generic;
using System.IO;

namespace RockPaperScissorsTest
{
    [TestClass]
    public class GameTest
    {
        private Game game;
        private StringWriter output;

        [TestInitialize]
        public void Setup()
        {
            game = new Game();
            output = new StringWriter();
            Console.SetOut(output);
        }

        [TestMethod]
        public void Game_Start_GameFlow_WorksCorrectly()
        {
            // Assemble
            var input = new StringReader("rock" + Environment.NewLine + "paper" + Environment.NewLine + "q");
            Console.SetIn(input);

            // Action
            game.Start();
            string consoleOutput = output.ToString();

            // Assert
            consoleOutput.Should().Contain("Welcome to Rock, Paper, Scissors!");
            consoleOutput.Should().Contain("Round #1");
            consoleOutput.Should().Contain("Player played rock");
            consoleOutput.Should().Contain("Computer played");
            consoleOutput.Should().Contain("Result:");
            consoleOutput.Should().Contain("Points: Player:");
            consoleOutput.Should().Contain("Thank you for playing!");
        }

        [TestMethod]
        public void Game_Start_InvalidMove_ContinuesToNextRound()
        {
            // Assemble
            var input = new StringReader("invalid" + Environment.NewLine + "rock" + Environment.NewLine + "q");
            Console.SetIn(input);

            // Action
            game.Start();
            string consoleOutput = output.ToString();

            // Assert
            consoleOutput.Should().Contain("Invalid move! Please try again");
            consoleOutput.Should().Contain("Round #1");
            consoleOutput.Should().Contain("Round #2");
            consoleOutput.Should().Contain("Player played rock");
            consoleOutput.Should().Contain("Computer played");
            consoleOutput.Should().Contain("Thank you for playing!");
        }

        [TestMethod]
        public void Game_IsValidMove_ValidMove_ReturnsTrue()
        {
            // Assemble
            string validMove = "rock";

            // Action
            bool isValid = game.IsValidMove(validMove);

            // Assert
            isValid.Should().BeTrue();
        }

        [TestMethod]
        public void Game_IsValidMove_InvalidMove_ReturnsFalse()
        {
            // Assemble
            string invalidMove = "invalid";

            // Action
            bool isValid = game.IsValidMove(invalidMove);

            // Assert
            isValid.Should().BeFalse();
        }

        [TestMethod]
        public void Game_RecordMoveFrequency_MovesRecordedCorrectly()
        {
            // Assemble
            string move = "rock";

            // Action
            game.RecordMoveFrequency(move);
            game.RecordMoveFrequency(move);
            game.RecordMoveFrequency(move);

            // Assert
            Dictionary<string, int> expectedMoves = new Dictionary<string, int>
            {
                ["rock"] = 3,
                ["paper"] = 0,
                ["scissors"] = 0
            };

            game.legalMoves.Should().BeEquivalentTo(expectedMoves);
        }

        [TestMethod]
        public void Game_GetResult_Tie_ReturnsTieMessage()
        {
            // Assemble
            string playerMove = "rock";
            string computerMove = "rock";

            // Action
            string result = game.GetResult(playerMove, computerMove);

            // Assert
            result.Should().Be("It's a tie. No points awarded");
        }

        [TestMethod]
        public void Game_GetResult_PlayerWins_ReturnsPlayerWinMessage()
        {
            // Assemble
            string playerMove = "rock";
            string computerMove = "scissors";

            // Action
            string result = game.GetResult(playerMove, computerMove);

            // Assert
            result.Should().Be("You win this round!");
        }

        [TestMethod]
        public void Game_GetResult_ComputerWins_ReturnsComputerWinMessage()
        {
            // Assemble
            string playerMove = "rock";
            string computerMove = "paper";

            // Action
            string result = game.GetResult(playerMove, computerMove);

            // Assert
            result.Should().Be("Computer wins this round!");
        }
        
        [TestMethod]
        public void Game_DetermineWinner_PlayerWins_ReturnsPlayer()
        {
            // Assemble
            game.playerPoints = 3;
            game.computerPoints = 1;

            string expectedWinner = "Player";

            // Action
            string winner = game.DetermineWinner();

            // Assert
            winner.Should().Be(expectedWinner);
        }

        [TestMethod]
        public void Game_DetermineWinner_ComputerWins_ReturnsComputer()
        {
            // Assemble
            game.playerPoints = 2;
            game.computerPoints = 4;
            string expectedWinner = "Computer";

            // Action
            string winner = game.DetermineWinner();

            // Assert
            winner.Should().Be(expectedWinner);
        }

        [TestMethod]
        public void Game_DetermineWinner_Tie_ReturnsTie()
        {
            // Assemble
            game.playerPoints = 2;
            game.computerPoints = 2;
            string expectedWinner = "No winner. It's a tie!";

            // Action
            string winner = game.DetermineWinner();

            // Assert
            winner.Should().Be(expectedWinner);
        }

        [TestMethod]
        public void Game_EndGameResult_GameResults_PrintsCorrectEndResults()
        {
            // Assemble
            game.playerPoints = 2;
            game.computerPoints = 3;
            game.round = 4;

            game.legalMoves = new Dictionary<string, int>
            {
                ["rock"] = 2,
                ["paper"] = 1,
                ["scissors"] = 1
            };

            string expectedOutput = "-----END RESULTS-----" + Environment.NewLine +
                                    "Total rounds played: 3" + Environment.NewLine +
                                    "Overall Winner: Computer" + Environment.NewLine +
                                    "Most frequent move played across all rounds: rock" + Environment.NewLine;

            // Action
            game.EndGameResult();
            string consoleOutput = output.ToString();

            // Assert
            consoleOutput.Should().Be(expectedOutput);
        }
    }
}
