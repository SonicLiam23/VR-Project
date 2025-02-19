using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// my own hashing method as XOR hashing cancells out 2 values of the same
public class Hash
{

    public static int GetHash(List<Direction> list)
    {
        int mult = 31;
        int hash = 17; //start with prime
        foreach (Direction dir in list)
        {
            hash = hash * mult + (int)dir;
            mult += 2;
            Debug.Log(hash);
        }

        return hash;
    }

    public static int GetHash(Direction dir, int mult = 31)
    {
        int hash = 17;
        hash = hash *  mult + (int)dir;

        return hash;
    }
}
