using UnityEngine;

public class PlaySEOnButton : MonoBehaviour
{
    public AudioSource seSource; // SE�p��AudioSource

    public void PlaySE()
    {
        if (seSource != null)
        {
            seSource.Play(); // ���ʉ����Đ�
        }
        else
        {
            Debug.LogWarning("AudioSource���A�^�b�`����Ă��܂���B");
        }
    }
}
