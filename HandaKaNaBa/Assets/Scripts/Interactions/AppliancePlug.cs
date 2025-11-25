using UnityEngine;

public class AppliancePlug : InteractableObject
{
    public override void Interact()
    {
        if (isCompleted) return;

        base.Interact();
        this.gameObject.SetActive(false);
    }
}
