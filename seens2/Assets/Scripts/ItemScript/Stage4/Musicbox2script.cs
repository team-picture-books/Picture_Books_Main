using UnityEngine;

public class Musicbox2script : MonoBehaviour
{
    public GameObject player; // �v���C���[�̃I�u�W�F�N�g
    public GameObject interactionUI; // �߂��ɗ����Ƃ��ɕ\������UI
    public GameObject itemUI; // �A�C�e�����擾�����Ƃ��ɕ\������UI
    public float interactionDistance = 3.0f; // �A�C�e���ɋ߂Â��鋗��
    public KeyCode interactKey = KeyCode.E; // �C���^���N�g�p�̃L�[
    public Scene3Transferscript scene3Transferscript;
    public ToggleObject ToggleObject;

    private bool isNearItem = false; // �v���C���[���A�C�e���ɋ߂����ǂ���

    void Start()
    {
        interactionUI.SetActive(false); // ������Ԃ�UI���\��
        itemUI.SetActive(false); // ������ԂŃA�C�e��UI���\��
    }

    void Update()
    {
        // �v���C���[�ƃA�C�e���̋������v�Z
        float distance = Vector3.Distance(player.transform.position, transform.position);

        // �v���C���[���A�C�e���̋߂��ɂ���ꍇ
        if (distance <= interactionDistance)
        {
            interactionUI.SetActive(true); // �C���^���N�gUI��\��
            isNearItem = true;

            // �v���C���[���L�[����������A�C�e�����擾
            if (Input.GetKeyDown(interactKey) || Input.GetButtonDown("Bbutton"))
            {
                CollectItem();
            }
        }
        else
        {
            interactionUI.SetActive(false); // �C���^���N�gUI���\��
            isNearItem = false;
        }
    }

    void CollectItem()
    {
        Debug.Log("�A�C�e�����擾���܂����I");
        interactionUI.SetActive(false); // �C���^���N�gUI���\��
        itemUI.SetActive(true); // �A�C�e��UI��\��
        Destroy(gameObject); // �A�C�e���I�u�W�F�N�g������
        scene3Transferscript.item2flag = true;
        if (scene3Transferscript.item2flag)
        {
            Debug.Log("item2falag true");
        }
        ToggleObject.toggleobeject();

    }
}