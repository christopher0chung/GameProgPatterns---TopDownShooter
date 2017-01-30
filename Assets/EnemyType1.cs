using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType1 : Mob {

    public MobClass thisMob; 

    public float shootDelay;
    public float shootInterval;
    public float moveLead;
    public float shootLead;
    public float moveForce;

    private Vector3 playerPos;
    private Vector3 moveDir;
    private Rigidbody myRB;
    private float shootTimer;

    

    // Use this for initialization
    void Start () {
        myRB = GetComponent<Rigidbody>();
        thisMob = new MobClass(1, DepotItem.enemyType1, DepotItem.bullet);
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
    //Inheritted funcs
    //------------

    public override void Move ()
    {
        myRB.AddForce(moveDir * moveForce);
    }

    public override void Look ()
    {
        TrackPlayerPosition();
    }

    //------------
    //Specific funcs
    //------------

    private void TrackPlayerPosition ()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        playerPos = player.transform.position;
        AssignPositionsOfInterest(player.transform);

        float lookAng = Mathf.Atan2(player.transform.position.x - transform.position.x , player.transform.position.z - transform.position.z) * Mathf.Rad2Deg;
        myRB.rotation = Quaternion.Euler(0, lookAng, 0);
    }

    private void AssignPositionsOfInterest (Transform player)
    {
        moveDir = Vector3.Normalize((player.transform.position - player.transform.forward * moveLead) - transform.position);
    }

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
