using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RustyPoker
{
    public class Deck
    {
        public static int DECK_CARDS_PER_DECK = 52;
        public static int DECK_CARDS_PER_SUIT = 13;
        public static int DECK_SUITS_COUNT = 4;

        //public List<Card> Cards { get; set; }
        public Stack<Card> Cards { get; set; }

        public Deck()
        {
            Cards = new Stack<Card>();
            buildCardsStack();
        }

        public Deck(Deck deck)
        {
            Cards = new Stack<Card>();

            var reversedCards = deck.Cards.Reverse();
            foreach (var card in reversedCards)
            {
                Cards.Push(card);
            }
        }

        private void buildCardsStack()
        {
            // reset cards stack
            this.Cards = new Stack<Card>();

            // trying to model card deck with stack
            // will use a list to compile all the cards we need and shuffle the order, then push them to the stack.

            var cards = new List<Card>();

            var suits = Enum.GetValues(typeof(Suits));
            var numbers = Enum.GetValues(typeof(Numbers));
            

            foreach (Suits suit in suits)
            {
                foreach (Numbers number in numbers)
                {
                    if (number != Numbers.Undefined)
                    {
                        var card = new Card(suit, number);
                        cards.Add(card);
                    }
                }
            }

            cards.Shuffle();

            foreach (var card in cards)
            {
                this.Cards.Push(card);
            }
        }
        public void Shuffle()
        {
            // basically just recompile the cards stack
            buildCardsStack();

        }
       

        public override int GetHashCode()
        {
            var cardNames = this.Cards.Select(a => a.Name);
            var sCardNames = String.Join("...", cardNames);
            return sCardNames.GetHashCode();
        }
    }
}
