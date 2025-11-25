using UnityEngine;

public class EndOfDayTrigger : MonoBehaviour
{
    [SerializeField] private EndOfDayManager endOfDayManager;
    [SerializeField] private string playerTag = "Player";

    private bool playerInside = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            playerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            playerInside = false;
        }
    }

    private void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.E))
        {
            endOfDayManager.StartEndOfDaySequence();
        }
    }
}
