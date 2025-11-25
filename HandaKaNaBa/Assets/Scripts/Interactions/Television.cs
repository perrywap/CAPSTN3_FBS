using UnityEngine;
using UnityEngine.UI;
using static Door;

public class Television : InteractableObject
{
    [SerializeField] private bool isOpen;
    [SerializeField] private GameObject screen;

    public override void Interact()
    {
        base.Interact();

        if (!isOpen)
        {
            screen.SetActive(true);
            isOpen = true;
        }
        else
        {
            screen.SetActive(false);
            isOpen = false;
        }
    }
}
