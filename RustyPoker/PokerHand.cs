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

        public List<Card> Cards { get; set; }
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

            this.Cards = cards.OrderBy(a => a.Number).ToList();
            this.BestFive = new List<Card>();
            this.Rank = PokerHandRanks.HighCard;
            this.Description = "";

            // testing best hand??
            //var defaultBestHand = this.InputCards.TakeLast(5).ToList();
            //SetBestHand(PokerHandRanks.FourOfAKind, defaultBestHand);
            EvaluatePokerHand();
            //SetBestHand(myHand, this.BestFive); 
        }
        protected void SetBestHand(PokerHandRanks rank, List<Card> bestFive)
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

        #region poker hand evaluation

        private void UpdateBestHand(HandEvaluation handEvaluation)
        {
            if (handEvaluation != null && handEvaluation.IsRequestedHand)
            {
                this.BestFive = handEvaluation.Cards;
            }
        }

        public PokerHandRanks EvaluatePokerHand()
        {
            // i currently see no better way than to just go through the whole list of hands
            // poker hand evaluation needs to : 
            // * determine the best five cards in the seven card group
            // * determine the rank of the hand
            // * determine the rank of the hand compared to other same ranked hands (9s full of 2s vs 2s full of 9s, or high card vs high card, evaluating kickers, etc)
            // * determine the name of the hand 
            this.Cards = this.Cards.OrderBy(a => a.Number).ToList();
            var isFlush = IsFlush(this.Cards);  consider sending the poker hand into this and other similar static functions and having them update the best hand before returning...
            UpdateBestHand(isFlush);

            if (isFlush) 
            {
                bool straightFlush = IsStraightFlush(this.Cards);
                if (straightFlush)
                {
                    bool isRoyalFlushResult = IsRoyalFlush(this.Cards);
                    if (isRoyalFlushResult)
                    {
                        return PokerHandRanks.RoyalFlush;
                    }
                    else
                    {
                        return PokerHandRanks.StraightFlush;
                    }
                }
            }

            var pairCheckResult = checkForPairs(this.Cards);
            switch (pairCheckResult)
            {
                //quads
                case PokerHandRanks.FourOfAKind:

                // stephanie dj and michelle
                case PokerHandRanks.FullHouse:
                    return pairCheckResult;

                default:
                    // regular flush
                    if (isFlush)
                    {
                        return PokerHandRanks.Flush;
                    }

                    // regular straight
                    if (IsStraight(this.Cards))
                    {
                        return PokerHandRanks.Straight;
                    }


                    // if not those, then paircheck result is best we got
                    // set
                    // two pair
                    // pair
                    // high card
                    return pairCheckResult;
            }
        }

        private static PokerHandRanks checkForPairs(List<Card> cards)
        {
            PokerHandRanks result = PokerHandRanks.HighCard;
            {
                var numberGroups = cards.GroupBy(a => a.Number);

                List<PokerHandRanks> handRanks = new List<PokerHandRanks>();

                foreach (var numberGroup in numberGroups)
                {
                    var numberGroupCount = numberGroup.Count();
                    switch (numberGroupCount)
                    {
                        case 4: handRanks.Add(PokerHandRanks.FourOfAKind); break;
                        case 3: handRanks.Add(PokerHandRanks.ThreeOfAKind); break;
                        case 2: handRanks.Add(PokerHandRanks.Pair); break;
                        case 1: handRanks.Add(PokerHandRanks.HighCard); break;
                        default:
                            throw new Exception($"unexpected numberGroupCount of {numberGroupCount} while looking for card matches in hand evaluation");
                    }
                }
                if (!handRanks.Any())
                {
                    throw new Exception("unexpected condition - handRanks list was empty while checking for pairs.");
                }

                handRanks.Sort();
                result = handRanks.LastOrDefault();
                if (handRanks.Count() > 0)
                {
                    if (handRanks.Contains(PokerHandRanks.ThreeOfAKind) &&
                            handRanks.Contains(PokerHandRanks.Pair))
                    {
                        result = PokerHandRanks.FullHouse;
                    }
                    else if (handRanks.Where(a => a == PokerHandRanks.Pair).Count() == 2)
                    {
                        result = PokerHandRanks.TwoPair;

                    }
                }
            }
            return result;
        }

        public static bool IsStraightFlush(List<Card> cards)
        {
            bool result = false;
            // straight flush
            // need to verify the flush cards are also the straight cards...
            var flushCards = getFlushCards(cards);
            if (flushCards != null)
            {
                result = IsStraight(flushCards);
            }
            return result;
        }
        public static bool IsRoyalFlush(List<Card> cards)
        {
            bool result = IsFlush(cards);  // would be nice if is flush returned the suit

            var suitGroups = cards.GroupBy(a => a.Suit);
            var flushGroup = suitGroups.FirstOrDefault(a => a.Count() >= 5);
            if (flushGroup == null)
            {
                throw new Exception("unexpected condition - flush group is null.");
            }

            cards = flushGroup.ToList();

            result = result && cards.Any(a => a.Number == Numbers.Ten);
            result = result && cards.Any(a => a.Number == Numbers.Jack);
            result = result && cards.Any(a => a.Number == Numbers.Queen);
            result = result && cards.Any(a => a.Number == Numbers.King);
            result = result && cards.Any(a => a.Number == Numbers.Ace);

            return result;
        }

        public static HandEvaluation IsStraight(List<Card> cards)
        {
            // make a local copy that we can manipulate if needed.
            // this is pretty ugly and confusing to have a local copy and still have access to the class copy...aye
            //var cards = this.Cards;

            cards = cards.OrderBy(a => a.Number).ToList();

            HandEvaluation isStraightResult = doIsStraight(cards);
            if (isStraightResult! && cards.Any(a => a.Number == Numbers.Ace))
            {
                // by default, the above the list is sorted with ace high
                // reorder the cards here with ace low and check it again

                var cards2 = new List<Card>();

                var aces = cards.Where(a => a.Number == Numbers.Ace);
                cards2.AddRange(aces);

                cards.RemoveAll(a => a.Number == Numbers.Ace);
                foreach (var card in cards)
                {
                    cards2.Add(card);
                }

                isStraightResult = doIsStraight(cards2);
            }
            

            return isStraightResult;
        }

        private static HandEvaluation doIsStraight(List<Card> cards)
        {
            HandEvaluation result = new HandEvaluation();

            const int STRAIGHT_SEQUENCE_CARD_COUNT = 5;

            int sequenceCounter = 0;

            // since we're testing sequence here with i and i+1,
            // only run the loop to the 2nd from last item in the array
            for (int i = 0; i < cards.Count - 1; i++)
            {
                if (cards[i].Number == cards[i + 1].Number)
                {
                    continue;
                }
                bool sequenceOk = isSequence(cards[i], cards[i + 1]);
                if (sequenceOk)
                {
                    sequenceCounter++;

                    // since we're checking if card1 and card2 are in a sequence,
                    // for five cards, there will only be four comparisons.
                    // once we get to four successful comparisons, we have a straight
                    if (sequenceCounter == STRAIGHT_SEQUENCE_CARD_COUNT - 1)
                    {
                        // this is tricky esp given comment below. need section of cards starting with i going back 5 cards
                        // needs a unit test
                        var bestCards = cards.Skip(i - STRAIGHT_SEQUENCE_CARD_COUNT + 1).Take(STRAIGHT_SEQUENCE_CARD_COUNT).ToList();
                        result = new HandEvaluation { IsRequestedHand = true, Cards = bestCards };
                        return result;
                        //return true; // todo problem with this is that it returns yes for a straight but it might not be the highest straight in the hand.
                    }
                }
                else
                {
                    sequenceCounter = 0;
                }

            }

            result = new HandEvaluation { IsRequestedHand = false };
            return result;
        }
        private static bool isSequence(Card card1, Card card2)
        {
            var number = 100;
            if (card1.Number == Numbers.Ace) // i think here ace from card1 should always be considered low. if high, then card2 def isnt in sequence.
            {
                number = 1;
            }
            else
            {
                number = (int)card1.Number;
            }
            var nextNumber = (Numbers)number + 1;
            return card2.Number == nextNumber;
        }

        private static List<Card>? getFlushCards(List<Card> cards)
        {
            List<Card>? result = null;
            const int FLUSH_SUITED_CARD_COUNT = 5;
            var suitGroups = cards.GroupBy(a => a.Suit);
            foreach (var suitGroup in suitGroups)
            {
                if (suitGroup.Count() >= FLUSH_SUITED_CARD_COUNT)
                {
                    result = suitGroup.ToList(); // if more than 5, should sort them and return the highest 5?
                }
            }
            return result;
        }

        public static HandEvaluation IsFlush(List<Card> cards)
        {
            HandEvaluation result = new HandEvaluation();
            var flushCards = getFlushCards(cards);
            if (flushCards != null)
            {
                result.IsRequestedHand = true;
                result.Cards = flushCards;
            }
            return result;
        }

        #endregion poker hand evaluation
    }

    public class HandEvaluation
    {
        public bool IsRequestedHand { get; set; }
        //public PokerHandRanks Rank { get; set; }
        public List<Card>? Cards { get; set; }
        public HandEvaluation()
        {
            //this.Rank = PokerHandRanks.HighCard;
            this.IsRequestedHand = false;
            this.Cards = null;
        }

        public static implicit operator bool(HandEvaluation obj)
        {
            return obj != null && obj.IsRequestedHand;
        }

    }
}
