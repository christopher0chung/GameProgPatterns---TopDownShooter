using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehavior : MonoBehaviour
{

    private PoolDepot myPD;
    private float timer;
    private float explosionDuration = 1;

    // Use this for initialization
    void Start()
    {
        myPD = GameObject.Find("PoolDepot").GetComponent<PoolDepot>();
    }

    void OnEnable()
    {
        timer = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        timer += Time.fixedDeltaTime;
        if (timer >= explosionDuration)
        {
            myPD.ObjReturn(this.gameObject, DepotItem.explosion);
        }
    }
}