using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Card : MonoBehaviour
{
    public GameEvent PurchasedEvent;
    public const float upperBrimlength = 0.05f;
    public int reservedBy = 0;
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
    public int slotIndex;
    public Type type;

    // Move card to given position
    public void move(Vector3 position)
    {
        this.transform.position = transform.position;
    }

    // Pay for the card
    virtual public bool pay(Player player)
    {
        Debug.Log("Card");

        if (reservedBy == 0 || reservedBy == player.Id){
            CoinCollection cost = new(this.costs);

            cost -= player.cardBalance;

            // if card free
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
                    purchase(player, cost);
                }
                else
                {
                    return false;
                }

            }
            return true;
        }
        else
        {
            return false;
        }
    }

    private void purchase(Player player,CoinCollection cost)
    {
        int index = (int)this.color;

        // Set Player balance
        player.coinsBalance -= cost;
        player.cardBalance.coinStacks[index].amount++;

        // Move Card and disable
        this.transform.position = player.cardPos[index].position;
        player.cardPos[index].position += new Vector3(-upperBrimlength, 0.001f, 0);
        BoxCollider collider = GetComponent<BoxCollider>();
        collider.enabled = false;

        // If card was reserved
        if(reservedBy > 0)
        {
            player.freeReservedPos[this.slotIndex] = true;
        }
        else // else purchased from the table
        {
            PurchasedEvent.Raise(this);     // Draw new card
        }

        PurchasedEvent.Raise(cost);     // Raise the amount of the chip bank
    }

    public void reserve(Transform transform, int id, int slotIndex)
    {
        this.reservedBy = id;
        this.move(transform.position);
        this.slotIndex = slotIndex;
        PurchasedEvent.Raise(this);     //draw new card
    }
}
