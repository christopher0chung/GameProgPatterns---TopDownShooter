using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : Manager<EffectsBase>
{
    [SerializeField]
    private float expireDuration;

    private readonly System.Random _rng = new System.Random();
    private int fXCount;

    public override EffectsBase Make(Vector3 where, string myTag)
    {
        GameObject hold;
        EffectsBase fX;

        if (myTag == "PlayerBullet")
            hold = (GameObject)Instantiate(Resources.Load("EnemyExplosion"), where, Quaternion.identity);
        else
            hold = (GameObject)Instantiate(Resources.Load("PlayerExplosion"), where, Quaternion.identity);

        fX = hold.GetComponent<EffectsBase>();
        fX.Init(fXCount);
        fXCount++;

        ManagedObjects.Add(fX);

        fX.OnMake();

        return fX;
    }

    public override List<EffectsBase> Make(uint n, Vector3 where, string myTag)
    {
        return null;
    }

    public override void Unmake(EffectsBase f)
    {
        ManagedObjects.Remove(f);
        f.OnUnmake();
    }

    public void Update()
    {
        DestroyExpired(Time.time);
    }

    private void DestroyExpired(double currentTime)
    {
        foreach (var fx in FindAll(f => f.spawnTime <= currentTime - expireDuration))
        {
            Unmake(fx);
        }
    }
}