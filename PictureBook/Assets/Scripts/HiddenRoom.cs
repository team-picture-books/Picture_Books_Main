using UnityEngine;

public class HiddenRoom : MonoBehaviour
{
    public Transform player; // プレイヤーのTransform
    public Vector3 triggerPosition; // 条件となる位置
    public float triggerRadius = 3f; // 条件となる範囲の半径
    private Renderer[] renderers;
    private bool isVisible = false;

    void Start()
    {
        // 隠し部屋のレンダラーを取得
        renderers = GetComponentsInChildren<Renderer>();

        // 初期状態で非表示
        SetVisibility(false);
    }

    void Update()
    {
        // プレイヤーとトリガー位置の距離を計算
        float distance = Vector3.Distance(player.position, triggerPosition);

        if (distance <= triggerRadius && !isVisible)
        {
            // 範囲内に入った場合、隠し部屋を表示
            SetVisibility(true);
            Debug.Log("隠し部屋が見えるようになりました！");
        }
        else if (distance > triggerRadius && isVisible)
        {
            // 範囲外に出た場合、隠し部屋を非表示
            SetVisibility(false);
            Debug.Log("隠し部屋が再び隠されました！");
        }
    }

    private void SetVisibility(bool visible)
    {
        isVisible = visible;

        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = visible;
        }
    }
}
