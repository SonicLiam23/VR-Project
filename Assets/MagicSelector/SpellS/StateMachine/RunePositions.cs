using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
public enum RuneSpawnPosition
{
    IN_FRONT_OF_PALM, BACK_OF_PALM, IN_FRONT_OF_FIST
};

public class RunePositions : MonoBehaviour
{
    [Header("Left Hand")]
    [SerializeField] private GameObject L_inFrontOfPalm;
    [SerializeField] private GameObject L_behindPalm;
    [SerializeField] private GameObject L_fist;
    [Header("Right Hand")]
    [SerializeField] private GameObject R_inFrontOfPalm;
    [SerializeField] private GameObject R_behindPalm;
    [SerializeField] private GameObject R_fist;


    private static GameObject[,] runePositions = { {null, null, null}, {null, null, null } };
    
    private void Awake()
    {
        runePositions[(int)ControllerSide.LEFT, (int)RuneSpawnPosition.IN_FRONT_OF_PALM] = L_inFrontOfPalm;
        runePositions[(int)ControllerSide.LEFT, (int)RuneSpawnPosition.BACK_OF_PALM] = L_behindPalm;
        runePositions[(int)ControllerSide.LEFT, (int)RuneSpawnPosition.IN_FRONT_OF_FIST] = L_fist;

        runePositions[(int)ControllerSide.RIGHT, (int)RuneSpawnPosition.IN_FRONT_OF_PALM] = R_inFrontOfPalm;
        runePositions[(int)ControllerSide.RIGHT, (int)RuneSpawnPosition.BACK_OF_PALM] = R_behindPalm;
        runePositions[(int)ControllerSide.RIGHT, (int)RuneSpawnPosition.IN_FRONT_OF_FIST] = R_fist;
    }

    public GameObject GetRuneSpawnObject(ControllerSide controller, RuneSpawnPosition spawnPosition)
    {
        return runePositions[(int)controller, (int)spawnPosition];
    }
}
