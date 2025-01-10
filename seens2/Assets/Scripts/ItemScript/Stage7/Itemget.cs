using UnityEngine;

public class itemget : MonoBehaviour
{
    public GameObject player; // プレイヤーのオブジェクト
    public GameObject interactionUI; // 近くに来たときに表示するUI
    public GameObject itemUI; // アイテムを取得したときに表示するUI
    public float interactionDistance = 3.0f; // アイテムに近づける距離
    public KeyCode interactKey = KeyCode.E; // インタラクト用のキー
    public ToggleObject ToggleObject;
    public int itemID;


    void Start()
    {
        interactionUI.SetActive(false); // 初期状態でUIを非表示
        itemUI.SetActive(false); // 初期状態でアイテムUIを非表示
    }

    void Update()
    {
        // プレイヤーとアイテムの距離を計算
        float distance = Vector3.Distance(player.transform.position, transform.position);

        // プレイヤーがアイテムの近くにいる場合
        if (distance <= interactionDistance)
        {
            interactionUI.SetActive(true); // インタラクトUIを表示


            // プレイヤーがキーを押したらアイテムを取得
            if (Input.GetKeyDown(interactKey) || Input.GetButtonDown("Bbutton"))
            {
                CollectItem();
            }
        }
        else
        {
            interactionUI.SetActive(false); // インタラクトUIを非表示

        }
    }

    void CollectItem()
    {
        Debug.Log("アイテムを取得しました！");
        interactionUI.SetActive(false); // インタラクトUIを非表示
        itemUI.SetActive(true); // アイテムUIを表示
        Destroy(gameObject); // アイテムオブジェクトを消去
        
        ToggleObject.toggleobeject();
        if(itemID == 1)
        {
            GameManager.Instance.DumbbellFlag = true;
            Debug.Log("ダンぺるフラグオン");
        }
        else if (itemID == 2)
        {
            GameManager.Instance.RopeFlag = true;
            Debug.Log("縄跳びフラグオン");

        }

    }
}