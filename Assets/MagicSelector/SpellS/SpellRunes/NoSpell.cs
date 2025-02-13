using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoSpell : SpellBase
{
    public void OnCast()
    {
        
    }

    override public void OnStateEnter(ControllerSide controller)
    {
        base.OnStateEnter(controller);
    }
      
    override public void OnStateLeave()
    {
        
    }

    // Start is called before the first frame update
    override protected void Start()
    {
        ISpellState.stateMachine.spellsList.Add(this);
        hash = -1;
    }
}
