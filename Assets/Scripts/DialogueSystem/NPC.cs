using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public string npcName;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] float talkingDistance = 18f;
    public GameObject npcCam;
    [SerializeField] DialogueManager dialogueManager;

    [TextArea(3, 6)]
    public string[] dialogLines;

    public bool isShopkeeper = false;

    public void TalkWithPlayer()
    { 
        npcCam.SetActive(true);

        MakePlayerMoveToDesiredPos();

        //if there are more dialogue lines, you should choose the correct ones to display.
        dialogueManager.StartDialogue(this);

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
