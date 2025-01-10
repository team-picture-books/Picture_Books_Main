using UnityEngine;

public class itemget : MonoBehaviour
{
    public GameObject player; // �v���C���[�̃I�u�W�F�N�g
    public GameObject interactionUI; // �߂��ɗ����Ƃ��ɕ\������UI
    public GameObject itemUI; // �A�C�e�����擾�����Ƃ��ɕ\������UI
    public float interactionDistance = 3.0f; // �A�C�e���ɋ߂Â��鋗��
    public KeyCode interactKey = KeyCode.E; // �C���^���N�g�p�̃L�[
    public ToggleObject ToggleObject;
    public int itemID;


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


            // �v���C���[���L�[����������A�C�e�����擾
            if (Input.GetKeyDown(interactKey) || Input.GetButtonDown("Bbutton"))
            {
                CollectItem();
            }
        }
        else
        {
            interactionUI.SetActive(false); // �C���^���N�gUI���\��

        }
    }

    void CollectItem()
    {
        Debug.Log("�A�C�e�����擾���܂����I");
        interactionUI.SetActive(false); // �C���^���N�gUI���\��
        itemUI.SetActive(true); // �A�C�e��UI��\��
        Destroy(gameObject); // �A�C�e���I�u�W�F�N�g������
        
        ToggleObject.toggleobeject();
        if(itemID == 1)
        {
            GameManager.Instance.DumbbellFlag = true;
            Debug.Log("�_���؂�t���O�I��");
        }
        else if (itemID == 2)
        {
            GameManager.Instance.RopeFlag = true;
            Debug.Log("�꒵�уt���O�I��");

        }

    }
}