using UnityEngine;

public class PlaySEOnButton : MonoBehaviour
{
    public AudioSource seSource; // SE用のAudioSource

    public void PlaySE()
    {
        if (seSource != null)
        {
            seSource.Play(); // 効果音を再生
        }
        else
        {
            Debug.LogWarning("AudioSourceがアタッチされていません。");
        }
    }
}
