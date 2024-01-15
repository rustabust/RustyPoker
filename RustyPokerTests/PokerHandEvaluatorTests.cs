using RustyPoker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RustyPokerTests
{
    [TestClass]
    public class SimplePokerHandEvaluatorTests
    {
        [TestMethod]
        public void TestHandEvaluator_Straight_NoAroundTheWorld()
        {
            var cards = new List<Card>
            {
                new Card(Suits.Spades, Numbers.Ace),
                new Card(Suits.Spades, Numbers.Five),
                new Card(Suits.Spades, Numbers.Queen),
                new Card(Suits.Spades, Numbers.King),
                new Card(Suits.Spades, Numbers.Two),
                new Card(Suits.Spades, Numbers.Queen),
                new Card(Suits.Spades, Numbers.Three)
            };

            bool isStraight = SimplePokerHandEvaluator.IsStraight(cards);

            Assert.IsFalse(isStraight);
        }

        [TestMethod]
        public void TestHandEvaluator_Straight_LowFromTwo()
        {
            var cards = new List<Card>
            {
                new Card(Suits.Spades, Numbers.Six),
                new Card(Suits.Spades, Numbers.Five),
                new Card(Suits.Spades, Numbers.Queen),
                new Card(Suits.Spades, Numbers.Four),
                new Card(Suits.Spades, Numbers.Two),
                new Card(Suits.Spades, Numbers.Queen),
                new Card(Suits.Spades, Numbers.Three)
            };

            bool isStraight = SimplePokerHandEvaluator.IsStraight(cards);

            Assert.IsTrue(isStraight);
        }

        [TestMethod]
        public void TestHandEvaluator_Straight_LowFromAce()
        {
            var cards = new List<Card>
            {
                new Card(Suits.Spades, Numbers.Ace),
                new Card(Suits.Spades, Numbers.Five),
                new Card(Suits.Spades, Numbers.Queen),
                new Card(Suits.Spades, Numbers.Four),
                new Card(Suits.Spades, Numbers.Two),
                new Card(Suits.Spades, Numbers.Queen),
                new Card(Suits.Spades, Numbers.Three)
            };

            bool isStraight = SimplePokerHandEvaluator.IsStraight(cards);

            Assert.IsTrue(isStraight);
        }

        [TestMethod]
        public void TestHandEvaluator_Straight_HighToAce()
        {
            var cards = new List<Card>
            {
                new Card(Suits.Spades, Numbers.Ace),
                new Card(Suits.Spades, Numbers.Five),
                new Card(Suits.Spades, Numbers.Queen),
                new Card(Suits.Spades, Numbers.Four),
                new Card(Suits.Spades, Numbers.Two),
                new Card(Suits.Spades, Numbers.Queen),
                new Card(Suits.Spades, Numbers.Three)
            };

            bool isStraight = SimplePokerHandEvaluator.IsStraight(cards);

            Assert.IsTrue(isStraight);
        }

        [TestMethod]
        public void TestHandEvaluator_Straight_ToNine()
        {
            var cards = new List<Card>
            {
                new Card(Suits.Spades, Numbers.Two),
                new Card(Suits.Spades, Numbers.Five),
                new Card(Suits.Spades, Numbers.Nine),
                new Card(Suits.Spades, Numbers.Seven),
                new Card(Suits.Spades, Numbers.Six),
                new Card(Suits.Spades, Numbers.Queen),
                new Card(Suits.Spades, Numbers.Eight)
            };

            bool isStraight = SimplePokerHandEvaluator.IsStraight(cards);

            Assert.IsTrue(isStraight);
        }

        [TestMethod]
        public void TestHandEvaluator_Straight_ToKing()
        {

            var cards = new List<Card>
            {
                new Card(Suits.Spades, Numbers.Two),
                new Card(Suits.Spades, Numbers.Five),
                new Card(Suits.Spades, Numbers.Nine),
                new Card(Suits.Spades, Numbers.Ten),
                new Card(Suits.Spades, Numbers.Jack),
                new Card(Suits.Spades, Numbers.Queen),
                new Card(Suits.Spades, Numbers.King)
            };

            bool isStraight = SimplePokerHandEvaluator.IsStraight(cards);

            Assert.IsTrue(isStraight);
        }

        [TestMethod]
        public void TestHandEvaluator_Straight_NotAStraight()
        {
            var cards = new List<Card>
            {
                new Card(Suits.Spades, Numbers.Two),
                new Card(Suits.Spades, Numbers.Five),
                new Card(Suits.Spades, Numbers.Nine),
                new Card(Suits.Spades, Numbers.Three),
                new Card(Suits.Spades, Numbers.Jack),
                new Card(Suits.Spades, Numbers.Queen),
                new Card(Suits.Spades, Numbers.King)
            };

            bool isStraight = SimplePokerHandEvaluator.IsStraight(cards);

            Assert.IsFalse(isStraight);
        }

        // todo write more tests for evalutatehand 

        [TestMethod]
        public void TestHandEvaluator_EvaluateHand_RoyalFlush()
        {
            var cards = new List<Card>
            {
                new Card(Suits.Spades, Numbers.Two),
                new Card(Suits.Spades, Numbers.Ten),
                new Card(Suits.Spades, Numbers.Nine),
                new Card(Suits.Spades, Numbers.Ace),
                new Card(Suits.Spades, Numbers.Jack),
                new Card(Suits.Spades, Numbers.Queen),
                new Card(Suits.Spades, Numbers.King)
            };

            var myHand = SimplePokerHandEvaluator.EvaluatePokerHand(cards);

            Assert.IsTrue(myHand == PokerHandRanks.RoyalFlush);
        }

        [TestMethod]
        public void TestHandEvaluator_EvaluateHand_IsNOTRoyalFlush1()
        {
            // this is a flush and includes royal cards but its not a royal flush
            var cards = new List<Card>
            {
                new Card(Suits.Spades, Numbers.Two),
                new Card(Suits.Spades, Numbers.Ten),
                new Card(Suits.Spades, Numbers.Nine),
                new Card(Suits.Hearts, Numbers.Ace),
                new Card(Suits.Spades, Numbers.Jack),
                new Card(Suits.Spades, Numbers.Queen),
                new Card(Suits.Spades, Numbers.King)
            };

            var myHand = SimplePokerHandEvaluator.EvaluatePokerHand(cards);

            Assert.IsFalse(myHand == PokerHandRanks.RoyalFlush);
        }

        [TestMethod]
        public void TestHandEvaluator_EvaluateHand_IsNOTRoyalFlush2()
        {
            // this is a straight flush to the king but not a royal flush
            var cards = new List<Card>
            {
                new Card(Suits.Spades, Numbers.Two),
                new Card(Suits.Spades, Numbers.Ten),
                new Card(Suits.Spades, Numbers.Nine),
                new Card(Suits.Spades, Numbers.Nine),
                new Card(Suits.Spades, Numbers.Jack),
                new Card(Suits.Spades, Numbers.Queen),
                new Card(Suits.Spades, Numbers.King)
            };

            var myHand = SimplePokerHandEvaluator.EvaluatePokerHand(cards);

            Assert.IsFalse(myHand == PokerHandRanks.RoyalFlush);
        }

        [TestMethod]
        public void TestHandEvaluator_EvaluateHand_IsStraightFlush()
        {
            var cards = new List<Card>
            {
                new Card(Suits.Spades, Numbers.Two),
                new Card(Suits.Diamonds, Numbers.Ten),
                new Card(Suits.Spades, Numbers.Three),
                new Card(Suits.Spades, Numbers.Four),
                new Card(Suits.Spades, Numbers.Six),
                new Card(Suits.Hearts, Numbers.Queen),
                new Card(Suits.Spades, Numbers.Five)
            };

            var myHand = SimplePokerHandEvaluator.EvaluatePokerHand(cards);

            Assert.IsTrue(myHand == PokerHandRanks.StraightFlush);
        }

        [TestMethod]
        public void TestHandEvaluator_EvaluateHand_StraightAndFlush()
        {
            var cards = new List<Card>
            {
                // this hand contains a straight and a flush but is not a straight flush.
                new Card(Suits.Spades, Numbers.Two),
                new Card(Suits.Hearts, Numbers.Three),
                new Card(Suits.Spades, Numbers.Four),
                new Card(Suits.Spades, Numbers.Five),
                new Card(Suits.Spades, Numbers.Six),
                new Card(Suits.Diamonds, Numbers.Ten),
                new Card(Suits.Spades, Numbers.Queen)
            };

            // ...             
            // evaluator should return flush
            // evaluator should not return straight
            // evaluator should not return straight flush
            var myHand = SimplePokerHandEvaluator.EvaluatePokerHand(cards);
            Assert.IsTrue(myHand == PokerHandRanks.Flush);
        }

        [TestMethod]
        public void TestHandEvaluator_EvaluateHand_Quads()
        {
            var cards = new List<Card>
            {
                new Card(Suits.Spades, Numbers.Two),
                new Card(Suits.Hearts, Numbers.Two),
                new Card(Suits.Spades, Numbers.Four),
                new Card(Suits.Clubs, Numbers.Two),
                new Card(Suits.Spades, Numbers.Six),
                new Card(Suits.Diamonds, Numbers.Two),
                new Card(Suits.Spades, Numbers.Queen)
            };

            var myHand = SimplePokerHandEvaluator.EvaluatePokerHand(cards);
            Assert.IsTrue(myHand == PokerHandRanks.FourOfAKind);
        }

        [TestMethod]
        public void TestHandEvaluator_EvaluateHand_QuadsAndFullHouse()
        {
            //this hand contains quads but also another pair technically making it a full house. 
            // functino should still return quads
            var cards = new List<Card>
            {
                new Card(Suits.Spades, Numbers.Two),
                new Card(Suits.Hearts, Numbers.Two),
                new Card(Suits.Spades, Numbers.Four),
                new Card(Suits.Clubs, Numbers.Two),
                new Card(Suits.Diamonds, Numbers.Four),
                new Card(Suits.Diamonds, Numbers.Two),
                new Card(Suits.Spades, Numbers.Queen)
            };

            var myHand = SimplePokerHandEvaluator.EvaluatePokerHand(cards);
            Assert.IsTrue(myHand == PokerHandRanks.FourOfAKind);
        }


        [TestMethod]
        public void TestHandEvaluator_EvaluateHand_FullHouse()
        {
            var cards = new List<Card>
            {
                new Card(Suits.Spades, Numbers.Two),
                new Card(Suits.Hearts, Numbers.Two),
                new Card(Suits.Spades, Numbers.Four),
                new Card(Suits.Hearts, Numbers.Four),
                new Card(Suits.Spades, Numbers.Six),
                new Card(Suits.Diamonds, Numbers.Four),
                new Card(Suits.Spades, Numbers.Queen)
            };

            var myHand = SimplePokerHandEvaluator.EvaluatePokerHand(cards);
            Assert.IsTrue(myHand == PokerHandRanks.FullHouse);
        }

        [TestMethod]
        public void TestHandEvaluator_EvaluateHand_Set()
        {
            var cards = new List<Card>
            {
                new Card(Suits.Spades, Numbers.Two),
                new Card(Suits.Hearts, Numbers.Three),
                new Card(Suits.Spades, Numbers.Four),
                new Card(Suits.Hearts, Numbers.Four),
                new Card(Suits.Spades, Numbers.Six),
                new Card(Suits.Diamonds, Numbers.Four),
                new Card(Suits.Spades, Numbers.Queen)
            };

            var myHand = SimplePokerHandEvaluator.EvaluatePokerHand(cards);
            Assert.IsTrue(myHand == PokerHandRanks.ThreeOfAKind);
        }

        [TestMethod]
        public void TestHandEvaluator_EvaluateHand_SetAndStraight()
        {
            var cards = new List<Card>
            {
                // this contains a set/three of a kind and a straight. should return straight.
               new Card(Suits.Spades, Numbers.Two),
                new Card(Suits.Hearts, Numbers.Three),
                new Card(Suits.Spades, Numbers.Four),
                new Card(Suits.Hearts, Numbers.Four),
                new Card(Suits.Spades, Numbers.Six),
                new Card(Suits.Diamonds, Numbers.Four),
                new Card(Suits.Spades, Numbers.Five)
            };

            var myHand = SimplePokerHandEvaluator.EvaluatePokerHand(cards);
            Assert.IsTrue(myHand == PokerHandRanks.Straight);
        }

        [TestMethod]
        public void TestHandEvaluator_EvaluateHand_SetAndFlush()
        {
            var cards = new List<Card>
            {
                // this contains a set/three of a kind and a flush. should return flush.
                new Card(Suits.Spades, Numbers.Four),
                new Card(Suits.Hearts, Numbers.Four),
                new Card(Suits.Diamonds, Numbers.Four),
                new Card(Suits.Spades, Numbers.Six),
                new Card(Suits.Spades, Numbers.Two),
                new Card(Suits.Spades, Numbers.Eight),
                new Card(Suits.Spades, Numbers.Five)
            };

            var myHand = SimplePokerHandEvaluator.EvaluatePokerHand(cards);
            Assert.IsTrue(myHand == PokerHandRanks.Flush);
        }

        [TestMethod]
        public void TestHandEvaluator_EvaluateHand_SetAndStraightFlush()
        {
            var cards = new List<Card>
            {
                // this contains a set/three of a kind and a straight flush. should return straight flush.
                new Card(Suits.Spades, Numbers.Four),
                new Card(Suits.Hearts, Numbers.Four),
                new Card(Suits.Diamonds, Numbers.Four),
                new Card(Suits.Spades, Numbers.Six),
                new Card(Suits.Spades, Numbers.Two),
                new Card(Suits.Spades, Numbers.Three),
                new Card(Suits.Spades, Numbers.Five)
            };

            var myHand = SimplePokerHandEvaluator.EvaluatePokerHand(cards);
            Assert.IsTrue(myHand == PokerHandRanks.StraightFlush);
        }

        [TestMethod]
        public void TestHandEvaluator_EvaluateHand_SetAndRoyalFlush()
        {
            var cards = new List<Card>
            {
                // this contains a set/three of a kind and a royal flush. should return royal flush.
                new Card(Suits.Diamonds, Numbers.Ten),
                new Card(Suits.Hearts, Numbers.Ten),
                new Card(Suits.Spades, Numbers.Ten),
                new Card(Suits.Spades, Numbers.Jack),
                new Card(Suits.Spades, Numbers.Queen),
                new Card(Suits.Spades, Numbers.King),
                new Card(Suits.Spades, Numbers.Ace)
            };

            var myHand = SimplePokerHandEvaluator.EvaluatePokerHand(cards);
            Assert.IsTrue(myHand == PokerHandRanks.RoyalFlush);
        }

        [TestMethod]
        public void TestHandEvaluator_EvaluateHand_TwoPair()
        {
            var cards = new List<Card>
            {
                new Card(Suits.Diamonds, Numbers.Ten),
                new Card(Suits.Hearts, Numbers.Ten),
                new Card(Suits.Spades, Numbers.Three),
                new Card(Suits.Hearts, Numbers.Three),
                new Card(Suits.Spades, Numbers.Queen),
                new Card(Suits.Spades, Numbers.King),
                new Card(Suits.Spades, Numbers.Ace)
            };

            var myHand = SimplePokerHandEvaluator.EvaluatePokerHand(cards);
            Assert.IsTrue(myHand == PokerHandRanks.TwoPair);
        }

        [TestMethod]
        public void TestHandEvaluator_EvaluateHand_FlushAndTwoPair()
        {
            var cards = new List<Card>
            {
                // this contains a flush and two pair. should return flush
                new Card(Suits.Diamonds, Numbers.Ten),
                new Card(Suits.Spades, Numbers.Ten),
                new Card(Suits.Spades, Numbers.Three),
                new Card(Suits.Hearts, Numbers.Three),
                new Card(Suits.Spades, Numbers.Queen),
                new Card(Suits.Spades, Numbers.King),
                new Card(Suits.Spades, Numbers.Ace)
            };

            var myHand = SimplePokerHandEvaluator.EvaluatePokerHand(cards);
            Assert.IsTrue(myHand == PokerHandRanks.Flush);
        }

        [TestMethod]
        public void TestHandEvaluator_EvaluateHand_Pair()
        {
            var cards = new List<Card>
            {
                new Card(Suits.Diamonds, Numbers.Ten),
                new Card(Suits.Spades, Numbers.Ten),
                new Card(Suits.Spades, Numbers.Eight),
                new Card(Suits.Hearts, Numbers.Three),
                new Card(Suits.Diamonds, Numbers.Queen),
                new Card(Suits.Spades, Numbers.King),
                new Card(Suits.Spades, Numbers.Ace)
            };

            var myHand = SimplePokerHandEvaluator.EvaluatePokerHand(cards);
            Assert.IsTrue(myHand == PokerHandRanks.Pair);
        }

        [TestMethod]
        public void TestHandEvaluator_EvaluateHand_HighCard()
        {
            var cards = new List<Card>
            {
                  new Card(Suits.Diamonds, Numbers.Ten),
                new Card(Suits.Spades, Numbers.Nine),
                new Card(Suits.Spades, Numbers.Eight),
                new Card(Suits.Hearts, Numbers.Three),
                new Card(Suits.Diamonds, Numbers.Queen),
                new Card(Suits.Spades, Numbers.King),
                new Card(Suits.Spades, Numbers.Ace)
            };

            var myHand = SimplePokerHandEvaluator.EvaluatePokerHand(cards);
            Assert.IsTrue(myHand == PokerHandRanks.HighCard);
        }


    }
}
