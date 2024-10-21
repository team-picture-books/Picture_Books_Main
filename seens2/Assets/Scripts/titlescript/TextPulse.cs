using System.Collections;
using UnityEngine;
using TMPro;

public class TextPulse : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public Color normalColor = Color.white;     // 通常表示の色（デフォルトは白）
    public Color dimmedColor = Color.gray;      // 50%トーンダウンの色（デフォルトはグレー）
    public float pulseDuration = 1.5f;          // パルスの周期時間

    private void Start()
    {
        // TextMeshProのコンポーネントがアタッチされていない場合、自動で取得する
        if (textMeshPro == null)
        {
            textMeshPro = GetComponent<TextMeshProUGUI>();
        }

        // コルーチンで色のトーンダウンを開始する
        StartCoroutine(PulseText());
    }

    private IEnumerator PulseText()
    {
        while (true)
        {
            // 通常色からトーンダウン色まで色を変化させる
            yield return StartCoroutine(LerpTextColor(normalColor, dimmedColor, pulseDuration / 2));

            // トーンダウン色から通常色まで色を戻す
            yield return StartCoroutine(LerpTextColor(dimmedColor, normalColor, pulseDuration / 2));
        }
    }

    private IEnumerator LerpTextColor(Color fromColor, Color toColor, float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            // 色を線形補間で徐々に変化させる
            textMeshPro.color = Color.Lerp(fromColor, toColor, time / duration);
            time += Time.deltaTime;
            yield return null; // 1フレーム待機
        }

        // 最終的に目的の色に設定する
        textMeshPro.color = toColor;
    }
}
