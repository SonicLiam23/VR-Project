using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Drawing;
using UnityEngine;

public class SpellBase : MonoBehaviour, ISpellState
{
    private Transform projSpawn;
    [Header("Customise Spell")]
    [SerializeField] protected GameObject runePrefab;
    protected GameObject spawnedRune;
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] private RuneSpawnPosition spawnPosition;
    [SerializeField] protected List<Direction> spellGesture = new();
    protected int hash;
    static protected RunePositions runePositions = new();

    public GameObject GetRune()
    {
        return runePrefab;
    }
    public int GetSpellHash()
    {
        return hash;
    }

    virtual public void OnStateEnter(ControllerSide controller)
    {
        // remove any rune from a previous spell after a small delay (to show a "switch" between runes)
        if (spawnedRune != null)
        {
            Destroy(spawnedRune, 1f);
        }
        if (runePrefab != null)
        {
            spawnedRune = Instantiate(runePrefab, runePositions.GetRuneSpawnObject(controller, spawnPosition).transform);
        }
        
    }
    virtual public void OnStateLeave()
    {
        // runs when a spell is selected and you let go of the grip, so by default, cast spell here
        OnCast();
        if (runePrefab != null)
        {
            Destroy(spawnedRune);
        }

    }

    virtual public void OnCast(Transform position)
    {
        // default cast. Release button -> fire projectile -> leave this spell state. 
        // can be overriden to create unique spell/firing effects for each spell
        if (projectilePrefab != null)
        {
            projSpawn = position;
            Instantiate(projectilePrefab, projSpawn.position, projSpawn.rotation);
        }
    }

    virtual public void OnCast()
    {
        // cast spell, but default it to the runes current position
        OnCast(spawnedRune.transform);
    }

    
    // Start is called before the first frame update
    virtual protected void Start()
    {
        hash = Hash.GetHash(spellGesture);
        Debug.Log("ShieldHash: " + hash);
        ISpellState.stateMachine.spellsList.Add(this);
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        
    }
}
