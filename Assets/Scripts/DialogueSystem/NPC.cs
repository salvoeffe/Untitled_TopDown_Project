using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] string npcName;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] float talkingDistance = 18f;
    [SerializeField] GameObject npcCam;
    [SerializeField] DialogueManager dialogueManager;

    [SerializeField] string[] testDialogLines;

    public void TalkWithPlayer()
    { 
        /*  0. Move Camera to desired spot with CM
         *  1. Make player reach nearest talking spot
         *  2. Start the Right Dialogue
         *  4. When Dialogue is over make player move again */

        npcCam.SetActive(true);

        MakePlayerMoveToDesiredPos();

        //should choose correct dialogue lines
        dialogueManager.StartDialogue(testDialogLines, npcName, npcCam);

    }

    private void MakePlayerMoveToDesiredPos()
    {
        Vector2 playerPos = playerMovement.transform.position;

        Vector2 firstPossiblePos = new Vector2(transform.position.x - talkingDistance, transform.position.y);
        Vector2 secondPossiblePos = new Vector2(transform.position.x + talkingDistance, transform.position.y);

        if (Vector2.Distance(playerPos, firstPossiblePos) < Vector2.Distance(playerPos, secondPossiblePos))
        {
            playerMovement.StartMovingToDesiredPos(firstPossiblePos);
        }
        else
        {
            playerMovement.StartMovingToDesiredPos(secondPossiblePos);
        }
    }
}
