using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipGuns : MonoBehaviour {

    private PoolDepot myPD;
    private float timer;
    public float reloadTime;

    public MobManager myMM;

	// Use this for initialization
	void Start () {
        myPD = GameObject.Find("PoolDepot").GetComponent<PoolDepot>();
        myMM = GameObject.Find("ObjectsManager").GetComponent<MobManager>();
    }

    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;

        if (Input.GetMouseButton(0) && timer >= reloadTime)
        {
            timer = 0;
            GameObject bullet = myPD.ObjRequest(DepotItem.bullet);
            bullet.transform.position = transform.position + transform.forward * 3;
            bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
            bullet.SetActive(true);
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 130f, ForceMode.Impulse);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(myMM.NumOfType(DepotItem.enemyType1));
        }
	}
}
