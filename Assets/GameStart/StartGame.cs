using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    private List<Material> materials;
    [SerializeField] private Color flashColour;    
    List<Color> startColours;
    [SerializeField] GameObject[] portalComponents;
    [SerializeField] private float flashDuration;
    public AnimationCurve curve;
     

    private void Awake()
    {
        materials = new();
        startColours = new();
        for(int i = 0; i < portalComponents.Length; ++i)
        {
            materials.Add(new Material(portalComponents[i].GetComponent<Renderer>().material));
            startColours.Add(materials[i].GetColor("_BaseColor"));
            portalComponents[i].GetComponent<Renderer>().material = materials[i];
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        // if the spawners arent active ...
        if (!GameManager.Instance.spawnersActive)
        {
            // ... and it was a hand that touched it
            if (other.gameObject.CompareTag("LeftHand") || other.gameObject.CompareTag("RightHand"))
            {
                StartCoroutine(FlashPortal(flashDuration));
                GameManager.Instance.PlayerStart.Invoke();
            }    
        }
    }

    IEnumerator FlashPortal(float duration = 5f)
    {
        float time = 0f;
        
        for(int i = 0;i < materials.Count; ++i)
        {
            materials[i].color = flashColour;
        }
        

        while (time < duration)
        {
            float t = curve.Evaluate(time/duration);
            for (int i = 0; i < materials.Count; ++i)
            {
                materials[i].SetColor("_BaseColor", Color.Lerp(flashColour, startColours[i], t));
            }
            time += Time.deltaTime;
            yield return null;
        }

        for (int i = 0; i < materials.Count; ++i)
        {
            materials[i].color = startColours[i];
        }
    }
}
