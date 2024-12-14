using UnityEngine;

public class HingedDoor : MonoBehaviour, IDoor
{
    public float openAngle = 90f; // ドアが開く角度
    public float openSpeed = 2f; // 開閉速度
    private bool isOpen = false; // ドアが開いているかどうか
    private Quaternion closedRotation; // 閉じた状態の回転
    private Quaternion openRotation; // 開いた状態の回転

    void Start()
    {
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, openAngle, 0));
    }

    void Update()
    {
        Quaternion targetRotation = isOpen ? openRotation : closedRotation;
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * openSpeed);
    }

    public void ToggleDoor()
    {
        isOpen = !isOpen; // 開閉状態を切り替え
    }
}