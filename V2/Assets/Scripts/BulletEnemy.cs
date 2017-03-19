using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : BulletBase {

    public override void OnMake()
    {
        base.OnMake();
        gameObject.tag = "EnemyBullet";
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.gameObject.GetComponent<IShootable>() != null && other.gameObject.tag != "Enemy")
        {
            other.transform.root.gameObject.GetComponent<IShootable>().OnShoot(bulletDamage);
            // explosion
            myFM.Make(transform.position, gameObject.tag);
            // get rid of bullet
            myBM.Unmake(this);
        }
        else
        {
            //Debug.Log("Unshootable");
        }
    }
}
