using System.Linq;
using UnityEngine;

public class DoorGap : InteractableObject
{
    [SerializeField] private GameObject sandbagObj;   // prefab to spawn
    [SerializeField] private Transform sandbagPos;    // where to spawn it

    [SerializeField] private Tool sandbagTool;        // the tool to check for

    public override void Interact()
    {
        Debug.Log("Interacted with " + this.name);
        if (isCompleted) return;

        // Check if player has the sandbag
        bool hasSandbag = InventoryManager.Instance.tools
            .Any(t => t.toolName == sandbagTool.toolName);

        if (hasSandbag)
        {
            RemoveSandbagFromInventory();

            // Complete task and spawn sandbag
            base.Interact();
            Instantiate(sandbagObj, sandbagPos.position, sandbagPos.rotation);
        }
        else
        {
            Debug.Log("You need a sandbag.");
        }
    }

    private void RemoveSandbagFromInventory()
    {
        var tools = InventoryManager.Instance.tools;

        Tool sandbag = tools.FirstOrDefault(t => t.toolName == sandbagTool.toolName);

        if (sandbag != null)
            tools.Remove(sandbag);
    }
}
