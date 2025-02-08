using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBase : MonoBehaviour
{
    [Header("Customise Spell")]
    [SerializeField] protected GameObject runeToSpawn;
    [SerializeField] private RuneSpawnPosition spawnPosition;

    protected int hash;

    public GameObject GetRune()
    {
        return runeToSpawn;
    }
    public RuneSpawnPosition GetSpawnPosition()
    {
        return spawnPosition; 
    }
    public int GetSpellHash()
    {
        return hash;
    }

    virtual public void OnStateEnter()
    {
        runeToSpawn.SetActive(true);

    }
    virtual public void OnStateLeave()
    {
        runeToSpawn.SetActive(false);
    }


    protected List<Direction> spellGesture = new();
    // Start is called before the first frame update
    virtual protected void Start()
    {
        hash = ISpellState.stateMachine.GetXORHashFromList(spellGesture);
        Debug.Log("ShieldHash: " + hash);
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        
    }
}
