using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassEnemyWaveSpawn : MonoBehaviour
{

    private PoolDepot myPD;
    private float timer;
    public float spawnIntervalTemp;

    private int horzNum;
    private int vertNum;
    private int circNum = 4;


    // Use this for initialization
    void Start()
    {
        myPD = GameObject.Find("PoolDepot").GetComponent<PoolDepot>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnIntervalTemp)
        {
            timer -= spawnIntervalTemp;

            int dir = Random.Range(0, 4);

            if (dir <= 1)
                SpawnTopBottom(dir);
            if (dir >= 2)
                SpawnLeftRight(dir);
            else
                CircSpawn(80);
        }
    }

    private void SpawnTopBottom (int dir)
    {
        horzNum++;
        for (int i = 0; i < horzNum; i++)
        {
            GameObject myEnemy = myPD.ObjRequest(DepotItem.enemyType1);
            myEnemy.transform.position = new Vector3(( (i + 1) * 150 / (horzNum + 1 )) - 75, 0, -45 + (90 * dir));
            myEnemy.SetActive(true);
        }
    }

    private void SpawnLeftRight (int dir)
    {
        dir -= 2;
        vertNum++;
        for (int i = 0; i < vertNum; i++)
        {
            GameObject myEnemy = myPD.ObjRequest(DepotItem.enemyType1);
            myEnemy.transform.position = new Vector3(-75 + (150 * dir), 0, ((i + 1) * 90 / (vertNum + 1)) - 45);
            myEnemy.SetActive(true);
        }
    }

    private void CircSpawn(float radius)
    {
        circNum++;
        for (int i = 0; i < circNum; i++)
        {
            float ang = ((float)i / circNum) * 360 * Mathf.Deg2Rad;

            Vector3 spawnPoint = new Vector3(radius * Mathf.Cos(ang), 0, radius * Mathf.Sin(ang));

            GameObject myEnemy = myPD.ObjRequest(DepotItem.enemyType2);
            myEnemy.transform.position = spawnPoint;
            myEnemy.SetActive(true);
        }
    }
}