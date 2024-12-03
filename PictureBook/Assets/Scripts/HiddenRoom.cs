using UnityEngine;

public class HiddenRoom : MonoBehaviour
{
    public Transform player; // �v���C���[��Transform
    public Vector3 triggerPosition; // �����ƂȂ�ʒu
    public float triggerRadius = 3f; // �����ƂȂ�͈͂̔��a
    private Renderer[] renderers;
    private bool isVisible = false;

    void Start()
    {
        // �B�������̃����_���[���擾
        renderers = GetComponentsInChildren<Renderer>();

        // ������ԂŔ�\��
        SetVisibility(false);
    }

    void Update()
    {
        // �v���C���[�ƃg���K�[�ʒu�̋������v�Z
        float distance = Vector3.Distance(player.position, triggerPosition);

        if (distance <= triggerRadius && !isVisible)
        {
            // �͈͓��ɓ������ꍇ�A�B��������\��
            SetVisibility(true);
            Debug.Log("�B��������������悤�ɂȂ�܂����I");
        }
        else if (distance > triggerRadius && isVisible)
        {
            // �͈͊O�ɏo���ꍇ�A�B���������\��
            SetVisibility(false);
            Debug.Log("�B���������ĂщB����܂����I");
        }
    }

    private void SetVisibility(bool visible)
    {
        isVisible = visible;

        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = visible;
        }
    }
}
