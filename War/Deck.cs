using System;
using System.Collections.Generic;
using System.Linq;

namespace War
{
    public class Deck
    {
        private Queue<Card> cards = new Queue<Card>();

        // Generate a brand new deck containing all cards
        public void CreateStartingDeck() 
        {
            Array suits = Enum.GetValues(typeof(Suit));

            // Loop through all card values
            for (int i = 2; i <= 14; i++)
            {
                // Loop through all suits
                foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                {
                    // Add new card to the deck
                    cards.Enqueue(new Card(suit, i));
                }
            }
        }

        // Shuffle the deck of cards
        public void Shuffle() 
        {
            List<Card> cardsToShuffle = cards.ToList();
            Random randomNumber = new Random();
            int n = cardsToShuffle.Count;

            // Loop through cards and switch places randomly
            while (n > 1) {
                n--;
                int k = randomNumber.Next(n + 1);
                Card card = cardsToShuffle[k];
                cardsToShuffle[k] = cardsToShuffle[n];
                cardsToShuffle[n] = card;
            }

            cards = new Queue<Card>(cardsToShuffle);
        }

        // Get count of cards in the deck
        public int GetCount()
        {
            return cards.Count();
        }

        // Get next card from deck
        public Card GetCard() 
        {
            return cards.Dequeue();
        }

        // Add a card to the deck and return the new count
        public int AddCard(Card card)
        {
            cards.Enqueue(card);

            return cards.Count;
        }

        // Print deck for debugging purposes
        public static void PrintDeck(Queue<Card> deck) 
        {
            List<Card> cardsToPrint = deck.ToList();

            for (int i = 0; i < cardsToPrint.Count; i++)
            {
                Card card = cardsToPrint[i];
                Console.WriteLine("Value: {0} | Suit: {1}", card.rawValue, card.suit);
            }
        }

        // 
        public static Deck[] DivideDeck(Deck startingDeck, int deckDivisions)
        {
            // Initialize array of decks
            Deck[] decks = new Deck[deckDivisions];
            int divisionIndex = deckDivisions;

            // Initialize the correct number of decks
            for (int i = 0; i < deckDivisions; i++)
            {
                decks[i] = new Deck();
            }

            // Loop through each card in the starting deck and assign it to the correct new deck
            while (startingDeck.GetCount() > 0)
            {
                Card card = startingDeck.GetCard();

                // Add card to the correct player's deck
                decks[divisionIndex - 1].AddCard(card);

                // Decrement the division index unless it hits 1, then reset
                if (divisionIndex == 1)
                {
                    divisionIndex = deckDivisions;
                } else
                {
                    divisionIndex--;
                }
            }

            return decks;
        }
   }
}
