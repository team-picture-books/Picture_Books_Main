using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    public TextMeshProUGUI[] menuItems;  // メニュー項目 (5つのTextMeshProUGUI)
    public string[] sceneNames;          // 各メニュー項目に対応するシーン名
    private int selectedIndex = 0;       // 現在選択されているインデックス
    private Coroutine blinkCoroutine;    // 点滅用コルーチン
    private bool isAxisInUse = false;    // アクシス連続入力防止用フラグ

    private void Start()
    {
        UpdateMenuDisplay();
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        float verticalInput = Input.GetAxis("Vertical");

        if (!isAxisInUse)
        {
            if (verticalInput > 0.5f) // 上方向の入力
            {
                selectedIndex--;
                if (selectedIndex < 0) selectedIndex = menuItems.Length - 1;
                UpdateMenuDisplay();
                StartCoroutine(ResetAxis());
            }
            else if (verticalInput < -0.5f) // 下方向の入力
            {
                selectedIndex++;
                if (selectedIndex >= menuItems.Length) selectedIndex = 0;
                UpdateMenuDisplay();
                StartCoroutine(ResetAxis());
            }
        }

        if (Input.GetButtonDown("Bbutton"))
        {
            // ボタン入力で選択されたシーンに遷移
            SceneManager.LoadScene(sceneNames[selectedIndex]);
        }
    }

    private IEnumerator ResetAxis()
    {
        isAxisInUse = true; // アクシス入力フラグを立てる
        yield return new WaitForSeconds(0.2f); // 必要に応じて調整
        isAxisInUse = false; // フラグをリセット
    }

    private void UpdateMenuDisplay()
    {
        // 全てのメニュー項目の点滅を停止
        for (int i = 0; i < menuItems.Length; i++)
        {
            StopBlinking(menuItems[i]);
        }

        // 選択されたメニュー項目のみ点滅開始
        blinkCoroutine = StartCoroutine(BlinkText(menuItems[selectedIndex]));
    }

    private IEnumerator BlinkText(TextMeshProUGUI text)
    {
        while (true)
        {
            text.alpha = 1f;  // 完全に表示
            yield return new WaitForSeconds(0.5f);
            text.alpha = 0.5f;  // 半透明
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void StopBlinking(TextMeshProUGUI text)
    {
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);  // 点滅を停止
            blinkCoroutine = null;
        }
        text.alpha = 1f;  // αを完全に戻す
    }
}

