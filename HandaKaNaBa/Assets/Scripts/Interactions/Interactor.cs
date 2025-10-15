using UnityEngine;

//interface IInteractable
//{
//    public void Interact();
//}

public class Interactor : MonoBehaviour
{
    public Transform interactorSource;
    public float interactRange;

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.E))
        {
            Ray ray = new Ray(interactorSource.position, interactorSource.forward);

            if(Physics.Raycast(ray, out RaycastHit hitInfo, interactRange))
            {
                if(hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    interactObj.Interact();
                }
            }
        }
    }
}
