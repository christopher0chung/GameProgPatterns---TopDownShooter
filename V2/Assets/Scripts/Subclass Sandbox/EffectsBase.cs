using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsBase : MonoBehaviour, IManaged
{
    public double spawnTime;
    public EffectsManager myFM;

    public virtual void Init(int num)
    {
        myFM = GameObject.Find("Managers").GetComponent<EffectsManager>();
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
