using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ItemTransfer : MonoBehaviour
{
    [SerializeField] private GameObject[] itemObjects; // アイテムオブジェクト
    [SerializeField] private string[] sceneNames;      // アイテムごとのシーン名
    [SerializeField] private Image[] itemIcons;        // アイテムごとのUIアイコン
    private bool[] itemAcquiredFlags;                 // アイテム取得フラグ
    private int selectedItemIndex = -1;               // 選択中のアイテムのインデックス
    [SerializeField] private float interactDistance = 3f; // アイテムを渡すNPCとの有効距離
    private GameObject player; // プレイヤーオブジェクト
    private Coroutine blinkCoroutine; // 点滅処理用のコルーチン
    private bool isSelectingItem = false; // アイテム選択モードフラグ

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

        // アイテム選択モードの切り替え
        if (Input.GetKeyDown(KeyCode.E)||Input.GetButtonDown("Ybutton"))
        {
            isSelectingItem = !isSelectingItem; // 選択モードをトグル
            Debug.Log(isSelectingItem ? "アイテム選択モードに入りました。" : "アイテム選択モードを終了しました。");
            if (isSelectingItem)
            {
                playerController.canMove = false;
            }
            if (!isSelectingItem)
            {
                playerController.canMove = true;
            }
        }

        // アイテム選択処理
        if (isSelectingItem)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)||Input.GetButtonDown("Xbutton"))
                TrySelectItem(0);
            if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetButtonDown("Abutton"))
                TrySelectItem(1);
            if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetButtonDown("Bbutton"))
                TrySelectItem(2);
            if (Input.GetKeyDown(KeyCode.Alpha4))
                TrySelectItem(3);
        }

        // NPCにアイテムを渡す処理
        if (distance <= interactDistance)
        {
            if (selectedItemIndex >= 0 && Input.GetKeyDown(KeyCode.F))
            {
                TransferItem();
            }

            if (selectedItemIndex >= 0 && Input.GetButtonDown("Abutton"))
            {
                TransferItem();
            }
        }
    }

    void TrySelectItem(int itemIndex)
    {
        if (itemIndex >= 0 && itemIndex < itemObjects.Length)
        {
            if (itemAcquiredFlags[itemIndex]) // アイテムが取得済みかチェック
            {
                SelectItem(itemIndex);
            }
            else
            {
                Debug.Log($"アイテム{itemIndex + 1}は未取得です。");
            }
        }
    }

    void SelectItem(int itemIndex)
    {
        if (selectedItemIndex != itemIndex)
        {
            // 以前の選択アイコンの点滅を停止
            if (blinkCoroutine != null)
            {
                StopCoroutine(blinkCoroutine);
                SetIconVisibility(selectedItemIndex, true); // 点滅を停止し表示を固定
            }

            selectedItemIndex = itemIndex;
            Debug.Log($"アイテム{itemIndex + 1}を選択しました。");

            // 新しい選択アイコンの点滅を開始
            blinkCoroutine = StartCoroutine(BlinkIcon(itemIcons[itemIndex]));
        }
    }

    IEnumerator BlinkIcon(Image icon)
    {
        while (true)
        {
            icon.enabled = !icon.enabled; // 表示/非表示を切り替え
            yield return new WaitForSeconds(0.5f); // 点滅速度を調整
        }
    }

    void SetIconVisibility(int itemIndex, bool visible)
    {
        if (itemIndex >= 0 && itemIndex < itemIcons.Length)
        {
            itemIcons[itemIndex].enabled = visible;
        }
    }

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

    void TransferItem()
    {
        if (selectedItemIndex >= 0 && selectedItemIndex < sceneNames.Length)
        {
            string targetScene = sceneNames[selectedItemIndex];
            Debug.Log($"アイテム{selectedItemIndex + 1}をNPCに渡しました。シーン {targetScene} に移行します。");

            // シーン遷移
            SceneManager.LoadScene(targetScene);
        }
        else
        {
            Debug.Log("無効なアイテム選択またはシーンが設定されていません。");
        }
    }
}
