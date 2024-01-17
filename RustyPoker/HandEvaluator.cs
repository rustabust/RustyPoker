using System;
using System.Collections.Frozen;
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
    //public class PokerHandEvaluator
    //{
    //  public PokerHand PokerHand { get; private set; }

    //    public PokerHandEvaluator(List<Card> cards)
    //    {
    //        this.PokerHand = new PokerHand(cards);
    //        inputCards = cards.OrderBy(a => a.Number).ToList();
    //        BestHand = new List<Card>();
    //    }

    //    public void EvaluatePokerHand()
    //    {
    //        CheckForFlush();

    //        if (isFlush)
    //        {
    //            bool straightFlush = IsStraightFlush(cards);
    //            if (straightFlush)
    //            {
    //                bool isRoyalFlushResult = IsRoyalFlush(cards);
    //                if (isRoyalFlushResult)
    //                {
    //                    return PokerHandRanks.RoyalFlush;
    //                }
    //                else
    //                {
    //                    return PokerHandRanks.StraightFlush;
    //                }
    //            }
    //        }

    //        var pairCheckResult = checkForPairs(cards);
    //        switch (pairCheckResult)
    //        {
    //            //quads
    //            case PokerHandRanks.FourOfAKind:

    //            // stephanie dj and michelle
    //            case PokerHandRanks.FullHouse:
    //                return pairCheckResult;

    //            default:
    //                // regular flush
    //                if (isFlush)
    //                {
    //                    return PokerHandRanks.Flush;
    //                }

    //                // regular straight
    //                if (IsStraight(cards))
    //                {
    //                    return PokerHandRanks.Straight;
    //                }


    //                // if not those, then paircheck result is best we got
    //                // set
    //                // two pair
    //                // pair
    //                // high card
    //                return pairCheckResult;
    //        }
    //    }

    //    private static PokerHandRanks checkForPairs(List<Card> cards)
    //    {
    //        PokerHandRanks result = PokerHandRanks.HighCard;
    //        {
    //            var numberGroups = cards.GroupBy(a => a.Number);

    //            List<PokerHandRanks> handRanks = new List<PokerHandRanks>();

    //            foreach (var numberGroup in numberGroups)
    //            {
    //                var numberGroupCount = numberGroup.Count();
    //                switch (numberGroupCount)
    //                {
    //                    case 4: handRanks.Add(PokerHandRanks.FourOfAKind); break;
    //                    case 3: handRanks.Add(PokerHandRanks.ThreeOfAKind); break;
    //                    case 2: handRanks.Add(PokerHandRanks.Pair); break;
    //                    case 1: handRanks.Add(PokerHandRanks.HighCard); break;
    //                    default:
    //                        throw new Exception($"unexpected numberGroupCount of {numberGroupCount} while looking for card matches in hand evaluation");
    //                }
    //            }
    //            if (!handRanks.Any())
    //            {
    //                throw new Exception("unexpected condition - handRanks list was empty while checking for pairs.");
    //            }

    //            handRanks.Sort();
    //            result = handRanks.LastOrDefault();
    //            if (handRanks.Count() > 0)
    //            {
    //                if (handRanks.Contains(PokerHandRanks.ThreeOfAKind) &&
    //                        handRanks.Contains(PokerHandRanks.Pair))
    //                {
    //                    result = PokerHandRanks.FullHouse;
    //                }
    //                else if (handRanks.Where(a => a == PokerHandRanks.Pair).Count() == 2)
    //                {
    //                    result = PokerHandRanks.TwoPair;

    //                }
    //            }
    //        }
    //        return result;
    //    }

    //    public static bool IsStraightFlush(List<Card> cards)
    //    {
    //        bool result = false;
    //        // straight flush
    //        // need to verify the flush cards are also the straight cards...
    //        var flushCards = getFlushCards(cards);
    //        if (flushCards != null)
    //        {
    //            result = IsStraight(flushCards);
    //        }
    //        return result;
    //    }
    //    public static bool IsRoyalFlush(List<Card> cards)
    //    {
    //        bool result = IsFlush(cards);  // would be nice if is flush returned the suit

    //        var suitGroups = cards.GroupBy(a => a.Suit);
    //        var flushGroup = suitGroups.FirstOrDefault(a => a.Count() >= 5);
    //        if (flushGroup == null)
    //        {
    //            throw new Exception("unexpected condition - flush group is null.");
    //        }

    //        cards = flushGroup.ToList();

    //        result = result && cards.Any(a => a.Number == Numbers.Ten);
    //        result = result && cards.Any(a => a.Number == Numbers.Jack);
    //        result = result && cards.Any(a => a.Number == Numbers.Queen);
    //        result = result && cards.Any(a => a.Number == Numbers.King);
    //        result = result && cards.Any(a => a.Number == Numbers.Ace);

    //        return result;
    //    }

    //    public static bool IsStraight(List<Card> cards)
    //    {

    //        cards = cards.OrderBy(a => a.Number).ToList();

    //        bool result = doIsStraight(cards);

    //        if (!result && cards.Any(a => a.Number == Numbers.Ace))
    //        {
    //            // be default above the list is sorted with ace high
    //            // reorder the cards here with ace low and check it again

    //            var cards2 = new List<Card>();

    //            var aces = cards.Where(a => a.Number == Numbers.Ace);
    //            cards2.AddRange(aces);

    //            cards.RemoveAll(a => a.Number == Numbers.Ace);
    //            foreach (var card in cards)
    //            {
    //                cards2.Add(card);
    //            }

    //            result = doIsStraight(cards2);
    //        }

    //        return result;
    //    }

    //    private static bool doIsStraight(List<Card> cards)
    //    {
    //        int sequenceCounter = 0;

    //        // since we're testing sequence here with i and i+1,
    //        // only run the loop to the 2nd from last item in the array
    //        for (int i = 0; i < cards.Count - 1; i++)
    //        {
    //            if (cards[i].Number == cards[i + 1].Number)
    //            {
    //                continue;
    //            }
    //            bool sequenceOk = isSequence(cards[i], cards[i + 1]);
    //            if (sequenceOk)
    //            {
    //                sequenceCounter++;

    //                // since we're checking if card1 and card2 are in a sequence,
    //                // for five cards, there will only be four comparisons.
    //                // once we get to four successful comparisons, we have a straight
    //                if (sequenceCounter == PokerHand.POKER_FULL_HAND_COUNT - 1)
    //                {
    //                    return true; // todo problem with this is that it returns yes for a straight but it might not be the highest straight in the hand.
    //                }
    //            }
    //            else
    //            {
    //                sequenceCounter = 0;
    //            }

    //        }
    //        return false;
    //    }
    //    private static bool isSequence(Card card1, Card card2)
    //    {
    //        var number = 100;
    //        if (card1.Number == Numbers.Ace) // i think here ace from card1 should always be considered low. if high, then card2 def isnt in sequence.
    //        {
    //            number = 1;
    //        }
    //        else
    //        {
    //            number = (int)card1.Number;
    //        }
    //        var nextNumber = (Numbers)number + 1;
    //        return card2.Number == nextNumber;
    //    }

    //    private static List<Card>? getFlushCards(List<Card> cards)
    //    {
    //        List<Card>? result = null;
    //        var suitGroups = cards.GroupBy(a => a.Suit);
    //        foreach (var suitGroup in suitGroups)
    //        {
    //            if (suitGroup.Count() >= PokerHand.POKER_FULL_HAND_COUNT)
    //            {
    //                result = suitGroup.ToList(); 
    //            }
    //        }
    //        return result;
    //    }

    //    private void SetBestHand(PokerHandRanks rank, List<Card> cards)
    //    {
    //        if (rank > this.HandRank)
    //        {
    //            this.HandRank = rank;
    //            this.BestHand = cards;
    //        }
    //    }

    //    protected void CheckForFlush()
    //    {
    //        var flushCards = getFlushCards(this.PokerHand.);
    //        if (flushCards != null)
    //        {
    //            // in case the hand has 6 or 7 suited cards, need to take top 5
    //            var bestFiveOfFlush = flushCards.OrderBy(a => a.Number).Take(5).ToList();
    //            SetBestHand(PokerHandRanks.Flush, bestFiveOfFlush);
    //        }
    //    }
    //}
}
