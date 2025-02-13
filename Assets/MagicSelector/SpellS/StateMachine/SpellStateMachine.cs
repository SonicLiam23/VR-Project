using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// will contain 2 state machines, one for the left and one for the right hand, these will both be stored here as some spells will have interactions with each other, so storing them together helps
public class SpellStateMachine : MonoBehaviour
{
    public List<ISpellState> spellsList;
    public Dictionary<int, ISpellState> spells;

    private ISpellState[] handStates;

    [SerializeField] private InputActionReference Left_selectButtonReference;
    [SerializeField] private InputActionReference Right_selectButtonReference;

    private void Awake()
    {
        spellsList = new List<ISpellState>();
        ISpellState.stateMachine = this;
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
        handStates[(int)ControllerSide.LEFT] = spells[-1];
        handStates[(int)ControllerSide.RIGHT] = spells[-1];
    }

    private void Update()
    {
        // if there is a spell on each hand and you let go of the grip, exit the current spell state and set it to no spell

        float leftSelect = Left_selectButtonReference.action.ReadValue<float>();
        if (handStates[(int)ControllerSide.LEFT] != spells[-1] && leftSelect == 0f)
        {
            ChangeState(-1, ControllerSide.LEFT);
        }

        float rightSelect = Right_selectButtonReference.action.ReadValue<float>();
        if (handStates[(int)ControllerSide.RIGHT] != spells[-1] && rightSelect == 0f)
        {
            ChangeState(-1, ControllerSide.RIGHT);
        }
    }


    // some utils that the state machine requires
    public bool ChangeState(int spellHash, ControllerSide controller)
    {
        bool spellFound = true;
        if (!spells.ContainsKey(spellHash))
        {
            // no spell index
            spellHash = -1;

            spellFound = false;
        }
        
        int controllerInd = (int)controller;
        handStates[controllerInd].OnStateLeave();
        handStates[controllerInd] = spells[spellHash];
        handStates[controllerInd].OnStateEnter(controller);

        //handStates.spa

        return spellFound;
    }
}
