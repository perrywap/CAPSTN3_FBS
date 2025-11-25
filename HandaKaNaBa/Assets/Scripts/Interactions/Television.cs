using UnityEngine;
using UnityEngine.UI;

public class Television : InteractableObject
{
    [SerializeField] private bool isOpen;          // TV on/off
    [SerializeField] private GameObject screen;    // The screen object
    [SerializeField] private GameObject plug;      // Assign the plug object
    [SerializeField] private bool isPlugged;       // True if plug.activeSelf == true

    public override void Interact()
    {
        base.Interact();

        // Determine if plugged in
        isPlugged = plug != null && plug.activeSelf;

        // If not plugged, do not turn on
        if (!isPlugged)
        {
            Debug.Log("TV is not plugged in!");
            return;
        }

        // Toggle ON/OFF
        isOpen = !isOpen;
        screen.SetActive(isOpen);
    }
}
