using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Configuration variables
    public int Id;          // Identifies the player
    public Hud hud;         // The Player hud which shows the players stats
    public Transform npos;  // Position for the nobel
    public Transform[] cardPos = new Transform[5];  // five slot for each color of the bought cards
    public Transform[] reservedPos = new Transform[4];  // Positions for the reserved cards
    public GameEvent EndTrunEvent;                      // Event to raise when turn is ended

    // Variables used by other classes
    public CoinCollection coinsBalance = new();         // Structure holding information about the possessed coins
    public CoinCollection cardBalance = new();          // Structure holding information about the possessed cards
    public bool hasNobel = false;                       // Flag indicating if nobel is already possessed
    public bool[] freeReservedPos = { true, true, true, true };     // Array holding the information which reserved card slots are free.

    private int score = 0;                               // Score of the player
    private List<Coins.Color> coinsTaken = new List<Coins.Color> ();
    private int turnActionCounter = 0;                   // Counter for computing which actions are allowed and when the turn is over.
    private const int maxTurnCounter = 3;


    private void takeCoin(Chip chip)
    {
        chip.amount--;
        CoinCollection coins = new(chip);
        coinsBalance += coins;
        coinsTaken.Add(chip.color);
        hud.updateCoins(coinsBalance);
    }

    // Reserve card
    private void reserveCard(Card card)
    {
        for (int i = 0; i < reservedPos.Length; i++)
        {
            if (freeReservedPos[i] == true)
            {
                card.reserve(Id, i);
                card.move(reservedPos[i].position);
                freeReservedPos[i] = false;
                endTurn();
                break;
            }
        }
    }

    // Get cards or nobel
    private void getCard(Card card)
    {
        if(card is not Nobel)
        {
            hud.updateCoins(coinsBalance);
            hud.updateCards(cardBalance);
        }

        score += card.points;
        hud.updateScore(score);
    }

    private void endTurn()
    {
        turnActionCounter = maxTurnCounter;
    }

    // When something is clicked
    public void onClickEvent(object data)
    {
        // If chips are clicked
        if (data is Chip)
        {
            Chip chip = (Chip)data;

            // Take if chip is gold and no gold taken yet
            if ((chip.color == Coins.Color.gold && coinsTaken.Count == 0)
                || chip.color != Coins.Color.gold) {
                // Take coin
                if (chip.amount > 0 && coinsBalance.getCoinAmount() < 10)
                {
                    // take a coin which was not taken yet
                    if (!coinsTaken.Contains(chip.color))
                    {
                        takeCoin(chip);

                        // Set turnActionCounter
                        if (chip.color == Coins.Color.gold)
                        {
                            turnActionCounter = 2;
                        }
                        else if (coinsBalance.getCoinAmount() >= 10)
                        {
                            endTurn();
                        }
                        else
                        {
                            turnActionCounter++;
                        }

                    }// Take a coin twice when there are enough coins and coins are not gold
                    else if (coinsTaken.Count == 1 && chip.amount >= 3 && chip.color != Coins.Color.gold)
                    {
                        takeCoin(chip);
                        endTurn(); ;
                    }
                }
            }
        }

        // When card or nobel is clicked
        if (data is Card)
        {
            Card card = (Card)data;
            // if no coins are taken yet
            if (turnActionCounter == 0)
            {
                if(card.pay(this) == true)
                {
                    getCard(card);
                    if(card is not Nobel)
                        endTurn();
                }
            } // if gold coin was taken
            else if(turnActionCounter == 2 && coinsTaken.Contains(Coins.Color.gold) && data is not Nobel)
            {
                if(card.reservedBy == 0)
                {
                    reserveCard(card);
                }
            }
        }

        // After last click end turn
        if (turnActionCounter >= maxTurnCounter)
        {
            // reset taken coins and turnActionCounter
            coinsTaken.Clear();
            turnActionCounter = 0;

            // raise End turn event
            EndTrunEvent.Raise(this);
        }
    }
}
