using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public Camera targetCamera; // UIが向くカメラ

    void Start()
    {
        // カメラが設定されていない場合、自動的にメインカメラを使用
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }
    }

    void Update()
    {
        if (targetCamera != null)
        {
            // カメラの方向を向く
            transform.LookAt(transform.position + targetCamera.transform.rotation * Vector3.forward,
                             targetCamera.transform.rotation * Vector3.up);
        }
    }
}

