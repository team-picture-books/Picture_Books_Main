using UnityEngine;
using TMPro;  // TextMeshProを使用するための名前空間を追加
using System.Collections.Generic;
public class NPCInteraction : MonoBehaviour
{
    public GameObject player;
    public GameObject talkButton;
    public GameObject npcSpeechBubble;
    public TextMeshProUGUI npcSpeechText;
    public GameObject playerSpeechBubble;
    public TextMeshProUGUI playerSpeechText;
    public float interactionDistance = 2.0f;
    public DialogueLoader dialogueLoader;
    public int npcID;
    public Transform npcHead;
    public bool hasChoices;
    public List<string> choices;
    public List<int> correspondingNPCIDs;
    public GameObject choiceUI;
    public TextMeshProUGUI[] choiceTexts;

    private List<DialogueLoader.Dialogue> dialogues;
    private int currentDialogueIndex = 0;
    private Camera mainCamera;
    private PlayerController playerController;
    private bool isTalking = false;
    private bool choicesDisplayed = false;

    void Start()
    {
        mainCamera = Camera.main;
        playerController = player.GetComponent<PlayerController>();
        dialogues = dialogueLoader.GetDialoguesForNPC(npcID);
        choiceUI.SetActive(false);
    }

    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance <= interactionDistance && !isTalking)
        {
            talkButton.SetActive(true);

            if (Input.GetButtonDown("Bbutton") || Input.GetKeyDown(KeyCode.Z))
            {
                OnTalkButtonPressed();
            }
        }
        else
        {
            talkButton.SetActive(false);
            if (!isTalking)
            {
                npcSpeechBubble.SetActive(false);
                playerSpeechBubble.SetActive(false);
                //playerController.canMove = true;
            }
        }

        if (npcSpeechBubble.activeSelf)
        {
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(npcHead.position);
            npcSpeechBubble.transform.position = screenPosition;
        }

        // 会話が進んでいる場合は次のセリフに進む
        if (isTalking && (Input.GetButtonDown("Bbutton") || Input.GetKeyDown(KeyCode.Z)))
        {
            ShowNextDialogue();
        }

        // 選択肢UIが表示されている場合のみ選択肢の入力を受け付ける
        if (choiceUI.activeSelf)
        {
            HandleChoiceInput();
        }
    }

    public void OnTalkButtonPressed()
    {
        if (dialogues.Count == 0)
        {
            dialogues = dialogueLoader.GetDialoguesForNPC(npcID);
        }

        if (dialogues.Count > 0)
        {
            isTalking = true;
            currentDialogueIndex = 0;
            ShowNextDialogue();
            playerController.canMove = false;  // 会話中は移動を制限
        }
        else
        {
            Debug.Log("このNPCには会話がありません。");
        }
    }

    private void ShowNextDialogue()
    {
        if (currentDialogueIndex < dialogues.Count)
        {
            var currentDialogue = dialogues[currentDialogueIndex];
            if (currentDialogue.speaker == DialogueLoader.SpeakerType.NPC)
            {
                npcSpeechText.text = currentDialogue.text;
                npcSpeechBubble.SetActive(true);
                playerSpeechBubble.SetActive(false);
            }
            else if (currentDialogue.speaker == DialogueLoader.SpeakerType.Player)
            {
                playerSpeechText.text = currentDialogue.text;
                playerSpeechBubble.SetActive(true);
                npcSpeechBubble.SetActive(false);
            }
            currentDialogueIndex++;
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        isTalking = false;
        npcSpeechBubble.SetActive(false);
        playerSpeechBubble.SetActive(false);
        playerController.canMove = true;

        // 会話が終了した後に選択肢を表示する場合、移動を制限
        if (!choicesDisplayed && hasChoices && choices.Count > 0)
        {
            DisplayChoices();
            playerController.canMove = false;  // 選択肢が表示されている間は移動を制限
            choicesDisplayed = true;
        }
        else
        {
            playerController.canMove = true;  // それ以外は移動可能にする
            Debug.Log("会話が終了しましたが、選択肢はありません。");
        }
    }

    private void DisplayChoices()
    {
        choiceUI.SetActive(true);

        for (int i = 0; i < choiceTexts.Length; i++)
        {
            if (i < choices.Count)
            {
                choiceTexts[i].text = choices[i];
                choiceTexts[i].transform.parent.gameObject.SetActive(true);
            }
            else
            {
                choiceTexts[i].transform.parent.gameObject.SetActive(false);
            }
        }
    }

    private void HandleChoiceInput()
    {
        for (int i = 0; i < choices.Count; i++)
        {
            // 選択肢UIが表示されている場合のみ、選択肢の入力を受け付ける
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))  // 1, 2, 3, ..., 9のキー入力を受け付ける
            {
                SelectChoice(i);
                break;
            }
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))  // 1, 2, 3, ..., 9のキー入力を受け付ける
            {
                SelectChoice(i);
                break;
            }
        }
    }

    public void SelectChoice(int choiceIndex)
    {
        if (choiceIndex < choices.Count && choiceIndex < correspondingNPCIDs.Count)
        {
            npcID = correspondingNPCIDs[choiceIndex];
            dialogues = dialogueLoader.GetDialoguesForNPC(npcID);
            Debug.Log($"選択肢 '{choices[choiceIndex]}' が選ばれました。NPC IDが {npcID} に変更されました。");
            choiceUI.SetActive(false);
            OnTalkButtonPressed();
        }
    }
}





