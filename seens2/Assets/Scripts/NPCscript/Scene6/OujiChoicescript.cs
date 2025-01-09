using UnityEngine;
using TMPro; // TextMeshPro���g�p
using UnityEngine.UI; // UI��Image���g�p
using UnityEngine.SceneManagement; // �V�[���J�ڗp

public class OujiChoicescript : MonoBehaviour
{
    public GameObject player; // �v���C���[�̃I�u�W�F�N�g
    public GameObject interactionUI; // �߂Â����Ƃ��ɕ\�������UI (�S�̂̃p�l��)
    public GameObject choiceUI; // �I������UI (�S�̂̃p�l��)
    public float interactionDistance = 3.0f; // �I�u�W�F�N�g�ɋ߂Â�����
    public TMP_Text choiceText; // �I������TextMeshPro�e�L�X�g

    // �C���X�y�N�^�Őݒ�\�ȃV�[����
    public string scene1; // �V�[��1�̖��O
    public string scene2; // �V�[��2�̖��O
    public string scene3; // �V�[��3�̖��O
    public string scene4; // �V�[��4�̖��O

    public AudioSource seSource;
    private PlayerController playerController;
    private bool isNearObject = false;

    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
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
            if (seSource != null)
            {
                seSource.Play();

            }
            playerController.canMove = false;
            choiceUI.SetActive(true);
            choiceText.text = "�킽�����Ƃ�ׂ��s����...\nY. �Ȃ���Ȃ�\nRB. ��킭������\nA. ���������悭������\nX. �Ȃ���";
        }

        // �I�����̃L�[����
        if (choiceUI.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetButtonDown("Ybutton")) // "1"�L�[
            {
                if (seSource != null)
                {
                    seSource.Play();

                }
                SceneManager.LoadScene(scene1); // �V�[��1�Ɉڍs
                Debug.Log("���ڂ����I�����܂����B");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetButtonDown("RBbutton")) // "2"�L�[
            {
                if (seSource != null)
                {
                    seSource.Play();

                }
                SceneManager.LoadScene(scene2); // �V�[��2�Ɉڍs
                Debug.Log("���ڂ��Ȃ���I�����܂����B");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3)|| Input.GetButtonDown("Abutton")) // "3"�L�[
            {
                if (seSource != null)
                {
                    seSource.Play();

                }
                SceneManager.LoadScene(scene3); // �V�[��3�Ɉڍs
                Debug.Log("�����ƌ����I�����܂����B");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4)|| Input.GetButtonDown("Xbutton")) // "4"�L�[
            {
                if (seSource != null)
                {
                    seSource.Play();

                }
                SceneManager.LoadScene(scene4); // �V�[��4�Ɉڍs
                Debug.Log("�ʂ�߂����I�����܂����B");
            }
        }
    }
}
