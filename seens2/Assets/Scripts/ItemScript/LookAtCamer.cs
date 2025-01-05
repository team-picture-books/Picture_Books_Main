using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public Camera targetCamera; // UI�������J����

    void Start()
    {
        // �J�������ݒ肳��Ă��Ȃ��ꍇ�A�����I�Ƀ��C���J�������g�p
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }
    }

    void Update()
    {
        if (targetCamera != null)
        {
            // �J�����̕���������
            transform.LookAt(transform.position + targetCamera.transform.rotation * Vector3.forward,
                             targetCamera.transform.rotation * Vector3.up);
        }
    }
}

