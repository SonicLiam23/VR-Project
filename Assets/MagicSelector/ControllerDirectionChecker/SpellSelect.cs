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
    [SerializeField] private PlayerSpellStateMachine stateMachine;


    private bool isWheelActive = false;
    private bool isSpellActive = false;
    private TrailRenderer trail;

    private void Start()
    {
        spellSelectWheel.GetComponent<DirectionManager>().spellSelector = this;
        trail = GetComponent<TrailRenderer>();
        trail.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        float selectValue = selectButtonReference.action.ReadValue<float>();
        //spawns the select wheel when button is held
        if (selectValue == 1f && !isWheelActive && !isSpellActive)
        {
            isWheelActive = true;

            SpawnSpellWheel();
            trail.Clear();
            trail.enabled = true;

            spellSelectWheel.SetActive(isWheelActive);
        }
        // deletes the spell wheel if button is released
        else if (selectValue == 0f)
        {
            isWheelActive = false;
            isSpellActive = false;
            spellSelectWheel.SetActive(isWheelActive);
            trail.enabled = false;
        }
    }

    // waits for spell enum to be given from DirectionManager, to check the spell and cast it if its valid
    public void CheckForSpell(int spellHash)
    {
        bool spellComplete;
        spellComplete = stateMachine.ChangeState(spellHash, spellSelectWheel.GetComponent<DirectionManager>().controllerToCheck);

        if (!spellComplete)
        {
            // resets the spell wheel position at the hand
            SpawnSpellWheel();
        }
        else
        {
            trail.enabled = false;
            isSpellActive = true;
            isWheelActive = false;
            spellSelectWheel.SetActive(isWheelActive);
        }
    }

    private void SpawnSpellWheel()
    {
        spellSelectWheel.transform.position = transform.position;

        //// it makes sense that pulling "outwards" from chest would perform the same spell on each hand
        //// this means that one side has to be mirrored. I will have to remember that on the left hand (my non-dominant hand)
        //// that Left = right and vice versa

        spellSelectWheel.transform.LookAt(Camera.main.transform.position);
    }
}
