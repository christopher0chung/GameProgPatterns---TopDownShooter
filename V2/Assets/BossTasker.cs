using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskMoveToRandom: Task
{
    private Vector3 destination;

    public override void Init()
    {
        destination = new Vector3(Random.Range(-30, 30), 0, Random.Range(-20, 20));
    }

    public override void TaskUpdate()
    {
        this.transform.position = ( Vector3.MoveTowards(transform.position, destination, 10 * Time.deltaTime));
        Debug.Log(transform.position + " " + destination);
        if (Vector3.Distance(transform.position, destination) < .25f)
        {
            transform.position = destination;
            SetStatus(TaskStatus.Success);
        }
    }
}

public class BossTasker : MonoBehaviour {

    private TaskManager myTM;

	// Use this for initialization
	void Start () {
        myTM = GetComponent<TaskManager>();
        myTM.AddTask(TaskType.Movement, new TaskMoveToRandom());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
