using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType1 : Mob, IShootable {

    private Rigidbody myRB;
    private float shootTimer;

    void Awake () {
        myRB = GetComponent<Rigidbody>();
        myType = ManagedObjectTypes.enemyType1;
        //thisMob = new Mob(1f, DepotItem.enemyType1, DepotItem.bullet, 70f);
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


    //------------
    //Overriden funcs
    //------------

    public override void Init(int iDN)
    {
        base.Init(iDN);
        myHealth = 5;
        myOrdinance = ManagedObjectTypes.bulletPlayer;
        moveForce = 100;

        shootDelay = 2;
        shootInterval = .5f;
    }


    //------------
    //iShootable Interface
    //------------

    public void OnShoot(int damage)
    {
        myHealth -= damage;

        if (myHealth <= 0)
        {
            myMM.Unmake(this);
        }
    }
}
