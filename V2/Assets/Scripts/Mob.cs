using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob: MonoBehaviour, IManaged
{
    public ManagedObjects myType;
    public ManagedObjects myOrdinance;
    public float myHealth;
    public float moveForce;

    public virtual void Init(float theHealth, ManagedObjects theType, ManagedObjects theOrdinance, float moveF)
    {
        myHealth = theHealth;
        myType = theType;
        myOrdinance = theOrdinance;
        moveForce = moveF;
    }

    public virtual void OnMake()
    {
        //Debug.Log("Withdrawn");
        return;
    }

    public virtual void OnUnmake()
    {
        //Debug.Log("Returned");
        return;
    }

    public virtual Vector3 ReturnTangentPoint(Vector3 here, Vector3 center, float radius)
    {
        float dist = Vector3.Distance(here, center);

        float tangentLength = Mathf.Sqrt((dist * dist) - (radius * radius));

        float angTangent = Mathf.Atan2(radius, tangentLength);

        float angToCenter = Mathf.Atan2(center.z - here.z, center.x - here.x);
        
        return new Vector3(here.x + tangentLength * Mathf.Cos(angTangent + angToCenter), 0, here.z + tangentLength * Mathf.Sin(angTangent + angToCenter)); 
    }

    public virtual Vector3 UnitVectorToPoint(Vector3 point)
    {
        return Vector3.Normalize(point - transform.position);
    }

    public virtual Vector3 RadarReturnPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        return player.transform.position;
    }

    public virtual Vector3 UnitVectorToPlayer()
    {
        Vector3 playerPos = RadarReturnPlayer();
        return Vector3.Normalize(playerPos - this.transform.position);
    }

    public virtual void Shoot(ManagedObjects myOrdinancePassed, Vector3 position)
    {
        // make a bullet
        GameObject myOrd;
        //myOrd.transform.position = position  + this.transform.forward * 3;
        //myOrd.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //myOrd.SetActive(true);
        //myOrd.GetComponent<Rigidbody>().AddForce(this.transform.forward * 30, ForceMode.Impulse);
    }

    public virtual void Kill(ManagedObjects myTypePassed)
    {
        // kill mob
    }

}



