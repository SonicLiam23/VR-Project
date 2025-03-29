using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private GameManager() { }

    public static GameManager Instance { get; private set; }
    public UnityEvent PlayerDied;
    public UnityEvent PlayerStart;
    public PlayerHealth mainPlayersHealth;
    public bool spawnersActive { get; private set; }
    [SerializeField] private GameObject spawners;

    private void Awake()
    { 
        if (Instance == null) Instance = this;
        Instance.spawnersActive = false;
    }

    

    public void ToggleSpawning()
    {
        Instance.spawnersActive = !Instance.spawnersActive;
        Instance.spawners.SetActive(Instance.spawnersActive);
    }
}
