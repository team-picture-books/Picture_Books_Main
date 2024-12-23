using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    public GameObject hiddenDoor; // �B������GameObject
    public GameObject breakEffect; // �ǂ�����G�t�F�N�g
    private bool isBroken = false;

    public void BreakWall()
    {
        if (isBroken) return;
        isBroken = true;

        // �ǂ��\���܂��͍폜
        gameObject.SetActive(false);

        // �ǂ�����G�t�F�N�g���Đ�
        if (breakEffect != null)
        {
            Instantiate(breakEffect, transform.position, Quaternion.identity);
        }

        // �B������\��
        if (hiddenDoor != null)
        {
            hiddenDoor.SetActive(true);
            Debug.Log("����\�����܂����I");
        }
        else
        {
            Debug.LogError("hiddenDoor ���ݒ肳��Ă��܂���I");
        }
    }
}
