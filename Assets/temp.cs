using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temp : MonoBehaviour
{
    private void OnDestroy()
    {
        var time = GetComponent<TrailRenderer>().time;
        Debug.Log(time);
    }
}
