using UnityEngine;

public class CloseWindow : InteractableObject
{
    [SerializeField] private MeshRenderer renderer;
    [SerializeField] private Material glassMat;
    private Material[] materials;

    private void Awake()
    {
        renderer = GetComponent<MeshRenderer>();
        materials = renderer.materials;
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
