using UnityEngine;
using UnityEngine.UI;

public class StoryItemPickup : MonoBehaviour
{
    [SerializeField] private GameObject interactButton; // �{�^��UI
    [SerializeField] private Image itemDisplayUI;       // �E����UI�i�A�C�e���\���p�j
    [SerializeField] private Sprite itemIcon;          // �擾�A�C�e���̃A�C�R��
    [SerializeField] private float interactDistance = 3f; // �A�C�e���Ƃ̗L������
    [SerializeField] private int itemIndex;            // �A�C�e���C���f�b�N�X
    [SerializeField] private StoryItemManager storyItemManager; // StoryItemManager �X�N���v�g�̎Q��

    private GameObject player;
    private bool itemAcquired = false; // �A�C�e�����擾�ς݂�

    void Start()
    {
        // UI������
        interactButton.SetActive(false);
        itemDisplayUI.enabled = false;

        // �v���C���[�I�u�W�F�N�g���擾
        player = GameObject.FindGameObjectWithTag("Player");

        // StoryItemManager �̎Q�Ƃ��擾�i�ݒ肳��Ă��Ȃ��ꍇ�j
        if (storyItemManager == null)
        {
            storyItemManager = FindObjectOfType<StoryItemManager>();
        }

        if (storyItemManager == null)
        {
            Debug.LogError("StoryItemManager ��������܂���B�V�[���ɒǉ�����Ă��邱�Ƃ��m�F���Ă��������B");
        }
    }

    void Update()
    {
        // ���Ɏ擾�ς݂̏ꍇ�͏������X�L�b�v
        if (itemAcquired) return;

        // �v���C���[�Ƃ̋����𑪒�
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        // �L���������ŃC���^���N�g�{�^����\��
        if (distanceToPlayer <= interactDistance)
        {
            interactButton.SetActive(true);

            // �v���C���[�� E �L�[���������ꍇ�ɃA�C�e���擾���������s
            if (Input.GetKeyDown(KeyCode.E))
            {
                AcquireItem();
            }
        }
        else
        {
            interactButton.SetActive(false);
        }
    }

    void AcquireItem()
    {
        // �A�C�e���擾�t���O��ݒ�
        itemAcquired = true;

        // UI�X�V
        interactButton.SetActive(false);
        itemDisplayUI.sprite = itemIcon;
        itemDisplayUI.enabled = true;

        // StoryItemManager �ɃA�C�e���擾��ʒm
        if (storyItemManager != null)
        {
            storyItemManager.MarkItemAsAcquired(itemIndex);
        }

        // �R���\�[�����O
        Debug.Log($"�A�C�e�� {itemIndex + 1} ���擾���܂����B");

        // �I�u�W�F�N�g���폜
        Destroy(gameObject);
    }
}


