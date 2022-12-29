using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nobel : Card
{
    public Nobel()
    {
        color = 0;
        points = 3;
    }

    override public bool pay(Player player)
    {
        Debug.Log("Nobel");

        if (player.hasNobel == false)
        {
            CoinCollection cost = new(this.costs);
            cost -= player.cardBalance;

            if (cost.isZero())
            {
                player.hasNobel = true;
                move(player.npos.position);
                return true;
            }
        }
        return false;
    }
}
