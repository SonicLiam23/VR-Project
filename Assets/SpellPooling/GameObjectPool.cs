using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;

public class GameObjectPool : MonoBehaviour
{
    public enum GetEntirePoolMode
    {
        GET_ONLY_ACTIVE, GET_ONLY_INACTIVE, GET_ALL
    };



    [SerializeField] private GameObject pooledObjects;
    [SerializeField] private int poolStartSize;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < poolStartSize; i++)
        {
            GameObject pooledProj = Instantiate(pooledObjects, this.transform);
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
            GameObject pooledObject = Instantiate(pooledObjects, this.transform);
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
        // You always should reuse emenys to prevent garbage collection
        //

    }

    public List<GameObject> GetEntirePool(GetEntirePoolMode mode = GetEntirePoolMode.GET_ALL)
    {
        List<GameObject> children = new List<GameObject>();
        // it makes me assign it here or it errors, it's a little dumb
        bool getOnlyActive = false;
        switch (mode)
        {
            case GetEntirePoolMode.GET_ALL:
                gameObject.GetChildGameObjects(children);
                return children;

            case GetEntirePoolMode.GET_ONLY_ACTIVE:
                getOnlyActive = true;
                break;

            case GetEntirePoolMode.GET_ONLY_INACTIVE:
                getOnlyActive = false;
                break;
        }

        // no idea if this is completley useless but... I feel like it's smart xD
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeInHierarchy == getOnlyActive)
            {
                children.Add(child.gameObject);
                //continue;
            }
        }
        return children;
    }

    private IEnumerator DelayBeforEnable(GameObject toEnable)
    {
        yield return new WaitForEndOfFrame();
        toEnable.SetActive(true);

    }

}
