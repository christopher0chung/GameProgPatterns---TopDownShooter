using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Manager<T>: MonoBehaviour where T : IManaged
{
    private readonly System.Random _rng = new System.Random();

    protected readonly List<T> ManagedObjects = new List<T>();

    public abstract T Make(Vector3 where, string myTag);

    public abstract List<T> Make(uint n, Vector3 where, string myTag);

    public abstract void Unmake(T o);

    public T Find(Predicate<T> predicate)
    {
        return ManagedObjects.Find(predicate);
    }

    public List<T> FindAll (Predicate<T> predicate)
    {
        return ManagedObjects.FindAll(predicate);
    }
}

public enum ManagedObjectTypes { bulletPlayer, bulletEnemy, explosion, enemyType1, enemyType2 }