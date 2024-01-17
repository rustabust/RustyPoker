﻿using RustyPoker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RustyPokerTests
{
    [TestClass]
    public class PokerHandTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestConstructor_TooFewCards()
        {
            var cards = new List<Card>
            {
                new Card(Suits.Spades, Numbers.Five),
                new Card(Suits.Spades, Numbers.Four),
                new Card(Suits.Spades, Numbers.Six),
                new Card(Suits.Clubs, Numbers.Two)
            };

            var ph = new PokerHand(cards);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestConstructor_TooManyCards()
        {
            var cards = new List<Card>
            {
                new Card(Suits.Spades, Numbers.Ace),
                new Card(Suits.Clubs, Numbers.Ace),
                new Card(Suits.Hearts, Numbers.Ace),
                new Card(Suits.Diamonds, Numbers.Five),
                new Card(Suits.Spades, Numbers.Five),
                new Card(Suits.Spades, Numbers.Four),
                new Card(Suits.Spades, Numbers.Six),
                new Card(Suits.Clubs, Numbers.Two)
            };

            var ph = new PokerHand(cards);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestConstructor_DuplicateCards()
        {
            var cards = new List<Card>
            {
                new Card(Suits.Spades, Numbers.Ace), // make sure dupe cards are not allowed
                new Card(Suits.Spades, Numbers.Ace),
                new Card(Suits.Clubs, Numbers.Ace),
                new Card(Suits.Hearts, Numbers.Ace),
                new Card(Suits.Diamonds, Numbers.Five),
                new Card(Suits.Spades, Numbers.Four),
                new Card(Suits.Clubs, Numbers.Two)
            };

            var ph = new PokerHand(cards);
        }

        [TestMethod]
        public void TestConstructor1_HandDescription1()
        {
            var cards = new List<Card>
            {
                new Card(Suits.Spades, Numbers.Ace),
                new Card(Suits.Diamonds, Numbers.Ace),
                new Card(Suits.Clubs, Numbers.Ace),
                new Card(Suits.Hearts, Numbers.Ace),
                new Card(Suits.Diamonds, Numbers.Five),
                new Card(Suits.Spades, Numbers.Four),
                new Card(Suits.Clubs, Numbers.Two)
            };

            var ph = new PokerHand(cards);
            Assert.AreEqual(ph.Description, "Four of a Kind, Aces");
        }
    }
}