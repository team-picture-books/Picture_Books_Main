using System.Collections; // これを追加
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
    [SerializeField] private float interactDistance = 3f; //アイテムをわたすNPCとの有効距離
    private GameObject player; // プレイヤーオブジェクト
    private Coroutine blinkCoroutine; // 点滅処理用のコルーチン

    void Start()
    {
        // アイテム取得フラグを初期化
        itemAcquiredFlags = new bool[itemObjects.Length];
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);

        // アイテム選択処理 (例: 1～4キーでアイテムを選択)
        for (int i = 0; i < itemObjects.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i)) // キー 1, 2, 3, 4 に対応
            {
                if (itemAcquiredFlags[i]) // フラグを確認
                {
                    SelectItem(i);
                }
                else
                {
                    Debug.Log($"アイテム{i + 1}は未取得です。");
                }
            }
        }

        // NPCにアイテムを渡す処理
        if (distance <= interactDistance)
        {
            if (selectedItemIndex >= 0 && Input.GetKeyDown(KeyCode.E))
            {
                TransferItem();
            }
            
            //if (selectedItemIndex >= 0 && Input.GetButtonDown("Bbutton"))
            //{
              //  TransferItem();

            //}
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


