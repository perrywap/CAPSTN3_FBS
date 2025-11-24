using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EmergencyKit : InteractableObject
{
    [SerializeField] private List<Tool> neededTools;       // Tools required by this kit
    [SerializeField] private List<Tool> toolsDeposited = new List<Tool>();

    public override void Interact()
    {
        if (isCompleted) return;

        List<Tool> playerTools = InventoryManager.Instance.tools;

        // Track if we deposited any tools this interaction
        bool depositedAny = false;

        // Group needed tools by name to handle duplicates
        var neededGroups = neededTools.GroupBy(nt => nt.toolName)
                                      .ToDictionary(g => g.Key, g => g.Count());

        foreach (var kvp in neededGroups)
        {
            string toolName = kvp.Key;
            int requiredCount = kvp.Value;

            // Count how many are already deposited
            int alreadyDeposited = toolsDeposited.Count(td => td.toolName == toolName);

            // How many still need to be deposited
            int remainingToDeposit = requiredCount - alreadyDeposited;

            if (remainingToDeposit <= 0) continue;

            // Find matching tools in player inventory
            var matchingTools = playerTools.Where(pt => pt.toolName == toolName).ToList();

            // Deposit only up to the remaining amount
            int depositCount = Mathf.Min(remainingToDeposit, matchingTools.Count);

            for (int i = 0; i < depositCount; i++)
            {
                Tool tool = matchingTools[i];

                //playerTools.Remove(tool);       // Remove from inventory
                InventoryManager.Instance.RemoveTool(tool);
                toolsDeposited.Add(tool);       // Add to kit

                Debug.Log($"Deposited {tool.toolName}");
                depositedAny = true;
            }
        }

        // Only call Interact() and mark complete if all needed tools are deposited
        bool allToolsDeposited = neededTools.All(nt =>
            toolsDeposited.Count(td => td.toolName == nt.toolName) >=
            neededTools.Count(t => t.toolName == nt.toolName));

        if (allToolsDeposited)
        {
            base.Interact();
            //`this.gameObject.SetActive(false);
            isCompleted = true;
        }
        else if (depositedAny)
        {
            Debug.Log("You deposited some tools, but not all yet.");
        }
        else
        {
            Debug.Log("You don't have any of the required tools!");
        }
    }
}
