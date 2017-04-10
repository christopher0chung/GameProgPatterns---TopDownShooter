using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipGuns : MonoBehaviour, ILevelManagerInitable
{

    private float timer;
    public float reloadTime;

    public BulletManager myBM;

    // Scene Manager
    // ------------------------------------------
    public void LevelLoaded (int level)
    {
        reloadTime += .005f * level;
    }

    public void LevelUnlaoded ()
    {
        GameObject.Find("Managers").GetComponent<GameStateInitialization>()._OnLoaded -= LevelLoaded;
        GameObject.Find("Managers").GetComponent<GameStateInitialization>()._Unload -= LevelUnlaoded;
    }

    void Awake ()
    {
        //Register into 
        GameObject.Find("Managers").GetComponent<GameStateInitialization>()._OnLoaded += LevelLoaded;
        GameObject.Find("Managers").GetComponent<GameStateInitialization>()._Unload += LevelUnlaoded;
    }
    // ------------------------------------------

    void Start () {
        myBM = GameObject.Find("Managers").GetComponent<BulletManager>();
    }

    void Update () {
        timer += Time.deltaTime;

        if (Input.GetMouseButton(0) && timer >= reloadTime)
        {
            timer = 0;
            BulletBase bullet = myBM.Make(transform.position + transform.forward * 3, this.gameObject.tag);
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 130f, ForceMode.Impulse);
        }

        //if (Input.GetMouseButtonDown(1))
        //{
        //    Debug.Log(myMM.NumOfType(DepotItem.enemyType1));
        //}
    }
}
