using System;
using System.Collections;
using System.Collections.Generic;

namespace War
{
    public class Game
    {
        private string[] playerNames;
        private List<Player> players = new List<Player>();
        private int turnCount = 0;
        public bool gameEnd = false;

        private List<Card> warCards = new List<Card>();

        public Game(string[] names)
        {
            playerNames = names;

            Console.Clear();
            Console.WriteLine("Starting a new game\n");
            // Instantiate players
            InitializePlayers(playerNames);

            // Build a starting deck
            Deck startingDeck = BuildDeck();

            // Divide the deck between players
            players = divideDeck(startingDeck, players);
        }

        private Card[] drawCards(bool hidden = false)
        {
            Card[] activeCards = new Card[players.Count];

            // Draw cards from both players
            foreach (Player player in players)
            {
                activeCards[players.IndexOf(player)] = player.DrawCard();

                if (player.GetScore() == 0)
                {
                    EndGame(player);

                    return null;
                }

                if (!hidden) 
                {
                     Console.WriteLine("Player {0} drew a {1} of {2}", player.GetName(), activeCards[players.IndexOf(player)].GetPrintValue(), activeCards[players.IndexOf(player)].suit);
                } else
                {
                    Console.WriteLine("Player {0} drew a face down card", player.GetName());
                }
            }

            return activeCards;
        }

        public void NewTurn() 
        {
            turnCount++;
            Console.Clear();
            Console.WriteLine("Turn #{0}", turnCount);

            Card[] newCards = drawCards();
            if (newCards != null)
            {
                // Check for winning player or go into War mode
                int outcome = newCards[0].IsHigherThan(newCards[1]);

                // Assign winning player the cards, -1 indicates tie and War happens
                if (outcome != -1)
                {
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
                    List<Card> potCards = new List<Card>(newCards);

                    Console.WriteLine("War!");
                    while (warStatus)
                    {
                        // Draw the face down cards
                        Card[] hiddenCards = drawCards(true);
                        if (hiddenCards != null)
                        {
                            // Add face down cards to the pot
                            potCards.AddRange(hiddenCards);

                            // Draw the face up cards
                            Card[] counterCards = drawCards();

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

                                    Player player = players[warOutcome] as Player;

                                    Console.WriteLine("Player {0} won {1} cards", player.GetName(), potCards.Count);
                                    // Assign winner the cards
                                    foreach (Card card in potCards)
                                    {
                                        player.scoreDeck.AddCard(card);
                                    }
                                }
                            } else {
                                return;
                            }
                        } else {
                            return;
                        }
                    }
                }
            }

            foreach (Player player in players)
            {
                Console.WriteLine("Player {0} has {1} cards", player.GetName(), player.GetScore());
            }
        }

        private void EndGame(Player losingPlayer) 
        {
            Console.WriteLine("*********** GAME OVER - {0} lost  ***********", losingPlayer.GetName());
            Console.WriteLine("If you would like to play again, press Enter/Return");

            gameEnd = true;
        }

        // Divide the deck between all players
        private static List<Player> divideDeck(Deck deck, List<Player> allPlayers) 
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

        private Deck BuildDeck() {
            // Instantiate a new deck
            Deck deck = new Deck();
            // Create a brand new deck containing all 52 cards
            deck.CreateStartingDeck();
            // Shuffle the new deck
            deck.Shuffle();

            return deck;
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
