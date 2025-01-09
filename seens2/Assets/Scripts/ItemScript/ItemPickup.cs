using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private GameObject interactButton; // ボタンUI
    [SerializeField] private Image itemDisplayUI;       // 右下のUI（アイテム表示用）
    [SerializeField] private Sprite itemIcon;           // 取得アイテムのアイコン
    [SerializeField] private float interactDistance = 3f; // アイテムとの有効距離
    [SerializeField] public bool itemAcquired = false;  // フラグ（取得済みか）
    [SerializeField] private int itemIndex;           // アイテムインデックス
    [SerializeField] private ItemTransfer itemTransfer; // ItemTransfer スクリプトの参照

    public ToggleObject ToggleObject;

    private GameObject player; // プレイヤーオブジェクト

    void Start()
    {
        // 初期化
        interactButton.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        itemDisplayUI.enabled = false;

        if (itemTransfer == null)
        {
            itemTransfer = FindObjectOfType<ItemTransfer>();
        }
    }

    void Update()
    {
        if (itemAcquired) return; // すでに取得済みなら処理をスキップ

        // プレイヤーとアイテムの距離を計算
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance <= interactDistance)
        {
            interactButton.SetActive(true); // ボタンを表示

            // ボタンを押す処理
            if (Input.GetKeyDown(KeyCode.E)) // 例: Eキーでアイテム取得
            {
                AcquireItem();
                ToggleObject.toggleobeject();
            }
            if (Input.GetButtonDown("Bbutton")) // 例: Eキーでアイテム取得
            {
                AcquireItem();
                ToggleObject.toggleobeject();

            }
        }
        else
        {
            interactButton.SetActive(false); // ボタンを非表示
        }
    }

    void AcquireItem()
    {
        itemTransfer.MarkItemAsAcquired(itemIndex);
        itemAcquired = true; // フラグをtrueに設定
        interactButton.SetActive(false); // ボタンを非表示
        itemDisplayUI.sprite = itemIcon; // アイコンをUIに設定
        itemDisplayUI.enabled = true;    // UIを表示
        Destroy(gameObject);             // アイテム（自身）を消去
    }
}

