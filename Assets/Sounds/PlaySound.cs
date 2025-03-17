
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField] private string soundName;
    [SerializeField] private bool playOnEnable;
    private void OnEnable()
    {
        //AudioManager.instance.Play(soundName);
    }

    public void Play()
    {
        AudioManager.instance.Play(soundName);
    }
}
