using NuGet.Frameworks;
using RustyPoker;
using System.Reflection;

namespace RustyPokerTests
{
    [TestClass]
    public class GameplayTests
    {

        [TestMethod]
        public void TestGameplayStart_HandComposition()
        {
            var gameplay = new Gameplay(2);
            gameplay.Start();
            
            foreach(var player in gameplay.players)
            {
                Assert.AreEqual(player.Hand.Count, Gameplay.GAMEPLAY_CARDS_PER_HAND);    
            }

        }

        

    }
}