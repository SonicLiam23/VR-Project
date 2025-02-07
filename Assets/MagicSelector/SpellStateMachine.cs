using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

// will contain 2 state machines, one for the left and one for the right hand, these will both be stored here as some spells will have interactions with each other, so storing them together helps
public class SpellStateMachine : MonoBehaviour
{
    // Start of spell states
    [SerializeField] private ISpellState shieldSpell;

    // End of spell states

    [SerializeField] public List<ISpellState> spellsList;
    public Dictionary<int, ISpellState> spells;

    private ISpellState[] handStates;

    public HandSpell[] handSpells;

    private void Awake()
    {
        spellsList = new List<ISpellState>();
        handSpells = new HandSpell[2];
        ISpellState.stateMachine = this;
        HandSpell.spellStateMachine = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        spells = new Dictionary<int, ISpellState>();
        handStates = new ISpellState[2];
 
 
        
        

        // each IState is set to add itself to the spellsList.
        // now we populate the dictionary
        foreach (ISpellState s in spellsList)
        {
            spells.Add(s.GetSpellHash(), s);
        }
              // set both hands to no spell
        handStates[0] = spells[-1];
        handStates[1] = spells[-1];
    }


    // some utils that the state machine requires
    public void ChangeState(int spellHash, ControllerSide controller)
    {
        if (!spells.ContainsKey(spellHash))
        {
            // no spell index
            spellHash = -1;
        }
        
        int controllerInd = (int)controller;
        handStates[controllerInd].OnStateLeave();
        handStates[controllerInd] = spells[spellHash];
        handStates[controllerInd].OnStateEnter();

        //handStates.spa

        handSpells[controllerInd].ChangeSpell(handStates[controllerInd]);
    }

    public int GetXORHashFromList(List<Direction> list)
    {
        int hash = 0;
        foreach (Direction direction in list)
        {
            hash ^= direction.GetHashCode();
        }

        return hash;
    }
}
