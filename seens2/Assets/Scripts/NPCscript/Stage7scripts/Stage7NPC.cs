using UnityEngine;
using TMPro;  // TextMeshProを使用するための名前空間を追加
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Stage7NPC : MonoBehaviour
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
    

    // アイテム関連の変数
    public bool canGiveItem = false;  // アイテムを渡せるNPCかどうか
    public GameObject itemUI;         // アイテムを表示するUI

    public ToggleObject ToggleObject;
    public AudioSource seSource;

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
        itemUI.SetActive(false);  // アイテムUIは初期状態では非表示
    }

    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance <= interactionDistance && !isTalking)
        {
            talkButton.SetActive(true);

            if (Input.GetButtonDown("Bbutton") || Input.GetKeyDown(KeyCode.Z))
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

        // 会話が進んでいる場合は次のセリフに進む
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
        playerController.canMove = true;  // 会話が終了した後に移動可能にする

        // アイテムをもらう処理
        if (canGiveItem)
        {
            GiveItem();
        }

        Debug.Log("会話が終了しました。");
    }

    private void GiveItem()
    {
        // アイテムUIを表示
        itemUI.SetActive(true);
        canGiveItem = false;

        // アイテムを取得したことをDebug.Logで通知
        Debug.Log("アイテムを取得しました！");
        
        ToggleObject.toggleobeject();
        if(npcID == 1)
        {
            GameManager.Instance.DumbbellFlag = true;
        }
        if (npcID == 2)
        {
            GameManager.Instance.ProteinFlag = true;
        }
        if (npcID == 3)
        {
            GameManager.Instance.RopeFlag = true;
        }
    }
}