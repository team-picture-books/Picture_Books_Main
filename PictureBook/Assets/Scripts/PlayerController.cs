using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    private CharacterController controller;
    private float gravity = 9.81f;

    // プレイヤーの移動や視点移動を制御するフラグ
    public bool canMove = true;

    // ハンマー関連
    public GameObject hammer; // ハンマーのプレハブまたはオブジェクト
    public Transform hammerHoldPosition; // ハンマーを持つ位置
    public float pickupRadius = 2f; // ハンマーを拾える範囲
    private bool isHoldingHammer = false;
    private Animator animator; // ハンマーを振るアニメーション

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>(); // プレイヤーのAnimator
    }

    void Update()
    {
        // 移動処理
        if (canMove)
        {
            HandleMovement();
        }

        // ハンマー取得処理
        HandleHammerPickup();

        // ハンマーを振る処理
        HandleHammerSwing();
    }

    private void HandleMovement()
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

    private void HandleHammerPickup()
    {
        if (isHoldingHammer) return;

        // ハンマーが範囲内にあるか確認
        if (hammer != null && Vector3.Distance(transform.position, hammer.transform.position) <= pickupRadius)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                // ハンマーを取得
                isHoldingHammer = true;
                hammer.transform.SetParent(hammerHoldPosition);
                hammer.transform.localPosition = Vector3.zero;
                hammer.transform.localRotation = Quaternion.identity;

                Debug.Log("ハンマーを拾いました！");
            }
        }
    }

    private void HandleHammerSwing()
    {
        if (isHoldingHammer && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("ハンマーを振りました！");
            if (animator != null)
            {
                animator.SetTrigger("Swing"); // アニメーションを再生
            }

            // 振った際に壁を破壊できるように
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
