using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject pauseMenuUI;  // ポーズ画面のUI
    private bool isPaused = false;  // ゲームがポーズ中かどうかのフラグ

    void Update()
    {
        // XboxコントローラーのStartボタンが押されたとき
        if (Input.GetButtonDown("Start"))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                Pause();
            }
        }
    }

    // ゲームを再開する処理
    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);     // ポーズメニューを非表示にする
        Time.timeScale = 1f;              // ゲームの時間を通常に戻す
        isPaused = false;                 // ポーズ状態を解除
    }

    // ゲームをポーズする処理
    void Pause()
    {
        pauseMenuUI.SetActive(true);      // ポーズメニューを表示する
        Time.timeScale = 0f;              // ゲームの時間を停止する
        isPaused = true;                  // ポーズ状態にする
    }
}
