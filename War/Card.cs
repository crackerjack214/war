namespace War
{
    // Card object contains value, suit, print value conversion, and card comparison
    public class Card
    {
        public Suit suit;
        // Raw value of the card 2-14
        public int rawValue;

        // Initialize a new card containing a suit and a raw value
        public Card(Suit cardSuit, int value) 
        {
            suit = cardSuit;
            rawValue = value;
        }

        // Return the value that would be printed on the card 2-10, J, Q, K, A
        public string GetPrintValue () {
            string printValue;

            // If card value is greater than 10 map it's face value
            switch (rawValue)
            {
                case 11:
                    printValue = "J";
                    break;
                case 12:
                    printValue = "Q";
                    break;
                case 13:
                    printValue = "K";
                    break;
                case 14:
                    printValue = "A";
                    break;
                default:
                    printValue = rawValue.ToString();
                    break;
            }

            return printValue;
        }

        // Check if this card is higher than a comparison card
        public int IsHigherThan(Card otherCard)
        {
            // -1 indicates a tie
            int isWinner = -1;

            if (otherCard.rawValue > rawValue)
            {
                // other card is winner
                isWinner = 1;
            } else if (otherCard.rawValue < rawValue)
            {
                // this card is winner
                isWinner = 0;
            }

            return isWinner;
        }
    }
}
