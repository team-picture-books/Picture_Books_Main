using System.Collections.Generic;
using UnityEngine;

public class DialogueLoader : MonoBehaviour
{
    public enum SpeakerType { NPC, Player }

    [System.Serializable]
    public class Dialogue
    {
        public SpeakerType speaker;  // 発言者（NPCまたはPlayer）
        public string text;          // 会話テキスト
        public List<string> options; // 選択肢（選択肢がない場合はnullまたは空リスト）
        public List<int> nextDialogueIndices; // 各選択肢に対応する次の会話インデックス

        public Dialogue(SpeakerType speaker, string text, List<string> options = null, List<int> nextDialogueIndices = null)
        {
            this.speaker = speaker;
            this.text = text;
            this.options = options ?? new List<string>();  // 選択肢がなければ空リスト
            this.nextDialogueIndices = nextDialogueIndices ?? new List<int>();  // 次の会話がない場合は空リスト
        }
    }

    public Dictionary<int, List<Dialogue>> npcDialogues = new Dictionary<int, List<Dialogue>>();
    public string csvFileName = "NPC_Dialogue";  // CSVファイル名（Resourcesフォルダ内）

    void Start()
    {
        LoadDialogueFromCSV();
    }

    void LoadDialogueFromCSV()
    {
        try
        {
            TextAsset csvFile = Resources.Load<TextAsset>(csvFileName);
            if (csvFile == null)
            {
                Debug.LogError("CSVファイルが見つかりません: " + csvFileName);
                return;
            }

            string[] lines = csvFile.text.Split('\n');
            bool isFirstLine = true;

            foreach (string line in lines)
            {
                if (isFirstLine)
                {
                    isFirstLine = false;
                    continue;
                }

                string[] values = line.Split(',');
                if (values.Length < 3) continue;

                int npcID = int.Parse(values[0]);
                SpeakerType speaker = (SpeakerType)System.Enum.Parse(typeof(SpeakerType), values[1].Trim());
                string dialogueText = values[2].Trim();

                List<string> options = new List<string>();
                List<int> nextDialogueIndices = new List<int>();

                if (values.Length > 3)
                {
                    for (int i = 3; i < values.Length - 1; i += 2)
                    {
                        options.Add(values[i].Trim());
                        nextDialogueIndices.Add(int.Parse(values[i + 1].Trim()));
                    }
                }

                Dialogue dialogue = new Dialogue(speaker, dialogueText, options, nextDialogueIndices);

                if (!npcDialogues.ContainsKey(npcID))
                {
                    npcDialogues[npcID] = new List<Dialogue>();
                }

                npcDialogues[npcID].Add(dialogue);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("CSVファイルの読み込み中にエラーが発生しました: " + e.Message);
        }
    }

    public List<Dialogue> GetDialoguesForNPC(int npcID)
    {
        if (npcDialogues.ContainsKey(npcID))
        {
            return npcDialogues[npcID];
        }
        return new List<Dialogue>();
    }
}
