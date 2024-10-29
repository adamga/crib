using System;
using System.Collections.Generic;

namespace Crib.Models
{
    public class Deck
    {
        private List<Card> cards;

        public Deck()
        {
            cards = new List<Card>();
            string[] suits = { "Hearts", "Diamonds", "Clubs", "Spades" };
            string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };

            foreach (var suit in suits)
            {
                foreach (var rank in ranks)
                {
                    cards.Add(new Card(suit, rank));
                }
            }
        }

        public void Shuffle()
        {
            Random rng = new Random();
            int n = cards.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Card value = cards[k];
                cards[k] = cards[n];
                cards[n] = value;
            }
        }

        public List<Card> Deal(int numberOfCards)
        {
            List<Card> dealtCards = new List<Card>();
            for (int i = 0; i < numberOfCards; i++)
            {
                if (cards.Count == 0)
                {
                    throw new InvalidOperationException("No more cards in the deck.");
                }
                dealtCards.Add(cards[0]);
                cards.RemoveAt(0);
            }
            return dealtCards;
        }
    }
}
