using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : BulletBase {

    public override void OnMake()
    {
        base.OnMake();
        gameObject.tag = "PlayerBullet";
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.gameObject.GetComponent<IShootable>() != null && other.gameObject.tag != "Player")
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
