﻿using Autofac.Extras.Moq;
using Monopoly;
using Monopoly.Tasks;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace MonopolyUnitTests
{
    [TestFixture]
    class BoardUnitTests
    {
        private Board board;
        
        private Mock<Player> mockPlayer;
        private Mock<Jailer> mockJailer;
        private Mock<Realtor> mockRealtor;
        private Mock<Banker> mockBanker;
        private Mock<Board> mockBoard;

        [SetUp]
        public void Init()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            mockJailer  = fixture.Create<Mock<Jailer>>();
            mockRealtor = fixture.Create<Mock<Realtor>>();
            mockBanker  = fixture.Create<Mock<Banker>>();
            mockBoard   = fixture.Create<Mock<Board>>();
            mockPlayer  = fixture.Create<Mock<Player>>();
            
            board = new Board(mockRealtor.Object, mockJailer.Object, mockBanker.Object);
        }

        [Test]
        public void DoTurn_CallsDoJailTurnWhenPlayerIsImprisoned()
        {
            int distance = 5;
            bool rolledDoubles = false;

            mockBoard.Object.SendPlayerToJail(mockPlayer.Object);
            mockBoard.Object.DoTurn(mockPlayer.Object, distance, rolledDoubles);
            mockBoard.Verify(x => x.DoJailTurn(It.IsAny<IPlayer>(), distance, rolledDoubles));
        }
 
        [Test]

        public void DrawsChanceCard_AdvanceToNearestUtility_MovesPlayerToElectricCompany([Values(22, 37)] int startingSpaceNumber)
        {
            int ExpectedUtilitySpaceNumber = 28;


            mockBoard.Setup(x => x.DrawChance())
                .Returns(new Card("Move To Closest Utility", new MoveToNearestPropertyGroupTask(board, PropertyGroup.Utility)));

            board.MovePlayerDirectlyToSpace(mockPlayer.Object, startingSpaceNumber);

            Assert.AreEqual(ExpectedUtilitySpaceNumber, mockPlayer.Object.PlayerLocation.SpaceNumber);
        }
    }
}