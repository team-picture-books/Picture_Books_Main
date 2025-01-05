using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // �t���O�Ȃǂ̃f�[�^
    public bool DumbbellFlag = false;
    public bool RopeFlag = false;
    public bool ProteinFlag = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �V�[�����܂����ŃI�u�W�F�N�g��ێ�
        }
        else
        {
            Destroy(gameObject); // �d����h��
        }
    }
}
