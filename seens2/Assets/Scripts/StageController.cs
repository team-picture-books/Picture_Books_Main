using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    public float moveSpeed = 5f;          // プレイヤーの移動速度
    public float rotationSpeed = 100f;    // プレイヤーの回転速度
    public float jumpSpeed = 5f;          // ジャンプの速度
    private CharacterController controller; // CharacterController コンポーネントの参照
    private Vector3 velocity;             // プレイヤーの垂直方向の速度
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        float rightStickX = Input.GetAxis("RightStickHorizontal");  // Xboxコントローラー右スティックの横移動
        transform.Rotate(Vector3.up, rightStickX * rotationSpeed * Time.deltaTime);
        float mouseX = Input.GetAxis("Mouse X"); //マウスポインタでの回転
        transform.Rotate(Vector3.up, mouseX * rotationSpeed * Time.fixedDeltaTime);
    }
}
