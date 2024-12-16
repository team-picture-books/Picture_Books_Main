using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TextReveal : MonoBehaviour
{
    [Header("Text Settings")]
    [Tooltip("TextMeshPro component to display the text.")]
    public TMP_Text textMeshPro;

    [Tooltip("The lines of text to display.")]
    [TextArea]
    public string[] lines;

    [Tooltip("Time it takes for a line to fade in completely.")]
    public float fadeInDuration = 1f;

    [Header("Scene Transition")]
    [Tooltip("Name of the scene to load after all lines are revealed.")]
    public string nextSceneName;

    private int currentLineIndex = 0;
    private bool isFading = false;

    void Start()
    {
        if (textMeshPro == null)
        {
            textMeshPro = GetComponent<TMP_Text>();
        }

        if (textMeshPro != null && lines.Length > 0)
        {
            textMeshPro.text = ""; // Clear initial text
            StartCoroutine(RevealText());
        }
        else
        {
            Debug.LogWarning("TextMeshPro component or lines not set.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isFading)
        {
            if (currentLineIndex < lines.Length)
            {
                StartCoroutine(RevealText());
            }
            else if (!string.IsNullOrEmpty(nextSceneName))
            {
                SceneManager.LoadScene(nextSceneName);
            }
        }
        if (Input.GetButtonDown("Bbutton"))
        {
            if (currentLineIndex < lines.Length)
            {
                StartCoroutine(RevealText());
            }
            else if (!string.IsNullOrEmpty(nextSceneName))
            {
                SceneManager.LoadScene(nextSceneName);
            }
        }
    }

    private IEnumerator RevealText()
    {
        isFading = true;

        string line = lines[currentLineIndex];
        textMeshPro.text = line;
        textMeshPro.alpha = 0f; // Start fully transparent

        float elapsedTime = 0f;
        while (elapsedTime < fadeInDuration)
        {
            textMeshPro.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeInDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        textMeshPro.alpha = 1f; // Ensure fully visible at the end

        currentLineIndex++;
        isFading = false;
    }
}

