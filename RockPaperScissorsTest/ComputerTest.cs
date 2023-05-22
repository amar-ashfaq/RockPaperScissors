using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RockPaperScissors;

namespace RockPaperScissorsTest
{
    /// <summary>
    /// Summary description for ComputerTest
    /// </summary>
    [TestClass]
    public class ComputerTest
    {
        private Computer computer;

        [TestInitialize]
        public void Setup()
        {
            computer = new Computer();
        }

        [TestMethod]
        public void Computer_GetMove_ReturnsValidMove()
        {
            // Action
            string move = computer.GetMove();

            // Assert
            move.Should().NotBeNullOrEmpty();
            move.Should().MatchRegex("rock|paper|scissors");
        }

        [TestMethod]
        public void Computer_GetMove_ReturnsRandomMoves()
        {
            // Assemble
            string[] moves = { "rock", "paper", "scissors" };
            int rockCount = 0, paperCount = 0, scissorsCount = 0;
            const int iterations = 10000;

            // Action
            for (int i = 0; i < iterations; i++)
            {
                string move = computer.GetMove();
                if (move == "rock")
                    rockCount++;
                else if (move == "paper")
                    paperCount++;
                else if (move == "scissors")
                    scissorsCount++;
            }

            // Assert
            rockCount.Should().BeGreaterThan(0);
            paperCount.Should().BeGreaterThan(0);
            scissorsCount.Should().BeGreaterThan(0);
            rockCount.Should().BeLessThan(iterations);
            paperCount.Should().BeLessThan(iterations);
            scissorsCount.Should().BeLessThan(iterations);
        }
    }
}
