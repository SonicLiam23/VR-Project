using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.XR.CoreUtils;
using UnityEngine;

public enum ControllerSide
{
    LEFT, RIGHT, NONE
}

// manages passing the required direction and controller info to the GestureManager (or similar) that will take the direction and cast the appropriate spell
// this also gives the individual direction scripts some basic functionality they need
public class DirectionManager : MonoBehaviour
{
    // set in editor
    [SerializeField] public ControllerSide controllerToCheck = ControllerSide.NONE;
    [HideInInspector] public SpellSelect spellSelector;
    // Returns the tag name given an ControllerSide enum
    public string GetTagFromEnum(ControllerSide controller)
    {
        switch (controller)
        {
            case ControllerSide.LEFT:
                return "LeftHand";

            case ControllerSide.RIGHT:
                return "RightHand";

            default:
                return "";
        }
    }

    public void SendDirection(Direction dir)
    {
        // send direction + controller to spell/gesture manager here
        spellSelector.SpellPerformed(GestureToSpell.ConvertGesture(dir));

        // for now, log the direction + controller
        Debug.Log("Controller: " + controllerToCheck + " Direction: " +  dir);

        // once the direction has been sent, destroy this object
        Destroy(gameObject);
    }
}


