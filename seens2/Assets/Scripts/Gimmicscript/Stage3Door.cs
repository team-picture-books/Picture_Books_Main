using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage3Door : MonoBehaviour
{
    public string targetSceneName; // 遷移するシーンの名前
    public GameObject uiElement; // 表示するUIオブジェクト
    public float interactionDistance = 3.0f; // プレイヤーとオブジェクトの距離
    public KeyCode interactionKey = KeyCode.E; // インタラクトキー
    

    private Transform player;

    void Start()
    {
        // プレイヤーを取得（タグをPlayerに設定しておく必要があります）
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // UIを非表示に設定
        if (uiElement != null)
        {
            uiElement.SetActive(false);
        }
    }

    void Update()
    {
        if (player == null) return;

        // オブジェクトとプレイヤーの距離を計算
        float distance = Vector3.Distance(transform.position, player.position);

        // 距離が一定以下ならUIを表示
        if (distance <= interactionDistance)
        {
            if (uiElement != null)
            {
                uiElement.SetActive(true);
            }

            // 指定されたキーが押されたらシーンを変更
            if (Input.GetKeyDown(interactionKey))
            {
                SceneManager.LoadScene(targetSceneName);
            }
        }
        else
        {
            // 距離が離れている場合はUIを非表示
            if (uiElement != null)
            {
                uiElement.SetActive(false);
            }
        }
    }
}
