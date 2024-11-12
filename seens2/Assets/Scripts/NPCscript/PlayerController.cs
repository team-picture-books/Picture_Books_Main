using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    private CharacterController controller;
    private float gravity = 9.81f;
    public GameObject itembar;

    // プレイヤーの移動や視点移動を制御するフラグ
    public bool canMove = true;

    // アイテムバーの表示状態を管理するフラグ
    private bool isItemBarVisible = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        // 初期状態でアイテムバーを非表示に設定
        itembar.SetActive(false);
    }

    void Update()
    {
        // canMoveがtrueのときのみ操作を許可
        if (canMove)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // LBとRBボタンで視点を回転
            float rotationInput = 0f;
            if (Input.GetButton("RBbutton"))
            {
                rotationInput = 1f; // RBボタンが押されているときは右回転
            }
            else if (Input.GetButton("LBbutton"))
            {
                rotationInput = -1f; // LBボタンが押されているときは左回転
            }

            // プレイヤーを回転させる
            transform.Rotate(Vector3.up, rotationInput * rotationSpeed * Time.fixedDeltaTime);

            // 移動方向の計算
            Vector3 moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;

            if (!controller.isGrounded)
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }

            controller.Move(moveDirection * moveSpeed * Time.deltaTime);

            // Xボタンでアイテムバーの表示/非表示を切り替え
            if (Input.GetButtonDown("Xbutton")) // Xボタンが押されたとき
            {
                isItemBarVisible = !isItemBarVisible; // 状態を切り替え
                itembar.SetActive(isItemBarVisible); // アイテムバーの表示状態を設定
            }
        }
    }
}

