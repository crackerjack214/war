using System;
using System.Collections.Generic;

namespace War
{
    public class Card
    {
        public Suit suit { get; set; }      
        public int rawValue { get; set; }   // Raw value of the card 2-14

        public Card(Suit cardSuit, int value) {
            suit = cardSuit;
            rawValue = value;
        }

        // Return the value that would be printed on the card 2-10, J, Q, K, A
        public string GetPrintValue () {
            string printValue = rawValue.ToString();

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
            }

            return printValue;
     
        }

        // Check if this card is higher than a comparison card
        public int IsHigherThan(Card otherCard)
        {
            int isWinner = -1;

            if (otherCard.rawValue > rawValue)
            {
                isWinner = 1;
            } else if (otherCard.rawValue < rawValue)
            {
                isWinner = 0;
            }

            return isWinner;
        }

    }
}
