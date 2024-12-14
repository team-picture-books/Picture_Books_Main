using System.Collections; // �����ǉ�
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ItemTransfer : MonoBehaviour
{
    [SerializeField] private GameObject[] itemObjects; // �A�C�e���I�u�W�F�N�g
    [SerializeField] private string[] sceneNames;      // �A�C�e�����Ƃ̃V�[����
    [SerializeField] private Image[] itemIcons;        // �A�C�e�����Ƃ�UI�A�C�R��
    private bool[] itemAcquiredFlags;                 // �A�C�e���擾�t���O
    private int selectedItemIndex = -1;               // �I�𒆂̃A�C�e���̃C���f�b�N�X
    [SerializeField] private float interactDistance = 3f; //�A�C�e�����킽��NPC�Ƃ̗L������
    private GameObject player; // �v���C���[�I�u�W�F�N�g
    private Coroutine blinkCoroutine; // �_�ŏ����p�̃R���[�`��

    void Start()
    {
        // �A�C�e���擾�t���O��������
        itemAcquiredFlags = new bool[itemObjects.Length];
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);

        // �A�C�e���I������ (��: 1�`4�L�[�ŃA�C�e����I��)
        for (int i = 0; i < itemObjects.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i)) // �L�[ 1, 2, 3, 4 �ɑΉ�
            {
                if (itemAcquiredFlags[i]) // �t���O���m�F
                {
                    SelectItem(i);
                }
                else
                {
                    Debug.Log($"�A�C�e��{i + 1}�͖��擾�ł��B");
                }
            }
        }

        // NPC�ɃA�C�e����n������
        if (distance <= interactDistance)
        {
            if (selectedItemIndex >= 0 && Input.GetKeyDown(KeyCode.E))
            {
                TransferItem();
            }
        }
    }

    void SelectItem(int itemIndex)
    {
        if (selectedItemIndex != itemIndex)
        {
            // �ȑO�̑I���A�C�R���̓_�ł��~
            if (blinkCoroutine != null)
            {
                StopCoroutine(blinkCoroutine);
                SetIconVisibility(selectedItemIndex, true); // �_�ł��~���\�����Œ�
            }

            selectedItemIndex = itemIndex;
            Debug.Log($"�A�C�e��{itemIndex + 1}��I�����܂����B");

            // �V�����I���A�C�R���̓_�ł��J�n
            blinkCoroutine = StartCoroutine(BlinkIcon(itemIcons[itemIndex]));
        }
    }

    IEnumerator BlinkIcon(Image icon)
    {
        while (true)
        {
            icon.enabled = !icon.enabled; // �\��/��\����؂�ւ�
            yield return new WaitForSeconds(0.5f); // �_�ő��x�𒲐�
        }
    }

    void SetIconVisibility(int itemIndex, bool visible)
    {
        if (itemIndex >= 0 && itemIndex < itemIcons.Length)
        {
            itemIcons[itemIndex].enabled = visible;
        }
    }

    public void MarkItemAsAcquired(int itemIndex)
    {
        if (itemIndex >= 0 && itemIndex < itemAcquiredFlags.Length)
        {
            itemAcquiredFlags[itemIndex] = true;
            Debug.Log($"�A�C�e��{itemIndex + 1}���擾�ς݂ɂȂ�܂����B");
        }
        else
        {
            Debug.LogError("�����ȃA�C�e���C���f�b�N�X�ł��B");
        }
    }

    void TransferItem()
    {
        if (selectedItemIndex >= 0 && selectedItemIndex < sceneNames.Length)
        {
            string targetScene = sceneNames[selectedItemIndex];
            Debug.Log($"�A�C�e��{selectedItemIndex + 1}��NPC�ɓn���܂����B�V�[�� {targetScene} �Ɉڍs���܂��B");
            

            // �V�[���J��
            SceneManager.LoadScene(targetScene);
        }
        else
        {
            Debug.Log("�����ȃA�C�e���I���܂��̓V�[�����ݒ肳��Ă��܂���B");
        }
    }
}


