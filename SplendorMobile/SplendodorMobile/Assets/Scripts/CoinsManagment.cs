using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinsManagment : MonoBehaviour
{
    public TMP_Text[] textArr = new TMP_Text[Coins.numCoins];

    private Coins playerCoins = new();
    public void setCoins(object data)
    {
        if (data is Coins)
        {
            Debug.Log(playerCoins);
            Coins coins = (Coins) data;
            playerCoins.addCoins(coins);

            for (int i = 0; i < Coins.numCoins; i++)
            {
                Debug.Log(i + " = " + playerCoins.coinsArr[i]);
                textArr[i].text = playerCoins.coinsArr[i].ToString();
            }
        }        
    }
}
