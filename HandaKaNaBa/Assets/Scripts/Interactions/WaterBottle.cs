using UnityEngine;

public class WaterBottle : Tool
{
    public Sprite newIcon;

    public void ReplaceIcon()
    {
        // Update this tool's icon
        this.icon = newIcon;

        // Refresh UI if inventory is open
        if (InventoryManager.Instance.inventoryUI.activeSelf)
        {
            InventoryManager.Instance.OnOpenInventory();
        }
    }
}