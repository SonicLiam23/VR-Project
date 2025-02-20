using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;

public class SpellPool : MonoBehaviour
{
    [SerializeField] private GameObject pooledProjectile;
    [SerializeField] private int projectilePoolSize;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < projectilePoolSize; i++)
        {
            Instantiate(pooledProjectile, this.transform).SetActive(false);
        }
    }

    public GameObject GetObject(Transform spawnTransform)
    {
        int children = transform.childCount;
        for (int i = 0; i < children; i++)
        {
            GameObject pooledSpell = transform.GetChild(i).gameObject;
            if (!pooledSpell.activeSelf)
            {
                pooledSpell.transform.position = spawnTransform.position;
                pooledSpell.transform.rotation = spawnTransform.rotation;
                pooledSpell.SetActive(true);
                return pooledSpell;
            }
        }

        return null;
    }

    public void ReturnObject(GameObject obj)
    {
        // ensures its in this pool
        if (obj.transform.parent.gameObject == this.gameObject)
        {
            obj.SetActive(false);
        }
    }
}
