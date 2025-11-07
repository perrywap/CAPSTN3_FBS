using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable  
{
    [Header("TASK SETTINGS")]
    public string taskName;
    public bool isTaskObject;
    public bool isCompleted;

    [Header("INTERACTION SETTINGS")]
    public GameObject interactTxt;


    void Start()
    {
        isCompleted = false;
        if(isTaskObject)
            TaskManager.Instance.RegisterTask(this.gameObject);
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
    }

    public virtual void CompleteTask()
    {
        if (isCompleted) return;
        if (!isTaskObject) return;

        isCompleted = true;
        Debug.Log($"{taskName} completed!");
        TaskManager.Instance.CheckAllTasks();
    }

    private void OnTriggerStay(Collider collision)
    {
        FirstPersonController player = collision.gameObject.GetComponent<FirstPersonController>();

        if (player != null)
        {
            //if (isCompleted) return;
            if (interactTxt == null) return;

            interactTxt.SetActive(true);
        }
    }

    public void OnTriggerEnter(Collider collision)
    {
        
    }

    public void OnTriggerExit(Collider collision)
    {
        FirstPersonController player = collision.gameObject.GetComponent<FirstPersonController>();

        if (player != null)
        {
            if (interactTxt == null) return;
            interactTxt.SetActive(false);
        }
    }
}
