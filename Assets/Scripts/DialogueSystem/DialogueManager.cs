using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DialogueManager : MonoBehaviour
{
    bool isTalking = false;

    [SerializeField] GameObject dialogueBoxGO;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] TextMeshProUGUI npcNameText;
    public string[] dialogueLines;
    public float typingSpeed;

    private int index;

    [SerializeField] PlayerMovement playerMovement;
    GameObject currentNPCcam;
    [SerializeField] GameObject gameUI_GO;

    [SerializeField] AudioClip talkingSFX;

    bool isTalkingWithShopkeeper = false;

    void Update()
    {
        if (!isTalking) { return; }

        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return))
        {
            if(dialogueText.text == dialogueLines[index])
            {
                GoToNextLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = dialogueLines[index];
            }
        }
    }

    public void StartDialogue(NPC npc)
    {
        isTalking = true;
        dialogueBoxGO.SetActive(true);
        gameUI_GO.SetActive(false);
        dialogueText.text = string.Empty;
        npcNameText.text = npc.npcName;

        if(npc.isShopkeeper)
        {
            isTalkingWithShopkeeper = true;
        }

        dialogueLines = npc.dialogLines;
        index = 0;
        StartCoroutine(TypeLine());

        currentNPCcam = npc.npcCam;
    }

    IEnumerator TypeLine()
    {
        bool playedSoundLastIteration = false;

        foreach (char c in dialogueLines[index].ToCharArray())
        {
            if (!playedSoundLastIteration)
            {
                if(c != ' ')
                {
                    SoundManager.i.PlaySound(talkingSFX);
                }
            }
            playedSoundLastIteration = !playedSoundLastIteration;

            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void GoToNextLine()
    {
        if(index < dialogueLines.Length - 1)
        {
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            TryToEndDialogue();
        }
    }

    private void TryToEndDialogue()
    {
        dialogueBoxGO.SetActive(false);

        if (isTalkingWithShopkeeper)
        {
            ShoppingManager.i.ShowShoppingChoices();
            isTalkingWithShopkeeper = false;
        }
        else
        {
            EndDialogue();
        }

        isTalking = false;

    }

    private void EndDialogue()
    {
        playerMovement.SetCanMove(true);
        currentNPCcam.SetActive(false);

        gameUI_GO.SetActive(true);
    }
}
