using UnityEngine;
using System.Collections.Generic; // List���g������

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    private CharacterController controller;
    public float gravity = 100f;

    public float pickupRadius = 20f;
    public GameObject intaractionUI;
    

    // �v���C���[�̈ړ��⎋�_�ړ��𐧌䂷��t���O
    public bool canMove = true;

    // �A�C�e���o�[�̕\����Ԃ��Ǘ�����t���O


    // �c�{�֘A
    public GameObject potPrefab; // �c�{�̃v���n�u
    // �A�C�e����ێ�����ʒu
    private List<GameObject> inventory = new List<GameObject>(); // �C���x���g��

    public Transform soldier;

    public GameObject potUI;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        // ������ԂŃA�C�e���o�[���\���ɐݒ�
        if(potUI != null)
        {
            potUI.SetActive(false);
        }
        
        
    }

    void Update()
    {
        // canMove��true�̂Ƃ��̂ݑ��������
        if (canMove)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // LB��RB�{�^���Ŏ��_����]
            float rotationInput = 0f;
            if (Input.GetButton("RBbutton"))
            {
                rotationInput = 1f; // RB�{�^����������Ă���Ƃ��͉E��]
            }
            else if (Input.GetButton("LBbutton"))
            {
                rotationInput = -1f; // LB�{�^����������Ă���Ƃ��͍���]
            }

            // �v���C���[����]������
            transform.Rotate(Vector3.up, rotationInput * rotationSpeed * Time.fixedDeltaTime);

            // �ړ������̌v�Z
            Vector3 moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;

            if (!controller.isGrounded)
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }

            controller.Move(moveDirection * moveSpeed * Time.deltaTime);
            
        }
        if (potPrefab != null)
        {
            // �c�{�E������
            HandlePotPickup();

            // �c�{�Đݒu����
            HandlePotPlacement();
        }
        
    }
    private void HandlePotPickup()
    {
        // �V�[�����̃c�{���擾
        GameObject[] pots = GameObject.FindGameObjectsWithTag("Pot");

        foreach (var pot in pots)
        {
            if (Vector3.Distance(transform.position, pot.transform.position) <= pickupRadius)
            {
                if(intaractionUI != null)
                {
                    intaractionUI.SetActive(true);
                    Debug.Log("�c�{�͈͓̔��ł�");
                }
                if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Bbutton"))
                {
                    // �c�{���E��
                    inventory.Add(pot);
                    pot.SetActive(false); // �c�{���\���ɂ���
                    potUI.SetActive(true);//��UI��\��
                    Debug.Log("�c�{���E���܂����I");
                    break;
                }
            }
            else
            {
                if (intaractionUI != null)
                {
                    intaractionUI.SetActive(false);
                }
            }
            
        }
    }
    private void HandlePotPlacement()
    {
        if (inventory.Count > 0 && Input.GetKeyDown(KeyCode.Space)|| inventory.Count > 0 && Input.GetButtonDown("Abutton"))
        {
            potUI.SetActive(false);
            // �z�u�ʒu���v�Z
            Vector3 placementPosition = transform.position + transform.forward * 2f;

            // Y�����Œ肷��i��: �n�ʂ̍�����0.5�ɌŒ�j
            placementPosition.y = 20f;

            // �A�C�e��������c�{���Đݒu
            GameObject pot = inventory[0];
            inventory.RemoveAt(0);
            pot.SetActive(true); // �c�{��\��
            pot.transform.position = placementPosition; // �v���C���[�̑O�ɐݒu
            Debug.Log("�c�{��ݒu���܂����I");

            // ���m�ɒʒm
            SoldierPatrol soldierScript = soldier.GetComponent<SoldierPatrol>();
            if (soldierScript != null)
            {
                soldierScript.OnPotPlaced(pot.transform.position);
            }
        }
    }
}

