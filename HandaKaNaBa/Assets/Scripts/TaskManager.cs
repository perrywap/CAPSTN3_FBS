using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance; 

    public List<GameObject> tasks = new List<GameObject>();
    public UnityEvent onAllTasksCompleted;

    public bool allTasksCompleted;

    private void Awake()
    {
        Instance = this;

    }

    public void RegisterTask(GameObject task)
    {
        if (!tasks.Contains(task))
            tasks.Add(task);
    }

    public void CheckAllTasks()
    {
        foreach (GameObject task in tasks)
        {
            if (!task.GetComponent<InteractableObject>().isCompleted)
                return; // If one task isn’t done, stop checking
        }

        // All tasks are completed
        onAllTasksCompleted?.Invoke();
        allTasksCompleted = true;
        Debug.Log("All tasks completed!");
    }
}
