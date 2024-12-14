using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class StoryItemManager : MonoBehaviour
{
    [SerializeField] private GameObject[] itemObjects; // �A�C�e���I�u�W�F�N�g
    [SerializeField] private string correctScene = "CorrectScene"; // �S�Đ������̃V�[����
    [SerializeField] private string incorrectScene = "IncorrectScene"; // 1�ł��s�����̃V�[����
    [SerializeField] private bool[] correctAnswers; // �A�C�e�����������ǂ����̃t���O
    [SerializeField] private Image[] itemIcons; // �A�C�e�����Ƃ�UI�A�C�R��
    [SerializeField] private int requiredItemsToTransfer = 3; // �K�v�ȃA�C�e����

    private bool[] itemAcquiredFlags; // �A�C�e���擾�t���O
    private int selectedItemIndex = -1; // �I�𒆂̃A�C�e���̃C���f�b�N�X
    private int transferredItemsCount = 0; // �n�����A�C�e���̃J�E���g
    private int correctItemsCount = 0; // �����̃A�C�e���̃J�E���g
    [SerializeField] private float interactDistance = 3f; // NPC�Ƃ̃C���^���N�V��������
    private GameObject player; // �v���C���[�I�u�W�F�N�g
    private Coroutine blinkCoroutine; // �A�C�R���_�ŗp�R���[�`��

    void Start()
    {
        // �A�C�e���擾�t���O��������
        itemAcquiredFlags = new bool[itemObjects.Length];
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);

        // �A�C�e���I������ (1�`4�L�[�ŃA�C�e����I��)
        for (int i = 0; i < itemObjects.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i)) // �L�[ 1, 2, 3, 4 �ɑΉ�
            {
                if (itemAcquiredFlags[i]) // �擾�ς݃A�C�e���̂ݑI���\
                {
                    SelectItem(i);
                }
                else
                {
                    Debug.Log($"�A�C�e��{i + 1}�͖��擾�ł��B");
                }
            }
        }

        // NPC�Ƃ̃C���^���N�V��������
        if (distance <= interactDistance)
        {
            if (selectedItemIndex >= 0 && Input.GetKeyDown(KeyCode.E))
            {
                TransferItem();
            }
        }
    }

    // �A�C�e���I��
    void SelectItem(int itemIndex)
    {
        if (itemAcquiredFlags[itemIndex]) // �A�C�e�����擾����Ă��邩�m�F
        {
            if (selectedItemIndex != itemIndex)
            {
                // �ȑO�̑I���A�C�R���̓_�ł��~
                if (blinkCoroutine != null)
                {
                    StopCoroutine(blinkCoroutine);
                    SetIconVisibility(selectedItemIndex, true); // �_�ł��~
                }

                selectedItemIndex = itemIndex;
                Debug.Log($"�A�C�e��{itemIndex + 1}��I�����܂����B");

                // �V�����I���A�C�R���̓_�ł��J�n
                blinkCoroutine = StartCoroutine(BlinkIcon(itemIcons[itemIndex]));
            }
        }
        else
        {
            Debug.Log($"�A�C�e��{itemIndex + 1}�͖��擾�ł��B"); // �A�C�e�������擾�̏ꍇ
        }
    }

    // �A�C�R����_�ł�����R���[�`��
    IEnumerator BlinkIcon(Image icon)
    {
        while (true)
        {
            icon.enabled = !icon.enabled; // �A�C�R���̕\��/��\����؂�ւ�
            yield return new WaitForSeconds(0.5f); // �_�ő��x�𒲐�
        }
    }

    // �A�C�R���̕\����Ԃ�ݒ�
    void SetIconVisibility(int itemIndex, bool visible)
    {
        if (itemIndex >= 0 && itemIndex < itemIcons.Length)
        {
            itemIcons[itemIndex].enabled = visible;
        }
    }

    // �A�C�e�����擾�ς݂Ƃ��ă}�[�N
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

    // �A�C�e����NPC�ɓn������
    void TransferItem()
    {
        if (selectedItemIndex >= 0 && selectedItemIndex < itemObjects.Length)
        {
            transferredItemsCount++;
            bool isCorrect = correctAnswers[selectedItemIndex];

            Debug.Log($"�A�C�e��{selectedItemIndex + 1}��NPC�ɓn���܂����B");

            // �����̏ꍇ�J�E���g�𑝂₷
            if (isCorrect)
            {
                correctItemsCount++;
                Debug.Log($"�A�C�e��{selectedItemIndex + 1}�͐����ł��I");
            }
            else
            {
                Debug.Log($"�A�C�e��{selectedItemIndex + 1}�͕s�����ł��B");
            }

            // �n�����A�C�e�����\���ɂ���
            SetIconVisibility(selectedItemIndex, false);
            selectedItemIndex = -1;

            // �K�v�Ȑ��̃A�C�e����n�����琳�𗦂��`�F�b�N
            if (transferredItemsCount >= requiredItemsToTransfer)
            {
                CheckAndFinalize();
            }
        }
    }

    // �A�C�e���̐��𗦂��`�F�b�N���A�V�[���J�ڂ��s��
    void CheckAndFinalize()
    {
        // �S�Đ����Ȃ琳���V�[���ցA1�ł��s����������Εs�����V�[���֑J��
        if (correctItemsCount == transferredItemsCount)
        {
            Debug.Log("���ׂĐ����ł��I �����V�[���ɑJ�ڂ��܂��B");
            SceneManager.LoadScene(correctScene);
        }
        else
        {
            Debug.Log("�s�������܂܂�Ă��܂��B�s�����V�[���ɑJ�ڂ��܂��B");
            SceneManager.LoadScene(incorrectScene);
        }
    }
}

