using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RustyPoker
{
    
    
    public class Card
    {
        public Suits Suit { get; private set; }
        public Numbers Number { get; private set; }
        public Card(Suits suit, Numbers number)
        {
            Suit = suit;
            Number = number;
        }
        public Card(Card card)
        {
            this.Suit = card.Suit;
            this.Number = card.Number;
        }

        public string Name { get { return $"{this.Number} of {this.Suit}"; } }

        public override bool Equals(object? obj)
        {
            bool result = false;
            var card = obj as Card; 
            if (card is Card)
            {
                if (card.Suit == this.Suit && card.Number == this.Number)
                {
                    result = true;
                }
            }
            return result;
        }

        //public override int GetHashCode()
        //{
        //    return this.Name.GetHashCode();
        //}
    }

    public enum Suits
    {
        Spades,
        Hearts, 
        Clubs,
        Diamonds
    }

    public enum Numbers
    {
        //ace low will be 1 for straights
        Two = 2,
        Three,
        Four,
        Five,
        Six, 
        Seven,
        Eight,
        Nine, 
        Ten,
        Jack,
        Queen,
        King,
        Ace

        
    }
}
