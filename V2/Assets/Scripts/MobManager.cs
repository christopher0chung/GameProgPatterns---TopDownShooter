using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobManager : Manager<Mob> {

    private readonly System.Random _rng = new System.Random();
    private int enemyCount;

    public override Mob Make (Vector3 where, string myTag)
    {
        GameObject hold;
        Mob mob;

        if (GetRandomType() == ManagedObjectTypes.enemyType1)
        {
            hold = (GameObject) Instantiate(Resources.Load("Mob1"), where, Quaternion.identity);
        }
        else
        {
            hold = (GameObject)Instantiate(Resources.Load("Mob1"), where, Quaternion.identity);
        }

        mob = hold.GetComponent<Mob>();
        mob.Init(enemyCount);
        enemyCount++;

        ManagedObjects.Add(mob);

        mob.OnMake();

        return mob;
    }

    public override List<Mob> Make (uint n, Vector3 where, string myTag)
    {
        var mobs = new List<Mob>();
        for (int i = 0; i < n; i++)
        {
            ManagedObjects.Add(Make(where, myTag));
        }
        return mobs;
    }

    public override void Unmake(Mob m)
    {
        ManagedObjects.Remove(m);
        m.OnUnmake();
    }

    private ManagedObjectTypes GetRandomType()
    {
        //Debug.Log(_rng);
        ManagedObjectTypes mO = global::ManagedObjectTypes.enemyType1;
        return (mO);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Make(Vector3.zero, "");
        }
    }
}
