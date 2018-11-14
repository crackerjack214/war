using System;

namespace War
{
    public class Player
    {
        private string name;
        public Deck drawDeck;
        public Deck scoreDeck;
        public Card activeCard;

        // Create a new player of name newName
        public Player(string newName)
        {
            name = newName;
        }

        // Return the current total number of cards in player's control
        public int GetScore() {
            return drawDeck.GetCount() + scoreDeck.GetCount();
        }

        public string GetName() 
        {
            return name;
        }

        public Card DrawCard() 
        {
            if (drawDeck.GetCount() > 0)
            {
                // If there are cards left in the draw deck, use them
                return drawDeck.GetCard();
            } else if (scoreDeck.GetCount() > 0) {
                // If no cards left in the draw deck, convert score deck to draw deck
                Console.WriteLine("** Shuffling deck for {0}", name);
                scoreDeck.Shuffle();
                drawDeck = scoreDeck;
                scoreDeck = new Deck();

                return drawDeck.GetCard();
            } else {
                // If no more cards are left this player loses
                return null;
            }
        }
    }
}
