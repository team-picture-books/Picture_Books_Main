using UnityEngine;
using TMPro;  // TextMeshProを使用するための名前空間を追加
using System.Collections.Generic;

public class NPCInteraction : MonoBehaviour
{
    public GameObject player;  // プレイヤーオブジェクト
    public GameObject talkButton;  // 話すボタン（UI）
    public GameObject npcSpeechBubble;  // NPCの吹き出しUI
    public TextMeshProUGUI npcSpeechText;  // NPC吹き出し内のテキスト
    public GameObject playerSpeechBubble;  // プレイヤーの吹き出しUI
    public TextMeshProUGUI playerSpeechText;  // プレイヤー吹き出し内のテキスト
    public float interactionDistance = 2.0f;  // プレイヤーとNPCの距離判定
    public DialogueLoader dialogueLoader;  // CSVから会話を読み込むスクリプト
    public int npcID;  // このNPCの識別ID
    public Transform npcHead;  // NPCの頭の位置（Transform参照）

    private List<DialogueLoader.Dialogue> dialogues;  // NPCの会話リスト
    private int currentDialogueIndex = 0;
    private Camera mainCamera;
    private PlayerController playerController;  // プレイヤーコントローラーの参照
    private bool isTalking = false;  // 会話中かどうかを示すフラグ

    void Start()
    {
        mainCamera = Camera.main;  // メインカメラを取得
        playerController = player.GetComponent<PlayerController>();  // PlayerControllerの参照を取得
        // NPCの会話リストを取得
        dialogues = dialogueLoader.GetDialoguesForNPC(npcID);
    }

    void Update()
    {
        // プレイヤーとの距離を測定
        float distance = Vector3.Distance(player.transform.position, transform.position);

        // NPCに近づいたら「話す」ボタンを表示
        if (distance <= interactionDistance && !isTalking)
        {
            talkButton.SetActive(true);

            // Bボタンが押されたら会話を開始
            if (Input.GetButtonDown("Bbutton")) // コントローラのBボタンを使用
            {
                OnTalkButtonPressed();
            }
            if (Input.GetKeyDown(KeyCode.Z)) // デバッグ用でZキーを使用
            {
                OnTalkButtonPressed();
            }
        }
        else
        {
            talkButton.SetActive(false);
            if (!isTalking)
            {
                npcSpeechBubble.SetActive(false);  // 離れたらNPCの吹き出しを非表示
                playerSpeechBubble.SetActive(false);  // プレイヤーの吹き出しも非表示
                playerController.canMove = true;  // 会話が終わったらプレイヤーの操作を許可
            }
        }

        // 吹き出しの位置をNPCの頭上に設定
        if (npcSpeechBubble.activeSelf)
        {
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(npcHead.position);  // NPCの頭の位置をスクリーン座標に変換
            npcSpeechBubble.transform.position = screenPosition;  // 吹き出しをNPCの頭上に配置
        }

        // 会話中でBボタンが押されたら次の会話テキストを表示
        if (isTalking && Input.GetButtonDown("Bbutton")) // コントローラのBボタンを使用
        {
            ShowNextDialogue();
        }
        if (Input.GetKeyDown(KeyCode.Z)) // デバッグ用でZキーを使用
        {
            ShowNextDialogue();
        }
    }

    // 「話す」ボタンが押されたときの処理
    public void OnTalkButtonPressed()
    {
        // 会話リストが空かどうかをチェック
        if (dialogues.Count == 0)
        {
            // 再度会話データを取得
            dialogues = dialogueLoader.GetDialoguesForNPC(npcID);
        }

        // 会話リストが空でない場合にのみ会話を開始
        if (dialogues.Count > 0)
        {
            isTalking = true;
            currentDialogueIndex = 0;
            ShowNextDialogue();

            // プレイヤーの操作を無効化
            playerController.canMove = false;
        }
        else
        {
            // NPCとの会話がない場合の処理（必要に応じて）
            Debug.Log("このNPCには会話がありません。");
        }
    }

    // 次の会話テキストを表示するメソッド
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
            // 会話が終了した場合
            EndDialogue();
        }
    }

    // 会話終了時の処理
    private void EndDialogue()
    {
        isTalking = false;
        npcSpeechBubble.SetActive(false);
        playerSpeechBubble.SetActive(false);
        playerController.canMove = true;  // プレイヤーの操作を許可
    }
}
