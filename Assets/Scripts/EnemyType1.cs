using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType1 : Mob {

    public MobClass thisMob; 

    public float shootDelay;
    public float shootInterval;

    private Rigidbody myRB;
    private float shootTimer;

    

    // Use this for initialization
    void Start () {
        myRB = GetComponent<Rigidbody>();
        thisMob = new MobClass(1, DepotItem.enemyType1, DepotItem.bullet, 70);
	}
	
	void FixedUpdate () {
        Look();
        Move();
        TimedShoot();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Bullet(Clone)")
        {
            base.Kill(thisMob.myType);
        }
    }


    //------------
    //Abstract funcs
    //------------

    public override void Move ()
    {
        myRB.AddForce(base.UnitVectorToPlayer() * thisMob.moveForce);
    }

    public override void Look ()
    {
        Vector3 playerPos = base.RadarReturnPlayer();
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
            base.Shoot(thisMob.myOrdinance, transform.position);
        }
    }
}
