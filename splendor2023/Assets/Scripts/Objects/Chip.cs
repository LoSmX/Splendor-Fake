using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chip : MonoBehaviour
{
    public Coins.Color color;
    public TextMesh text;

    public int amount = 4;
    private void Start()
    {
        if (color != Coins.Color.gold)
            amount = 4;
        else
            amount = 3;
    }
    void Update()
    {
        text.text = amount.ToString();
    }

    public void onPurchaseEvent(object data)
    {
        if (data is CoinCollection)
        {
            CoinCollection cc = (CoinCollection)data;
            amount += cc.coinStacks[(int)color].amount;
        }
    }
}
