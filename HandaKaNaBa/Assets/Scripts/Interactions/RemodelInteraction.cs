using UnityEngine;

public class RemodelInteraction : InteractableObject
{
    [SerializeField] private GameObject currentGO;
    [SerializeField] private GameObject newGO;

    public override void Interact()
    {
        base.Interact();
        currentGO.SetActive(false);
        newGO.SetActive(true);
    }
}
