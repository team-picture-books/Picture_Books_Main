using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jump : MonoBehaviour
{
    public float jumpSpeed = 5f;          // �W�����v�̑��x

    private CharacterController controller; // CharacterController �R���|�[�l���g�̎Q��

    private Vector3 velocity;             // �v���C���[�̐��������̑��x
    public float gravity = 100f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!controller.isGrounded)
        {
            velocity.y = -15f;  // ���n���Ă���Ƃ��́A�y���n�ʂɗ}����

            if (Input.GetButtonDown("Ybutton"))  // �f�t�H���g�ł́uJump�v�̓X�y�[�X�L�[�ɐݒ肳��Ă��܂�
            {
                Debug.Log("�W�����v");
                velocity.y = jumpSpeed;  // �W�����v
            }
        }
        else
        {
            // �󒆂ɂ���Ƃ��ɏd�͂�K�p
            velocity.y -= gravity * Time.deltaTime;
        }
    }
}
