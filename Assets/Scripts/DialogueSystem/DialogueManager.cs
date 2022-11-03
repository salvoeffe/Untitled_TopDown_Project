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

    [SerializeField] AudioClip talkingSFX;

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

    public void StartDialogue(string[] lines, string npcName, GameObject npcCam)
    {
        isTalking = true;
        dialogueBoxGO.SetActive(true);
        dialogueText.text = string.Empty;
        npcNameText.text = npcName;
        dialogueLines = lines;
        index = 0;
        StartCoroutine(TypeLine());

        currentNPCcam = npcCam;
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
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        dialogueBoxGO.SetActive(false);
        playerMovement.SetCanMove(true);
        currentNPCcam.SetActive(false);
        isTalking = false;
    }
}
