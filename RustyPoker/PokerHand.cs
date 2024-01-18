using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RustyPoker
{
    public class PokerHand
    {
        public static int POKER_HAND_PERSONAL_CARD_COUNT = 2;
        public static int POKER_HAND_FULL_HAND_COUNT = 5; // for texas hold em at least
        public static int POKER_HAND_PERSONAL_PLUS_BOARD_CARD_COUNT = POKER_HAND_PERSONAL_CARD_COUNT + POKER_HAND_FULL_HAND_COUNT;
        
        public List<Card> InputCards { get; set; }
        public List<Card> BestFive { get; private set; }
        public PokerHandRanks Rank { get; set; }
        public string Description { get; set; }
        public PokerHand(List<Card> cards)
        {
            // validate cards
            if (cards.Count > POKER_HAND_PERSONAL_PLUS_BOARD_CARD_COUNT)
            {
                throw new ArgumentOutOfRangeException("max card count for constructor is " + POKER_HAND_PERSONAL_PLUS_BOARD_CARD_COUNT);
            }
            if (cards.Count < POKER_HAND_FULL_HAND_COUNT)
            {
                throw new ArgumentOutOfRangeException("min card count for constructor is " + POKER_HAND_FULL_HAND_COUNT);
            }

            if (cards.Distinct().Count() != cards.Count)
            {
                throw new ArgumentException("duplicate cards are not allowed in a poker hand.");
            }

            this.InputCards = cards.OrderBy(a => a.Number).ToList();
            this.BestFive = new List<Card>();
            this.Rank = PokerHandRanks.HighCard;
            this.Description = "";

            var defaultBestHand = this.InputCards.TakeLast(5).ToList();
            SetBestHand(PokerHandRanks.FullHouse, defaultBestHand);
        }
        public void SetBestHand(PokerHandRanks rank, List<Card> bestFive)
        {
            if (rank >= this.Rank)
            {
                this.Rank = rank;
                this.BestFive = bestFive;
                setDescription();
            }
        }

        private void setDescription()
        {
            switch (this.Rank)
            {
                case PokerHandRanks.HighCard:
                    break;
                case PokerHandRanks.Pair:
                    break;
                case PokerHandRanks.TwoPair:
                    break;
                case PokerHandRanks.ThreeOfAKind:
                    break;
                case PokerHandRanks.Straight:
                    break;
                case PokerHandRanks.Flush:
                    break;
                case PokerHandRanks.FullHouse:
                    Numbers setNumber = GetCardNumberByCount(3);
                    Numbers pairNumber = GetCardNumberByCount(2);
                    this.Description = $"Full House, {setNumber}s full of {pairNumber}s";
                    break;
                case PokerHandRanks.FourOfAKind:
                    Numbers cardNumber = GetCardNumberByCount(4);
                    this.Description = $"Four of a Kind, {cardNumber}s"; // these could be constantized
                    break;
                case PokerHandRanks.StraightFlush:
                    this.Description = "Straight Flush"; // suit needed?
                    break;
                case PokerHandRanks.RoyalFlush:
                    this.Description = "Royal Flush"; // suit needed?
                    break;
            }
        }

        private Numbers GetCardNumberByCount(int count)
        {
            Numbers result = Numbers.Undefined;
            var group = this.BestFive.GroupBy(a => a.Number).FirstOrDefault(b => b.Count() == count);
            if (group != null)
            {
                result = group.Key;
            }
            return result;
        }
    }
}
