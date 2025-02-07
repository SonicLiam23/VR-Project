using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum Direction
{
    UP, DOWN, LEFT, RIGHT, LEFT_DOWN, RIGHT_DOWN, LEFT_UP, RIGHT_UP
}

public class ObjectDirection : MonoBehaviour
{
    // set in editor for each direction
    [SerializeField] private Direction direction;

    private DirectionManager manager;


    private void Start()
    {
        manager = transform.parent.gameObject.GetComponent<DirectionManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // check if the the controller that collided with this direction is the one that summoned it
        if (other.CompareTag(manager.GetTagFromEnum(manager.controllerToCheck)))
        {
            manager.AddDirectionToList(direction);
        }
    }
}
