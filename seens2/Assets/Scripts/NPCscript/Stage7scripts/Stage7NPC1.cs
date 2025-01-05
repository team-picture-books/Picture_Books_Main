using UnityEngine;
using UnityEngine.UI;


public class Stage7NPC1 : MonoBehaviour
{

    public GameObject player;          // �v���C���[�I�u�W�F�N�g
    public GameObject uiPrompt;       // UI�v�����v�g (�߂Â����Ƃ��ɕ\��)
    public Image canvasImage;         // Canvas���Image
    public float interactionDistance = 3f; // �C���^���N�V��������
    public KeyCode interactKey = KeyCode.E; // �C���^���N�V�����L�[

  

    private bool isPlayerNearby = false;

    void Start()
    {
        if (uiPrompt != null)
            uiPrompt.SetActive(false); // UI�v�����v�g���\��
        if (canvasImage != null)
            canvasImage.gameObject.SetActive(false); // Canvas��Image���\��
    }

    void Update()
    {
        // �v���C���[�Ƃ̋����𑪒�
        float distance = Vector3.Distance(player.transform.position, transform.position);
        isPlayerNearby = distance <= interactionDistance;

        // �v���C���[���߂��ɂ���ꍇ
        if (isPlayerNearby)
        {
            if (uiPrompt != null)
                uiPrompt.SetActive(true); // UI�v�����v�g��\��

            // �w��L�[�������ꂽ��Canvas��Image��\��
            if (Input.GetKeyDown(interactKey))
            {
                if (canvasImage != null)
                    canvasImage.gameObject.SetActive(true);
                GameManager.Instance.DumbbellFlag = true;

            }
        }
        else
        {
            if (uiPrompt != null)
                uiPrompt.SetActive(false); // UI�v�����v�g���\��
        }
    }
}
