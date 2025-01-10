using UnityEngine;
using TMPro;  // TextMeshProを使用するための名前空間を追加
using System.Collections.Generic;

public class NightTownMainNPC2 : MonoBehaviour
{
    public GameObject player;
    public GameObject talkButton;
    public GameObject npcSpeechBubble;
    public TextMeshProUGUI npcSpeechText;
    public GameObject playerSpeechBubble;
    public TextMeshProUGUI playerSpeechText;
    public float interactionDistance = 15.0f;
    public DialogueLoader dialogueLoader;
    public int npcID;
    public Transform npcHead;

    // アイテム関連の変数
    public bool canGiveItem = false;  // アイテムを渡せるNPCかどうか
    public GameObject itemUI;         // アイテムを表示するUI

    public NightTownMainNPC NightTownMainNPC;

    private AudioSource audioSource; // AudioSourceコンポーネント
    [SerializeField] private AudioClip seClip; // 再生するSEの音声クリップ

    private List<DialogueLoader.Dialogue> dialogues;
    private int currentDialogueIndex = 0;
    private Camera mainCamera;
    private PlayerController playerController;
    private bool isTalking = false;
    public bool Cantalk = true;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false; // 再生しない設定
        audioSource.clip = seClip; // 音声クリップを設定

        mainCamera = Camera.main;
        playerController = player.GetComponent<PlayerController>();
        dialogues = dialogueLoader.GetDialoguesForNPC(npcID);
        if (itemUI != null)
        {
            itemUI.SetActive(false);  // アイテムUIは初期状態では非表示

        }
    }

    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance <= interactionDistance && !isTalking)
        {
            talkButton.SetActive(true);

            if (Input.GetButtonDown("Bbutton") && Cantalk || Input.GetKeyDown(KeyCode.Z) && Cantalk)
            {
                playerController.canMove = false;
                OnTalkButtonPressed();
                PlaySE();
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
            PlaySE();
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
            playerController.canMove = false;
            NightTownMainNPC.StartDialogue();
        }
    }

    private void EndDialogue()
    {
        Cantalk = false;

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

        // アイテムを取得したことをDebug.Logで通知
        Debug.Log("アイテムを取得しました！");
    }
    private void PlaySE()
    {
        if (seClip != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.Log("SEクリップが設定されていません！");
        }
    }
}