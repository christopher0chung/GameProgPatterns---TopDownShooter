using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TaskType { Movement, Combat }
public enum TaskStatus : byte { Detached, Pending, Working, Success, Fail, Aborted }

public class Task : MonoBehaviour
{
    public TaskStatus Status { get; private set; }
    public Task NextTask { get; private set; }

    public bool IsDetached { get { return Status == TaskStatus.Detached; } }
    public bool IsAttached { get { return Status != TaskStatus.Detached; } }
    public bool IsPending { get { return Status == TaskStatus.Pending; } }
    public bool IsWorking { get { return Status == TaskStatus.Working; } }
    public bool IsSuccessful { get { return Status == TaskStatus.Success; } }
    public bool IsFailed { get { return Status == TaskStatus.Fail; } }
    public bool IsAborted { get { return Status == TaskStatus.Aborted; } }
    public bool IsFinished { get { return (Status == TaskStatus.Fail || Status == TaskStatus.Success || Status == TaskStatus.Aborted); } }

    internal void SetStatus(TaskStatus newStatus)
    {
        if (Status == newStatus) return;

        Status = newStatus;

        switch (newStatus)
        {
            case TaskStatus.Working:
                Init();
                break;

            case TaskStatus.Success:
                OnSuccess();
                CleanUp();
                break;

            case TaskStatus.Aborted:
                OnAbort();
                CleanUp();
                break;

            case TaskStatus.Fail:
                OnFail();
                CleanUp();
                break;

            case TaskStatus.Detached:
            case TaskStatus.Pending:
                break;
            default:
                Debug.Log("Task error:" + Status);
                break;
        }
    }


    public virtual void Init() { }

    public virtual void TaskUpdate() { }

    public virtual void CleanUp() { }

    public virtual void OnSuccess() { }

    public virtual void OnFail() { }

    public virtual void OnAbort() { }

    public virtual void Abort()
    {
        SetStatus(TaskStatus.Aborted);
    }

    public Task Then(Task task)
    {
        Debug.Assert(!task.IsAttached);
        NextTask = task;
        return task;
    }
}

public class TaskManager : MonoBehaviour {

    [SerializeField] private List<Task> movementTasks = new List<Task>();
    [SerializeField] private List<Task> combatTasks = new List<Task>();

	void Start () {
		
	}
	
	void Update () {

        for (int i = movementTasks.Count - 1; i >= 0; --i)
        {
            Task task = movementTasks[i];

            if (task.IsPending)
            {
                task.SetStatus(TaskStatus.Working);
            }

            if (task.IsFinished)
            {
                HandleCompletion(TaskType.Movement, task, i);
            }
            else
            {
                task.TaskUpdate();
                if (task.IsFinished)
                {
                    HandleCompletion(TaskType.Movement, task, i);
                }
            }
        }

        for (int i = combatTasks.Count - 1; i >= 0; --i)
        {
            Task task = combatTasks[i];

            if (task.IsPending)
            {
                task.SetStatus(TaskStatus.Working);
            }

            if (task.IsFinished)
            {
                HandleCompletion(TaskType.Combat, task, i);
            }
            else
            {
                task.TaskUpdate();
                if (task.IsFinished)
                {
                    HandleCompletion(TaskType.Combat, task, i);
                }
            }
        }
    }

    public void AddTask(TaskType myType, Task myTask)
    {
        Debug.Assert(myTask != null);
        Debug.Assert(!myTask.IsAttached);
        if (myType==TaskType.Movement)
            movementTasks.Add(myTask);
        else if (myType == TaskType.Combat)
            combatTasks.Add(myTask);
        myTask.SetStatus(TaskStatus.Pending);
    }

    public void AbortAll(TaskType myType)
    {
        if (myType == TaskType.Movement)
        {
            foreach (Task thisTask in movementTasks)
            {
                thisTask.SetStatus(TaskStatus.Aborted);
            }
        }
    }

    public void AbortType (TaskType myType, Task T)
    {
        if (myType == TaskType.Movement)
        {
            foreach (Task thisTask in movementTasks)
            {
                if (thisTask.GetType() == T.GetType())
                    thisTask.SetStatus(TaskStatus.Aborted);
            }
        }
    }

    private void HandleCompletion(TaskType myType, Task task, int taskIndex)
    {
        if (task.NextTask != null && task.IsSuccessful)
        {
            AddTask(myType, task.NextTask);
        }

        if (myType == TaskType.Movement)
            movementTasks.RemoveAt(taskIndex);
        else if (myType == TaskType.Combat)
            combatTasks.RemoveAt(taskIndex);

        task.SetStatus(TaskStatus.Detached);
    }
}
