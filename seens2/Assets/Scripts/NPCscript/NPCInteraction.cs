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

    private bool isChoosingOption = false;  // 選択肢を選んでいるかのフラグ
    private int selectedOptionIndex = -1;  // 選択された選択肢のインデックス（初期化）
    private List<string> currentOptions = new List<string>();  // 現在表示中の選択肢リスト

    public bool shouldChangeNPCID = false;  // NPCIDを変更するかどうかを決定するフラグ
    public int newNPCID;  // 新しいNPCID（変更する場合に設定）

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
        if (isTalking && !isChoosingOption && Input.GetButtonDown("Bbutton")) // コントローラのBボタンを使用
        {
            ShowNextDialogue();
        }
        if (Input.GetKeyDown(KeyCode.Z)) // デバッグ用でZキーを使用
        {
            ShowNextDialogue();
        }

        // 選択肢が表示されているときに、キーボードで選択
        if (isChoosingOption)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) { SelectOption(0); }
            else if (Input.GetKeyDown(KeyCode.Alpha2)) { SelectOption(1); }
            else if (Input.GetKeyDown(KeyCode.Alpha3)) { SelectOption(2); }
            // 他のキーも追加可能
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

            // 次の会話が選択肢を含む場合
            if (currentDialogue.options.Count > 0)
            {
                ShowOptions(currentDialogue);  // 選択肢を表示
            }
            else
            {
                currentDialogueIndex++;
            }
        }
        else
        {
            // 会話が終了した場合
            EndDialogue();
        }
    }

    // 選択肢を表示
    private void ShowOptions(DialogueLoader.Dialogue currentDialogue)
    {
        isChoosingOption = true;
        currentOptions = currentDialogue.options;
        // 例えば、選択肢がある場合はテキストで表示する（UI上に表示することも可能）
        npcSpeechText.text = "選択肢を選んでください:";  // メッセージを変更
    }

    // 選択肢を選択した時
    private void SelectOption(int optionIndex)
    {
        if (optionIndex < currentOptions.Count)
        {
            selectedOptionIndex = optionIndex;  // 選択されたインデックスを保存
            var selectedOption = currentOptions[selectedOptionIndex];
            Debug.Log($"選択された選択肢: {selectedOption}");

            // 選択肢に基づいて次の会話を表示
            var currentDialogue = dialogues[currentDialogueIndex];
            currentDialogueIndex = currentDialogue.nextDialogueIndices[selectedOptionIndex];  // 選択肢に対応する次の会話インデックスを設定
            ShowNextDialogue();  // 次の会話を表示

            isChoosingOption = false;  // 選択肢の表示を終了
        }
    }

    // 会話終了時の処理
    private void EndDialogue()
    {
        isTalking = false;
        npcSpeechBubble.SetActive(false);
        playerSpeechBubble.SetActive(false);
        playerController.canMove = true;  // プレイヤーの操作を許可

        // NPCIDを変更する場合、設定された新しいnpcIDを反映
        if (shouldChangeNPCID)
        {
            npcID = newNPCID;
            dialogues = dialogueLoader.GetDialoguesForNPC(npcID);  // 新しいNPCの会話データをロード
            Debug.Log($"NPC IDが変更されました: 新しいNPC ID = {npcID}");
        }
    }
}
