using System.Collections;
using UnityEngine;

[System.Serializable]
public class GameObjectPool : MonoBehaviour
{
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
    }
    
    public void ReturnAllToPool()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            Transform child = transform.GetChild(i);
            if (child.gameObject.activeInHierarchy) child .gameObject.SetActive(false);
        }
    }

    public int CurrentlyActiveCount()
    {
        int count = 0;
        for (int i = 0; i < transform.childCount; ++i)
        {
            Transform child = transform.GetChild(i);
            if (child.gameObject.activeInHierarchy) count++;
        }

        return count;
    }

    private IEnumerator DelayBeforEnable(GameObject toEnable)
    {
        yield return new WaitForEndOfFrame();
        toEnable.SetActive(true);

    }
}
