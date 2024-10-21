using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class NPCInteraction : MonoBehaviour
{
    public GameObject player;  // プレイヤーオブジェクト
    public GameObject talkButton;  // 話すボタン（UI）
    public GameObject speechBubble;  // 吹き出しUI
    public Text speechText;  // 吹き出し内のテキスト
    public float interactionDistance = 3.0f;  // プレイヤーとNPCの距離判定
    public DialogueLoader dialogueLoader;  // CSVから会話を読み込むスクリプト
    public int npcID;  // このNPCの識別ID
    public Transform npcHead;  // NPCの頭の位置（Transform参照）

    private List<string> dialogues;  // このNPCの会話リスト
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
        }
        else
        {
            talkButton.SetActive(false);
            if (!isTalking)
            {
                speechBubble.SetActive(false);  // 離れたら吹き出しも非表示にする
                playerController.canMove = true;  // 会話が終わったらプレイヤーの操作を許可
            }
        }

        // 吹き出しの位置をNPCの頭上に設定
        if (speechBubble.activeSelf)
        {
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(npcHead.position);  // NPCの頭の位置をスクリーン座標に変換
            speechBubble.transform.position = screenPosition;  // 吹き出しをNPCの頭上に配置
        }

        // 会話中でスペースキーが押されたら次の会話テキストを表示
        if (isTalking && Input.GetKeyDown(KeyCode.Space))
        {
            ShowNextDialogue();
        }
    }

    // 「話す」ボタンが押されたときの処理
    public void OnTalkButtonPressed()
    {
        if (dialogues.Count > 0)
        {
            isTalking = true;
            speechBubble.SetActive(true);
            currentDialogueIndex = 0;
            speechText.text = dialogues[currentDialogueIndex];

            // プレイヤーの操作を無効化
            playerController.canMove = false;
        }
    }

    // 次の会話テキストを表示するメソッド
    private void ShowNextDialogue()
    {
        currentDialogueIndex++;
        if (currentDialogueIndex < dialogues.Count)
        {
            // 次の会話を表示
            speechText.text = dialogues[currentDialogueIndex];
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
        speechBubble.SetActive(false);
        playerController.canMove = true;  // プレイヤーの操作を許可
    }
}
