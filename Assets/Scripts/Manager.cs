using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Manager<T>: MonoBehaviour where T : IManaged
{
    protected readonly List<T> ManagedObjects = new List<T>();

    public abstract void DepotToManager(T o);

    public abstract void ManagerToDepot(T o);

    public T Find(Predicate<T> predicate)
    {
        return ManagedObjects.Find(predicate);
    }

    public List<T> FindAll (Predicate<T> predicate)
    {
        return ManagedObjects.FindAll(predicate);
    }
}