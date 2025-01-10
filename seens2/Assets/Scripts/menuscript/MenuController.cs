using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    public TextMeshProUGUI[] menuItems;  // ���j���[���� (5��TextMeshProUGUI)
    public string[] sceneNames;          // �e���j���[���ڂɑΉ�����V�[����
    private int selectedIndex = 0;       // ���ݑI������Ă���C���f�b�N�X
    private Coroutine blinkCoroutine;    // �_�ŗp�R���[�`��
    private bool isAxisInUse = false;    // �A�N�V�X�A�����͖h�~�p�t���O

    private void Start()
    {
        UpdateMenuDisplay();
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        float verticalInput = Input.GetAxis("Vertical");

        if (!isAxisInUse)
        {
            if (verticalInput > 0.5f) // ������̓���
            {
                selectedIndex--;
                if (selectedIndex < 0) selectedIndex = menuItems.Length - 1;
                UpdateMenuDisplay();
                StartCoroutine(ResetAxis());
            }
            else if (verticalInput < -0.5f) // �������̓���
            {
                selectedIndex++;
                if (selectedIndex >= menuItems.Length) selectedIndex = 0;
                UpdateMenuDisplay();
                StartCoroutine(ResetAxis());
            }
        }

        if (Input.GetButtonDown("Bbutton"))
        {
            // �{�^�����͂őI�����ꂽ�V�[���ɑJ��
            SceneManager.LoadScene(sceneNames[selectedIndex]);
        }
    }

    private IEnumerator ResetAxis()
    {
        isAxisInUse = true; // �A�N�V�X���̓t���O�𗧂Ă�
        yield return new WaitForSeconds(0.2f); // �K�v�ɉ����Ē���
        isAxisInUse = false; // �t���O�����Z�b�g
    }

    private void UpdateMenuDisplay()
    {
        // �S�Ẵ��j���[���ڂ̓_�ł��~
        for (int i = 0; i < menuItems.Length; i++)
        {
            StopBlinking(menuItems[i]);
        }

        // �I�����ꂽ���j���[���ڂ̂ݓ_�ŊJ�n
        blinkCoroutine = StartCoroutine(BlinkText(menuItems[selectedIndex]));
    }

    private IEnumerator BlinkText(TextMeshProUGUI text)
    {
        while (true)
        {
            text.alpha = 1f;  // ���S�ɕ\��
            yield return new WaitForSeconds(0.5f);
            text.alpha = 0.5f;  // ������
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void StopBlinking(TextMeshProUGUI text)
    {
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);  // �_�ł��~
            blinkCoroutine = null;
        }
        text.alpha = 1f;  // �������S�ɖ߂�
    }
}

