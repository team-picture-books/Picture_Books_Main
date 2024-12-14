using UnityEngine;

public class HingedDoor2 : MonoBehaviour, IDoor
{
    public Transform leftDoor; // �����̃h�A
    public Transform rightDoor; // �E���̃h�A
    public float openAngle = 90f; // �h�A���J���p�x
    public float openSpeed = 2f; // �J���x
    private bool isOpen = false; // �h�A���J���Ă��邩�ǂ���
    private Quaternion leftClosedRotation;
    private Quaternion leftOpenRotation;
    private Quaternion rightClosedRotation;
    private Quaternion rightOpenRotation;

    void Start()
    {
        // ���h�A�ƉE�h�A�̏�����]��ۑ�
        leftClosedRotation = leftDoor.rotation;
        rightClosedRotation = rightDoor.rotation;

        // �J�����Ƃ��̉�]���v�Z
        leftOpenRotation = Quaternion.Euler(leftDoor.eulerAngles + new Vector3(0, openAngle, 0));
        rightOpenRotation = Quaternion.Euler(rightDoor.eulerAngles - new Vector3(0, openAngle, 0));
    }

    void Update()
    {
        // ���h�A�ƉE�h�A�̌��݂̉�]���^�[�Q�b�g��]�ɋ߂Â���
        Quaternion leftTarget = isOpen ? leftOpenRotation : leftClosedRotation;
        Quaternion rightTarget = isOpen ? rightOpenRotation : rightClosedRotation;

        leftDoor.rotation = Quaternion.Lerp(leftDoor.rotation, leftTarget, Time.deltaTime * openSpeed);
        rightDoor.rotation = Quaternion.Lerp(rightDoor.rotation, rightTarget, Time.deltaTime * openSpeed);
    }

    public void ToggleDoor()
    {
        isOpen = !isOpen; // �J��Ԃ�؂�ւ�
    }
}
