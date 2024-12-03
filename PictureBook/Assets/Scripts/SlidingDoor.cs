using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    public Transform player; // プレイヤーのTransform
    public Vector3 openOffset = new Vector3(0, 0, 3); // 開いたときの位置のオフセット
    public float openSpeed = 2f; // 開閉速度
    public float activationDistance = 3f; // プレイヤーが近づく距離の閾値
    private Vector3 closedPosition; // ドアの閉じた位置
    private Vector3 openPosition; // ドアの開いた位置
    private bool isOpen = false; // ドアが開いているかどうか

    void Start()
    {
        closedPosition = transform.position; // 初期位置を閉じた状態として保存
        openPosition = closedPosition + openOffset; // 開いたときの位置を計算
    }

    void Update()
    {
        // プレイヤーとの距離を計算
        float distance = Vector3.Distance(player.position, transform.position);

        // プレイヤーが範囲内にいてEキーが押されたらドアを開閉
        if (distance <= activationDistance && Input.GetKeyDown(KeyCode.E))
        {
            ToggleDoor();
        }

        // ドアの位置をターゲット位置に近づける
        Vector3 targetPosition = isOpen ? openPosition : closedPosition;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * openSpeed);
    }

    public void ToggleDoor()
    {
        isOpen = !isOpen; // 開閉状態を切り替え
        Debug.Log(isOpen ? "ドアが開きました！" : "ドアが閉まりました！");
    }
}
