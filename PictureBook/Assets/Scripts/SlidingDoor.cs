using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    public Transform player; // �v���C���[��Transform
    public Vector3 openOffset = new Vector3(0, 0, 3); // �J�����Ƃ��̈ʒu�̃I�t�Z�b�g
    public float openSpeed = 2f; // �J���x
    public float activationDistance = 3f; // �v���C���[���߂Â�������臒l
    private Vector3 closedPosition; // �h�A�̕����ʒu
    private Vector3 openPosition; // �h�A�̊J�����ʒu
    private bool isOpen = false; // �h�A���J���Ă��邩�ǂ���

    void Start()
    {
        closedPosition = transform.position; // �����ʒu�������ԂƂ��ĕۑ�
        openPosition = closedPosition + openOffset; // �J�����Ƃ��̈ʒu���v�Z
    }

    void Update()
    {
        // �v���C���[�Ƃ̋������v�Z
        float distance = Vector3.Distance(player.position, transform.position);

        // �v���C���[���͈͓��ɂ���E�L�[�������ꂽ��h�A���J��
        if (distance <= activationDistance && Input.GetKeyDown(KeyCode.E))
        {
            ToggleDoor();
        }

        // �h�A�̈ʒu���^�[�Q�b�g�ʒu�ɋ߂Â���
        Vector3 targetPosition = isOpen ? openPosition : closedPosition;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * openSpeed);
    }

    public void ToggleDoor()
    {
        isOpen = !isOpen; // �J��Ԃ�؂�ւ�
        Debug.Log(isOpen ? "�h�A���J���܂����I" : "�h�A���܂�܂����I");
    }
}
