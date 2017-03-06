using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : Manager<BulletNormal> {

    [SerializeField] private float expireDuration;

    private readonly System.Random _rng = new System.Random();
    private int bulletCount;

    public override BulletNormal Make(Vector3 where)
    {
        GameObject hold;
        BulletNormal bullet;

        hold = (GameObject)Instantiate(Resources.Load("Bullet"), where, Quaternion.identity);

        bullet = hold.GetComponent<BulletNormal>();
        bullet.Init(bulletCount);
        bulletCount++;

        ManagedObjects.Add(bullet);

        bullet.OnMake();

        return bullet;
    }

    public override List<BulletNormal> Make(uint n, Vector3 where)
    {
        return null;
    }

    public override void Unmake(BulletNormal b)
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