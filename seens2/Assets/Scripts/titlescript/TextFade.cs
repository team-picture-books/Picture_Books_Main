using System.Collections;
using UnityEngine;
using TMPro;

public class TextFade : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public float pulseDuration = 1.5f;  // パルスの周期時間

    private void Start()
    {
        // TextMeshProのコンポーネントがアタッチされていない場合、自動で取得する
        if (textMeshPro == null)
        {
            textMeshPro = GetComponent<TextMeshProUGUI>();
        }

        // コルーチンでフェードイン・フェードアウトを開始
        StartCoroutine(FadeText());
    }

    private IEnumerator FadeText()
    {
        while (true)
        {
            // 完全表示から50%透明にフェードアウト
            yield return StartCoroutine(LerpTextAlpha(1f, 0.3f, pulseDuration / 2));

            // 50%透明から完全表示にフェードイン
            yield return StartCoroutine(LerpTextAlpha(0.3f, 1f, pulseDuration / 2));
        }
    }

    private IEnumerator LerpTextAlpha(float fromAlpha, float toAlpha, float duration)
    {
        float time = 0f;
        Color currentColor = textMeshPro.color;

        while (time < duration)
        {
            float alpha = Mathf.Lerp(fromAlpha, toAlpha, time / duration);
            textMeshPro.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
            time += Time.deltaTime;
            yield return null;  // 1フレーム待機
        }

        // 最終的に目的のアルファ値に設定する
        textMeshPro.color = new Color(currentColor.r, currentColor.g, currentColor.b, toAlpha);
    }
}
