using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuContoroller : MonoBehaviour
{
    public TextMeshProUGUI[] menuItems;  // メニュー項目 (5つのTextMeshProUGUI)
    public string[] sceneNames;           // 各メニュー項目に対応するシーン名
    private int selectedIndex = 0;        // 現在選択されているインデックス
    private Coroutine blinkCoroutine;     // 点滅用コルーチン

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
        // Xboxコントローラーの左スティックのY軸の入力を取得
        float verticalInput = Input.GetAxis("Vertical"); // "Vertical" はデフォルトで上下に設定されている

        if (verticalInput > 0.5f) // 上にスワイプ
        {
            selectedIndex--;
            if (selectedIndex < 0) selectedIndex = menuItems.Length - 1;
            UpdateMenuDisplay();
            // 軸入力をリセットして連続入力を防ぐ
            while (verticalInput > 0.5f)
                verticalInput = Input.GetAxis("Vertical");
        }
        else if (verticalInput < -0.5f) // 下にスワイプ
        {
            selectedIndex++;
            if (selectedIndex >= menuItems.Length) selectedIndex = 0;
            UpdateMenuDisplay();
            // 軸入力をリセットして連続入力を防ぐ
            while (verticalInput < -0.5f)
                verticalInput = Input.GetAxis("Vertical");
        }

        if (Input.GetButtonDown("Bbutton")) // Bボタンで決定
        {
            // 対応するシーンに遷移
            SceneManager.LoadScene(sceneNames[selectedIndex]);
        }
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

