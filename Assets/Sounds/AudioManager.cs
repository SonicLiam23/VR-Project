using UnityEngine.Audio;
using UnityEngine;


public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;
    public Sound[] sounds;
    bool initialized = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
        }
    }

    private void Start()
    {
        initialized = true;
    }

    public void Play(string name)
    {
        // I only want sound to play AFTER the game has started. As projectiles use OnEnable, they will be created before the game starts, and all sounds will play at once. This prevents that.
        if (initialized)
        {
            Sound s = System.Array.Find(sounds, sound => sound.name == name);
            if (s != null)
            {
                s.source.PlayOneShot(s.clip, 0.7f);
            }
            else
            {
                Debug.LogWarning("Sound: " + name + " not found!");
            }
        }
    }
}
