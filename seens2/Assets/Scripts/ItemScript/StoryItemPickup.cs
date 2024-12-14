using UnityEngine;
using UnityEngine.UI;

public class StoryItemPickup : MonoBehaviour
{
    [SerializeField] private GameObject interactButton; // ボタンUI
    [SerializeField] private Image itemDisplayUI;       // 右下のUI（アイテム表示用）
    [SerializeField] private Sprite itemIcon;          // 取得アイテムのアイコン
    [SerializeField] private float interactDistance = 3f; // アイテムとの有効距離
    [SerializeField] private int itemIndex;            // アイテムインデックス
    [SerializeField] private StoryItemManager storyItemManager; // StoryItemManager スクリプトの参照

    private GameObject player;
    private bool itemAcquired = false; // アイテムが取得済みか

    void Start()
    {
        // UI初期化
        interactButton.SetActive(false);
        itemDisplayUI.enabled = false;

        // プレイヤーオブジェクトを取得
        player = GameObject.FindGameObjectWithTag("Player");

        // StoryItemManager の参照を取得（設定されていない場合）
        if (storyItemManager == null)
        {
            storyItemManager = FindObjectOfType<StoryItemManager>();
        }

        if (storyItemManager == null)
        {
            Debug.LogError("StoryItemManager が見つかりません。シーンに追加されていることを確認してください。");
        }
    }

    void Update()
    {
        // 既に取得済みの場合は処理をスキップ
        if (itemAcquired) return;

        // プレイヤーとの距離を測定
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        // 有効距離内でインタラクトボタンを表示
        if (distanceToPlayer <= interactDistance)
        {
            interactButton.SetActive(true);

            // プレイヤーが E キーを押した場合にアイテム取得処理を実行
            if (Input.GetKeyDown(KeyCode.E))
            {
                AcquireItem();
            }
        }
        else
        {
            interactButton.SetActive(false);
        }
    }

    void AcquireItem()
    {
        // アイテム取得フラグを設定
        itemAcquired = true;

        // UI更新
        interactButton.SetActive(false);
        itemDisplayUI.sprite = itemIcon;
        itemDisplayUI.enabled = true;

        // StoryItemManager にアイテム取得を通知
        if (storyItemManager != null)
        {
            storyItemManager.MarkItemAsAcquired(itemIndex);
        }

        // コンソールログ
        Debug.Log($"アイテム {itemIndex + 1} を取得しました。");

        // オブジェクトを削除
        Destroy(gameObject);
    }
}


