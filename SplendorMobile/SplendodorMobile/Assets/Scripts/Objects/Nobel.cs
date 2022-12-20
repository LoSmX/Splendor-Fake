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

    override public bool buy(Player player)
    {
        if(player.hasNobel == true)
        {
            return false;
        }
        else
        {
            CoinCollection cost = new(this.costs);
            cost -= player.cardBalance;

            if(cost.isZero())
            {
                player.hasNobel = true;
                player.score += this.points;
                player.turnActionCounter = 3;
                this.transform.position = player.npos.position;
            }
            else
            {
                return false;
            }
        }
        return true;
    }
}
