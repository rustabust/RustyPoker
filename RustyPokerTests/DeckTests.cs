using RustyPoker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RustyPokerTests
{
    [TestClass]
    public class DeckTests
    {
        [TestMethod]
        public void TestDeckContents()
        {
            // arrange 
            // nothing really to arrange

            // act - test parameters of what makes a deck
            var deck = new Deck();

            // assert
            Assert.IsNotNull(deck);
            Assert.IsNotNull(deck.Cards);
            Assert.AreEqual(deck.Cards.Count, Deck.DECK_CARDS_PER_DECK);
            Assert.AreEqual(deck.Cards.Distinct().Count(), 52); // make sure no dupes. needs comparer
            Assert.AreEqual(deck.Cards.Where(a => a.Suit == Suits.Hearts).Count(), Deck.DECK_CARDS_PER_SUIT);
            Assert.AreEqual(deck.Cards.Where(a => a.Suit == Suits.Spades).Count(), Deck.DECK_CARDS_PER_SUIT);
            Assert.AreEqual(deck.Cards.Where(a => a.Suit == Suits.Clubs).Count(), Deck.DECK_CARDS_PER_SUIT);
            Assert.AreEqual(deck.Cards.Where(a => a.Suit == Suits.Diamonds).Count(), Deck.DECK_CARDS_PER_SUIT);

            var numbers = Enum.GetValues(typeof(Numbers));
            foreach(Numbers number in numbers)
            {
                var numberCount = deck.Cards.Where(a => a.Number == number).Count();
                Assert.AreEqual(numberCount, Deck.DECK_SUITS_COUNT);
            }
            // probably need to make a Hand class that will test this too.

        }

        [TestMethod]
        public void TestDeckCopy()
        {
            // arrange
            var deck1 = new Deck();
            var deck2 = new Deck(deck1);

            for (int i = 0; i < Deck.DECK_CARDS_PER_DECK; i++)
            {
                var deck1Card = deck1.Cards.Pop();
                var deck2Card = deck2.Cards.Pop();
                Assert.AreEqual(deck1Card, deck2Card);
            }
        }

        [TestMethod]
        public void TestDeckShuffle()
        {
            var deck1 = new Deck();
            // var deck2 = new Deck(deck1);

            // how to actually test this? just make sure the ordering changes?
            // how to even check the ordering absolutely?
            // a single swap of two cards in the ordering will pass...

            var beforeShuffleHashCode = deck1.GetHashCode();
            deck1.Shuffle();
            var afterShuffleHashCode = deck1.GetHashCode();

            Assert.AreNotEqual(beforeShuffleHashCode, afterShuffleHashCode);
        }
    }
}
