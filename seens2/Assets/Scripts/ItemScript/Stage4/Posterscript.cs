using UnityEngine;
using TMPro; // TextMeshProを使用
using UnityEngine.UI; // UIのImageを使用

public class Posterscript : MonoBehaviour
{
    public GameObject player; // プレイヤーのオブジェクト
    public GameObject interactionUI; // 近づいたときに表示されるUI (全体のパネル)
    public GameObject choiceUI; // 選択肢のUI (全体のパネル)
    public float interactionDistance = 3.0f; // オブジェクトに近づく距離
    public TMP_Text choiceText; // 選択肢のTextMeshProテキスト
    public Image choiceImage; // 選択肢の画像

    public Sprite choiceImageSprite; // 表示する画像 (インスペクタで指定可能)

    private bool isNearObject = false;
    private bool destroyFlag = false;
    private bool keepFlag = false;

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
            choiceText.text = "ムキムキの人間たちのポスターだ\nY. おぼえる\nA. おぼえない";
            choiceImage.sprite = choiceImageSprite; // 画像を設定
        }

        // 選択肢のキー入力
        if (choiceUI.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)|| Input.GetButtonDown("Ybutton")) // "1"キー
            {
                destroyFlag = true;
                choiceUI.SetActive(false);
                Debug.Log("こわすを選択しました。");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2)||Input.GetButtonDown("Abutton")) // "2"キー
            {
                keepFlag = true;
                choiceUI.SetActive(false);
                Debug.Log("そのままにするを選択しました。");
            }
        }
    }

    public bool GetDestroyFlag()
    {
        return destroyFlag;
    }

    public bool GetKeepFlag()
    {
        return keepFlag;
    }
}
