using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// THESE ARE SET TO RANDOM NUMBERS TO DECREASE THE LIKELYHOOD OF HASH COLLISIONS    
// yes, it was minimal anyway but when testing it would perform a random spell when testing lots of directions
public enum Direction
{
    UP = 6259, DOWN = 173, LEFT = 9023, RIGHT = 4783, LEFT_DOWN = 2367, RIGHT_DOWN = 7591, LEFT_UP = 3921, RIGHT_UP = 821
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
