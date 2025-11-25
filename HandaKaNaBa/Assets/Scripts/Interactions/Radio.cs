using UnityEngine;

public class Radio : InteractableObject
{
    public bool isOpen;

    public override void Interact()
    {
        if( isOpen )
        {
            isOpen = false;

            if (src == null || clip == null) return;
            src.Stop();
        }
        else
        {
            isOpen = true;
            PlaySound();
        }
    }
}
