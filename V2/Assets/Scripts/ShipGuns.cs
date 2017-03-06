using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipGuns : MonoBehaviour {

    private float timer;
    public float reloadTime;

    public BulletManager myBM;

    void Start () {
        myBM = GameObject.Find("Managers").GetComponent<BulletManager>();
    }

    void Update () {
        timer += Time.deltaTime;

        if (Input.GetMouseButton(0) && timer >= reloadTime)
        {
            timer = 0;
            BulletBase bullet = myBM.Make(transform.position + transform.forward * 3, this.gameObject.tag);
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 130f, ForceMode.Impulse);
        }

        //if (Input.GetMouseButtonDown(1))
        //{
        //    Debug.Log(myMM.NumOfType(DepotItem.enemyType1));
        //}
    }
}
