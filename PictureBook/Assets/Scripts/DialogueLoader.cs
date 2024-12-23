using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DialogueLoader : MonoBehaviour
{
    public Dictionary<int, List<string>> npcDialogues = new Dictionary<int, List<string>>();
    public string csvFilePath = "Assets/Resources/NPC_Dialogue.csv";

    void Start()
    {
        LoadDialogueFromCSV();
    }

    void LoadDialogueFromCSV()
    {
        try
        {
            using (StreamReader reader = new StreamReader(csvFilePath))
            {
                bool isFirstLine = true;
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();

                    if (isFirstLine)
                    {
                        isFirstLine = false;
                        continue;
                    }

                    string[] values = line.Split(',');
                    int npcID = int.Parse(values[0]);
                    string dialogue = values[1];

                    if (!npcDialogues.ContainsKey(npcID))
                    {
                        npcDialogues[npcID] = new List<string>();
                    }

                    npcDialogues[npcID].Add(dialogue);
                }
            }
        }
        catch (IOException e)
        {
            Debug.LogError($"CSV読み込みエラー: {e.Message}");
        }
    }

    public List<string> GetDialoguesForNPC(int npcID)
    {
        if (npcDialogues.ContainsKey(npcID))
        {
            return npcDialogues[npcID];
        }
        else
        {
            return new List<string> { "会話が見つかりません。" };
        }
    }
}
