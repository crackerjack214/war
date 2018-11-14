using System;
using System.Collections.Generic;
using System.Linq;

namespace War
{
    // Deck object contains a queue of cards and supports shuffling, 
    // building a deck, drawing cards, adding cards, and splitting the queue
    public class Deck
    {
        private Queue<Card> cards = new Queue<Card>();

        // Generate a brand new deck containing all 52 cards
        public void CreateStartingDeck() 
        {
            Array suits = Enum.GetValues(typeof(Suit));

            // Loop through all card values 2-14
            for (int i = 2; i <= 14; i++)
            {
                // Loop through all suits
                foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                {
                    // Add new card to the deck with suit and value
                    cards.Enqueue(new Card(suit, i));
                }
            }

            // Shuffle the newly created deck
            Shuffle();
        }

        // Shuffle the queue of cards
        public void Shuffle() 
        {
            // Convert queue to list
            List<Card> cardsToShuffle = cards.ToList();

            // Generate a random number to be used in place switching
            Random randomNumber = new Random();

            int n = cardsToShuffle.Count;

            // Loop through cards and switch places randomly
            while (n > 1) {
                n--;
                int rand = randomNumber.Next(n + 1);
                Card card = cardsToShuffle[rand];
                cardsToShuffle[rand] = cardsToShuffle[n];
                cardsToShuffle[n] = card;
            }

            // Convert list back to queue
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
        // Todo Add support for adding multiple cards at once
        public int AddCard(Card card)
        {
            cards.Enqueue(card);

            return cards.Count;
        }

        // Split the deck into defined number of smaller decks
        public static Deck[] DivideDeck(Deck startingDeck, int deckDivisions)
        {
            // Initialize array of decks of length deckDivisions
            Deck[] decks = new Deck[deckDivisions];
            int divisionIndex = deckDivisions;

            // Initialize the correct number of decks
            for (int i = 0; i < deckDivisions; i++)
            {
                decks[i] = new Deck();
            }

            // Loop through each card in the starting deck and assign it to the correct new deck
            // This is better than just splitting the deck down the middle as it more closely simulates dealing cards
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
