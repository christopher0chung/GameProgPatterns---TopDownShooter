using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletNormal : MonoBehaviour, IManaged {

    public int bulletDamage;
    public int bulletNum;
    public double spawnTime;
    public BulletManager myBM;

    public void Init (int num)
    {
        bulletNum = num;
        myBM = GameObject.Find("Managers").GetComponent<BulletManager>();
    }

    public virtual void OnMake()
    {
        spawnTime = Time.time;
        return;
    }

    public virtual void OnUnmake()
    {
        Destroy(this.gameObject);
        return;
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.transform.root.gameObject.GetComponent<IShootable>() != null)
        {
            other.transform.root.gameObject.GetComponent<IShootable>().OnShoot(bulletDamage);
            // explosion
            // get rid of bullet
            myBM.Unmake(this);
        }
        else
        {
            Debug.Log("Unshootable");
        }
    }
}
