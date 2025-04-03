
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField] private string soundName;
    [SerializeField] private bool playOnEnable;
    private void OnEnable()
    {
        if (playOnEnable)
        {
            Play();
        }
    }

    public void Play()
    {
        AudioManager.instance.Play(soundName);
    }
}
