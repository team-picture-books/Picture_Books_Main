using System.Collections.Generic;
using UnityEngine;


public class DialogueLoader : MonoBehaviour
{
    // NPCごとの会話内容を保持する辞書
    public Dictionary<int, List<string>> npcDialogues = new Dictionary<int, List<string>>();

    // CSVファイルの名前（Resourcesフォルダ内に配置する）
    public string csvFileName = "NPC_Dialogue";  // ファイル名のみ、拡張子は不要

    void Start()
    {
        LoadDialogueFromCSV();
    }

    // CSVファイルを読み込むメソッド
    void LoadDialogueFromCSV()
    {
        try
        {
            // ResourcesフォルダからCSVファイルを読み込む
            TextAsset csvFile = Resources.Load<TextAsset>(csvFileName);
            if (csvFile == null)
            {
                Debug.LogError("CSVファイルが見つかりません: " + csvFileName);
                return;
            }

            // CSVファイルの内容を文字列として取得し、各行を分割
            string[] lines = csvFile.text.Split('\n');
            bool isFirstLine = true;

            foreach (string line in lines)
            {
                // ヘッダー行をスキップ
                if (isFirstLine)
                {
                    isFirstLine = false;
                    continue;
                }

                // カンマで分割してNPC_IDと会話内容を取得
                string[] values = line.Split(',');
                if (values.Length < 2) continue;  // データが不完全な場合をスキップ

                int npcID = int.Parse(values[0]);
                string dialogue = values[1].Trim();

                // NPCのIDに対応するリストがまだ存在しない場合、リストを作成
                if (!npcDialogues.ContainsKey(npcID))
                {
                    npcDialogues[npcID] = new List<string>();
                }

                // NPCの会話リストに追加
                npcDialogues[npcID].Add(dialogue);

                // デバッグ用にNPC IDと会話内容を出力
                Debug.Log("CSVデータ読み込み: NPC ID = " + npcID + ", 会話 = " + dialogue);
            }

            // 最終的なデータの状態を確認
            Debug.Log("全てのNPCの会話データ: " + npcDialogues.Count + " entries loaded.");
        }
        catch (System.Exception e)
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
        // NPC ID が存在しない場合は空のリストを返す
        return new List<string>();
    }
}
