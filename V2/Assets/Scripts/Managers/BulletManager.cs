using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : Manager<BulletBase> {

    [SerializeField] private float expireDuration;

    private readonly System.Random _rng = new System.Random();
    private int bulletCount;

    public override BulletBase Make(Vector3 where, string myTag)
    {
        GameObject hold;
        BulletBase bullet;

        if (myTag == "Player")
            hold = (GameObject)Instantiate(Resources.Load("BulletPlayer"), where, Quaternion.identity);
        else
            hold = (GameObject)Instantiate(Resources.Load("BulletEnemy"), where, Quaternion.identity);

        bullet = hold.GetComponent<BulletBase>();
        bullet.Init(bulletCount);
        bulletCount++;

        ManagedObjects.Add(bullet);

        bullet.OnMake();

        return bullet;
    }

    public override List<BulletBase> Make(uint n, Vector3 where, string myTag)
    {
        return null;
    }

    public override void Unmake(BulletBase b)
    {
        ManagedObjects.Remove(b);
        b.OnUnmake();
    }

    public void Update ()
    {
        DestroyExpired(Time.time);
    }

    private void DestroyExpired (double currentTime)
    {
        foreach (var bullet in FindAll(b => b.spawnTime <=  currentTime - expireDuration))
        {
            Unmake(bullet);
        }
    }
}