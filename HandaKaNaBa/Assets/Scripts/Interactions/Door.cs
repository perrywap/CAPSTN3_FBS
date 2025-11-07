using UnityEngine;
using System.Collections;

public class Door : InteractableObject
{
    [SerializeField] private Transform pivot;
    [SerializeField] private bool isOpen;
    [SerializeField] private float openAngle = 90f;
    [SerializeField] private float rotationSpeed = 2f;

    public enum DoorDirection { Push, Pull }
    [SerializeField] private DoorDirection doorDirection = DoorDirection.Push;

    private Coroutine rotateCoroutine;

    public override void Interact()
    {
        base.Interact();

        if (rotateCoroutine != null)
            StopCoroutine(rotateCoroutine);

        // Determine target rotation based on open/close state and direction
        Quaternion targetRotation;

        if (!isOpen)
        {
            float targetAngle = (doorDirection == DoorDirection.Push) ? openAngle : -openAngle;
            targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
            rotateCoroutine = StartCoroutine(RotateDoor(targetRotation));
            isOpen = true;
        }
        else
        {
            targetRotation = Quaternion.Euler(0f, 0f, 0f);
            rotateCoroutine = StartCoroutine(RotateDoor(targetRotation));
            isOpen = false;
        }
    }

    private IEnumerator RotateDoor(Quaternion targetRotation)
    {
        Quaternion startRotation = pivot.localRotation;
        float time = 0f;

        while (time < 1f)
        {
            this.GetComponent<BoxCollider>().enabled = false;
            time += Time.deltaTime * rotationSpeed;
            pivot.localRotation = Quaternion.Slerp(startRotation, targetRotation, time);
            yield return null;
        }
        this.GetComponent<BoxCollider>().enabled = true;
        pivot.localRotation = targetRotation;
    }
}
