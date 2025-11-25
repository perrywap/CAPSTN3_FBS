using UnityEngine;

public class CloseWindow : InteractableObject
{
    [SerializeField] private Material glassMat;
    private Material[] materials;

    private void Awake()
    {
        materials = this.GetComponent<MeshRenderer>().materials;
        glassMat = materials[3];

        glassMat.SetInt("_Cull", 1);
    }
    public override void Interact()
    {
        if (isCompleted) return;

        base.Interact();
        glassMat.SetInt("_Cull", 2);
    }
}
