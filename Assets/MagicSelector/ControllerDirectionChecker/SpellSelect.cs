using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpellSelect : MonoBehaviour
{
    [SerializeField] private InputActionReference selectButtonReference;
    [SerializeField] private GameObject spellSelectWheel;
    [SerializeField] private Transform cameraDirection;
    [SerializeField] private GameObject XROrigin;
    [SerializeField] private Transform runeCentre;
    private GameObject activeRune;
    // set to none, this will be set in editor for each controller
    [SerializeField] private ControllerSide thisController = ControllerSide.NONE;

    [Header("TESTING OBJECT")]
    [SerializeField] private GameObject runeToSpawn;

    private GameObject wheel;
    private bool isWheelActive = false;

    // Update is called once per frame
    void Update()
    {
        float selectValue = selectButtonReference.action.ReadValue<float>();
        //spawns the select wheel when button is held
        if (selectValue == 1f && !isWheelActive)
        {
            isWheelActive = true;
            wheel = Instantiate(spellSelectWheel, transform.position, cameraDirection.rotation, XROrigin.transform);
            wheel.transform.rotation = Quaternion.Euler(-90, wheel.transform.eulerAngles.y, wheel.transform.eulerAngles.z);

            // it makes sense that pulling "outwards" from chest" would perform the same spell on each hand
            // this means that one side has to be mirrored. I will have to remember that on the left hand (my non-dominant hand)
            // that Left = right and vice versa
            if (thisController == ControllerSide.LEFT)
            {
                wheel.transform.Rotate(eulers:(Vector3.forward * 180), Space.Self);
            }

            wheel.GetComponent<DirectionManager>().controllerToCheck = thisController;
            wheel.GetComponent<DirectionManager>().spellSelector = this;
        }
        // deletes the spell circle if button is released
        else if (selectValue == 0f)
        {
            if (wheel != null)
            {
                Destroy(wheel);
                wheel = null;
            }
            if (activeRune != null)
            {
                Destroy(activeRune);
                activeRune = null;
            }
            
            isWheelActive = false;
        }
    }

    // waits for spell enum to be given from DirectionManager, to spawn the spell
    public void SpellPerformed(Spells spell)
    {
        // get correct game object from spell, for now, just 1 rune
        // in future, this will hopefully tell a statemachine to change state to
        // the correct spell and the rest is handled from there
        switch (spell)
        {
            case Spells.Shield:
                // temp whilst i have no way of chosing each rune
                activeRune = runeToSpawn;
                break;

        }

        activeRune = Instantiate(activeRune, runeCentre);
    }
}
