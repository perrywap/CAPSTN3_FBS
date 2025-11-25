using Unity.VisualScripting;
using UnityEngine;

public class Tool : MonoBehaviour, IInteractable
{
    public string toolName;
    public Sprite icon;
    public GameObject prefab;

    private void Start()
    {
        prefab = this.gameObject;
    }

    public virtual void Interact()
    {
        Debug.Log("Interacted with: " + this.name);

        if (InventoryManager.Instance.toolCount >= InventoryManager.Instance.maxToolCount)
        {
            // NOTIFY PLAYER THAT INVENTORY IS FULL
            Debug.Log("Inventory is full");
            return;
        }
            

        GameObject toolGO = Instantiate(this.gameObject);
        toolGO.transform.SetParent(InventoryManager.Instance.transform);
        toolGO.SetActive(false);

        //InventoryManager.Instance.tools.Add(toolGO.GetComponent<Tool>());
        InventoryManager.Instance.AddTool(toolGO.GetComponent<Tool>());

        Destroy(this.gameObject);
        
    }
}
