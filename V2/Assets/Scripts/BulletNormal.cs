using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletNormal : MonoBehaviour, IManaged {

    private float timer;
    private float flightTime = 3;

    public int bulletDamage;

	// Use this for initialization
	void Start () {
	}

    public virtual void OnMake()
    {
        //Debug.Log("Withdrawn");
        return;
    }

    public virtual void OnUnmake()
    {
        //Debug.Log("Returned");
        return;
    }

    // Update is called once per frame
    void FixedUpdate () {

        timer += Time.fixedDeltaTime;
        if (timer >= flightTime)
        {
        }		
	}

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.GetComponent<IShootable>() != null)
        {
            other.gameObject.GetComponent<IShootable>().OnShoot(bulletDamage);
            // explosion
            // get rid of bullet
        }
    }
}
