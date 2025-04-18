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
    static public PlayerSpellStateMachine stateMachine;
    
    public void OnStateEnter(ControllerSide controller);
    public void OnStateLeave();
    public int GetManaCost();

    public int GetSpellHash();
}
