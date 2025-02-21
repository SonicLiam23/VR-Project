using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject pooledProjectile;
    [SerializeField] private int projectilePoolSize;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < projectilePoolSize; i++)
        {
            Instantiate(pooledProjectile, this.transform);

        }
    }

    public GameObject GetObject(Transform spawnTransform, bool instantiateIfUnavailable = false)
    {
        int children = transform.childCount;
        GameObject pooledObject = null;

        for (int i = 0; i < children; i++)
        {
            pooledObject = transform.GetChild(i).gameObject;
            if (!pooledObject.activeSelf)
            {
                pooledObject.transform.SetPositionAndRotation(spawnTransform.position, spawnTransform.rotation);
                pooledObject.SetActive(true);
                return pooledObject;
            }
        }

        if (instantiateIfUnavailable)
        {
            pooledObject = Instantiate(pooledProjectile, this.transform);
            pooledObject.transform.SetPositionAndRotation(spawnTransform.position, spawnTransform.rotation);
            return pooledObject;
        }

        return pooledObject;
    }

    public void ReturnObject(GameObject obj)
    {
        // ensures its in this pool
        if (obj.transform.parent.gameObject == this.gameObject)
        {
            obj.SetActive(false);
        }
    }

    public List<GameObject> GetEntirePool(bool includeEnabled = false)
    {
        return GetComponentsInChildren<GameObject>(!includeEnabled).ToList();
    }
}
