using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    public GameObject doorObject; // �h�A�̃I�u�W�F�N�g
    private IDoor door; // ���ʃC���^�[�t�F�[�X�Ő���
    private bool playerNearby = false;

    void Start()
    {
        door = doorObject.GetComponent<IDoor>(); // �h�A�̃C���^�[�t�F�[�X���擾
        if (door == null)
        {
            Debug.LogError("DoorButton: IDoor �����������X�N���v�g���A�^�b�`����Ă��܂���B");
        }
    }

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            door?.ToggleDoor(); // �h�A���J��
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }
}
