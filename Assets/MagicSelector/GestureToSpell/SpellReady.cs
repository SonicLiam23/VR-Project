using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Transactions;
using UnityEngine;

// spanws the rune at the hand, signalling to the player that the spell is ready to cast
public class SpellReady : MonoBehaviour
{
    [SerializeField] private GameObject currentRune = null;

    public void GetAndSpawnRune(Spells spell)
    {


        currentRune = Instantiate(currentRune, transform);
    }
}
