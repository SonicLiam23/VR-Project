using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Rendering;

public class Shield : MonoBehaviour, ISpellState
{
    [SerializeField] private GameObject rune;
    [SerializeField] public RuneSpawnPosition spawnPosition;
    private int hash;

    // you must do this for every spell
    private void Start()
    {
        List<Direction> spellGesture = new List<Direction>();
        spellGesture.Add(Direction.RIGHT_DOWN);

        hash = ISpellState.stateMachine.GetXORHashFromList(spellGesture);
        ISpellState.stateMachine.spellsList.Add(this);
    }

    public GameObject GetRune()
    {
        return rune;
    }

    public RuneSpawnPosition GetSpawnPosition()
    {
        return spawnPosition; 
    }

    public int GetSpellHash()
    {
        return hash;
    }

    public void OnCast()
    {
        
    }

    public void OnStateEnter()
    {
        rune.SetActive(true);
    }

    public void OnStateLeave()
    {
        rune.SetActive(false);
    }

    public void PerformSpell()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
