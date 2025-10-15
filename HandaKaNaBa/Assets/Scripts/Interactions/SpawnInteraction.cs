using UnityEngine;

public class SpawnInteraction : InteractableObject
{
    [SerializeField] private GameObject spawnedObj;
    [SerializeField] private Transform spawnPos;

    public override void Interact()
    {
        if (isCompleted) return;

        base.Interact();
        GameObject spawnedGO = Instantiate(spawnedObj, spawnPos.position, Quaternion.identity);
    }
}
