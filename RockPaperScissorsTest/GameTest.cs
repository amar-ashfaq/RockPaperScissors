using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RockPaperScissors;

namespace RockPaperScissorsTest
{
    [TestClass]
    public class GameTest
    {
        private Game game;
        private Player player;
        [TestInitialize]

        public void Setup()
        {
            game = new Game();
            player = new Player();
        }

        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            int x = 3;
            int y = 4;

            Game game = new Game();

            // Action
            int result = x + y;

            // Assert
            result.Should().Be(7);
        }

        [TestMethod]
        public void Game_Start_GameEndsWhenPlayerInputsQ()
        {
            // Assemble
            string playerMove = player.GetMove();

            // Act
            game.Start();

            // Assert
            //output.ToString().Should().Contain("Thank you for playing!");
        }
    }
}
