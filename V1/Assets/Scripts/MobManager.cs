using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobManager : Manager<MobSubclassSandbox>
{
    private static readonly Array ArrayOfTypes = Enum.GetValues(typeof(DepotItem));

    public override void DepotToManager(MobSubclassSandbox thisMob)
    {
        ManagedObjects.Add(thisMob);
    }

    public override void ManagerToDepot(MobSubclassSandbox thisMob)
    {
        ManagedObjects.Remove(thisMob);
    }

    public int NumOfType(DepotItem thisType)
    {
        //Debug.Log(FindAll(m => m.myType == thisType).Count);
        return FindAll(m => m.myType == thisType).Count;
    }

    public void TestIfICanTalkToManager ()
    {
        Debug.Log("Hello");
    }
}