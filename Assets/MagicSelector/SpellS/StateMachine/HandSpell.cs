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

    public ISpellState currentSpell;
    private GameObject currentRune = null;
    [Header("Put GameObjects where runes will spawn")]
    [SerializeField] private GameObject frontOfPalm;
    [SerializeField] private GameObject backOfPalm;
    [SerializeField] private GameObject fist;
    private GameObject[] locations;

    private void Start()
    {
        spellStateMachine.handSpells[(int)controller] = this;
        locations = new GameObject[3];
        locations[(int)RuneSpawnPosition.IN_BACK_OF_PALM] = backOfPalm; 
        locations[(int)RuneSpawnPosition.IN_FRONT_OF_PALM] = frontOfPalm; 
        locations[(int)RuneSpawnPosition.IN_FRONT_OF_FIST] = fist; 
    }

    // SPAWN SPELL HERE 
    public void ChangeSpell(ISpellState spell)
    {
        if (currentRune != null)
        {
            Destroy(currentRune);
        }
        currentSpell = spell;
        currentRune = currentSpell.GetRune();

        //currentRune = Instantiate(currentRune, transform.position, transform.rotation, locations[(int)currentSpell.GetSpawnPosition()].transform);
        currentRune = Instantiate(currentRune, locations[(int)currentSpell.GetSpawnPosition()].transform);
    }

    private void Update()
    {
        float selectValue = selectButtonReference.action.ReadValue<float>();
        // deletes the rune  if button is released
        if (selectValue == 0f && currentRune != null)
        {
            Destroy(currentRune);
            currentRune = null;
        }
    }
}
