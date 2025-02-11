using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum RuneSpawnPosition
{
    IN_FRONT_OF_PALM, IN_BACK_OF_PALM, IN_FRONT_OF_FIST
};


// handles placing the rune onto the hand
public class HandSpell : MonoBehaviour
{
    public static SpellStateMachine spellStateMachine;
    [SerializeField] private ControllerSide controller;
    [SerializeField] private InputActionReference selectButtonReference;

    public SpellBase currentSpell;
    private GameObject currentRune = null;
    [Header("Put GameObjects where runes will spawn")]
    [SerializeField] private GameObject frontOfPalm;
    [SerializeField] private GameObject backOfPalm;
    [SerializeField] private GameObject fist;
    public static GameObject[] locations;

    private void Start()
    {
        spellStateMachine.handSpells[(int)controller] = this;
        locations = new GameObject[3];
        locations[(int)RuneSpawnPosition.IN_BACK_OF_PALM] = backOfPalm; 
        locations[(int)RuneSpawnPosition.IN_FRONT_OF_PALM] = frontOfPalm; 
        locations[(int)RuneSpawnPosition.IN_FRONT_OF_FIST] = fist; 
    }

    // SPAWN SPELL HERE 
    public void ChangeSpell(SpellBase spell)
    {
        if (currentRune != null)
        {
            Destroy(currentRune);
        }
        currentSpell = spell;
        currentRune = currentSpell.GetRune();

        //currentRune = Instantiate(currentRune, transform.position, transform.rotation, locations[(int)currentSpell.GetSpawnPosition()].transform);
        if (currentRune != null)
        {
            currentRune = Instantiate(currentRune, locations[(int)currentSpell.GetSpawnPosition()].transform);
        }

    }

    private void Update()
    {
        float selectValue = selectButtonReference.action.ReadValue<float>();
        // deletes the rune  if button is released
        if (selectValue == 0f && currentRune != null)
        {
            // only 1 projectile can be fired at a time (2 is just one after the other very quickly) so I set the spawn for this projectile here
            currentSpell.OnCast();
            Destroy(currentRune);
            currentRune = null;
        }
    }
}
