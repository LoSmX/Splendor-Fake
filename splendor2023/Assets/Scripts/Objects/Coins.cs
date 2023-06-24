using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins
{
    public const int  numOfColors = 6;
    public enum Color
    {
        white,
        brown,
        red,
        green,
        blue,
        gold,
    }

    public Color[] colorOrder =
    {
        Color.white,
        Color.brown,
        Color.red,
        Color.green,
        Color.blue,
        Color.gold
    };
}
