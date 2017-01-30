using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mob : MonoBehaviour {

    public class MobClass
    {
        public DepotItem myType;
        public DepotItem myOrdinance;
        public float myHealth;

        public MobClass(float theHealth, DepotItem theType, DepotItem theOrdinance)
        {
            myHealth = theHealth;
            myType = theType;
            myOrdinance = theOrdinance;
        }
    }

    public abstract void Move();

    public abstract void Look();

    public virtual void Shoot(DepotItem myOrdinancePassed, Vector3 position)
    {
        GameObject myOrd = GameObject.Find("PoolDepot").GetComponent<PoolDepot>().ObjRequest(myOrdinancePassed);
        myOrd.transform.position = position  + transform.forward * 3;
        myOrd.GetComponent<Rigidbody>().velocity = Vector3.zero;
        myOrd.SetActive(true);
        myOrd.GetComponent<Rigidbody>().AddForce(transform.forward * 130f, ForceMode.Impulse);
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


