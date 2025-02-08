using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.Interaction.Toolkit;

public class Shield : SpellBase, ISpellState
{
    override protected void Start()
    {
        //ADD GESTR
        spellGesture.Add(Direction.RIGHT_DOWN);

        base.Start();

        ISpellState.stateMachine.spellsList.Add(this);
    }

    public void OnCast()
    {
        
    }

    override public void OnStateEnter()
    {
        base.OnStateEnter();
    }

    override public void OnStateLeave()
    {
        base.OnStateLeave();
    }
}
