using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinStack : Coins

{
    public Color color;
    public int amount = 0;

// Constructors
    public CoinStack(Color color, int amount)
    {
        this.color = color;
        this.amount = amount;
    }

// Methods
    private static bool isSameColor(CoinStack s1, CoinStack s2)
    {
        if(s1.color == s2.color)
        {
            return true;
        }
        else
        {
            Debug.LogException(new System.Exception("Error: stacks have different colors"));
            return false;
        }
    }
    public void add(CoinStack cs)
    {
        if (isSameColor(this, cs))
            this.amount += cs.amount;
    }

    public void sub(CoinStack cs)
    {
        if (isSameColor(this, cs))
            this.amount -= cs.amount;
    }
}
