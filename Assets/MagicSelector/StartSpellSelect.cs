using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StartSpellSelect : MonoBehaviour
{
    [SerializeField] private InputActionReference selectButtonReference;
    [SerializeField] private GameObject spellSelectWheel;
    [SerializeField] private Transform cameraDirection;
    private bool isWheelActive = false;

    // Update is called once per frame
    void Update()
    {
        float selectValue = selectButtonReference.action.ReadValue<float>();
        if (selectValue == 1f && !isWheelActive)
        {
            isWheelActive = true;
            GameObject wheel = Instantiate(spellSelectWheel, transform.position, cameraDirection.rotation, null);
            wheel.transform.rotation = Quaternion.Euler(-90, wheel.transform.eulerAngles.y, wheel.transform.eulerAngles.z);
        }
        else if (selectValue == 0f)
        {
            isWheelActive = false;
        }
    }
}
