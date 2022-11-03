using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Interactable")
        {
            Interactable interactable = other.GetComponent<Interactable>();
            if(interactable != null)
            {
                interactable.PlayerIsInRange(true);
            }
            else
            {
                Debug.LogError("Interactable Component not found!");
            }   
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Interactable interactable = other.GetComponent<Interactable>();

        if (other.gameObject.tag == "Interactable")
        {
            if (interactable != null)
            {
                interactable.PlayerIsInRange(false);
            }
            else
            {
                Debug.LogError("Interactable Component not found!");
            }
        }

    }
}
