using UnityEngine;
using TMPro; // TextMeshPro�p�̖��O���
using UnityEngine.SceneManagement;

public class MajoNPC2 : MonoBehaviour
{
    public GameObject player; // �v���C���[�̃I�u�W�F�N�g
    public GameObject interactionUI; // �u��b�J�n�v�Ȃǂ�UI
    public GameObject dialogueUI; // ��bUI�S��
    public TextMeshProUGUI npcDialogueText; // NPC�̃Z���t�p��TextMeshPro
    public float interactionDistance = 3.0f; // NPC�Ƃ̉�b�\����
    public KeyCode interactKey = KeyCode.Z; // ��b�J�n�̃L�[
    public KeyCode nextTextKey = KeyCode.Z; // ���̃e�L�X�g��\������L�[
    public KeyCode choice1Key = KeyCode.Alpha1; // �I����1
    public KeyCode choice2Key = KeyCode.Alpha2; // �I����2
    public Transform npcHead;

    public MajoNPC majoNPC;
    public AudioSource seSource;



    private bool isNearNPC = false; // �v���C���[��NPC�ɋ߂����ǂ���
    private int dialogueIndex = 0; // ���݂̉�b�C���f�b�N�X
    private bool isDialogueActive = false; // ��b�����ǂ���
    private bool isChoosing = false; // �I�������\������Ă��邩�ǂ���
    private string[] currentDialogue; // ���݂̉�b���X�g
    private PlayerController playerController;
    private Camera mainCamera;


    // ��b�f�[�^�i�I������̃e�L�X�g���܂ށj
    private string[] dialogueTextsAfterChoiceA = new string[]
    {
        "���ꂾ�Ǝv��������傤���킽���ɂ킽���Ă�"
    };

    private string[] dialogueTextsAfterChoiceB = new string[]
    {
        "��̂�����傤�̏ꏊ�H",
        "���ꂼ�ꂩ�����Ƃт�̒��ɂ����",
        "�Ԃ��Ƃт�ɊC�����A���Ƃт�Ƀq�g�f�����邩��Ƃ��Ă���",
        "��Ɏg���̂̓��R���R�ȊC�����ƃL���L���ȃq�g�f����"
    };

    void Start()
    {
        mainCamera = Camera.main;
        interactionUI.SetActive(false);
        dialogueUI.SetActive(false);
        playerController = player.GetComponent<PlayerController>();

    }

    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance <= interactionDistance)
        {
            interactionUI.SetActive(true);
            isNearNPC = true;

            if (Input.GetKeyDown(interactKey) && !isDialogueActive && majoNPC.cantalk == false || Input.GetButtonDown("Bbutton") && !isDialogueActive && majoNPC.cantalk == false)
            {
                if (seSource != null)
                {
                    seSource.Play();

                }
                playerController.canMove = false;
                StartDialogue();
            }
        }
        else
        {
            interactionUI.SetActive(false);
            isNearNPC = false;
        }
        if (dialogueUI.activeSelf)
        {
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(npcHead.position);
            dialogueUI.transform.position = screenPosition;
        }

        // ��b���őI����������ꍇ
        if (isChoosing)
        {
            if (Input.GetKeyDown(choice1Key) || Input.GetButtonDown("Ybutton")) // �I����1
            {
                if (seSource != null)
                {
                    seSource.Play();

                }
                OnChoiceSelected("A");
            }
            else if (Input.GetKeyDown(choice2Key)|| Input.GetButtonDown("Abutton")) // �I����2
            {
                if (seSource != null)
                {
                    seSource.Play();

                }
                OnChoiceSelected("B");
            }
        }

        // ��b���őI�������Ȃ��ꍇ
        if (isDialogueActive && !isChoosing && Input.GetKeyDown(nextTextKey) || isDialogueActive && !isChoosing && Input.GetButtonDown("Bbutton"))
        {
            if (seSource != null)
            {
                seSource.Play();

            }
            ShowNextDialogue();
        }
    }

    void StartDialogue()
    {
        interactionUI.SetActive(false);
        dialogueUI.SetActive(true);
        dialogueIndex = 0;
       
        
        npcDialogueText.text = "�ǂ������́H\nY:������傤�����������H\nA:����Ƃ�����H";
        

        isChoosing = true; // �I������L����
        isDialogueActive = true;
    }

    void OnChoiceSelected(string choice)
    {
        isChoosing = false; // �I�����𖳌���
        dialogueIndex = 0; // �I������̃e�L�X�g���ŏ�����\��

        if (choice == "A")
        {
            currentDialogue = dialogueTextsAfterChoiceA;
        }
        else if (choice == "B")
        {
            currentDialogue = dialogueTextsAfterChoiceB;
        }

        npcDialogueText.text = currentDialogue[dialogueIndex];
    }

    void ShowNextDialogue()
    {
        dialogueIndex++;

        if (dialogueIndex < currentDialogue.Length)
        {
            npcDialogueText.text = currentDialogue[dialogueIndex];
        }
        else
        {
            EndDialogue();
            playerController.canMove = true;
        }
    }

    void EndDialogue()
    {
        dialogueUI.SetActive(false);
        isDialogueActive = false;
        

    }
}