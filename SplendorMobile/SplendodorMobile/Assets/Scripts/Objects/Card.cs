using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Card : MonoBehaviour
{
    public GameEvent PurchasedEvent;
    public const float upperBrimlength = 0.05f;
    public enum Type
    {
        green,
        yellow,
        blue,
        nobel,
    }

    public int[] costs = new int[5];
    public Coins.Color color;
    public int points;
    public int posIndex;
    public Type type;

    virtual public bool buy(Player player)
    {
     
        CoinCollection cost = new(this.costs);

        cost -= player.cardBalance;
        
        if (cost.isZero())
        {
            int index = (int)this.color;

            // Set Player Variables
            purchase(player, cost);
        }
        else
        {
            
            if (player.coinsBalance.isAffordable(cost))
            {
                player.coinsBalance -= cost;
                purchase(player, cost);
            }
            else
            {
                return false;
            }
            
        }
        return true;
    }

    private void purchase(Player player,CoinCollection cost)
    {
        int index = (int)this.color;

        // Set Player Variables
        player.cardBalance.coinStacks[index].amount++;
        player.score += this.points;
        player.cardPos[index].position += new Vector3(-upperBrimlength, 0.001f, 0);
        player.turnActionCounter = 3;

        // Move Card and disable
        this.transform.position = player.cardPos[index].position;
        BoxCollider collider = GetComponent<BoxCollider>();
        collider.enabled = false;

        // Call purchase event
        PurchasedEvent.Raise(this);
        PurchasedEvent.Raise(cost);
    }
}
