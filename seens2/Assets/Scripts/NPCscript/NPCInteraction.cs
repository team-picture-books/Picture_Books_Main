using UnityEngine;
using TMPro;  // TextMeshPro���g�p���邽�߂̖��O��Ԃ�ǉ�
using System.Collections.Generic;

public class NPCInteraction : MonoBehaviour
{
    public GameObject player;  // �v���C���[�I�u�W�F�N�g
    public GameObject talkButton;  // �b���{�^���iUI�j
    public GameObject speechBubble;  // �����o��UI
    public TextMeshProUGUI speechText;  // �����o�����̃e�L�X�g��TextMeshPro�ɕύX
    public float interactionDistance = 2.0f;  // �v���C���[��NPC�̋�������
    public DialogueLoader dialogueLoader;  // CSV�����b��ǂݍ��ރX�N���v�g
    public int npcID;  // ����NPC�̎���ID
    public Transform npcHead;  // NPC�̓��̈ʒu�iTransform�Q�Ɓj

    private List<string> dialogues;  // ����NPC�̉�b���X�g
    private int currentDialogueIndex = 0;
    private Camera mainCamera;
    private PlayerController playerController;  // �v���C���[�R���g���[���[�̎Q��
    private bool isTalking = false;  // ��b�����ǂ����������t���O

    void Start()
    {
        mainCamera = Camera.main;  // ���C���J�������擾
        playerController = player.GetComponent<PlayerController>();  // PlayerController�̎Q�Ƃ��擾
        // NPC�̉�b���X�g���擾
        dialogues = dialogueLoader.GetDialoguesForNPC(npcID);
    }

    void Update()
    {
        // �v���C���[�Ƃ̋����𑪒�
        float distance = Vector3.Distance(player.transform.position, transform.position);

        // NPC�ɋ߂Â�����u�b���v�{�^����\��
        if (distance <= interactionDistance && !isTalking)
        {
            talkButton.SetActive(true);

            // B�{�^���������ꂽ���b���J�n
            if (Input.GetButtonDown("Bbutton")) // �R���g���[����B�{�^�����g�p
            {
                OnTalkButtonPressed();
            }
            if (Input.GetKeyDown(KeyCode.Z)) // �R���g���[����B�{�^�����g�p
            {
                OnTalkButtonPressed();
            }
        }
        else
        {
            talkButton.SetActive(false);
            if (!isTalking)
            {
                speechBubble.SetActive(false);  // ���ꂽ�琁���o������\���ɂ���
                playerController.canMove = true;  // ��b���I�������v���C���[�̑��������
            }
        }

        // �����o���̈ʒu��NPC�̓���ɐݒ�
        if (speechBubble.activeSelf)
        {
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(npcHead.position);  // NPC�̓��̈ʒu���X�N���[�����W�ɕϊ�
            speechBubble.transform.position = screenPosition;  // �����o����NPC�̓���ɔz�u
        }

        // ��b����B�{�^���������ꂽ�玟�̉�b�e�L�X�g��\��
        if (isTalking && Input.GetButtonDown("Bbutton")) // �R���g���[����B�{�^�����g�p
        {
            ShowNextDialogue();
        }
    }

    // �u�b���v�{�^���������ꂽ�Ƃ��̏���
    public void OnTalkButtonPressed()
    {
        // ��b���X�g���󂩂ǂ������`�F�b�N
        if (dialogues.Count == 0)
        {
            // �ēx��b�f�[�^���擾
            dialogues = dialogueLoader.GetDialoguesForNPC(npcID);
        }

        // ��b���X�g����łȂ��ꍇ�ɂ̂݉�b���J�n
        if (dialogues.Count > 0)
        {
            isTalking = true;
            speechBubble.SetActive(true);
            currentDialogueIndex = 0;
            speechText.text = dialogues[currentDialogueIndex];

            // �v���C���[�̑���𖳌���
            playerController.canMove = false;
        }
        else
        {
            // NPC�Ƃ̉�b���Ȃ��ꍇ�̏����i�K�v�ɉ����āj
            Debug.Log("����NPC�ɂ͉�b������܂���B");
        }
    }

    // ���̉�b�e�L�X�g��\�����郁�\�b�h
    private void ShowNextDialogue()
    {
        currentDialogueIndex++;
        if (currentDialogueIndex < dialogues.Count)
        {
            // ���̉�b��\��
            speechText.text = dialogues[currentDialogueIndex];
        }
        else
        {
            // ��b���I�������ꍇ
            EndDialogue();
        }
    }

    // ��b�I�����̏���
    private void EndDialogue()
    {
        isTalking = false;
        speechBubble.SetActive(false);
        playerController.canMove = true;  // �v���C���[�̑��������
    }
}
