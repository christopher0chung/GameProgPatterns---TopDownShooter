using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType2 : MobSubclassSandbox {

    public Mob thisMob;

    public float shootDelay;
    public float shootInterval;

    private Rigidbody myRB;
    private float shootTimer;

    // Use this for initialization
    void Awake () {
        myRB = GetComponent<Rigidbody>();
        thisMob = new Mob(1, DepotItem.enemyType2, DepotItem.bullet, 40);
    }

    void OnEnable()
    {
        thisMob.OnManagerWithdraw();
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
            thisMob.OnManagerReturn();
            base.Kill(thisMob.myType);
        }
    }
    

    //------------
    //Abstract funcs
    //------------

    public void Move()
    {
        //if (Vector3.Distance(transform.position, Vector3.zero) > 10.2f || Vector3.Distance(transform.position, Vector3.zero) < 9.8f)
            myRB.AddForce(base.UnitVectorToPoint(base.ReturnTangentPoint(transform.position, Vector3.zero, 30)) * thisMob.moveForce);
        //else
        //    myRB.AddForce(base.UnitVectorToPoint(transform.position + transform.right) * thisMob.moveForce);
    }

    public void Look()
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