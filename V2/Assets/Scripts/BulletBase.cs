using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour, IManaged {

    public int bulletDamage;
    public int bulletNum;
    public double spawnTime;
    public BulletManager myBM;

    public virtual void Init (int num)
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


}
