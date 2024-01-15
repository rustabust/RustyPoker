using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RustyPoker
{
    public class Player
    {
        public string Name { get; set; }
        public List<Card> Hand { get; private set; }
        public PokerHandRanks HandRank { get; set; }
        
        public Player(string name)
        {
            this.Name = name;
            this.Hand = new List<Card>();
        }
        
        private void DealCard(Card card)
        {
            
        }
    }

}
