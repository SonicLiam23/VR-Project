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
using static UnityEngine.Rendering.DebugUI.Table;

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
	[SerializeField] private Vector3 direction;
	private Rigidbody rb;
    private GameObject target = null;
	private bool initialized = false;

	// each projectile will have its own Muzzle and Hit particle objects assigned to them
	static private GameObject particlesPool;
	private GameObject muzzleVFXParent;
	private ParticleSystem[] muzzleVFXComponents;
	private PlaySound muzzleSound;

    private GameObject hitVFXParent;
	private ParticleSystem[] hitVFXComponents;
    private PlaySound hitSound;

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
			particlesPool = GameObject.Find("_ParticlesPool");
		}

		if (muzzlePrefab != null)
		{
			muzzleVFXParent = Instantiate(muzzlePrefab, particlesPool.transform);
			int children = muzzleVFXParent.transform.childCount;
			if (children > 0)
			{
				muzzleVFXComponents = muzzleVFXParent.GetComponentsInChildren<ParticleSystem>(true);
			}

            muzzleSound = muzzleVFXParent.GetComponent<PlaySound>();
        }
        rb = GetComponent<Rigidbody>();

        if (hitPrefab != null)
		{
			hitVFXParent = Instantiate(hitPrefab, particlesPool.transform);
			int children = hitVFXParent.transform.childCount;
			if (children > 0)
			{
				hitVFXComponents = hitVFXParent.GetComponentsInChildren<ParticleSystem>(true);
			}

            hitSound = hitVFXParent.GetComponent<PlaySound>();
        }

		trails = GetComponentsInChildren<TrailRenderer>(true);
		// this one setting caused me HOURS of pain, so this is to make bloody sure it's off
		foreach (TrailRenderer trail in trails)
		{
			trail.autodestruct = false;
		}
	}
	
    void OnEnable () {
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
		// transform.forward seems to change by very small amounts during runtime so, set it here and only change when intended
		direction = transform.forward;

			
		if (muzzleVFXParent != null) 
		{
			muzzleVFXParent.transform.position = transform.position ;
			muzzleVFXParent.transform.forward = direction + offset;
            foreach (ParticleSystem ps in muzzleVFXComponents)
            {
				ps.Clear();
                ps.time = 0f;
                ps.Play();

            }

			if (muzzleSound != null && initialized)
			{
				muzzleSound.Play();
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

		initialized = true;
	}

	void FixedUpdate () 
	{
		transform.forward = direction;
		if (speed != 0 && rb != null)
		{
			rb.position += (direction + offset) * (speed * Time.deltaTime);
		}

		if (homingAmount > 0 && target != null)
		{
			if (target.activeInHierarchy)
			{
				Vector3 delta = target.transform.position - transform.position;
				transform.forward = Vector3.Slerp(transform.forward, delta.normalized, homingAmount * Time.deltaTime);
				direction = transform.forward;
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
        }

        StartCoroutine(PoolParticle());
	}

	public IEnumerator PoolParticle (float waitTime = 0f) 
	{
		yield return new WaitForSeconds (waitTime);
        if (hitPrefab != null)
        {

            foreach (ParticleSystem ps in hitVFXComponents)
            {
                ps.Clear();
                ps.time = 0f;
                ps.Play();
            }

			if (hitSound != null)
			{
                hitSound.Play();
            }
        }
        gameObject.SetActive(false);
	}

    public void SetTarget (GameObject trg)
    {
        target = trg;
    }
}
