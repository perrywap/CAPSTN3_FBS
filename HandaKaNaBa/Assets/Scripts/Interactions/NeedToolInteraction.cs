using UnityEngine;

public class NeedToolInteraction : InteractableObject
{
    [SerializeField] private Tool neededTool;

    public override void Interact()
    {
        if (isCompleted) return;

        foreach (Tool tool in InventoryManager.Instance.tools)
        {
            if (tool.toolName == neededTool.toolName)
            {
                base.Interact();
                this.transform.parent.gameObject.SetActive(false);
            }
        }
    }
}
