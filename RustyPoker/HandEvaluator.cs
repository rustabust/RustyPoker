using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RustyPoker
{
    /// <summary>
    /// could consider making this a non static class, 
    /// this way we can keep track of checks we've done on the cards
    /// i.e. avoiding checking for straight multiple times. 
    /// we can also keep track of the best five of seven cards, etc...
    /// </summary>
    public class PokerHandEvaluator
    {
        private List<Card> inputCards { get; set; }
        public List<Card> BestHand { get; private set; }
        public PokerHandRanks HandRank { get; set; }
        public string HandDescription { get; set; }

        public PokerHandEvaluator(List<Card> cards)
        {
            inputCards = cards.OrderBy(a => a.Number).ToList();
            BestHand = new List<Card>();
        }

        public PokerHandRanks EvaluatePokerHand()
        {

            // i currently see no better way than to just go through the whole list of hands
            // poker hand evaluation needs to : 
            // * determine the best five cards in the seven card group
            // * determine the rank of the hand
            // * determine the rank of the hand compared to other same ranked hands (9s full of 2s vs 2s full of 9s, or high card vs high card, evaluating kickers, etc)
            // * determine the name of the hand 

            // #pickuphere i started making functions for hand evaluation referencing the static base class but that is not going to work since I want to keep track of the cards...

            if (this.isFlush)
            {
                bool straightFlush = IsStraightFlush();
                if (straightFlush)
                {
                    bool isRoyalFlushResult = isRoyalFlush(cards);
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

            var pairCheckResult = checkForPairs(cards);
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
                    if (IsStraight(cards))
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

        private bool isStraightFlush
        {
            get
            {
                return SimplePokerHandEvaluator.IsStraightFlush(this.inputCards);
            }
        }

        private static bool isRoyalFlush(List<Card> cards)
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

        public bool IsStraight
        {
            get
            {
                return SimplePokerHandEvaluator.IsStraight(this.inputCards); 
            }
        }

        //private static bool doIsStraight(List<Card> cards)
        //{
        //    const int STRAIGHT_SEQUENCE_CARD_COUNT = 5;

        //    int sequenceCounter = 0;

        //    // since we're testing sequence here with i and i+1,
        //    // only run the loop to the 2nd from last item in the array
        //    for (int i = 0; i < cards.Count - 1; i++)
        //    {
        //        if (cards[i].Number == cards[i + 1].Number)
        //        {
        //            continue;
        //        }
        //        bool sequenceOk = isSequence(cards[i], cards[i + 1]);
        //        if (sequenceOk)
        //        {
        //            sequenceCounter++;

        //            // since we're checking if card1 and card2 are in a sequence,
        //            // for five cards, there will only be four comparisons.
        //            // once we get to four successful comparisons, we have a straight
        //            if (sequenceCounter == STRAIGHT_SEQUENCE_CARD_COUNT - 1)
        //            {
        //                return true; // todo problem with this is that it returns yes for a straight but it might not be the highest straight in the hand.
        //            }
        //        }
        //        else
        //        {
        //            sequenceCounter = 0;
        //        }

        //    }
        //    return false;
        //}

        //private static bool isSequence(Card card1, Card card2)
        //{
        //    var number = 100;
        //    if (card1.Number == Numbers.Ace) // i think here ace from card1 should always be considered low. if high, then card2 def isnt in sequence.
        //    {
        //        number = 1;
        //    }
        //    else
        //    {
        //        number = (int)card1.Number;
        //    }
        //    var nextNumber = (Numbers)number + 1;
        //    return card2.Number == nextNumber;
        //}

       


        private bool isFlush
        {
            get
            {
                return SimplePokerHandEvaluator.IsFlush(this.inputCards);
            }
        }
    }

    
}
