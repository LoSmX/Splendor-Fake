using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins
{ 

    public enum CoinColors
    {
        white = 0,
        brown,
        red,
        green,
        blue,
        gold
    }

    public const int numCoins = 6;

    public int[] coinsArr = {0, 0, 0, 0, 0, 0};

    public void addCoins(Coins coins)
    {
        for(int i = 0; i < numCoins; i++)
            this.coinsArr[i] += coins.coinsArr[i];
    }
}
