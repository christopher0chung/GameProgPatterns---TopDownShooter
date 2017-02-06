using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mob : MonoBehaviour {

    public class MobClass
    {
        public DepotItem myType;
        public DepotItem myOrdinance;
        public float myHealth;
        public float moveForce;

        public MobClass(float theHealth, DepotItem theType, DepotItem theOrdinance, float moveF)
        {
            myHealth = theHealth;
            myType = theType;
            myOrdinance = theOrdinance;
            moveForce = moveF;
        }
    }

    // -------------------------------------------------------------


    public abstract void Move();

    public abstract void Look();


    // -------------------------------------------------------------

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
        return Vector3.Normalize(playerPos - transform.position);
    }

    public virtual void Shoot(DepotItem myOrdinancePassed, Vector3 position)
    {
        GameObject myOrd = GameObject.Find("PoolDepot").GetComponent<PoolDepot>().ObjRequest(myOrdinancePassed);
        myOrd.transform.position = position  + transform.forward * 3;
        myOrd.GetComponent<Rigidbody>().velocity = Vector3.zero;
        myOrd.SetActive(true);
        myOrd.GetComponent<Rigidbody>().AddForce(transform.forward * 30, ForceMode.Impulse);
    }

    public virtual void Kill(DepotItem myTypePassed)
    {
        GameObject myExp = GameObject.Find("PoolDepot").GetComponent<PoolDepot>().ObjRequest(DepotItem.explosion);
        myExp.transform.position = transform.position;
        myExp.SetActive(true);
        GameObject.Find("PoolDepot").GetComponent<PoolDepot>().ObjReturn(this.gameObject, myTypePassed);
        //Debug.Log(myTypePassed);
    }

}


