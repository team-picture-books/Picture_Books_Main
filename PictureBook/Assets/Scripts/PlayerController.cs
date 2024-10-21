using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    private CharacterController controller;
    private float gravity = 9.81f;

    // プレイヤーの移動や視点移動を制御するフラグ
    public bool canMove = true;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // canMoveがtrueのときのみ操作を許可
        if (canMove)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // マウスの移動量を取得してプレイヤーを回転させる
            float mouseX = Input.GetAxis("Mouse X");
            transform.Rotate(Vector3.up, mouseX * rotationSpeed * Time.fixedDeltaTime);

            // 移動方向の計算
            Vector3 moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;

            if (!controller.isGrounded)
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }

            controller.Move(moveDirection * moveSpeed * Time.deltaTime);
        }
    }
}
