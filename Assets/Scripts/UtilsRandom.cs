using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilsRandom
{
    static private System.Random s_random = new System.Random();

    static public bool CheckChance(float chance)
    {
        if (s_random.NextDouble() <= chance)
            return true;
        
        return false;
    }

    static public int GetRandomNumber(int min, int max)
    {
        return s_random.Next(min, max + 1);
    }

    static public Color GetRandomColor()
    {
        float r = Convert.ToSingle(s_random.NextDouble());
        float g = Convert.ToSingle(s_random.NextDouble());
        float b = Convert.ToSingle(s_random.NextDouble());
        
        return new Color(r, g, b);
    }
}
