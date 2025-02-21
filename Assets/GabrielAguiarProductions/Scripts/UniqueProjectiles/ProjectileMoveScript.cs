//
//
//NOTES:
//
//This script is used for DEMONSTRATION porpuses of the Projectiles. I recommend everyone to create their own code for their own projects.
//THIS IS JUST A BASIC EXAMPLE PUT TOGETHER TO DEMONSTRATE VFX ASSETS.
//
//




#pragma warning disable 0414 // private field assigned but not used.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMoveScript : MonoBehaviour {


    public float speed;
	[Tooltip("From 0% to 100%")]
	public float accuracy;
	[Tooltip("0 for no homing")]
	public float homingAmount;
	public GameObject muzzlePrefab;
	public GameObject hitPrefab;
    [Tooltip("Delete the projectile after seconds. 0 for no delete")]
    [SerializeField] private float autoDeleteAfter;

    private Vector3 offset;
	private Rigidbody rb;
    private GameObject target = null;
	private bool initialized = false;

	// each projectile will have its own Muzzle and Hit particle objects assigned to them
	static private GameObject particlesPool;
	private GameObject muzzleVFXParent;
	private ParticleSystem[] muzzleVFXComponents;
	private GameObject hitVFXParent;
	private ParticleSystem[] hitVFXComponents;
    private TrailRenderer[] trails;

    private void OnDisable()
    {
        target = null;
		rb.isKinematic = false;
	}

	// upon start, the order is as follows awake -> OnEnable of all objects. Then it runs start on all object.
	private void Awake()
	{
		if (particlesPool == null)
		{
			particlesPool = GameObject.Find("ParticlesPool");
		}

		if (muzzlePrefab != null)
		{
			muzzleVFXParent = Instantiate(muzzlePrefab, particlesPool.transform);
			int children = muzzleVFXParent.transform.childCount;
			if (children > 0)
			{
				muzzleVFXComponents = muzzleVFXParent.GetComponentsInChildren<ParticleSystem>(true);
			}
		}


		if (hitPrefab != null)
		{
			hitVFXParent = Instantiate(hitPrefab, particlesPool.transform);
			int children = hitVFXParent.transform.childCount;
			if (children > 0)
			{
				hitVFXComponents = hitVFXParent.GetComponentsInChildren<ParticleSystem>(true);
			}
		}

		trails = GetComponentsInChildren<TrailRenderer>(true);
		// this one setting caused me HOURS of pain, so this is to make bloody sure it's off
		foreach (TrailRenderer trail in trails)
		{
			trail.autodestruct = false;
		}
}

private void Start()
    {
		// run once at the start AFTER OnEnable for the first time
		initialized = true;
        rb = GetComponent<Rigidbody>();
        gameObject.SetActive(false);
    }
    void OnEnable () {
		if (initialized)
		{
			//used to create a radius for the accuracy and have a very unique randomness
			if (accuracy != 100) {
				var accuracyPercent = 1 - (accuracy / 100);

				for (int i = 0; i < 2; i++) {
					var val = 1 * Random.Range (-accuracyPercent, accuracyPercent);
					var index = Random.Range (0, 2);
					if (i == 0) 
					{
						if (index == 0)
							offset = new Vector3 (0, -val, 0);
						else
							offset = new Vector3 (0, val, 0);
					} 
					else 
					{
						if (index == 0)
							offset = new Vector3 (0, offset.y, -val);
						else
							offset = new Vector3 (0, offset.y, val);
					}
				}
			}
			
			if (muzzleVFXParent != null) 
			{
				muzzleVFXParent.transform.position = transform.position;
				muzzleVFXParent.transform.forward = gameObject.transform.forward + offset;
                foreach (ParticleSystem ps in muzzleVFXComponents)
                {
					ps.Clear();
                    ps.time = 0f;
                    ps.Play();

                }
            }

			// clear the previous projectile trail renderer
			foreach (TrailRenderer tr in  trails)
			{ 
				tr.Clear(); 
			}

			if (autoDeleteAfter > 0)
			{
				StartCoroutine(PoolParticle(autoDeleteAfter));
			}
		}
	}

	void FixedUpdate () 
	{
		if (speed != 0 && rb != null)
		{
			rb.position += (transform.forward + offset) * (speed * Time.deltaTime);
		}

		if (homingAmount > 0 && target != null)
		{
			if (target.activeInHierarchy)
			{
				Vector3 delta = target.transform.position - transform.position;
				transform.forward = Vector3.Slerp(transform.forward, delta.normalized, homingAmount * Time.deltaTime); ;
				transform.position += (speed * Time.deltaTime * transform.forward);
			}
        }
    }

	void OnCollisionEnter (Collision co) 
	{
        ContactPoint contact = co.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;

        if (hitPrefab != null)
        {
            hitVFXParent.transform.SetPositionAndRotation(pos, rot);
            foreach (ParticleSystem ps in hitVFXComponents)
            {
                ps.Clear();
                ps.time = 0f;
                ps.Play();
            }
        }

        StartCoroutine(PoolParticle(0f));
	}

	public IEnumerator PoolParticle (float waitTime) {

		yield return new WaitForSeconds (waitTime);
		gameObject.SetActive(false);
		StopAllCoroutines();
	}

    public void SetTarget (GameObject trg)
    {
        target = trg;
    }
}
