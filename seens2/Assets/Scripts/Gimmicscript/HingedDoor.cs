using UnityEngine;

public class HingedDoor : MonoBehaviour, IDoor
{
    public float openAngle = 90f; // �h�A���J���p�x
    public float openSpeed = 2f; // �J���x
    private bool isOpen = false; // �h�A���J���Ă��邩�ǂ���
    private Quaternion closedRotation; // ������Ԃ̉�]
    private Quaternion openRotation; // �J������Ԃ̉�]

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
        isOpen = !isOpen; // �J��Ԃ�؂�ւ�
    }
}