using UnityEngine;

public class RemodelInteraction : InteractableObject
{
    [SerializeField] private GameObject currentGO;
    [SerializeField] private GameObject newGO;

    public override void Interact()
    {
        Debug.Log("Interacted with " + this.name);
        if (isCompleted) return;

        base.Interact();
        currentGO.SetActive(false);
        newGO.SetActive(true);
    }
}
