using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UserUtils
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
}
