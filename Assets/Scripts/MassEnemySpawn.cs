using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassEnemySpawn : MonoBehaviour {

    private PoolDepot myPD;
    private float timer;
    public float spawnIntervalTemp;
    public DepotItem whatToSpawn;

    // Use this for initialization
    void Start()
    {
        myPD = GameObject.Find("PoolDepot").GetComponent<PoolDepot>();
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.fixedDeltaTime;
        if (timer >= spawnIntervalTemp)
        {
            timer = 0;
            MassSpawn(7, 80);
        }
    }

    private void MassSpawn (int num, float radius)
    {
        float total = (float)num;
        for (int i = 0; i < num; i++)
        {
            float ang = ((float)i / total) * 360 * Mathf.Deg2Rad;

            Vector3 spawnPoint = new Vector3(radius * Mathf.Cos(ang), 0, radius * Mathf.Sin(ang));

            //Debug.Log(spawnPoint + " " + ang);

            GameObject myEnemy = myPD.ObjRequest(whatToSpawn);
            myEnemy.transform.position = spawnPoint;
            myEnemy.SetActive(true);
        }
    }
}
