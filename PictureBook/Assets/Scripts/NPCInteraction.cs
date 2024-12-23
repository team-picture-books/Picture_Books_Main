using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class NPCInteraction : MonoBehaviour
{
    public GameObject player;
    public GameObject talkButton;
    public GameObject speechBubble;
    public TextMeshProUGUI speechText;
    public FuriganaDisplay furiganaDisplay;
    public float interactionDistance = 3.0f;
    public DialogueLoader dialogueLoader;
    public int npcID;
    public Transform npcHead;
    private List<string> dialogues;
    private int currentDialogueIndex = 0;
    private Camera mainCamera;
    private PlayerController playerController;
    private bool isTalking = false;

    void Start()
    {
        mainCamera = Camera.main;
        playerController = player.GetComponent<PlayerController>();
        dialogues = dialogueLoader.GetDialoguesForNPC(npcID);
    }

    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance <= interactionDistance && !isTalking)
        {
            talkButton.SetActive(true);
        }
        else
        {
            talkButton.SetActive(false);
            if (!isTalking)
            {
                EndDialogue();
            }
        }

        if (speechBubble.activeSelf)
        {
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(npcHead.position);
            speechBubble.transform.position = screenPosition;
        }

        if (isTalking && Input.GetKeyDown(KeyCode.Space))
        {
            ShowNextDialogue();
        }
    }

    public void OnTalkButtonPressed()
    {
        if (dialogues.Count > 0)
        {
            isTalking = true;
            speechBubble.SetActive(true);
            currentDialogueIndex = 0;

            furiganaDisplay.SetTextWithFurigana(dialogues[currentDialogueIndex]);
            playerController.canMove = false;
        }
    }

    private void ShowNextDialogue()
    {
        currentDialogueIndex++;
        if (currentDialogueIndex < dialogues.Count)
        {
            furiganaDisplay.SetTextWithFurigana(dialogues[currentDialogueIndex]);
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        isTalking = false;
        speechBubble.SetActive(false);
        playerController.canMove = true;
    }
}
