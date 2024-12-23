using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class FuriganaDisplay : MonoBehaviour
{
    public Text uiText;

    public void SetTextWithFurigana(string text)
    {
        string parsedText = ParseFuriganaTags(text);
        uiText.text = parsedText;
    }

    private string ParseFuriganaTags(string text)
    {
        string pattern = @"<ruby=(.+?)>(.+?)</ruby>";
        return Regex.Replace(text, pattern, "<size=70%><color=grey>$1</color></size>$2");
    }
}
