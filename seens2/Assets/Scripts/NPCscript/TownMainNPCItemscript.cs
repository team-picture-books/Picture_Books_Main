using UnityEngine;
using TMPro; // TextMeshPro�p�̖��O���
using UnityEngine.SceneManagement;

public class TownMainNPCItemscript : MonoBehaviour
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
    public Scene3Transferscript scene3Transferscript;
    public Transform npcHead;

    public string scenename1;
    public string scenename2;

    public TownMainNPCscript townMainNPCscript;

    public AudioSource seSource;
    public Posterscript posterscript;

    private bool isNearNPC = false; // �v���C���[��NPC�ɋ߂����ǂ���
    private int dialogueIndex = 0; // ���݂̉�b�C���f�b�N�X
    private bool isDialogueActive = false; // ��b�����ǂ���
    private bool isChoosing = false; // �I�������\������Ă��邩�ǂ���
    private string[] currentDialogue; // ���݂̉�b���X�g
    private bool transferflag =false;
    private PlayerController playerController;
    private Camera mainCamera;



    // ��b�f�[�^�i�I������̃e�L�X�g���܂ށj
    private string[] dialogueTextsAfterChoiceA = new string[]
    {
        "�����Ȃ́A�킽���̓I���S�[���̓y��͌������񂾂���",
        "���̃p�[�c����������Ȃ��āc",
        "�����Â��p�[�c��������낵���ˁI"
    };

    private string[] dialogueTextsAfterChoiceB = new string[]
    {
        "�ق�ƁI�킽���͓y�䌩�������猩�����p�[�c��g�ݍ��킹�悤�I",
        "���肪�Ƃ��A���Ȃ��̂������ŃI���S�[�������ǂ��Ă����I",
        "����Ƀp��������I",
        "�������p���������牽�����܂������Ƃ��������痈�ĂˁI"
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

            if (Input.GetKeyDown(interactKey) && !isDialogueActive && townMainNPCscript.cantalk == false || Input.GetButtonDown("Bbutton") && !isDialogueActive && townMainNPCscript.cantalk == false)
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
            else if (Input.GetKeyDown(choice2Key) && scene3Transferscript.item1flag&& scene3Transferscript.item2flag&& scene3Transferscript.item3flag || Input.GetButtonDown("Abutton") && scene3Transferscript.item1flag && scene3Transferscript.item2flag && scene3Transferscript.item3flag) // �I����2
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
        if(scene3Transferscript.textflag == false)
        {
            npcDialogueText.text = "�ǂ��H�p�[�c���������H\nY: ������\n";
        }
        if (scene3Transferscript.textflag == true)
        {
            npcDialogueText.text = "�ǂ��H�p�[�c���������H\nY: ������\nA:�͂�";
        }

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
            transferflag = true;
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
        if (transferflag)
        {
            if (posterscript.destroyFlag)
            {
                SceneManager.LoadScene(scenename2);
            }
            else
            {
                SceneManager.LoadScene(scenename1);

            }

        }
        
    }
}
