using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable  
{
    [Header("TASK SETTINGS")]
    public string taskName;
    public bool isCompleted;

    [Header("INTERACTION SETTINGS")]
    public GameObject interactTxt;
    public GameObject spawnedObj;
    public Transform spawnPos;

    void Start()
    {
        isCompleted = false;
        TaskManager.Instance.RegisterTask(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Interact()
    {
        Debug.Log("Interacted with: " + this.name);
        CompleteTask();
    }

    public virtual void CompleteTask()
    {
        if (isCompleted) return;

        isCompleted = true;
        Debug.Log($"{taskName} completed!");
        TaskManager.Instance.CheckAllTasks();
    }

    public void OnTriggerEnter(Collider collision)
    {
        FirstPersonController player = collision.gameObject.GetComponent<FirstPersonController>();

        if (player != null)
        {
            interactTxt.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider collision)
    {
        FirstPersonController player = collision.gameObject.GetComponent<FirstPersonController>();

        if (player != null)
        {
            interactTxt.SetActive(false);
        }
    }
}
