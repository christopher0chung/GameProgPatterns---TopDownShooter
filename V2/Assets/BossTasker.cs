using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskMoveToRandom: Task
{
    private Vector3 destination;

    public override void Init()
    {
        destination = new Vector3(Random.Range(-30, 30), 0, Random.Range(-20, 20));
        Debug.Log(destination);
    }

    public override void TaskUpdate()
    {
        this.transform.position = (Vector3.MoveTowards(transform.position, destination, 10 * Time.deltaTime));
        if (Vector3.Distance(transform.position, destination) < .25f)
        {
            transform.position = destination;
            SetStatus(TaskStatus.Success);
        }
    }

    public override void OnSuccess()
    {
        base.OnSuccess();
        TaskMoveToRandom myTMTR = gameObject.AddComponent<TaskMoveToRandom>();
        Debug.Log(myTMTR.Status);
        GetComponent<TaskManager>().AddTask(TaskType.Movement, myTMTR);
    }
}

public class TaskFlee : Task
{
    private Vector3 destination;

    public override void Init()
    {
        float dir = Random.Range(0, 360);
        destination = GameObject.FindGameObjectWithTag("Player").transform.position + 10 * new Vector3(Mathf.Sin(dir * Mathf.Deg2Rad), 0, Mathf.Cos(dir * Mathf.Deg2Rad));
        Debug.Log(destination);
    }

    public override void TaskUpdate()
    {
        this.transform.position = (Vector3.MoveTowards(transform.position, destination, 10 * Time.deltaTime));
        if (Vector3.Distance(transform.position, destination) < .25f)
        {
            transform.position = destination;
            SetStatus(TaskStatus.Success);
        }
    }

    public override void OnSuccess()
    {
        base.OnSuccess();
        TaskSeek myTS = gameObject.AddComponent<TaskSeek>();
        Debug.Log(myTS.Status);
        GetComponent<TaskManager>().AddTask(TaskType.Movement, myTS);
    }
}

public class TaskSeek : Task
{
    private Vector3 destination;

    public override void Init()
    {
        destination = GameObject.FindGameObjectWithTag("Player").transform.position;
        Debug.Log(destination);
    }

    public override void TaskUpdate()
    {
        this.transform.position = (Vector3.MoveTowards(transform.position, destination, 30 * Time.deltaTime));
        if (Vector3.Distance(transform.position, destination) < 5f)
        {
            transform.position = destination;
            SetStatus(TaskStatus.Success);
        }
    }

    public override void OnSuccess()
    {
        base.OnSuccess();
        TaskSeek myTS = gameObject.AddComponent<TaskSeek>();
        Debug.Log(myTS.Status);
        GetComponent<TaskManager>().AddTask(TaskType.Movement, myTS);
    }
}

public class TaskGrowToFull : Task
{
    private Vector3 startSize;
    private Vector3 finishSize;

    public override void Init()
    {
        transform.localScale = new Vector3(.01f, .01f, .01f);
    }

    public override void TaskUpdate()
    {
        this.transform.localScale = transform.localScale *= 1.1f;
        if (transform.localScale.x >= .99f)
        {
            transform.localScale = Vector3.one;
            SetStatus(TaskStatus.Success);
        }
    }

    public override void OnSuccess()
    {
        base.OnSuccess();
        TaskSpawnBaddies myTSB = gameObject.AddComponent<TaskSpawnBaddies>();
        //Debug.Log(myTSB.Status);
        Then(myTSB);
    }
}

public class TaskSpawnBaddies : Task
{

    public override void Init()
    {
        GameObject.Find("Managers").GetComponent<MobManager>().Make(2, transform.position + Vector3.one, "Enemy");
    }

    public override void TaskUpdate()
    {
        if (GameObject.Find("Managers").GetComponent<MobManager>().NumOfType(ManagedObjectTypes.enemyType1) + GameObject.Find("Managers").GetComponent<MobManager>().NumOfType(ManagedObjectTypes.enemyType2) <= 0)
        {
            GameObject.Find("Managers").GetComponent<MobManager>().Make(2, transform.position + Vector3.one, "Enemy");
        }
    }

    public override void OnSuccess()
    {
        base.OnSuccess();
        TaskSpawnBaddies myTSB = gameObject.AddComponent<TaskSpawnBaddies>();
        Debug.Log(myTSB.Status);
        Then(myTSB);
    }
}

public class TaskSpawnBaddiesConstantly : Task
{
    float timer;
    public override void Init()
    {
        GameObject.Find("Managers").GetComponent<MobManager>().Make(5, transform.position, "Enemy");
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
        TaskSpawnBaddiesConstantly myTSBC = gameObject.AddComponent<TaskSpawnBaddiesConstantly>();
        Debug.Log(myTSBC.Status);
        GetComponent<TaskManager>().AddTask(TaskType.Movement, myTSBC);
    }
}

public class TaskFireAtRandom : Task
{
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
            BulletBase b = myBM.Make(transform.position + 5 * new Vector3(Mathf.Sin(dir * Mathf.Deg2Rad), 0, Mathf.Cos(dir * Mathf.Deg2Rad)), "Enemy");
            b.GetComponent<Rigidbody>().AddForce(new Vector3(Mathf.Sin(dir * Mathf.Deg2Rad), 0, Mathf.Cos(dir * Mathf.Deg2Rad)) * 130f, ForceMode.Impulse);
        }
    }

    public override void OnSuccess()
    {
        //base.OnSuccess();
        //TaskFireAtRandom myTFAR = gameObject.AddComponent<TaskFireAtRandom>();1
        //Debug.Log(myTFAR.Status);
        //GetComponent<TaskManager>().AddTask(TaskType.Movement, myTFAR);
    }
}

public class BossTasker : MonoBehaviour, IShootable
{
    [SerializeField] private int health;
    private int prevHealth;

    private TaskManager myTM;

	void Start () {
        health = 100;
        myTM = GetComponent<TaskManager>();
        TaskGrowToFull myTGTF = gameObject.AddComponent<TaskGrowToFull>();
        myTM.AddTask(TaskType.Movement, myTGTF);
	}

    public void OnShoot(int dmg)
    {
        prevHealth = health;
        health -= dmg;

        if (prevHealth >= 50 && health < 50)
        {
            myTM.AbortAll(TaskType.Movement);
            TaskFireAtRandom myTFAR = gameObject.AddComponent<TaskFireAtRandom>();
            myTM.AddTask(TaskType.Movement, myTFAR);
        }

        if (prevHealth >= 30 && health < 30)
        {

        }

        if (prevHealth >= 15 && health < 15)
        {
            myTM.AbortAll(TaskType.Movement);
            TaskSeek myTS = gameObject.AddComponent<TaskSeek>();
            myTM.AddTask(TaskType.Movement, myTS);
        }
    }
}
