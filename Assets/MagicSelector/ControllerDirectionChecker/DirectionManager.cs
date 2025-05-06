using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.OpenXR.Input;

public enum ControllerSide
{
    LEFT = 0, RIGHT = 1, NONE
}

// manages passing the required direction and controller info to the GestureManager (or similar) that will take the direction and cast the appropriate spell
// this also gives the individual direction scripts some basic functionality they need
public class DirectionManager : MonoBehaviour
{
    // set in editor
    public ControllerSide controllerToCheck = ControllerSide.NONE;
    [HideInInspector] public SpellSelect spellSelector;
    private List<Direction> currentGesture = new();

    [SerializeField] InputActionReference controllerHaptics;
    XRBaseController controller;
    bool hapticsEnabled = false;


    private void Start()
    {
        if (controllerToCheck != ControllerSide.NONE) controller = GameObject.FindGameObjectWithTag((controllerToCheck == ControllerSide.LEFT ? "LeftHand" : "RightHand")).GetComponent<XRBaseController>();

        if (controller)
        {
            hapticsEnabled = true;
        }
    }

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
        if (hapticsEnabled) controller.SendHapticImpulse(1f, 0.1f);
        spellSelector.CheckForSpell(Hash.GetHash(currentGesture));
    }
}


