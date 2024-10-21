using UnityEngine;

public class PlayerXboxController : MonoBehaviour
{
    public float moveSpeed = 5f;          // プレイヤーの移動速度
    public float rotationSpeed = 100f;    // プレイヤーの回転速度
    public float jumpSpeed = 5f;          // ジャンプの速度
    private CharacterController controller; // CharacterController コンポーネントの参照
    private float gravity = 9.81f;        // 重力の加速度
    private Vector3 velocity;             // プレイヤーの垂直方向の速度

    void Start()
    {
        // CharacterController コンポーネントを取得
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // キーボードとXboxコントローラーの入力を取得
        float horizontalInput = Input.GetAxis("Horizontal");  // 左スティック (x 軸) および A, D キー
        float verticalInput = Input.GetAxis("Vertical");      // 左スティック (y 軸) および W, S キー

        // 右スティックの移動量を取得してプレイヤーを回転させる
        float rightStickX = Input.GetAxis("RightStickHorizontal");  // Xboxコントローラー右スティックの横移動
        transform.Rotate(Vector3.up, rightStickX * rotationSpeed * Time.deltaTime);
        float mouseX = Input.GetAxis("Mouse X"); //マウスポインタでの回転
        transform.Rotate(Vector3.up, mouseX * rotationSpeed * Time.fixedDeltaTime);

        // 移動方向の計算
        Vector3 moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;

        // ジャンプボタンの取得 (A ボタンまたはスペースキー)
        if (controller.isGrounded)
        {
            velocity.y = -0.5f;  // 着地しているときは、軽く地面に抑える

            if (Input.GetButtonDown("Jump"))  // デフォルトでは「Jump」はスペースキーに設定されています
            {
                velocity.y = jumpSpeed;  // ジャンプ
            }
        }
        else
        {
            // 空中にいるときに重力を適用
            velocity.y -= gravity * Time.deltaTime;
        }

        // 移動
        controller.Move((moveDirection * moveSpeed + velocity) * Time.deltaTime);
    }
}
