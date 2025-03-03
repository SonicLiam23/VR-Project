using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// start needs to be run after all spell base starts (so theyre added to the spellsList)
[DefaultExecutionOrder(1)]
// will contain 2 state machines, one for the left and one for the right hand, these will both be stored here as some spells will have interactions with each other, so storing them together helps
public class PlayerSpellStateMachine : MonoBehaviour
{
    public List<ISpellState> spellsList;
    public Dictionary<int, ISpellState> spells;

    private ISpellState[] handStates;

    [SerializeField] private InputActionReference Left_selectButtonReference;
    [SerializeField] private InputActionReference Right_selectButtonReference;
    [SerializeField] public ManaComponent manaSystem;

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
        int controllerInd = (int)controller;
        // will always flip between 0 and 1;
        int otherController = (controllerInd * -1) + 1;

        if (!spells.ContainsKey(spellHash))
        {
            // no spell index, if you try to draw another spell with one equiped (which should be impossible, but gotta account for everything), remove the spell

            spellHash = -1;

            spellFound = false;
        }
        else if (spells[spellHash].GetManaCost() > manaSystem.mana || handStates[otherController] == spells[spellHash])
        {

            // OR if a spell has been found, check there is enough mana

            // OR make sure the same spell cant be on both hands (will handle 2 handed spells later)

            // it found a spell, so return spellFound as true, to terminate the spell, selection
            spellHash = -1;
        }

        handStates[controllerInd].OnStateLeave();
        handStates[controllerInd] = spells[spellHash];
        handStates[controllerInd].OnStateEnter(controller);

        return spellFound;
    }
}
