using UnityEngine;

public class Interactor : MonoBehaviour
{
    public System.Action<Collider> OnStay;
    public System.Action<Collider> OnExit;

    private void OnTriggerStay(Collider other)
    {
        OnStay?.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        OnExit?.Invoke(other);
    }
}
