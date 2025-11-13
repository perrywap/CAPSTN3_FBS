
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NeedToolInteraction : InteractableObject
{
    [SerializeField] private List<Tool> neededTools;
    [SerializeField] private List<Tool> toolsDeposited;

    public override void Interact()
    {
        if (isCompleted) return;

        bool hasAllTools = neededTools.All(needed =>
            InventoryManager.Instance.tools.Any(playerTool => playerTool.toolName == needed.toolName)
        );
        

        if (hasAllTools)
        {
            base.Interact();
            this.transform.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("You don't have all the required tools!");
        }
    }
}
