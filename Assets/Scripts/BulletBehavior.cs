using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour {

    private PoolDepot myPD;
    private float timer;
    private float flightTime = 2;

    public int bulletDamage;

	// Use this for initialization
	void Start () {
        myPD = GameObject.Find("PoolDepot").GetComponent<PoolDepot>();
	}

    void OnEnable()
    {
        timer = 0;
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        timer += Time.fixedDeltaTime;
        if (timer >= flightTime)
        {
            myPD.ObjReturn(this.gameObject, DepotItem.bullet);
        }		
	}

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.GetComponent<iShootable>() != null)
        {
            other.gameObject.GetComponent<iShootable>().OnShoot(bulletDamage);
            GameObject myExplosion = myPD.ObjRequest(DepotItem.explosion);
            myExplosion.transform.position = transform.position;
            myExplosion.SetActive(true);
            myPD.ObjReturn(this.gameObject, DepotItem.bullet);
        }
    }
}
