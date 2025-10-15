using UnityEngine;

public class RemodelInteraction : InteractableObject
{
    [SerializeField] private GameObject currentGO;
    [SerializeField] private GameObject newGO;

    public override void Interact()
    {
        if (isCompleted) return;

        base.Interact();
        currentGO.SetActive(false);
        newGO.SetActive(true);
    }
}
