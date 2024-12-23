using UnityEngine;

public class WarpPoint : MonoBehaviour
{
    public Transform targetWarpPoint; // ���[�v��̃|�C���g
    private float stayTime = 0f;      // ���[�v�|�C���g�ł̑؍ݎ���
    public float requiredStayTime = 2f; // ���[�v����̂ɕK�v�ȑ؍ݎ���

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) // �v���C���[���G���A���ɂ���ꍇ
        {
            stayTime += Time.deltaTime; // �؍ݎ��Ԃ����Z

            if (stayTime >= requiredStayTime) // �K�v�ȑ؍ݎ��Ԃ𒴂����ꍇ
            {
                CharacterController playerController = other.GetComponent<CharacterController>();

                if (playerController != null)
                {
                    // ���[�v��Ƀv���C���[���ړ�
                    playerController.enabled = false; // CharacterController���ꎞ���������ă��[�v����
                    other.transform.position = targetWarpPoint.position;
                    other.transform.rotation = targetWarpPoint.rotation;
                    playerController.enabled = true; // �ĂїL����
                }

                stayTime = 0f; // �؍ݎ��Ԃ����Z�b�g
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // �v���C���[���G���A����o���ꍇ
        {
            stayTime = 0f; // �؍ݎ��Ԃ����Z�b�g
        }
    }
}
