using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    public GameObject hiddenDoor; // 隠し扉のGameObject
    public GameObject breakEffect; // 壁が壊れるエフェクト
    private bool isBroken = false;

    public void BreakWall()
    {
        if (isBroken) return;
        isBroken = true;

        // 壁を非表示または削除
        gameObject.SetActive(false);

        // 壁が壊れるエフェクトを再生
        if (breakEffect != null)
        {
            Instantiate(breakEffect, transform.position, Quaternion.identity);
        }

        // 隠し扉を表示
        if (hiddenDoor != null)
        {
            hiddenDoor.SetActive(true);
            Debug.Log("扉を表示しました！");
        }
        else
        {
            Debug.LogError("hiddenDoor が設定されていません！");
        }
    }
}
