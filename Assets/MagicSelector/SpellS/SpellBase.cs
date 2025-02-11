using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Drawing;
using UnityEngine;

public class SpellBase : MonoBehaviour
{
    private Transform projSpawn;
    [Header("Customise Spell")]
    [SerializeField] protected GameObject runeToSpawn;
    [SerializeField] protected GameObject projectile;
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
        if (runeToSpawn != null)
        {
            runeToSpawn.SetActive(true);
        }
        

    }
    virtual public void OnStateLeave()
    {
        if (runeToSpawn != null)
        {
            runeToSpawn.SetActive(false);
        }

    }

    public void OnCast()
    {
        if (projectile != null)
        {
            projSpawn = HandSpell.locations[(int)spawnPosition].transform;
            Instantiate(projectile, projSpawn.position, projSpawn.rotation);
        }
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
