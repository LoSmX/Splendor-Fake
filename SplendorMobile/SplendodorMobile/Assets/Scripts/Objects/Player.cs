using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Hud hud;
    public Transform npos;
    public Transform [] cardPos = new Transform[5];
    public GameEvent EndTrunEvent;


    public CoinCollection coinsBalance = new();
    public CoinCollection cardBalance = new();
    public int score = 0;
    public bool hasNobel = false;
    public int turnActionCounter = 0;

    public List<Coins.Color> coinsTaken = new List<Coins.Color> ();
    public const int maxTurnCounter = 3;
    public void onClickEvent(object data)
    {
        if (data is Chip)
        {
            Chip chip = (Chip) data;

            if (chip.amount > 0 && coinsBalance.getCoinAmount() < 10)
            {
                if(!coinsTaken.Contains(chip.color))
                {
                    chip.amount--;
                    CoinCollection coins = new(chip);
                    coinsBalance += coins;
                    coinsTaken.Add(chip.color);
                    hud.updateCoins(coinsBalance);
                    if(coinsBalance.getCoinAmount() >= 10)
                    {
                        turnActionCounter = maxTurnCounter;
                    }
                    else
                    {
                        turnActionCounter++;
                    }

                }
                else if(coinsTaken.Count == 1 && chip.amount >= 3)
                {
                    chip.amount--;
                    CoinCollection coins = new(chip);
                    coinsBalance += coins;
                    hud.updateCoins(coinsBalance);
                    turnActionCounter = maxTurnCounter;
                }
            }
            
        }

        if (data is Card)
        {
            if (turnActionCounter == 0)
            {
                if (data is Nobel)
                {
                    if (hasNobel == false)
                    {
                        Nobel card = (Nobel)data;
                        if (card.buy(this))
                        {
                            hud.updateScore(card);
                        }
                    }
                }
                else
                {
                    Card card = (Card)data;
                    if (card.buy(this))
                    {
                        turnActionCounter = 3;
                        hud.updateCoins(coinsBalance);
                        hud.updateCards(cardBalance);
                        hud.updateScore(card);
                    }
                }
            }
        }
        
        if (turnActionCounter >= maxTurnCounter)
        {
            coinsTaken.Clear();
            EndTrunEvent.Raise(this);
            turnActionCounter = 0;
        }
    }
}
