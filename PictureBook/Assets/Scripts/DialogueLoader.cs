using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DialogueLoader : MonoBehaviour
{
    // NPCごとの会話内容を保持する辞書
    public Dictionary<int, List<string>> npcDialogues = new Dictionary<int, List<string>>();

    // CSVファイルのパス（Resourcesフォルダに配置）
    public string csvFilePath = "Assets/Resources/NPC_Dialogue.csv";

    void Start()
    {
        LoadDialogueFromCSV();
    }

    // CSVファイルを読み込むメソッド
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

                    // ヘッダー行をスキップ
                    if (isFirstLine)
                    {
                        isFirstLine = false;
                        continue;
                    }

                    // カンマで分割してNPC_IDと会話内容を取得
                    string[] values = line.Split(',');
                    int npcID = int.Parse(values[0]);
                    string dialogue = values[1];

                    // NPCのIDに対応するリストがまだ存在しない場合、リストを作成
                    if (!npcDialogues.ContainsKey(npcID))
                    {
                        npcDialogues[npcID] = new List<string>();
                    }

                    // NPCの会話リストに追加
                    npcDialogues[npcID].Add(dialogue);
                }
            }
        }
        catch (IOException e)
        {
            Debug.LogError("CSVファイルの読み込み中にエラーが発生しました: " + e.Message);
        }
    }

    // 特定のNPCの会話リストを取得
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
