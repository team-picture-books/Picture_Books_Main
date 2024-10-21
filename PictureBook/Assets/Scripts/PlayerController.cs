using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    private CharacterController controller;
    private float gravity = 9.81f;

    // �v���C���[�̈ړ��⎋�_�ړ��𐧌䂷��t���O
    public bool canMove = true;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // canMove��true�̂Ƃ��̂ݑ��������
        if (canMove)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // �}�E�X�̈ړ��ʂ��擾���ăv���C���[����]������
            float mouseX = Input.GetAxis("Mouse X");
            transform.Rotate(Vector3.up, mouseX * rotationSpeed * Time.fixedDeltaTime);

            // �ړ������̌v�Z
            Vector3 moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;

            if (!controller.isGrounded)
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }

            controller.Move(moveDirection * moveSpeed * Time.deltaTime);
        }
    }
}
