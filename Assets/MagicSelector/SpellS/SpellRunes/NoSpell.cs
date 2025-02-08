using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoSpell : SpellBase, ISpellState
{
    public void OnCast()
    {
        
    }

    override public void OnStateEnter()
    {
        base.OnStateEnter();
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
