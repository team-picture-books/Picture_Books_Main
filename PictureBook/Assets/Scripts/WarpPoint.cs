using UnityEngine;

public class WarpPoint : MonoBehaviour
{
    public Transform targetWarpPoint; // ワープ先のポイント
    private float stayTime = 0f;      // ワープポイントでの滞在時間
    public float requiredStayTime = 2f; // ワープするのに必要な滞在時間

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) // プレイヤーがエリア内にいる場合
        {
            stayTime += Time.deltaTime; // 滞在時間を加算

            if (stayTime >= requiredStayTime) // 必要な滞在時間を超えた場合
            {
                CharacterController playerController = other.GetComponent<CharacterController>();

                if (playerController != null)
                {
                    // ワープ先にプレイヤーを移動
                    playerController.enabled = false; // CharacterControllerを一時無効化してワープ処理
                    other.transform.position = targetWarpPoint.position;
                    other.transform.rotation = targetWarpPoint.rotation;
                    playerController.enabled = true; // 再び有効化
                }

                stayTime = 0f; // 滞在時間をリセット
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // プレイヤーがエリアから出た場合
        {
            stayTime = 0f; // 滞在時間をリセット
        }
    }
}
