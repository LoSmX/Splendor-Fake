using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollection : Coins
{
    // fields
    public CoinStack[] coinStacks = new CoinStack[numOfColors];

    // Constructor
    public CoinCollection()
    {
        for (int i = 0; i < coinStacks.Length; i++)
        {
            coinStacks[i] = new(colorOrder[i], 0);
        }

    }
    public CoinCollection(int[] costs)
    {
        for (int i = 0; i < numOfColors; i++)
        {
            if(i < costs.Length)
                coinStacks[i] = new(colorOrder[i], costs[i]);
            else
                coinStacks[i] = new(colorOrder[i], 0);
        }

    }

    public CoinCollection(int amount)
    {
        for(int i = 0; i < coinStacks.Length; i++)
        {
            coinStacks[i] = new(colorOrder[i], amount);
        }

        coinStacks[(int)Color.gold].amount--;
    }

    public CoinCollection(Chip chip)
    {
        for (int i = 0; i < coinStacks.Length; i++)
        {
            coinStacks[i] = new(colorOrder[i], 0);
        }

        coinStacks[(int)chip.color].amount = 1;
    }

    // Override operators
    public static CoinCollection operator+(CoinCollection balance, CoinStack cs)
    {
        // Add amount of coin stack to the corresponding coin stack
        for(int i = 0; i < balance.coinStacks.Length; i++)
        {
            if(balance.coinStacks[i].color == cs.color)
            {
                balance.coinStacks[i].add(cs);
            }
            else
            {
                continue;
            }
        }
            
        return balance;
    }

    public static CoinCollection operator +(CoinCollection balance, CoinCollection amount)
    {
        // Add two Collections together
        for (int i = 0; i < balance.coinStacks.Length; i++)
        {
            balance.coinStacks[i].add(amount.coinStacks[i]);
        }
        return balance;
    }

    public static CoinCollection operator - (CoinCollection balance, CoinCollection amount)
    {
        // Sub two Collections
        for (int i = 0; i < balance.coinStacks.Length; i++)
        {
            balance.coinStacks[i].sub(amount.coinStacks[i]);
        }
        return balance;
    }

    // Methods
    public bool isAffordable(CoinCollection cost)
    {
        //Check if balance is greater or equal the cost
        for (int i = 0; i < this.coinStacks.Length; i++)
        {
            if(this.coinStacks[i].amount < cost.coinStacks[i].amount)
            {
                Debug.Log("Missing color = " + colorOrder[i].ToString());
                Debug.Log("Balance = " + this.coinStacks[i].amount.ToString() + 
                    " and costs are " + cost.coinStacks[i].amount.ToString());
                return false;
            }    
        }

        return true;
    }

    public bool isZero()
    {
        foreach(CoinStack cs in coinStacks)
        {
            if(cs.amount > 0)
            {
                return false;
            }
        }
        return true;
    }
    public int getCoinAmount()
    {
        int amount = 0;
        foreach (CoinStack cs in coinStacks)
        {
            amount += cs.amount;
        }
        return amount;
    }
}
