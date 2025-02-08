using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private int currentGestureHash;

    private void OnEnable()
    {
        // new spell being drawn, reset hash
        currentGestureHash = 0;
    }



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

    public void AddDirectionToList(Direction dir)
    {
        // incrementally update hash
        currentGestureHash += Hash.GetHash(dir);
        spellSelector.CheckForSpell(currentGestureHash);
        Debug.Log(currentGestureHash);

        // for now, log the direction + controller
        // Debug.Log("Controller: " + controllerToCheck + " Direction: " +  dir);
    }
}


