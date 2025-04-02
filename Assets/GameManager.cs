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
    public int maxEnemies;
    
    [SerializeField] private GameObject spawners;
    public bool spawnersActive { get { return spawners.activeInHierarchy; } }

    private void Awake()
    { 
        if (Instance == null) Instance = this;
        
    }

    

    public void ToggleSpawning()
    {
        Instance.spawners.SetActive(!Instance.spawnersActive);
    }
}
