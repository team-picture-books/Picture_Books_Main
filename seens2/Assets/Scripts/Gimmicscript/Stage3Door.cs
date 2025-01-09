using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage3Door : MonoBehaviour
{
    public string targetSceneName; // �J�ڂ���V�[���̖��O
    public GameObject uiElement; // �\������UI�I�u�W�F�N�g
    public float interactionDistance = 3.0f; // �v���C���[�ƃI�u�W�F�N�g�̋���
    public KeyCode interactionKey = KeyCode.E; // �C���^���N�g�L�[
    

    private Transform player;

    void Start()
    {
        // �v���C���[���擾�i�^�O��Player�ɐݒ肵�Ă����K�v������܂��j
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // UI���\���ɐݒ�
        if (uiElement != null)
        {
            uiElement.SetActive(false);
        }
    }

    void Update()
    {
        if (player == null) return;

        // �I�u�W�F�N�g�ƃv���C���[�̋������v�Z
        float distance = Vector3.Distance(transform.position, player.position);

        // ���������ȉ��Ȃ�UI��\��
        if (distance <= interactionDistance)
        {
            if (uiElement != null)
            {
                uiElement.SetActive(true);
            }

            // �w�肳�ꂽ�L�[�������ꂽ��V�[����ύX
            if (Input.GetKeyDown(interactionKey) || Input.GetButtonDown("Bbutton"))
            {
                SceneManager.LoadScene(targetSceneName);
            }
        }
        else
        {
            // ����������Ă���ꍇ��UI���\��
            if (uiElement != null)
            {
                uiElement.SetActive(false);
            }
        }
    }
}
