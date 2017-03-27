using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskMoveToRandom: Task
{
    private Vector3 destination;

    public TaskMoveToRandom (GameObject gO)
    {
        GORef = gO;
    }

    public override void Init()
    {
        destination = new Vector3(Random.Range(-30, 30), 0, Random.Range(-20, 20));
        Debug.Log(destination);
    }

    public override void TaskUpdate()
    {
        GORef.transform.position = (Vector3.MoveTowards(GORef.transform.position, destination, 10 * Time.deltaTime));
        if (Vector3.Distance(GORef.transform.position, destination) < .25f)
        {
            GORef.transform.position = destination;
            SetStatus(TaskStatus.Success);
        }
    }

    public override void OnSuccess()
    {
        base.OnSuccess();
    }
}

public class TaskFlee : Task
{
    public TaskFlee(GameObject gO)
    {
        GORef = gO;
    }

    private Vector3 destination;

    public override void Init()
    {
        float dir = Random.Range(0, 360);
        destination = GameObject.FindGameObjectWithTag("Player").transform.position + 10 * new Vector3(Mathf.Sin(dir * Mathf.Deg2Rad), 0, Mathf.Cos(dir * Mathf.Deg2Rad));
        Debug.Log(destination);
    }

    public override void TaskUpdate()
    {
        GORef.transform.position = (Vector3.MoveTowards(GORef.transform.position, destination, 10 * Time.deltaTime));
        if (Vector3.Distance(GORef.transform.position, destination) < .25f)
        {
            GORef.transform.position = destination;
            SetStatus(TaskStatus.Success);
        }
    }

    public override void OnSuccess()
    {
        base.OnSuccess();
    }
}

public class TaskSeek : Task
{
    public TaskSeek(GameObject gO)
    {
        GORef = gO;
    }

    private Vector3 destination;

    public override void Init()
    {
        destination = GameObject.FindGameObjectWithTag("Player").transform.position;
        Debug.Log(destination);
    }

    public override void TaskUpdate()
    {
        GORef.transform.position = (Vector3.MoveTowards(GORef.transform.position, destination, 30 * Time.deltaTime));
        if (Vector3.Distance(GORef.transform.position, destination) < 5f)
        {
            GORef.transform.position = destination;
            SetStatus(TaskStatus.Success);
        }
    }

    public override void OnSuccess()
    {
        base.OnSuccess();
    }
}

public class TaskGrowToFull : Task
{
    public TaskGrowToFull(GameObject gO)
    {
        GORef = gO;
    }

    private Vector3 startSize;
    private Vector3 finishSize;

    public override void Init()
    {
        GORef.transform.localScale = new Vector3(.01f, .01f, .01f);
    }

    public override void TaskUpdate()
    {
        GORef.transform.localScale = GORef.transform.localScale *= 1.1f;
        if (GORef.transform.localScale.x >= .99f)
        {
            GORef.transform.localScale = Vector3.one;
            SetStatus(TaskStatus.Success);
        }
    }

    public override void OnSuccess()
    {
        base.OnSuccess();
    }
}

public class TaskSpawnBaddies : Task
{
    public TaskSpawnBaddies(GameObject gO)
    {
        GORef = gO;
    }

public override void Init()
    {
        GameObject.Find("Managers").GetComponent<MobManager>().Make(2, GORef.transform.position + Vector3.one, "Enemy");
    }

    public override void TaskUpdate()
    {
        if (GameObject.Find("Managers").GetComponent<MobManager>().NumOfType(ManagedObjectTypes.enemyType1) + GameObject.Find("Managers").GetComponent<MobManager>().NumOfType(ManagedObjectTypes.enemyType2) <= 0)
        {
            GameObject.Find("Managers").GetComponent<MobManager>().Make(2, GORef.transform.position + Vector3.one, "Enemy");
        }
    }

    public override void OnSuccess()
    {
        base.OnSuccess();
    }
}

public class TaskSpawnBaddiesConstantly : Task
{
    public TaskSpawnBaddiesConstantly(GameObject gO)
    {
        GORef = gO;
    }

    float timer;
    public override void Init()
    {
        GameObject.Find("Managers").GetComponent<MobManager>().Make(5, GORef.transform.position, "Enemy");
    }

    public override void TaskUpdate()
    {
        timer += Time.deltaTime;
        if (timer > .5f)
            SetStatus(TaskStatus.Success);
    }

    public override void OnSuccess()
    {
        base.OnSuccess();
    }
}

public class TaskFireAtRandom : Task
{
    public TaskFireAtRandom(GameObject gO)
    {
        GORef = gO;
    }

    private BulletManager myBM;
    private float time;

    public override void Init()
    {
        myBM = GameObject.Find("Managers").GetComponent<BulletManager>();
    }

    public override void TaskUpdate()
    {
        time += Time.deltaTime;
        if (time >= .6f)
        {
            time -= .6f;
            float dir = Random.Range(0, 360);
            BulletBase b = myBM.Make(GORef.transform.position + 5 * new Vector3(Mathf.Sin(dir * Mathf.Deg2Rad), 0, Mathf.Cos(dir * Mathf.Deg2Rad)), "Enemy");
            b.GetComponent<Rigidbody>().AddForce(new Vector3(Mathf.Sin(dir * Mathf.Deg2Rad), 0, Mathf.Cos(dir * Mathf.Deg2Rad)) * 130f, ForceMode.Impulse);
        }
    }

    public override void OnSuccess()
    {
        base.OnSuccess();
    }
}

//public class BossTasker : MonoBehaviour, IShootable
//{
//    [SerializeField] private int health;
//    private int prevHealth;

//    private TaskManager myTM;

//	void Start () {
//        health = 100;
//        myTM = GameObject.Find("Managers").GetComponent<TaskManager>();
//        TaskGrowToFull myTGTF = new TaskGrowToFull(this.gameObject);
//        myTM.AddTask(TaskType.Movement, myTGTF);
//	}

//    public void OnShoot(int dmg)
//    {
//        prevHealth = health;
//        health -= dmg;

//        if (prevHealth >= 50 && health < 50)
//        {
//            myTM.AbortAll(TaskType.Movement);
//            TaskFireAtRandom myTFAR = new TaskFireAtRandom(this.gameObject);
//            myTM.AddTask(TaskType.Movement, myTFAR);
//        }

//        if (prevHealth >= 30 && health < 30)
//        {

//        }

//        if (prevHealth >= 15 && health < 15)
//        {
//            //myTM.AbortAll(TaskType.Movement);
//            //TaskSeek myTS = gameObject.AddComponent<TaskSeek>();
//            //myTM.AddTask(TaskType.Movement, myTS);
//        }
//    }
//}

