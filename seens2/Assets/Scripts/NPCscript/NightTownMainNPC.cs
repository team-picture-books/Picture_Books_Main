using UnityEngine;
using TMPro; // TextMeshPro用の名前空間
using UnityEngine.SceneManagement;

public class NightTownMainNPC : MonoBehaviour
{
    public GameObject player; // プレイヤーのオブジェクト
    public GameObject interactionUI; // 「会話開始」などのUI
    public GameObject dialogueUI; // 会話UI全体
    public TextMeshProUGUI npcDialogueText; // NPCのセリフ用のTextMeshPro
    public float interactionDistance = 3.0f; // NPCとの会話可能距離
    public KeyCode interactKey = KeyCode.E; // 会話開始のキー
    public KeyCode nextTextKey = KeyCode.Space; // 次のテキストを表示するキー
    public KeyCode choice1Key = KeyCode.Alpha1; // 選択肢1
    public KeyCode choice2Key = KeyCode.Alpha2; // 選択肢2

    public Transform npcHead;

    public string scenename1;
    public string scenename2;
    public string scenename3;

    public Posterscript posterscript;

    public AudioSource seSource;

    public NightTownMainNPC2 npc;


    private bool isNearNPC = false; // プレイヤーがNPCに近いかどうか
    private int dialogueIndex = 0; // 現在の会話インデックス
    private bool isDialogueActive = false; // 会話中かどうか
    private bool isChoosing = false; // 選択肢が表示されているかどうか
    private string[] currentDialogue; // 現在の会話リスト
    
    private PlayerController playerController;
    private Camera mainCamera;


    private bool transferflag1 = false;
    private bool transferflag2 = false;


    // 会話データ（選択肢後のテキストを含む）
    private string[] dialogueTextsAfterChoiceA = new string[]
    {
        "いたっ！"
    };

    private string[] dialogueTextsAfterChoiceB = new string[]
    {
        "イタッ！"
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

        // 会話中で選択肢がある場合
        if (isChoosing)
        {
            if (Input.GetKeyDown(choice1Key) || Input.GetButtonDown("Ybutton")) // 選択肢1
            {
                if (seSource != null)
                {
                    seSource.Play();

                }

                OnChoiceSelected("A");
                transferflag1 = true;
            }
            else if (Input.GetKeyDown(choice2Key) || Input.GetButtonDown("Abutton") ) // 選択肢2
            {
                if (seSource != null)
                {
                    seSource.Play();

                }

                OnChoiceSelected("B");
                transferflag2 = true;
            }
        }

        // 会話中で選択肢がない場合
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

        npcDialogueText.text = "そ、そんな！じゃあ、いそいでわたしをたたいて！\nY:よわくたたく\nA:つよくたたく";

        isChoosing = true; // 選択肢を有効化
        isDialogueActive = true;
    }

    void OnChoiceSelected(string choice)
    {
        isChoosing = false; // 選択肢を無効化
        dialogueIndex = 0; // 選択肢後のテキストを最初から表示

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