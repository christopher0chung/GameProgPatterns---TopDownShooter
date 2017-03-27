using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob: MonoBehaviour, IManaged
{
    public BulletManager myBM;
    public MobManager myMM;

    public ManagedObjectTypes myType;
    public ManagedObjectTypes myOrdinance;
    public float myHealth;
    public float moveForce;
    public float iDNum;

    public float shootDelay;
    public float shootInterval;


    public virtual void Init(int iDN)
    {
        iDNum = iDN;
        myBM = GameObject.Find("Managers").GetComponent<BulletManager>();
        myMM = GameObject.Find("Managers").GetComponent<MobManager>();
        gameObject.tag = "Enemy";
    }

    public virtual void OnMake()
    {
        return;
    }

    public virtual void OnUnmake()
    {
        Destroy(this.gameObject);
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

    public virtual void Shoot(ManagedObjectTypes myOrdinancePassed, Vector3 position)
    {
        if (myOrdinancePassed == ManagedObjectTypes.bulletPlayer)
        {
            // make a bullet
            BulletBase myOrd = myBM.Make(position + this.transform.forward * 3, this.gameObject.tag);
            myOrd.GetComponent<Rigidbody>().AddForce(this.transform.forward * 100, ForceMode.Impulse);
        }
    }

    public virtual void Kill(ManagedObjectTypes myTypePassed)
    {
        // kill mob
    }

}



