using UnityEngine;

public class SampleInteractable : InteractableObject
{
    public override void Interact()
    {
        base.Interact();
        GameObject spawnedGO = Instantiate(spawnedObj, spawnPos.position, Quaternion.identity);
    }
}
