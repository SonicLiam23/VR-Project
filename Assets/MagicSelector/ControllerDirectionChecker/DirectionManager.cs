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
    public ControllerSide controllerToCheck = ControllerSide.NONE;
    [HideInInspector] public SpellSelect spellSelector;
    private List<Direction> currentGesture = new();

    private void OnEnable()
    {
        // new spell being drawn, reset hash
        currentGesture.Clear();
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
        currentGesture.Add(dir);
        // incrementally update hash
        // currentGestureHash *= Hash.GetHash(dir);
        spellSelector.CheckForSpell(Hash.GetHash(currentGesture));

        // for now, log the direction + controller
        // Debug.Log("Controller: " + controllerToCheck + " Direction: " +  dir);
    }
}


