using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] private Material materialToChange;
    [SerializeField] private Color flashColour;
    Color startColour;
    
    
    private void Awake()
    {
        startColour = materialToChange.GetColor("Color_da8fc87eb56f4cd9912e2daa94a68802");
    }

    private void OnTriggerEnter(Collider other)
    {
        // if the spawners arent active ...
        if (!GameManager.Instance.spawnersActive)
        {
            // ... and it was a hand that touched it
            if (other.gameObject.CompareTag("LeftHand") || other.gameObject.CompareTag("RightHand"))
            {
                StartCoroutine(FlashPortal());
                GameManager.Instance.PlayerStart.Invoke();
            }    
        }
    }

    IEnumerator FlashPortal(float duration = 5f)
    {
        float time = 0f;
        

        while (time < duration)
        {
            materialToChange.SetColor("Color_da8fc87eb56f4cd9912e2daa94a68802", Color.Lerp(flashColour, startColour, time / duration));
            time += Time.deltaTime;
            yield return null;
        }
        materialToChange.color = startColour;
    }
}
