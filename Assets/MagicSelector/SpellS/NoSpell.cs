using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoSpell : MonoBehaviour, ISpellState
{
    public GameObject GetRune()
    {
        return null;
    }
    public int GetSpellHash()
    {
        return -1;
    }

    public void OnCast()
    {
        
    }

    public void OnStateEnter()
    {
        
    }

    public void OnStateLeave()
    {
        
    }

    public void PerformSpell()
    {
        
    }

    RuneSpawnPosition ISpellState.GetSpawnPosition()
    {
        // no spell so it doesnt matter really
        return RuneSpawnPosition.IN_BACK_OF_PALM;
    }

    // Start is called before the first frame update
    void Start()
    {
        ISpellState.stateMachine.spellsList.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
