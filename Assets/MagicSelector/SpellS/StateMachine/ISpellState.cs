using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/*
 * spells that cast instantly (such as a shield or "push") will have their cast behaviour in "OnStateEnter"
 * spells that require a release of the button (such as projectiles) will have their behaviour in "OnCast()"
 */




public interface ISpellState
{
    static public SpellStateMachine stateMachine;
    public void OnStateEnter();
    public void OnStateLeave();
    public void OnCast();
    
    // in SpellBase.cs
    public GameObject GetRune();
    public RuneSpawnPosition GetSpawnPosition();
    public int GetSpellHash();
}
