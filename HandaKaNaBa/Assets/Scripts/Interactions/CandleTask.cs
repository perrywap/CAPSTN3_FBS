using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CandleTask : InteractableObject
{
    [SerializeField] private List<Tool> neededTools;
    [SerializeField] private bool hasAllTools;

    private void Update()
    {
        if(isCompleted)
            return;

        hasAllTools = neededTools.All(needed => InventoryManager.Instance.tools.Any(playerTool => playerTool.toolName == needed.toolName));

        if (hasAllTools)
        {
            Debug.Log("Interacted with: " + this.name);
            CompleteTask();
            PlaySound();
        }
    }
}
