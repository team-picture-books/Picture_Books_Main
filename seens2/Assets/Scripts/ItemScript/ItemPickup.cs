using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private GameObject interactButton; // �{�^��UI
    [SerializeField] private Image itemDisplayUI;       // �E����UI�i�A�C�e���\���p�j
    [SerializeField] private Sprite itemIcon;           // �擾�A�C�e���̃A�C�R��
    [SerializeField] private float interactDistance = 3f; // �A�C�e���Ƃ̗L������
    [SerializeField] public bool itemAcquired = false;  // �t���O�i�擾�ς݂��j
    [SerializeField] private int itemIndex;           // �A�C�e���C���f�b�N�X
    [SerializeField] private ItemTransfer itemTransfer; // ItemTransfer �X�N���v�g�̎Q��

    private GameObject player; // �v���C���[�I�u�W�F�N�g

    void Start()
    {
        // ������
        interactButton.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        itemDisplayUI.enabled = false;

        if (itemTransfer == null)
        {
            itemTransfer = FindObjectOfType<ItemTransfer>();
        }
    }

    void Update()
    {
        if (itemAcquired) return; // ���łɎ擾�ς݂Ȃ珈�����X�L�b�v

        // �v���C���[�ƃA�C�e���̋������v�Z
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance <= interactDistance)
        {
            interactButton.SetActive(true); // �{�^����\��

            // �{�^������������
            if (Input.GetKeyDown(KeyCode.E)) // ��: E�L�[�ŃA�C�e���擾
            {
                AcquireItem();
            }
        }
        else
        {
            interactButton.SetActive(false); // �{�^�����\��
        }
    }

    void AcquireItem()
    {
        itemTransfer.MarkItemAsAcquired(itemIndex);
        itemAcquired = true; // �t���O��true�ɐݒ�
        interactButton.SetActive(false); // �{�^�����\��
        itemDisplayUI.sprite = itemIcon; // �A�C�R����UI�ɐݒ�
        itemDisplayUI.enabled = true;    // UI��\��
        Destroy(gameObject);             // �A�C�e���i���g�j������
    }
}

