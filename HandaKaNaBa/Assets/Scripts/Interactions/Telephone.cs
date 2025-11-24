using UnityEngine;

public class Telephone :InteractableObject
{
    public override void Interact()
    {
        if (isCompleted) return;
        
        base.Interact();
        // Put code for cloud bubble interaction
    }
}
