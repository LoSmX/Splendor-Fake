using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    public TMP_Text[] coinBalance = new TMP_Text[Coins.numOfColors];
    public TMP_Text[] cardBalance = new TMP_Text[Coins.numOfColors - 1];
    public TMP_Text score;
    public Image isActivePlayer;

    public void updateCoins(CoinCollection CoinBalance)
    {
        for (int i = 0; i < Coins.numOfColors; i++)
        {
            coinBalance[i].text = CoinBalance.coinStacks[i].amount.ToString();
        }
    }

    public void updateCards(CoinCollection cards)
    {
        for (int i = 0; i < cardBalance.Length; i++)
        {
            cardBalance[i].text = cards.coinStacks[i].amount.ToString();
        }
    }
    public void updateScore(Card card)
    {
        int points = System.Int32.Parse(score.text);
        score.text = (points + card.points).ToString();
    }

    public void onEndTurnEvent(object data)
    {
        isActivePlayer.enabled = !isActivePlayer.enabled;
    }
}
