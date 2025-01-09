using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class StoryItemManager : MonoBehaviour
{
    [SerializeField] private GameObject[] itemObjects; // アイテムオブジェクト
    [SerializeField] private string correctScene = "CorrectScene"; // 全て正解時のシーン名
    [SerializeField] private string incorrectScene = "IncorrectScene"; // 1つでも不正解のシーン名
    [SerializeField] private bool[] correctAnswers; // アイテムが正解かどうかのフラグ
    [SerializeField] private Image[] itemIcons; // アイテムごとのUIアイコン
    [SerializeField] private int requiredItemsToTransfer = 3; // 必要なアイテム数

    private bool[] itemAcquiredFlags; // アイテム取得フラグ
    private int selectedItemIndex = -1; // 選択中のアイテムのインデックス
    private int transferredItemsCount = 0; // 渡したアイテムのカウント
    private int correctItemsCount = 0; // 正解のアイテムのカウント
    [SerializeField] private float interactDistance = 3f; // NPCとのインタラクション距離
    private GameObject player; // プレイヤーオブジェクト
    private Coroutine blinkCoroutine; // アイコン点滅用コルーチン

    private bool cantransfer = false;

    public PlayerController playerController;

    void Start()
    {
        // アイテム取得フラグを初期化
        itemAcquiredFlags = new bool[itemObjects.Length];
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);

        // アイテム選択処理 (1～4キーでアイテムを選択)
        //for (int i = 0; i < itemObjects.Length; i++)
        //{
        //    if (Input.GetKeyDown(KeyCode.Alpha1 + i)) // キー 1, 2, 3, 4 に対応
        //    {
        //        if (itemAcquiredFlags[i]) // 取得済みアイテムのみ選択可能
        //        {
        //            SelectItem(i);
        //        }
        //        else
        //        {
        //            Debug.Log($"アイテム{i + 1}は未取得です。");
        //        }
        //    }
        //}
        if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Ybutton"))
        {
            cantransfer = !cantransfer;
            Debug.Log("アイテム選択中です");
            if (cantransfer)
            {
                playerController.canMove = false;
            }
            if (!cantransfer)
            {
                playerController.canMove = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && cantransfer|| Input.GetButtonDown("Xbutton") && cantransfer)
        {
            if (itemAcquiredFlags[0])
            {
                SelectItem(0);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && cantransfer || Input.GetButtonDown("Abutton") && cantransfer)
        {
            if (itemAcquiredFlags[1])
            {
                SelectItem(1);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && cantransfer || Input.GetButtonDown("Bbutton") && cantransfer)
        {
            if (itemAcquiredFlags[2])
            {
                SelectItem(2);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && cantransfer || Input.GetButtonDown("RBbutton") && cantransfer)
        {
            if (itemAcquiredFlags[3])
            {
                SelectItem(3);
            }
        }

        // NPCとのインタラクション処理
        if (distance <= interactDistance)
        {
            if (selectedItemIndex >= 0 && Input.GetKeyDown(KeyCode.F)|| selectedItemIndex >= 0 && Input.GetButtonDown("Abutton"))
            {
                TransferItem();
            }
            //if (selectedItemIndex >= 0 && Input.GetButtonDown("Bbutton"))
            //{
            //  TransferItem();
            //}
        }
    }

    // アイテム選択
    void SelectItem(int itemIndex)
    {
        if (itemAcquiredFlags[itemIndex]) // アイテムが取得されているか確認
        {
            if (selectedItemIndex != itemIndex)
            {
                // 以前の選択アイコンの点滅を停止
                if (blinkCoroutine != null)
                {
                    StopCoroutine(blinkCoroutine);
                    SetIconVisibility(selectedItemIndex, true); // 点滅を停止
                }

                selectedItemIndex = itemIndex;
                Debug.Log($"アイテム{itemIndex + 1}を選択しました。");

                // 新しい選択アイコンの点滅を開始
                blinkCoroutine = StartCoroutine(BlinkIcon(itemIcons[itemIndex]));
            }
        }
        else
        {
            Debug.Log($"アイテム{itemIndex + 1}は未取得です。"); // アイテムが未取得の場合
        }
    }

    // アイコンを点滅させるコルーチン
    IEnumerator BlinkIcon(Image icon)
    {
        while (true)
        {
            icon.enabled = !icon.enabled; // アイコンの表示/非表示を切り替え
            yield return new WaitForSeconds(0.5f); // 点滅速度を調整
        }
    }

    // アイコンの表示状態を設定
    void SetIconVisibility(int itemIndex, bool visible)
    {
        if (itemIndex >= 0 && itemIndex < itemIcons.Length)
        {
            itemIcons[itemIndex].enabled = visible;
        }
    }

    // アイテムを取得済みとしてマーク
    public void MarkItemAsAcquired(int itemIndex)
    {
        if (itemIndex >= 0 && itemIndex < itemAcquiredFlags.Length)
        {
            itemAcquiredFlags[itemIndex] = true;
            Debug.Log($"アイテム{itemIndex + 1}が取得済みになりました。");
        }
        else
        {
            Debug.LogError("無効なアイテムインデックスです。");
        }
    }

    // アイテムをNPCに渡す処理
    void TransferItem()
    {
        if (selectedItemIndex >= 0 && selectedItemIndex < itemObjects.Length)
        {
            transferredItemsCount++;
            bool isCorrect = correctAnswers[selectedItemIndex];

            Debug.Log($"アイテム{selectedItemIndex + 1}をNPCに渡しました。");

            // 正解の場合カウントを増やす
            if (isCorrect)
            {
                correctItemsCount++;
                Debug.Log($"アイテム{selectedItemIndex + 1}は正解です！");
            }
            else
            {
                Debug.Log($"アイテム{selectedItemIndex + 1}は不正解です。");
            }

            // 渡したアイテムを非表示にする
            StopCoroutine(blinkCoroutine);
            SetIconVisibility(selectedItemIndex, false);
            
            selectedItemIndex = -1;

            // 必要な数のアイテムを渡したら正解率をチェック
            if (transferredItemsCount >= requiredItemsToTransfer)
            {
                CheckAndFinalize();
            }
        }
    }

    // アイテムの正解率をチェックし、シーン遷移を行う
    void CheckAndFinalize()
    {
        // 全て正解なら正解シーンへ、1つでも不正解があれば不正解シーンへ遷移
        if (correctItemsCount == transferredItemsCount)
        {
            Debug.Log("すべて正解です！ 正解シーンに遷移します。");
            SceneManager.LoadScene(correctScene);
        }
        else
        {
            Debug.Log("不正解が含まれています。不正解シーンに遷移します。");
            SceneManager.LoadScene(incorrectScene);
        }
    }
}
