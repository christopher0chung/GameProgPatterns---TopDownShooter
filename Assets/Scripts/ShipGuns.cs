using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipGuns : MonoBehaviour {

    public PoolDepot myPD;

	// Use this for initialization
	void Start () {
        myPD = GameObject.Find("PoolDepot").GetComponent<PoolDepot>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bullet = myPD.ObjRequest(DepotItem.bullet);
            bullet.transform.position = transform.position + transform.forward * 3;
            bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
            bullet.SetActive(true);
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 130f, ForceMode.Impulse);
        }
	}
}
