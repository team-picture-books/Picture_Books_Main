using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    private CharacterController controller;
    private float gravity = 9.81f;

    // �v���C���[�̈ړ��⎋�_�ړ��𐧌䂷��t���O
    public bool canMove = true;

    // �n���}�[�֘A
    public GameObject hammer; // �n���}�[�̃v���n�u�܂��̓I�u�W�F�N�g
    public Transform hammerHoldPosition; // �n���}�[�����ʒu
    public float pickupRadius = 2f; // �n���}�[���E����͈�
    private bool isHoldingHammer = false;
    private Animator animator; // �n���}�[��U��A�j���[�V����

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>(); // �v���C���[��Animator
    }

    void Update()
    {
        // �ړ�����
        if (canMove)
        {
            HandleMovement();
        }

        // �n���}�[�擾����
        HandleHammerPickup();

        // �n���}�[��U�鏈��
        HandleHammerSwing();
    }

    private void HandleMovement()
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

    private void HandleHammerPickup()
    {
        if (isHoldingHammer) return;

        // �n���}�[���͈͓��ɂ��邩�m�F
        if (hammer != null && Vector3.Distance(transform.position, hammer.transform.position) <= pickupRadius)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                // �n���}�[���擾
                isHoldingHammer = true;
                hammer.transform.SetParent(hammerHoldPosition);
                hammer.transform.localPosition = Vector3.zero;
                hammer.transform.localRotation = Quaternion.identity;

                Debug.Log("�n���}�[���E���܂����I");
            }
        }
    }

    private void HandleHammerSwing()
    {
        if (isHoldingHammer && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("�n���}�[��U��܂����I");
            if (animator != null)
            {
                animator.SetTrigger("Swing"); // �A�j���[�V�������Đ�
            }

            // �U�����ۂɕǂ�j��ł���悤��
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 2f))
            {
                if (hit.collider.CompareTag("BreakableWall"))
                {
                    hit.collider.GetComponent<BreakableWall>()?.BreakWall();
                }
            }
        }
    }
}
