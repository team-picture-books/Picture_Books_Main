using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;      // プレイヤーの移動速度
    public float rotationSpeed = 100f;  // プレイヤーの回転速度
    private CharacterController controller;  // CharacterController コンポーネントの参照
    private float gravity = 9.81f;  // 重力の加速度

    void Start()
    {
        // CharacterController コンポーネントを取得
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // キーボード入力の取得
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // マウスの移動量を取得してプレイヤーを回転させる
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up, mouseX * rotationSpeed * Time.fixedDeltaTime);

        // 移動方向の計算
        Vector3 moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;

        // 重力の適用
        if (!controller.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // 移動
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
    }
}
