using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    bool playerIsInRange = false;
    [SerializeField] Animator anim;
    [SerializeField] AudioClip popUpSFX;

    private void Update()
    {
        if (!playerIsInRange) { return; }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    public void PlayerIsInRange(bool value)
    {
        playerIsInRange = value;

        anim.SetBool("CanInteract", value);

        SoundManager.i.PlaySound(popUpSFX);
    }

    public void Interact()
    {
        NPC npc = GetComponent<NPC>();
        PickableItem pickableItem = GetComponent<PickableItem>();

        if(npc != null)
        {
            PlayerIsInRange(false);
            npc.TalkWithPlayer();
        }

        if (pickableItem != null)
        {
            PlayerIsInRange(false);
            pickableItem.PickUpItem();
        }
    }
}
