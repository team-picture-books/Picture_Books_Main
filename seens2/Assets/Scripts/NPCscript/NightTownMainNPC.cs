using UnityEngine;
using TMPro; // TextMeshPro�p�̖��O���
using UnityEngine.SceneManagement;

public class NightTownMainNPC : MonoBehaviour
{
    public GameObject player; // �v���C���[�̃I�u�W�F�N�g
    public GameObject interactionUI; // �u��b�J�n�v�Ȃǂ�UI
    public GameObject dialogueUI; // ��bUI�S��
    public TextMeshProUGUI npcDialogueText; // NPC�̃Z���t�p��TextMeshPro
    public float interactionDistance = 3.0f; // NPC�Ƃ̉�b�\����
    public KeyCode interactKey = KeyCode.E; // ��b�J�n�̃L�[
    public KeyCode nextTextKey = KeyCode.Space; // ���̃e�L�X�g��\������L�[
    public KeyCode choice1Key = KeyCode.Alpha1; // �I����1
    public KeyCode choice2Key = KeyCode.Alpha2; // �I����2

    public Transform npcHead;

    public string scenename1;
    public string scenename2;
    public string scenename3;

    public Posterscript posterscript;

    public AudioSource seSource;

    public NightTownMainNPC2 npc;


    private bool isNearNPC = false; // �v���C���[��NPC�ɋ߂����ǂ���
    private int dialogueIndex = 0; // ���݂̉�b�C���f�b�N�X
    private bool isDialogueActive = false; // ��b�����ǂ���
    private bool isChoosing = false; // �I�������\������Ă��邩�ǂ���
    private string[] currentDialogue; // ���݂̉�b���X�g
    
    private PlayerController playerController;
    private Camera mainCamera;


    private bool transferflag1 = false;
    private bool transferflag2 = false;


    // ��b�f�[�^�i�I������̃e�L�X�g���܂ށj
    private string[] dialogueTextsAfterChoiceA = new string[]
    {
        "�������I"
    };

    private string[] dialogueTextsAfterChoiceB = new string[]
    {
        "�C�^�b�I"
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

            //if (Input.GetKeyDown(interactKey) && !isDialogueActive && !npc.Cantalk|| Input.GetButtonDown("Bbutton") && !isDialogueActive && !npc.Cantalk)
            //{
            //    if (seSource != null)
            //    {
            //        seSource.Play();

            //    }
            //    playerController.canMove = false;
            //    StartDialogue();
            //}
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
                transferflag1 = true;
            }
            else if (Input.GetKeyDown(choice2Key) || Input.GetButtonDown("Abutton") ) // �I����2
            {
                if (seSource != null)
                {
                    seSource.Play();

                }

                OnChoiceSelected("B");
                transferflag2 = true;
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

    public void StartDialogue()
    {
        interactionUI.SetActive(false);
        dialogueUI.SetActive(true);
        dialogueIndex = 0;

        npcDialogueText.text = "���A����ȁI���Ⴀ�A�������ł킽�����������āI\nY:��킭������\nA:�悭������";

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
        if (posterscript.destroyFlag)
        {
            SceneManager.LoadScene(scenename1);
        }


        if (transferflag1 && !posterscript.destroyFlag)
        {
            SceneManager.LoadScene(scenename2);

        }

        if (transferflag2 && !posterscript.destroyFlag)
        {
            SceneManager.LoadScene(scenename3);

        }

    }
}