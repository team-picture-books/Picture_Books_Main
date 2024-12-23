using UnityEngine;
using TMPro; // TextMeshProを使用
using UnityEngine.UI; // UIのImageを使用
using UnityEngine.SceneManagement; // シーン遷移用

public class OujiChoicescript : MonoBehaviour
{
    public GameObject player; // プレイヤーのオブジェクト
    public GameObject interactionUI; // 近づいたときに表示されるUI (全体のパネル)
    public GameObject choiceUI; // 選択肢のUI (全体のパネル)
    public float interactionDistance = 3.0f; // オブジェクトに近づく距離
    public TMP_Text choiceText; // 選択肢のTextMeshProテキスト

    // インスペクタで設定可能なシーン名
    public string scene1; // シーン1の名前
    public string scene2; // シーン2の名前
    public string scene3; // シーン3の名前
    public string scene4; // シーン4の名前

    private bool isNearObject = false;

    void Start()
    {
        interactionUI.SetActive(false); // 初期状態で非表示
        choiceUI.SetActive(false);     // 初期状態で非表示
    }

    void Update()
    {
        // プレイヤーとオブジェクトの距離を計算
        float distance = Vector3.Distance(player.transform.position, transform.position);

        // プレイヤーが近づいたらUIを表示
        if (distance <= interactionDistance)
        {
            interactionUI.SetActive(true);
            isNearObject = true;
        }
        else
        {
            interactionUI.SetActive(false);
            choiceUI.SetActive(false);
            isNearObject = false;
        }

        // 近くでキーを押したら選択肢UIを表示
        if (isNearObject && Input.GetKeyDown(KeyCode.E)|| isNearObject && Input.GetButtonDown("Bbutton")) // "E"キーで選択肢を表示
        {
            choiceUI.SetActive(true);
            choiceText.text = "わたしがとるべき行動は...\nY. なぐらない\nRB. よわくたたく\nA. そこそこつよくたたく\nX. なぐる";
        }

        // 選択肢のキー入力
        if (choiceUI.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetButtonDown("Ybutton")) // "1"キー
            {
                SceneManager.LoadScene(scene1); // シーン1に移行
                Debug.Log("おぼえるを選択しました。");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetButtonDown("RBbutton")) // "2"キー
            {
                SceneManager.LoadScene(scene2); // シーン2に移行
                Debug.Log("おぼえないを選択しました。");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3)|| Input.GetButtonDown("Abutton")) // "3"キー
            {
                SceneManager.LoadScene(scene3); // シーン3に移行
                Debug.Log("じっと見るを選択しました。");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4)|| Input.GetButtonDown("Xbutton")) // "4"キー
            {
                SceneManager.LoadScene(scene4); // シーン4に移行
                Debug.Log("通り過ぎるを選択しました。");
            }
        }
    }
}
