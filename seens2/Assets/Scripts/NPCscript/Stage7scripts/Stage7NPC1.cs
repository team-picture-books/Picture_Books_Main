using UnityEngine;
using UnityEngine.UI;


public class Stage7NPC1 : MonoBehaviour
{

    public GameObject player;          // プレイヤーオブジェクト
    public GameObject uiPrompt;       // UIプロンプト (近づいたときに表示)
    public Image canvasImage;         // Canvas上のImage
    public float interactionDistance = 3f; // インタラクション距離
    public KeyCode interactKey = KeyCode.E; // インタラクションキー

  

    private bool isPlayerNearby = false;

    void Start()
    {
        if (uiPrompt != null)
            uiPrompt.SetActive(false); // UIプロンプトを非表示
        if (canvasImage != null)
            canvasImage.gameObject.SetActive(false); // CanvasのImageを非表示
    }

    void Update()
    {
        // プレイヤーとの距離を測定
        float distance = Vector3.Distance(player.transform.position, transform.position);
        isPlayerNearby = distance <= interactionDistance;

        // プレイヤーが近くにいる場合
        if (isPlayerNearby)
        {
            if (uiPrompt != null)
                uiPrompt.SetActive(true); // UIプロンプトを表示

            // 指定キーが押されたらCanvasのImageを表示
            if (Input.GetKeyDown(interactKey))
            {
                if (canvasImage != null)
                    canvasImage.gameObject.SetActive(true);
                GameManager.Instance.DumbbellFlag = true;

            }
        }
        else
        {
            if (uiPrompt != null)
                uiPrompt.SetActive(false); // UIプロンプトを非表示
        }
    }
}
