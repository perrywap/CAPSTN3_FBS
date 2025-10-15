using Unity.VisualScripting;
using UnityEngine;

public class Tool : MonoBehaviour, IInteractable
{
    public string toolName;
    public Sprite icon;
    public GameObject interactTxt;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Interact()
    {
        Debug.Log("Interacted with: " + this.name);

        GameObject toolGO = Instantiate(this.gameObject);
        toolGO.transform.SetParent(InventoryManager.Instance.transform);
        toolGO.SetActive(false);

        InventoryManager.Instance.tools.Add(toolGO.GetComponent<Tool>());
        Destroy(this.gameObject);
        
    }

    private void OnTriggerStay(Collider collision)
    {
        FirstPersonController player = collision.gameObject.GetComponent<FirstPersonController>();

        if (player != null)
        {
            //if (isCompleted) return;

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
            interactTxt.SetActive(false);
        }
    }
}
