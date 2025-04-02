using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerVignette : MonoBehaviour
{
    [SerializeField] public float intensity;

    PostProcessVolume volume;
    Vignette vignette;

    // Start is called before the first frame update
    void Start()
    {
        intensity = 0f;
        volume = GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out vignette);

        if (vignette != null)
        {
            vignette.enabled.Override(false);
        }
    }

    public IEnumerator StartEffect()
    {
        intensity = 0.4f;
        vignette.enabled.Override(true);
        vignette.intensity.Override(intensity);

        yield return new WaitForSeconds(0.1f);

        while (intensity >= 0.1f)
        {
            intensity -= 0.1f;
            vignette.intensity.Override(intensity);
            yield return new WaitForSeconds(0.1f);
        }

        vignette.enabled.Override(false);
        yield break;
    }
}
