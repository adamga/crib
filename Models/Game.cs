using System.Collections.Generic;

namespace Crib.Models
{
    public class Game
    {
        public List<Card> Player1Hand { get; set; }
        public List<Card> Player2Hand { get; set; }
        public List<Card> Crib { get; set; }
        public Deck Deck { get; set; }
        public int Player1Score { get; set; }
        public int Player2Score { get; set; }

        public Game()
        {
            Player1Hand = new List<Card>();
            Player2Hand = new List<Card>();
            Crib = new List<Card>();
            Deck = new Deck();
            Player1Score = 0;
            Player2Score = 0;
        }

        public void ShuffleAndDeal()
        {
            Deck.Shuffle();
            Player1Hand = Deck.Deal(6);
            Player2Hand = Deck.Deal(6);
            Crib = Deck.Deal(4);
        }

        public int CalculateScore(List<Card> hand)
        {
            // Implement scoring logic based on the rules of cribbage
            int score = 0;
            // Example scoring logic (simplified)
            foreach (var card in hand)
            {
                if (card.Rank == "J" || card.Rank == "Q" || card.Rank == "K")
                {
                    score += 10;
                }
                else if (card.Rank == "A")
                {
                    score += 1;
                }
                else
                {
                    score += int.Parse(card.Rank);
                }
            }
            return score;
        }

        public bool ValidateMove(Card card, List<Card> hand)
        {
            // Implement move validation logic based on the rules of cribbage
            return hand.Contains(card);
        }
    }
}
