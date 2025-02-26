using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject pooledProjectile;
    [SerializeField] private int poolStartSize;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < poolStartSize; i++)
        {
            GameObject pooledProj = Instantiate(pooledProjectile, this.transform);
            pooledProj.SetActive(false);
        }

    }

    public GameObject GetObject(bool instantiateIfUnavailable = false)
    {
        
        foreach (Transform child in transform.GetComponentInChildren<Transform>(true)) 
        {
            if (child.gameObject.activeInHierarchy)
            {
                continue;
            }
            StartCoroutine(DelayBeforEnable(child.gameObject));
            return child.gameObject;
        }

        if (instantiateIfUnavailable)
        {
            GameObject pooledObject = Instantiate(pooledProjectile, this.transform);
            StartCoroutine(DelayBeforEnable(pooledObject));
            return pooledObject;
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

    public List<GameObject> GetEntirePool(bool includeEnabled = false)
    {
        return GetComponentsInChildren<GameObject>(!includeEnabled).ToList();
    }

    private IEnumerator DelayBeforEnable(GameObject toEnable)
    {
        yield return new WaitForEndOfFrame();
        toEnable.SetActive(true);

    }

}
