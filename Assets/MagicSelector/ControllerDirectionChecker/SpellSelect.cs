using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;






public class SpellSelect : MonoBehaviour
{
    [SerializeField] private InputActionReference selectButtonReference;
    [SerializeField] private GameObject spellSelectWheel;
    [SerializeField] private Transform cameraDirection;
    [SerializeField] private GameObject XROrigin;
    [SerializeField] private Transform runeCentre;
    [SerializeField] private SpellStateMachine stateMachine;
    private GameObject activeRune;

    [Header("TESTING OBJECT")]
    [SerializeField] private GameObject runeToSpawn;

    private bool isWheelActive = false;

    private void Start()
    {
        spellSelectWheel.GetComponent<DirectionManager>().spellSelector = this;
    }

    // Update is called once per frame
    void Update()
    {
        float selectValue = selectButtonReference.action.ReadValue<float>();
        //spawns the select wheel when button is held
        if (selectValue == 1f && !isWheelActive)
        {
            isWheelActive = true;

            spellSelectWheel.transform.position = transform.position;
            spellSelectWheel.transform.rotation = cameraDirection.rotation;
            spellSelectWheel.transform.rotation = Quaternion.Euler(-90, spellSelectWheel.transform.eulerAngles.y, spellSelectWheel.transform.eulerAngles.z);

            // it makes sense that pulling "outwards" from chest" would perform the same spell on each hand
            // this means that one side has to be mirrored. I will have to remember that on the left hand (my non-dominant hand)
            // that Left = right and vice versa
            if (spellSelectWheel.GetComponent<DirectionManager>().controllerToCheck == ControllerSide.LEFT)
            {
                spellSelectWheel.transform.Rotate(eulers:(Vector3.forward * 180), Space.Self);
            }
            spellSelectWheel.SetActive(isWheelActive);
        }
        // deletes the spell circle if button is released
        else if (selectValue == 0f)
        {
            isWheelActive = false;
            spellSelectWheel.SetActive(isWheelActive);
        }
    }

    // waits for spell enum to be given from DirectionManager, to check the spell and cast it if its valid
    public void CheckForSpell(int spellHash)
    {
        stateMachine.ChangeState(spellHash, spellSelectWheel.GetComponent<DirectionManager>().controllerToCheck);
    }
}
