using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    public TMP_Text[] coinBalance = new TMP_Text[Coins.numOfColors];        // Text displaying the amount of coins possessed
    public TMP_Text[] cardBalance = new TMP_Text[Coins.numOfColors - 1];    // Text displaying the amount of cards possessed
    public TMP_Text score;      // Text displaying the players score
    public Image isActivePlayer;    // Image showing active player

    // Update coins text
    public void updateCoins(CoinCollection CoinBalance)
    {
        for (int i = 0; i < Coins.numOfColors; i++)
        {
            coinBalance[i].text = CoinBalance.coinStacks[i].amount.ToString();
        }
    }

    // Update cards text
    public void updateCards(CoinCollection cards)
    {
        for (int i = 0; i < cardBalance.Length; i++)
        {
            cardBalance[i].text = cards.coinStacks[i].amount.ToString();
        }
    }

    // Update score text
    public void updateScore(int score)
    {
        this.score.text = score.ToString();
    }

    public void onEndTurnEvent(object data)
    {
        isActivePlayer.enabled = !isActivePlayer.enabled;
    }
}
