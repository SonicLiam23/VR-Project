using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum direction
{
    UP, DOWN, LEFT, RIGHT, LEFT_DOWN, RIGHT_DOWN, LEFT_UP, RIGHT_UP
}

public enum ControllerSide
{
    LEFT, RIGHT, NONE
}


public class ObjectDirection : MonoBehaviour
{
    //set in editor
    [SerializeField] private direction Direction;
    [SerializeField] private ControllerSide currentController = ControllerSide.NONE;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GetTagFromEnum(currentController)))
        {
            Debug.Log(Direction + " " + currentController);
            Destroy(transform.parent.gameObject);
        }
    }

    private string GetTagFromEnum(ControllerSide controller)
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
}
