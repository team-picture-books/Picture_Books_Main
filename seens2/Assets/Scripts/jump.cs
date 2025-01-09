using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jump : MonoBehaviour
{
    public float jumpSpeed = 5f;          // ジャンプの速度

    private CharacterController controller; // CharacterController コンポーネントの参照

    private Vector3 velocity;             // プレイヤーの垂直方向の速度
    public float gravity = 100f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!controller.isGrounded)
        {
            velocity.y = -15f;  // 着地しているときは、軽く地面に抑える

            if (Input.GetButtonDown("Ybutton"))  // デフォルトでは「Jump」はスペースキーに設定されています
            {
                Debug.Log("ジャンプ");
                velocity.y = jumpSpeed;  // ジャンプ
            }
        }
        else
        {
            // 空中にいるときに重力を適用
            velocity.y -= gravity * Time.deltaTime;
        }
    }
}
