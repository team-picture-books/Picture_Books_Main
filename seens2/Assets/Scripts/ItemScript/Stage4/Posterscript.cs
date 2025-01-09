using UnityEngine;
using TMPro; // TextMeshPro���g�p
using UnityEngine.UI; // UI��Image���g�p

public class Posterscript : MonoBehaviour
{
    public GameObject player; // �v���C���[�̃I�u�W�F�N�g
    public GameObject interactionUI; // �߂Â����Ƃ��ɕ\�������UI (�S�̂̃p�l��)
    public GameObject choiceUI; // �I������UI (�S�̂̃p�l��)
    public float interactionDistance = 3.0f; // �I�u�W�F�N�g�ɋ߂Â�����
    public TMP_Text choiceText; // �I������TextMeshPro�e�L�X�g
    public Image choiceImage; // �I�����̉摜

    public Sprite choiceImageSprite; // �\������摜 (�C���X�y�N�^�Ŏw��\)

    private bool isNearObject = false;
    public bool destroyFlag = false;
    public bool keepFlag = false;

    void Start()
    {
        interactionUI.SetActive(false); // ������ԂŔ�\��
        choiceUI.SetActive(false);     // ������ԂŔ�\��
    }

    void Update()
    {
        // �v���C���[�ƃI�u�W�F�N�g�̋������v�Z
        float distance = Vector3.Distance(player.transform.position, transform.position);

        // �v���C���[���߂Â�����UI��\��
        if (distance <= interactionDistance)
        {
            interactionUI.SetActive(true);
            isNearObject = true;
        }
        else
        {
            interactionUI.SetActive(false);
            choiceUI.SetActive(false);
            isNearObject = false;
        }

        // �߂��ŃL�[����������I����UI��\��
        if (isNearObject && Input.GetKeyDown(KeyCode.E)|| isNearObject && Input.GetButtonDown("Bbutton")) // "E"�L�[�őI������\��
        {
            choiceUI.SetActive(true);
            choiceText.text = "���L���L�̐l�Ԃ����̃|�X�^�[��\nY. ���ڂ���\nA. ���ڂ��Ȃ�";
            choiceImage.sprite = choiceImageSprite; // �摜��ݒ�
        }

        // �I�����̃L�[����
        if (choiceUI.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)|| Input.GetButtonDown("Ybutton")) // "1"�L�[
            {
                destroyFlag = true;
                choiceUI.SetActive(false);
                Debug.Log("�o�����I�����܂����B");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2)||Input.GetButtonDown("Abutton")) // "2"�L�[
            {
                keepFlag = true;
                choiceUI.SetActive(false);
                Debug.Log("�o���Ȃ���I�����܂����B");
            }
        }
    }

    public bool GetDestroyFlag()
    {
        return destroyFlag;
    }

    public bool GetKeepFlag()
    {
        return keepFlag;
    }
}
