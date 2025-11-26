using UnityEngine;

public class StandFan : InteractableObject
{
    [SerializeField] private GameObject plug;
    [SerializeField] private GameObject fan;
    [SerializeField] private bool isOn;
    [SerializeField] private bool isPlugged;

    [SerializeField] private float rotationSpeed = 300f;

    private void Update()
    {
        // Update isPlugged based on plug active state
        isPlugged = plug != null && plug.activeSelf;

        // Rotate only if plugged AND turned on
        if (isPlugged && isOn)
        {
            fan.transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }

        if (!isPlugged)
        {
            isTaskObject = true;
            isCompleted = true;
            Debug.Log("Fan is not plugged in!");
            return;
        }
    }

    public override void Interact()
    {
        base.Interact();

        // Re-check plug state
        isPlugged = plug != null && plug.activeSelf;

        if (!isPlugged)
        {
            Debug.Log("Fan is not plugged in!");
            return;
        }

        // Toggle ON/OFF
        isOn = !isOn;

        if (isOn)
            Debug.Log("Fan turned ON");
        else
            Debug.Log("Fan turned OFF");
    }
}
