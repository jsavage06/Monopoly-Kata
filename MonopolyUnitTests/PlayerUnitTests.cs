﻿using NUnit.Framework;
using Monopoly;

namespace MonopolyUnitTests
{

    [TestFixture]
    public class PlayerUnitTests
    {
        private IPlayer player;
        private ILocation startingLocation;

        [SetUp]
        public void Init()
        {

            player = new Player(startingLocation);
        }

        [Test]
        public void PlayerStartsOutWithNoGetOutOfJailCards_HasGetOutOfJailCardReturnsFalse()
        {
            Assert.False(player.HasGetOutOfJailCard());
        }

        [Test]
        public void AddingAGetOutOfJailCardCorrectlyAdjustsCardBalance()
        {
            player.AddGetOutOfJailCard();

            Assert.True(player.HasGetOutOfJailCard());
        }

        [Test]
        public void UsingAGetOutOfJailCard_DecrementsCardBalance()
        {
            player.AddGetOutOfJailCard();

            player.UseGetOutOfJailCard();

            Assert.False(player.HasGetOutOfJailCard());
        }
    }
}
