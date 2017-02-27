using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Manager<T>: MonoBehaviour where T : IManaged
{
    protected readonly List<T> ManagedObjects = new List<T>();

    public abstract void Make(T o);

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

public enum ManagedObjects { bulletNormal, explosion, enemyType1, enemyType2 }