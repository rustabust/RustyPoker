using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RustyPoker
{
    public class Gameplay
    {
        public List<Player> players { get; private set; }
        private int playerCount { get { return players.Count; } }
        private Deck deck = new Deck();
        public static PokerGames PokerGame = PokerGames.TexasHoldEm;
        public static int GAMEPLAY_CARDS_PER_HAND
        {
            get
            {
                switch(PokerGame)
                {
                    case PokerGames.TexasHoldEm: return 2;
                    default:
                        throw new NotImplementedException($"poker game {PokerGame} is not fully implemented.");
                }
            }
        }
        public Gameplay(int playerCount)
        {
            players = new List<Player>();
            for (int i = 0; i < playerCount; i++)
            {
                players.Add(new Player("Player " + i));
            }
        }
        public void Start()
        {
            Console.WriteLine("gameplay started.");
            Console.WriteLine("...starting with " + this.playerCount + " players : ");
            foreach (var player in players)
            {
                Console.WriteLine("......" + player.Name);
            }

            Console.WriteLine("...deck created and shuffled.");

            DealCards(GAMEPLAY_CARDS_PER_HAND);

            Console.WriteLine("...hands have been dealt.");
            foreach(var player in players)
            {
                Console.WriteLine($"......{player.Name} has");
                foreach (var card in player.Hand)
                {
                    Console.WriteLine($"..........{card.Name}");
                }
            }

            // to do - betting

            // deal the board
            
            var board = new List<Card>();

            // flop
            deck.Cards.Pop(); //burn
            board.Add(deck.Cards.Pop());
            board.Add(deck.Cards.Pop());
            board.Add(deck.Cards.Pop());

            // todo betting round

            // turn
            deck.Cards.Pop(); // burn
            board.Add(deck.Cards.Pop());

            // river
            deck.Cards.Pop(); // burn
            board.Add(deck.Cards.Pop());

            Console.WriteLine("the board has been dealt:");
            foreach(var card in board)
            {
                Console.WriteLine($"...{card.Name}");
            }

            // write out what hand each player has
            foreach(var player in players)
            {
                var playerHand = new List<Card>();
                playerHand.AddRange(player.Hand);
                playerHand.AddRange(board);
                player.HandRank = SimplePokerHandEvaluator.EvaluatePokerHand(playerHand);
                Console.WriteLine($"player {player.Name} has a {player.HandRank}");
            }

            // figure out how to compare hands

        }
        public void DealCards(int cardsPerHand)
        {
            for (int i = 0; i < cardsPerHand; i++)
            {
                foreach (var player in players)
                {
                    player.Hand.Add(deck.Cards.Pop()); // theres nothing preventing someone from dealing out more than the desired number of cards
                }
            }
        }
        public void Stop() { Console.WriteLine("gameplay ended."); }

       

    }

    

    public enum PokerGames
    {
        TexasHoldEm
    }

}
