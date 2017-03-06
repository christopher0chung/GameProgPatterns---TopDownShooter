using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : BulletBase {

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.gameObject.GetComponent<IShootable>() != null && other.gameObject.tag != "Enemy")
        {
            other.transform.root.gameObject.GetComponent<IShootable>().OnShoot(bulletDamage);
            // explosion
            // get rid of bullet
            myBM.Unmake(this);
        }
        else
        {
            //Debug.Log("Unshootable");
        }
    }
}
