using UnityEngine;

public class WaterDispenser : NeedToolInteraction
{
    public override void Interact()
    {
        if (isCompleted) return;

        base.Interact();

        // Search for a WaterBottle inside the player's inventory
        foreach (Tool tool in InventoryManager.Instance.tools)
        {
            WaterBottle bottle = tool as WaterBottle;

            if (bottle != null)
            {
                bottle.ReplaceIcon();
                Debug.Log("Water bottle refilled!");
                return;
            }
        }

        // If player has no water bottle
        Debug.Log("You don't have an empty water bottle.");
    }
}
