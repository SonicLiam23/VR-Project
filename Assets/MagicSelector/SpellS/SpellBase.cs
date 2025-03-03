using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Drawing;
using UnityEngine;

public abstract class SpellBase : MonoBehaviour, ISpellState
{
    protected Transform projSpawn;
    [Header("Customise Spell")]
    [SerializeField] protected GameObject runePrefab;
    protected GameObject spawnedRune;
    [SerializeField] protected ObjectPool projectilePool;
    [SerializeField] private RuneSpawnPosition spawnPosition;
    [SerializeField] protected List<Direction> spellGesture = new();

    protected int hash;
    [SerializeField] protected ControllerSide controllerSide;

    [SerializeField] protected int manaCost;

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
        controllerSide = controller;
        // remove any rune from a previous spell after a small delay (to show a "switch" between runes)
        if (spawnedRune != null)
        {
            Destroy(spawnedRune, 1f);
        }
        if (runePrefab != null)
        {
            spawnedRune = Instantiate(runePrefab, RunePositions.GetRuneSpawnPositionObject(controllerSide, spawnPosition).transform);
        }
        
    }
    virtual public void OnStateLeave()
    {
        // runs when a spell is selected and you let go of the grip, so by default, cast spell here
        OnCast();
        if (spawnedRune != null)
        {
            Destroy(spawnedRune);
        }
        // if the spell actually costs mana and mana isnt already being regenerated from a previous spell
        if (manaCost > 0 && !ISpellState.stateMachine.manaSystem.isRegeneratingMana)
        {
            StartCoroutine(ISpellState.stateMachine.manaSystem.RegenMana());
        }
    }

    virtual public void OnCast(Transform position)
    {
        // default cast. Release button -> fire projectile -> leave this spell state. 
        // can be overriden to create unique spell/firing effects for each spell
        if (projectilePool != null)
        {
            projSpawn = position;
            GameObject firedProj = projectilePool.GetObject(true);
            firedProj.transform.SetPositionAndRotation(projSpawn.position, projSpawn.rotation);
        }
        ISpellState.stateMachine.manaSystem.SpendMana(manaCost);
    }

    virtual public void OnCast()
    {
        // cast spell, but default it to the runes current position
        OnCast(spawnedRune.transform);
    }

    public int GetManaCost()
    {
        return manaCost;
    }

    
    // Start is called before the first frame update
    virtual protected void Start()
    {
        hash = Hash.GetHash(spellGesture);
        ISpellState.stateMachine.spellsList.Add(this);
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        
    }

}
