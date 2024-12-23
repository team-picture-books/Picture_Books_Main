using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public GameObject player; // プレイヤーのオブジェクト
    public GameObject interactionUI; // 近づいたときに表示されるUI (例えばボタンアイコン)
    public float interactionDistance = 3.0f; // ドアに近づく距離
    public bool canOpenDoor = false; // ドアを開けるフラグ
    public float rotationSpeed = 2.0f; // ドアの回転速度
    public Vector3 openRotation = new Vector3(0, 90, 0); // ドアを開いたときの回転角度

    private bool isNearDoor = false; // プレイヤーが近くにいるかどうか
    private Quaternion initialRotation; // ドアの初期回転
    private Quaternion targetRotation; // ドアが開いたときの目標回転
    private bool isDoorOpen = false; // ドアが開いているかどうか

    void Start()
    {
        interactionUI.SetActive(false); // 初期状態で非表示
        initialRotation = transform.rotation; // ドアの初期回転を保存
        targetRotation = Quaternion.Euler(initialRotation.eulerAngles + openRotation); // ドアが開いたときの回転を計算
    }

    void Update()
    {
        // プレイヤーとドアの距離を計算
        float distance = Vector3.Distance(player.transform.position, transform.position);

        // プレイヤーが近づいたらUIを表示
        if (distance <= interactionDistance)
        {
            interactionUI.SetActive(true);
            isNearDoor = true;
        }
        else
        {
            interactionUI.SetActive(false);
            isNearDoor = false;
        }

        // キー入力でドアを開ける処理
        if (isNearDoor && canOpenDoor && Input.GetKeyDown(KeyCode.E)||isNearDoor && canOpenDoor && Input.GetButtonDown("Bbutton"))
        {
            if (!isDoorOpen)
            {
                isDoorOpen = true; // ドアを開く
                Debug.Log("ドアが開きます！");
            }
        }

        // ドアを開く回転処理
        if (isDoorOpen)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
