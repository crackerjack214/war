using System;
using System.Collections.Generic;

namespace War
{
    // Game class contains all of the game mechanics such as 
    // turn taking, scoring, game starting, and game ending
    public class Game
    {
        private string[] playerNames;
        private List<Player> players = new List<Player>();
        private int turnCount = 0;
        public bool gameEnd = false;

        private List<Card> warCards = new List<Card>();

        // Initialize the game
        public Game(string[] names)
        {
            Console.Clear();
            Console.WriteLine("Starting a new game\n");

            // Set player names
            playerNames = names;

            // Instantiate player objects for each player
            InitializePlayers(playerNames);

            // Build a starting deck and shuffle it
            Deck startingDeck = new Deck();
            startingDeck.CreateStartingDeck();

            // Divide the deck between players
            players = DivideDeck(startingDeck, players);
        }

        // Draw a card from each player, hidden is used for face down cards
        private Card[] DrawCards(bool facedown = false)
        {
            // Initialize an array of cards with length equal to the number of players
            Card[] activeCards = new Card[players.Count];

            // Draw cards from each player
            foreach (Player player in players)
            {
                activeCards[players.IndexOf(player)] = player.DrawCard();

                // If player has no more cards, they lose
                if (player.GetScore() == 0)
                {
                    EndGame(player);

                    return null;
                }

                // Handle console output based on face down or face up
                if (!facedown) 
                {
                     Console.WriteLine("Player {0} drew a {1} of {2}", player.GetName(), activeCards[players.IndexOf(player)].GetPrintValue(), activeCards[players.IndexOf(player)].suit);
                } else
                {
                    Console.WriteLine("Player {0} drew a face down card", player.GetName());
                }
            }

            return activeCards;
        }

        // Execute a new turn of players drawing cards and potentially going to war
        public void NewTurn() 
        {
            turnCount++;

            Console.Clear();
            Console.WriteLine("Turn #{0}", turnCount);

            // Draw cards for each player
            Card[] newCards = DrawCards();
            if (newCards != null)
            {
                // Check for winning player
                // TODO Possibly add support for more than 2 players here
                int outcome = newCards[0].IsHigherThan(newCards[1]);

                // Assign winning player the cards, -1 indicates tie and War happens
                if (outcome != -1)
                {
                    // This is the winning player
                    Player player = players[outcome] as Player;

                    // Assign winner the cards
                    foreach (Card card in newCards)
                    {
                        player.scoreDeck.AddCard(card);
                    }
                }
                else
                {
                    // Enter war mode
                    bool warStatus = true;

                    // Place previous played cards into a pot which will ultimately go to the winner of the War
                    List<Card> potCards = new List<Card>(newCards);

                    Console.WriteLine("War!");

                    // Keep looping through a round of War as long as faceup cards are equal
                    while (warStatus)
                    {
                        // Draw the face down cards
                        Card[] facedownCards = DrawCards(true);

                        // Only continue as long as there are facedown cards available
                        if (facedownCards != null)
                        {
                            // Add face down cards to the pot
                            potCards.AddRange(facedownCards);

                            // Draw the faceup cards
                            Card[] counterCards = DrawCards();
                           
                            // Only continue as long as there are faceup cards available
                            if (counterCards != null)
                            {
                                // Add the face up cards to the pot
                                potCards.AddRange(counterCards);

                                // Determine who wins or tied
                                int warOutcome = counterCards[0].IsHigherThan(counterCards[1]);

                                // If outcome is not a tie, award the winning player the cards and get out of war mode
                                if (warOutcome != -1)
                                {
                                    warStatus = false;

                                    // Winning player
                                    Player player = players[warOutcome] as Player;

                                    Console.WriteLine("Player {0} won {1} cards", player.GetName(), potCards.Count);

                                    // Assign winner the cards
                                    foreach (Card card in potCards)
                                    {
                                        player.scoreDeck.AddCard(card);
                                    }
                                }
                            } else 
                            {
                                return;
                            }
                        } else 
                        {
                            return;
                        }
                    }
                }
            }

            // Print number of cards each player has left after the turn
            foreach (Player player in players)
            {
                Console.WriteLine("Player {0} has {1} cards", player.GetName(), player.GetScore());
            }
        }

        // Output the loser and trigger game end
        private void EndGame(Player losingPlayer) 
        {
            Console.WriteLine("*********** GAME OVER - {0} lost  ***********", losingPlayer.GetName());
            Console.WriteLine("If you would like to play again, press Enter/Return");

            gameEnd = true;
        }

        // Divide the deck between all players
        private static List<Player> DivideDeck(Deck deck, List<Player> allPlayers) 
        {
            // Split provided deck into two
            Deck[] playerDecks = Deck.DivideDeck(deck, allPlayers.Count);

            // Assign each deck to each player
            foreach (Player player in allPlayers)
            {
                player.drawDeck = playerDecks[allPlayers.IndexOf(player)];
                player.scoreDeck = new Deck();
                Console.WriteLine("Cards in {0} deck {1}", player.GetName(), player.drawDeck.GetCount());
            }

            return allPlayers;
        }

        // Create new player objects for each name provided
        private void InitializePlayers(string[] names)
        {
            for (int i = 0; i < names.Length; i++)
            {
                players.Add(new Player(names[i]));
            }
        }

    }
}
