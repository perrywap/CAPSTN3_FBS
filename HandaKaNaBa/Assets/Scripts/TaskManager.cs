using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class TaskManager : MonoBehaviour
{
    /*
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

    public void CheckTaskProgress()
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
    */

    public static TaskManager Instance { get; private set; }

    // Group all TaskObjects in the scene
    [SerializeField] private Dictionary<TaskType, List<InteractableObject>> taskGroups = new Dictionary<TaskType, List<InteractableObject>>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void RegisterTask(InteractableObject taskObject)
    {
        if (!taskGroups.ContainsKey(taskObject.taskType))
            taskGroups[taskObject.taskType] = new List<InteractableObject>();

        taskGroups[taskObject.taskType].Add(taskObject);
    }

    /// <summary>
    /// Called when a TaskObject is completed
    /// </summary>
    public void CheckTaskProgress(TaskType type)
    {
        if (IsTaskTypeComplete(type))
        {
            Debug.Log($"Task Completed: {type}");
        }
        else
        {
            Debug.Log($"Task {type} still in progress...");
        }

        // Optionally: check if all tasks in the game are done
        if (AreAllTasksCompleted())
        {
            Debug.Log("All tasks completed! Player can proceed.");
        }
    }

    public bool IsTaskTypeComplete(TaskType type)
    {
        if (!taskGroups.ContainsKey(type)) return false;

        // All TaskObjects under this type must be completed
        return taskGroups[type].All(t => t.isCompleted);
    }

    public bool AreAllTasksCompleted()
    {
        return taskGroups.Values.All(group => group.All(t => t.isCompleted));
    }
}
