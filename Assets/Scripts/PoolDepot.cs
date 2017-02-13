using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolDepot : MonoBehaviour {

    private MobManager myMM;

    private GameObject bullet;
    public Queue<GameObject> bullets = new Queue<GameObject>();

    private GameObject explosion;
    public Queue<GameObject> explosions = new Queue<GameObject>();

    private GameObject enemyType1;
    public Queue<GameObject> enemiesType1 = new Queue<GameObject>();

    private GameObject enemyType2;
    public Queue<GameObject> enemiesType2 = new Queue<GameObject>();

    private GameObject playerDeath;
    public Queue<GameObject> playerDeaths = new Queue<GameObject>();

    void Awake ()
    {
        myMM = GameObject.Find("ObjectsManager").GetComponent<MobManager>();

        bullet = (GameObject) Resources.Load("Bullet");
        explosion = (GameObject)Resources.Load("Explosion");
        enemyType1 = (GameObject)Resources.Load("EnemyType1");
        enemyType2 = (GameObject)Resources.Load("EnemyType2");
        playerDeath = (GameObject)Resources.Load("PlayerDeath");
    }

    public GameObject ObjRequest (DepotItem item)
    {
        if (item == DepotItem.bullet)
        {
            if (bullets.Count > 0) {
                return bullets.Dequeue();
            }
            else
            {
                return Instantiate(bullet, transform);
            }
        }
        else if (item == DepotItem.explosion)
        {
            if (explosions.Count > 0)
            {
                return explosions.Dequeue();
            }
            else
            {
                return Instantiate(explosion, transform);
            }
        }
        else if (item == DepotItem.enemyType1)
        {
            if (enemiesType1.Count > 0)
            {
                GameObject thisMob = (GameObject)enemiesType1.Dequeue();
                myMM.DepotToManager(thisMob.GetComponent<MobSubclassSandbox>());
                return thisMob;
            }
            else
            {
                GameObject thisMob = (GameObject)Instantiate(enemyType1, transform);
                myMM.DepotToManager(thisMob.GetComponent<MobSubclassSandbox>());
                return thisMob; 
            }
        }
        else if (item == DepotItem.enemyType2)
        {
            if (enemiesType2.Count > 0)
            {
                GameObject thisMob = (GameObject)enemiesType2.Dequeue();
                myMM.DepotToManager(thisMob.GetComponent<MobSubclassSandbox>());
                return thisMob;
            }
            else
            {
                GameObject thisMob = (GameObject)Instantiate(enemyType2, transform);
                myMM.DepotToManager(thisMob.GetComponent<MobSubclassSandbox>());
                return thisMob;
            }
        }
        else if (item == DepotItem.playerDeath)
        {
            if (enemiesType2.Count > 0)
            {
                return enemiesType2.Dequeue();
            }
            else
            {
                return Instantiate(enemyType2, transform);
            }
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
        else if (itemType == DepotItem.enemyType2)
        {
            enemiesType2.Enqueue(theObj);
            theObj.SetActive(false);
        }
        else if (itemType == DepotItem.playerDeath)
        {
            playerDeaths.Enqueue(theObj);
            theObj.SetActive(false);
        }
    }
}

public enum DepotItem { bullet, explosion, enemyType1, enemyType2, playerDeath }

