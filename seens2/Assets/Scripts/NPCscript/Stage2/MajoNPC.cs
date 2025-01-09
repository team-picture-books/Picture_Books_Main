using UnityEngine;
using TMPro;  // TextMeshPro���g�p���邽�߂̖��O��Ԃ�ǉ�
using System.Collections;
using System.Collections.Generic;

public class MajoNPC : MonoBehaviour
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
    public bool cantalk = true;
    public AudioSource seSource;


    // �A�C�e���֘A�̕ϐ�
    public bool canGiveItem = false;  // �A�C�e����n����NPC���ǂ���
    public GameObject itemUI;         // �A�C�e����\������UI

    private List<DialogueLoader.Dialogue> dialogues;
    private int currentDialogueIndex = 0;
    private Camera mainCamera;
    private PlayerController playerController;
    private bool isTalking = false;

    void Start()
    {
        mainCamera = Camera.main;
        playerController = player.GetComponent<PlayerController>();
        dialogues = dialogueLoader.GetDialoguesForNPC(npcID);
        if(itemUI != null)
        {
            itemUI.SetActive(false);  // �A�C�e��UI�͏�����Ԃł͔�\��

        }
    }

    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance <= interactionDistance && !isTalking)
        {
            talkButton.SetActive(true);

            if (Input.GetButtonDown("Bbutton") && cantalk == true || Input.GetKeyDown(KeyCode.Z) && cantalk == true)
            {
                if (seSource != null)
                {
                    seSource.Play();

                }
                playerController.canMove = false;
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
            if (seSource != null)
            {
                seSource.Play();

            }
            ShowNextDialogue();
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
        playerController.canMove = true;  // ��b���I��������Ɉړ��\�ɂ���

        // �A�C�e�������炤����
        if (canGiveItem)
        {
            GiveItem();
        }
        StartCoroutine(DelayedProcess());
        Debug.Log("��b���I�����܂����B");
    }

    private void GiveItem()
    {
        // �A�C�e��UI��\��
        itemUI.SetActive(true);

        // �A�C�e�����擾�������Ƃ�Debug.Log�Œʒm
        Debug.Log("�A�C�e�����擾���܂����I");
    }
    IEnumerator DelayedProcess()
    {

        // 1�b�ҋ@
        yield return new WaitForSeconds(0.1f);
        cantalk = false;

        Debug.Log("2�s�ڂ̏���: 1�b��Ɏ��s");
    }
}