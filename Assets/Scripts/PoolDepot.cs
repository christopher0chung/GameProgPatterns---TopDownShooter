using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolDepot : MonoBehaviour {

    private GameObject bullet;
    public Queue<GameObject> bullets = new Queue<GameObject>();

    private GameObject explosion;
    public Queue<GameObject> explosions = new Queue<GameObject>();

    void Awake ()
    {
        bullet = (GameObject) Resources.Load("Bullet");
        explosion = (GameObject)Resources.Load("Explosion");
    }

    public GameObject ObjRequest (DepotItem item)
    {
        if (item == DepotItem.bullet)
        {
            if (bullets.Count > 0)
                return bullets.Dequeue();
            else
                return Instantiate(bullet, transform);
        }
        else if (item == DepotItem.explosion)
        {
            if (explosions.Count > 0)
                return explosions.Dequeue();
            else
                return Instantiate(explosion, transform);
        }
        else return null;
    }

    public void ObjReturn (GameObject theObj, DepotItem itemType) 
    {
        if (itemType == DepotItem.bullet)
        {
            bullets.Enqueue(theObj);
            theObj.SetActive(false);
        }
        else if (itemType == DepotItem.explosion)
        {
            explosions.Enqueue(theObj);
            theObj.SetActive(false);
        }
    }


}

public enum DepotItem { bullet, explosion }
