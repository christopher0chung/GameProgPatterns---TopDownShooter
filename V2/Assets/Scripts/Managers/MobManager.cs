using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobManager : Manager<Mob> {

    private readonly System.Random _rng = new System.Random();
    private int enemyCount;

    private uint waveCount = 1;

    public override Mob Make (Vector3 where, string myTag)
    {
        GameObject hold;
        Mob mob;

        if (GetRandomType() == ManagedObjectTypes.enemyType1)
        {
            hold = (GameObject)Instantiate(Resources.Load("Mob1"), where, Quaternion.identity);
        }
        else
        {
            hold = (GameObject)Instantiate(Resources.Load("Mob2"), where, Quaternion.identity);
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
        if (NumOfType(ManagedObjectTypes.enemyType1) + NumOfType(ManagedObjectTypes.enemyType2) == 0)
        {
            EventManager.instance.Fire(new EventWaveEliminated());
            //Debug.Log("Firing Event");
        }
        else
        {
            //Debug.Log("Not Firing Event");
        }
    }

    public int NumOfType(ManagedObjectTypes mO)
    {
        return FindAll(m => m.myType == mO).Count;
    }

    private ManagedObjectTypes GetRandomType()
    {
        ManagedObjectTypes mO;
        int num = _rng.Next(0, 2);
        //Debug.Log(num);
        if (num == 0)
            mO = global::ManagedObjectTypes.enemyType1;
        else
            mO = global::ManagedObjectTypes.enemyType2;
        return (mO);
    }

    void Start()
    {
        EventManager.instance.Register<EventWaveEliminated>(NewWave);
        EventManager.instance.Fire(new EventWaveEliminated());
    }

    public void NewWave (GameEvent myGE)
    {
        // Currently not making use of myGE
        if (waveCount == 2)
        {
            Instantiate(Resources.Load("Boss"), Vector3.zero, Quaternion.identity);
        }
        else if (waveCount >=3)
        {
            return;
        }
        else
        {
            for (int i = 0; i < waveCount; i++)
            {
                Invoke("RandomPosSpawn", 1);
            }
        }
        waveCount++;
    }

    private void RandomPosSpawn()
    {
        Make(new Vector3(UnityEngine.Random.Range(-30f, 30f), 0, UnityEngine.Random.Range(-20f, 20f)), "Enemy");
    }

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        Make(Vector3.zero, "");
    //    }
    //}
}


