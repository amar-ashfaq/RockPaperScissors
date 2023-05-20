﻿using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RockPaperScissors;
using System;
using System.IO;

namespace RockPaperScissorsTest
{
    [TestClass]
    public class PlayerTests
    {
        private Player player;
        private StringWriter output;

        [TestInitialize]
        public void Setup()
        {
            player = new Player();
            output = new StringWriter();
            Console.SetOut(output);
        }

        [TestCleanup]
        public void Cleanup()
        {
            output.Dispose();
        }

        [TestMethod]
        public void Player_GetMove_PlayerInputsRock_RockReturned()
        {
            // Arrange
            var input = new StringReader("Rock");
            Console.SetIn(input);

            // Act
            string move = player.GetMove();

            // Assert
            move.Should().Be("rock");
        }

        [TestMethod]
        public void Player_GetMove_PlayerInputsPaper_PaperReturned()
        {
            // Arrange
            var input = new StringReader("paPer");
            Console.SetIn(input);

            // Act
            string move = player.GetMove();

            // Assert
            move.Should().Be("paper");
        }

        [TestMethod]
        public void Player_GetMove_PlayerInputsScissors_ScissorsReturned()
        {
            // Arrange
            var input = new StringReader("scIssOrs ");
            Console.SetIn(input);

            // Act
            string move = player.GetMove();

            // Assert
            move.Should().Be("scissors");
        }

        [TestMethod]
        public void Player_GetMove_PlayerInputsQ_QuitCommandReturned()
        {
            // Arrange
            var input = new StringReader("Q");
            Console.SetIn(input);

            // Act
            string move = player.GetMove();

            // Assert
            move.Should().Be("q");
        }
    }
}
