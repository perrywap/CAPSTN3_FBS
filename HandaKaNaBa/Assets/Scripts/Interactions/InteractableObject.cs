using UnityEngine;

public enum TaskType
{
    None,
    LockDoors,
    LockWindows,
    EmergencyKit,
    WaterRefill,
    SecureValuables
}

public class InteractableObject : MonoBehaviour, IInteractable  
{
    [Header("TASK SETTINGS")]
    public TaskType taskType;
    public string taskName;
    public bool isTaskObject;
    public bool isCompleted;
    public bool hideOnComplete;

    [Header("SFX SETTINGS")]
    public AudioSource src;
    public AudioClip clip;

    void Start()
    {
        isCompleted = false;
        if (isTaskObject)
            TaskManager.Instance.RegisterTask(this);
        else
            taskType = TaskType.None;
    }

    public virtual void Interact()
    {
        Debug.Log("Interacted with: " + this.name);
        CompleteTask();
        PlaySound();
    }

    protected void PlaySound()
    {
        // If object doesn't need sfx, return
        if (src == null || clip == null) return;

        src.clip = clip;
        src.Play();
    }
    public virtual void CompleteTask()
    {
        if (isCompleted) return;
        if (!isTaskObject) return;

        isCompleted = true;
        TaskManager.Instance.CheckTaskProgress(taskType);
    }
}
