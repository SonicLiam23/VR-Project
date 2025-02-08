using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// my own hashing method as XOR hashing cancells out 2 values of the same
public class Hash
{
    public static int GetHash(List<Direction> list)
    {
        int hash = 0;
        foreach (Direction dir in list)
        {
            hash += ((int)dir * 31);  
        }

        return hash;
    }

    public static int GetHash(Direction dir)
    {
        int hash = 0;
        hash = (int)dir * 31;

        return hash;
    }
}
