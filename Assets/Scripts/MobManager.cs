using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobManager : Manager<Mob>
{
    private static readonly Array ArrayOfTypes = Enum.GetValues(typeof(DepotItem));

    public override void DepotToManager(Mob thisMob)
    {
        ManagedObjects.Add(thisMob);
    }

    public override void ManagerToDepot(Mob thisMob)
    {
        ManagedObjects.Remove(thisMob);
    }

    public int NumOfType(DepotItem thisType)
    {
        return FindAll(m => m.myType == thisType).Count;
    }
}