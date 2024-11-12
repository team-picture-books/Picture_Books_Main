using System.Collections;
using UnityEngine;
using TMPro;

public class TitleScreenManager : MonoBehaviour
{
    public CanvasGroup background;        // 背景のCanvasGroup
    public CanvasGroup titleLogo;         // タイトルロゴのCanvasGroup
    public CanvasGroup teamName;          // 制作チーム名のCanvasGroup
    public TextMeshProUGUI startText;     // 「Bボタンではじめる」TextMeshProオブジェクト

    private float fadeDuration = 0.5f;    // フェードイン/アウトの時間
    private float blinkDuration = 1.5f;   // 点滅の周期時間

    private void Start()
    {
        // 初期設定
        background.alpha = 0;
        titleLogo.alpha = 0;
        teamName.alpha = 0;
        startText.alpha = 0;

        // 演出を開始
        StartCoroutine(ShowTitleSequence());
    }

    private IEnumerator ShowTitleSequence()
    {
        // 背景をフェードイン
        yield return StartCoroutine(FadeIn(background, fadeDuration));

        // タイトルロゴをフェードイン
        yield return StartCoroutine(FadeIn(titleLogo, fadeDuration));

        // 制作チーム名をフェードイン
        yield return StartCoroutine(FadeIn(teamName, fadeDuration));

        // 「Bボタンではじめる」を点滅
        StartCoroutine(BlinkText());
    }

    private IEnumerator FadeIn(CanvasGroup canvasGroup, float duration)
    {
        float time = 0;
        while (time < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1;
    }

    private IEnumerator BlinkText()
    {
        while (true)
        {
            // テキストを徐々にフェードイン
            float time = 0;
            while (time < blinkDuration / 2)
            {
                startText.alpha = Mathf.Lerp(0.5f, 1f, time / (blinkDuration / 2));
                time += Time.deltaTime;
                yield return null;
            }

            // テキストを徐々にフェードアウト
            time = 0;
            while (time < blinkDuration / 2)
            {
                startText.alpha = Mathf.Lerp(1f, 0.5f, time / (blinkDuration / 2));
                time += Time.deltaTime;
                yield return null;
            }
        }
    }
}
