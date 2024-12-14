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
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedIndex--;
            if (selectedIndex < 0) selectedIndex = menuItems.Length - 1;
            UpdateMenuDisplay();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedIndex++;
            if (selectedIndex >= menuItems.Length) selectedIndex = 0;
            UpdateMenuDisplay();
        }
        if (Input.GetButtonDown("Ybutton"))
        {
            selectedIndex--;
            if (selectedIndex < 0) selectedIndex = menuItems.Length - 1;
            UpdateMenuDisplay();
        }
        else if (Input.GetButtonDown("Abutton"))
        {
            selectedIndex++;
            if (selectedIndex >= menuItems.Length) selectedIndex = 0;
            UpdateMenuDisplay();
        }


        if (Input.GetButtonDown("Bbutton"))
        {
            // �X�y�[�X�L�[����������A�Ή�����V�[���ɑJ��
            SceneManager.LoadScene(sceneNames[selectedIndex]);
        }
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
