using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // フラグなどのデータ
    public bool DumbbellFlag = false;
    public bool RopeFlag = false;
    public bool ProteinFlag = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーンをまたいでオブジェクトを保持
        }
        else
        {
            Destroy(gameObject); // 重複を防ぐ
        }
    }
}
