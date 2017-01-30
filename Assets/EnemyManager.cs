using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    private PoolDepot myPD;
    private float timer;
    public float spawnIntervalTemp;

	// Use this for initialization
	void Start () {
        myPD = GameObject.Find("PoolDepot").GetComponent<PoolDepot>();
	}
	
	// Update is called once per frame
	void Update () {

        timer += Time.fixedDeltaTime;
        if (timer >= spawnIntervalTemp)
        {
            timer = 0;
            GameObject myEnemy = myPD.ObjRequest(DepotItem.enemyType1);
            myEnemy.transform.position = new Vector3(Random.Range(-100, 100), 0, Random.Range(-50, 50));
            myEnemy.SetActive(true);
        }
		
	}
}
