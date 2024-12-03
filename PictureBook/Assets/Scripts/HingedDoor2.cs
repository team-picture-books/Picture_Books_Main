using UnityEngine;

public class HingedDoor2 : MonoBehaviour, IDoor
{
    public Transform leftDoor; // 左側のドア
    public Transform rightDoor; // 右側のドア
    public float openAngle = 90f; // ドアが開く角度
    public float openSpeed = 2f; // 開閉速度
    private bool isOpen = false; // ドアが開いているかどうか
    private Quaternion leftClosedRotation;
    private Quaternion leftOpenRotation;
    private Quaternion rightClosedRotation;
    private Quaternion rightOpenRotation;

    void Start()
    {
        // 左ドアと右ドアの初期回転を保存
        leftClosedRotation = leftDoor.rotation;
        rightClosedRotation = rightDoor.rotation;

        // 開いたときの回転を計算
        leftOpenRotation = Quaternion.Euler(leftDoor.eulerAngles + new Vector3(0, openAngle, 0));
        rightOpenRotation = Quaternion.Euler(rightDoor.eulerAngles - new Vector3(0, openAngle, 0));
    }

    void Update()
    {
        // 左ドアと右ドアの現在の回転をターゲット回転に近づける
        Quaternion leftTarget = isOpen ? leftOpenRotation : leftClosedRotation;
        Quaternion rightTarget = isOpen ? rightOpenRotation : rightClosedRotation;

        leftDoor.rotation = Quaternion.Lerp(leftDoor.rotation, leftTarget, Time.deltaTime * openSpeed);
        rightDoor.rotation = Quaternion.Lerp(rightDoor.rotation, rightTarget, Time.deltaTime * openSpeed);
    }

    public void ToggleDoor()
    {
        isOpen = !isOpen; // 開閉状態を切り替え
    }
}
