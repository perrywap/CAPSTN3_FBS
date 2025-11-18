using UnityEngine;

public enum TaskType
{
    None,
    LockDoors,
    LockWindows,
    EmergencyKit,
}

public class InteractableObject : MonoBehaviour, IInteractable  
{
    [Header("TASK SETTINGS")]
    public TaskType taskType;
    public string taskName;
    public bool isTaskObject;
    public bool isCompleted;

    [Header("INTERACTION SETTINGS")]
    public GameObject interactTxt;

    [Header("SFX SETTINGS")]
    public AudioSource src;
    public AudioClip clip;

    [SerializeField] private Interactor interactor;

    void Start()
    {
        interactor = GetComponentInChildren<Interactor>();
        if (interactor != null)
        {
            interactor.OnStay += OnStayHandler;
            interactor.OnExit += OnExitHandler;
        }

        isCompleted = false;
        if (isTaskObject)
            //TaskManager.Instance.RegisterTask(this.gameObject);
            TaskManager.Instance.RegisterTask(this);
        else
            taskType = TaskType.None;
    }

    // Update is called once per frame
    void Update()
    {
        if(isCompleted)
            interactTxt.SetActive(false);
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

    private void OnStayHandler(Collider collision)
    {
        FirstPersonController player = collision.gameObject.GetComponent<FirstPersonController>();

        if (player != null)
        {
            if (interactTxt == null) return;
            interactTxt.SetActive(true);
        }
    }

    private void OnExitHandler(Collider collision)
    {
        FirstPersonController player = collision.gameObject.GetComponent<FirstPersonController>();

        if (player != null)
        {
            if (interactTxt == null) return;
            interactTxt.SetActive(false);
        }
    }
}
