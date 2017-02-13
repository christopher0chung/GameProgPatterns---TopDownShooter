using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType1 : MobSubclassSandbox {

    //public Mob thisMob; 


    public float shootDelay;
    public float shootInterval;

    private Rigidbody myRB;
    private float shootTimer;

    // Use this for initialization
    void Awake () {
        myRB = GetComponent<Rigidbody>();
        //thisMob = new Mob(1f, DepotItem.enemyType1, DepotItem.bullet, 70f);
	}

    void OnEnable()
    {
        base.OnManagerWithdraw();
        //Debug.Log(myHealth);
        Init(1, DepotItem.enemyType1, DepotItem.bullet, 70);

    }

    void FixedUpdate()
    {
        Look();
        Move();
        TimedShoot();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Bullet(Clone)")
        {
            OnManagerReturn();
            Kill(myType);
        }
    }


    //------------
    //Abstract funcs
    //------------

    public void Move ()
    {
        myRB.AddForce(UnitVectorToPlayer() * moveForce);
    }

    public void Look ()
    {
        Vector3 playerPos = RadarReturnPlayer();
        float lookAng = Mathf.Atan2(playerPos.x - transform.position.x, playerPos.z - transform.position.z) * Mathf.Rad2Deg;
        myRB.rotation = Quaternion.Euler(0, lookAng, 0);
    }


    //------------
    //Specific funcs
    //------------

    private void TimedShoot()
    {
        shootTimer += Time.fixedDeltaTime;
        if (shootTimer >= shootInterval)
        {
            shootTimer = 0;
            Shoot(myOrdinance, transform.position);
        }
    }
}
