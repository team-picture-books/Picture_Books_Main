using UnityEngine;
using System.Collections.Generic; // Listを使うため

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    private CharacterController controller;
    public float gravity = 100f;

    public float pickupRadius = 20f;

    // プレイヤーの移動や視点移動を制御するフラグ
    public bool canMove = true;

    // アイテムバーの表示状態を管理するフラグ


    // ツボ関連
    public GameObject potPrefab; // ツボのプレハブ
    // アイテムを保持する位置
    private List<GameObject> inventory = new List<GameObject>(); // インベントリ

    public Transform soldier;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        // 初期状態でアイテムバーを非表示に設定
        
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
            
        }
        // ツボ拾得処理
        HandlePotPickup();

        // ツボ再設置処理
        HandlePotPlacement();
    }
    private void HandlePotPickup()
    {
        // シーン内のツボを取得
        GameObject[] pots = GameObject.FindGameObjectsWithTag("Pot");

        foreach (var pot in pots)
        {
            if (Vector3.Distance(transform.position, pot.transform.position) <= pickupRadius)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    // ツボを拾う
                    inventory.Add(pot);
                    pot.SetActive(false); // ツボを非表示にする
                    Debug.Log("ツボを拾いました！");
                    break;
                }
            }
        }
    }
    private void HandlePotPlacement()
    {
        if (inventory.Count > 0 && Input.GetKeyDown(KeyCode.Space))
        {
            // 配置位置を計算
            Vector3 placementPosition = transform.position + transform.forward * 2f;

            // Y軸を固定する（例: 地面の高さを0.5に固定）
            placementPosition.y = 20f;

            // アイテム欄からツボを再設置
            GameObject pot = inventory[0];
            inventory.RemoveAt(0);
            pot.SetActive(true); // ツボを表示
            pot.transform.position = placementPosition; // プレイヤーの前に設置
            Debug.Log("ツボを設置しました！");

            // 兵士に通知
            SoldierPatrol soldierScript = soldier.GetComponent<SoldierPatrol>();
            if (soldierScript != null)
            {
                soldierScript.OnPotPlaced(pot.transform.position);
            }
        }
    }
}

