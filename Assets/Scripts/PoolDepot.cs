﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolDepot : MonoBehaviour {

    private GameObject bullet;
    public Queue<GameObject> bullets = new Queue<GameObject>();

    private GameObject explosion;
    public Queue<GameObject> explosions = new Queue<GameObject>();

    private GameObject enemyType1;
    public Queue<GameObject> enemiesType1 = new Queue<GameObject>();

    void Awake ()
    {
        bullet = (GameObject) Resources.Load("Bullet");
        explosion = (GameObject)Resources.Load("Explosion");
        enemyType1 = (GameObject)Resources.Load("EnemyType1");
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
        else if (item == DepotItem.enemyType1)
        {
            if (enemiesType1.Count > 0)
                return enemiesType1.Dequeue();
            else
                return Instantiate(enemyType1, transform);
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
            //Debug.Log(explosions.Count);
        }
        else if (itemType == DepotItem.enemyType1)
        {
            enemiesType1.Enqueue(theObj);
            theObj.SetActive(false);
        }
    }


}

public enum DepotItem { bullet, explosion, enemyType1, enemyType2 }

