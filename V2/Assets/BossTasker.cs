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

public class BossTasker : MonoBehaviour {

    private TaskManager myTM;

	void Start () {
        myTM = GetComponent<TaskManager>();
        TaskMoveToRandom myTMTR = gameObject.AddComponent<TaskMoveToRandom>();
        Debug.Log(myTMTR.Status);
        myTM.AddTask(TaskType.Movement, myTMTR);
	}
}
