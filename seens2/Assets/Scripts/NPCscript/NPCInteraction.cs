using UnityEngine;
using TMPro;  // TextMeshPro���g�p���邽�߂̖��O��Ԃ�ǉ�
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

        // ��b���i��ł���ꍇ�͎��̃Z���t�ɐi��
        if (isTalking && (Input.GetButtonDown("Bbutton") || Input.GetKeyDown(KeyCode.Z)))
        {
            ShowNextDialogue();
        }

        // �I����UI���\������Ă���ꍇ�̂ݑI�����̓��͂��󂯕t����
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
            playerController.canMove = false;  // ��b���͈ړ��𐧌�
        }
        else
        {
            Debug.Log("����NPC�ɂ͉�b������܂���B");
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

        // ��b���I��������ɑI������\������ꍇ�A�ړ��𐧌�
        if (!choicesDisplayed && hasChoices && choices.Count > 0)
        {
            DisplayChoices();
            playerController.canMove = false;  // �I�������\������Ă���Ԃ͈ړ��𐧌�
            choicesDisplayed = true;
        }
        else
        {
            playerController.canMove = true;  // ����ȊO�͈ړ��\�ɂ���
            Debug.Log("��b���I�����܂������A�I�����͂���܂���B");
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
            // �I����UI���\������Ă���ꍇ�̂݁A�I�����̓��͂��󂯕t����
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))  // 1, 2, 3, ..., 9�̃L�[���͂��󂯕t����
            {
                SelectChoice(i);
                break;
            }
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))  // 1, 2, 3, ..., 9�̃L�[���͂��󂯕t����
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
            Debug.Log($"�I���� '{choices[choiceIndex]}' ���I�΂�܂����BNPC ID�� {npcID} �ɕύX����܂����B");
            choiceUI.SetActive(false);
            OnTalkButtonPressed();
        }
    }
}





