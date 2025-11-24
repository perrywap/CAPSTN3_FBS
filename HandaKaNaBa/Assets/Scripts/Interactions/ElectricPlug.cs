using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ElectricPlug : InteractableObject
{
    [SerializeField] private GameObject chargerObj;
    [SerializeField] private Transform chargerPos;

    [SerializeField] private GameObject phoneObj;
    [SerializeField] private Transform phonePos;

    [SerializeField] private List<Tool> neededTools;

    private void Update()
    {
        if (this.gameObject.GetComponentInChildren<AppliancePlug>())
        {
            this.GetComponent<BoxCollider>().enabled = false;
        }
        else
            this.GetComponent<BoxCollider>().enabled = true;

    }

    public override void Interact()
    {
        Debug.Log("Interacted with " + this.name);
        if (isCompleted) return;

        

        List<Tool> playerTools = InventoryManager.Instance.tools;

        bool hasAllTools = neededTools.All(needed =>
           InventoryManager.Instance.tools.Any(playerTool => playerTool.toolName == needed.toolName));

        if (hasAllTools)
        {
            RemoveNeededToolsFromInventory();

            base.Interact();
            GameObject chargerGO = Instantiate(chargerObj, chargerPos.position, chargerPos.rotation);
            GameObject phoneGO = Instantiate(phoneObj, phonePos.position, phonePos.rotation);

            CompleteAllOtherPlugs();
        }
        else
            Debug.Log("You need both charger and phone");
    }

    private void CompleteAllOtherPlugs()
    {
        ElectricPlug[] allPlugs = FindObjectsByType<ElectricPlug>(FindObjectsSortMode.None);

        foreach (ElectricPlug plug in allPlugs)
        {
            if (plug == this) continue; // skip the one we interacted with

            if (plug.isTaskObject && !plug.isCompleted)
            {
                plug.isCompleted = true;
            }
        }
    }


    private void RemoveNeededToolsFromInventory()
    {
        List<Tool> playerTools = InventoryManager.Instance.tools;

        foreach (Tool needed in neededTools)
        {
            Tool toolToRemove = playerTools
                .FirstOrDefault(t => t.toolName == needed.toolName);

            if (toolToRemove != null)
                playerTools.Remove(toolToRemove);
        }
    }

}
