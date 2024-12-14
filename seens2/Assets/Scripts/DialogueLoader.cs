using System.Collections.Generic;
using UnityEngine;

public class DialogueLoader : MonoBehaviour
{
    // 発言者を区別するための列挙型
    public enum SpeakerType { NPC, Player }

    // 会話内容を保持するクラス
    [System.Serializable]
    public class Dialogue
    {
        public SpeakerType speaker;  // 発言者（NPCまたはPlayer）
        public string text;          // 会話テキスト

        public Dialogue(SpeakerType speaker, string text)
        {
            this.speaker = speaker;
            this.text = text;
        }
    }

    // NPCごとの会話内容を保持する辞書
    public Dictionary<int, List<Dialogue>> npcDialogues = new Dictionary<int, List<Dialogue>>();

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

                // カンマで分割してNPC_ID、発言者、会話内容を取得
                string[] values = line.Split(',');
                if (values.Length < 3) continue;  // データが不完全な場合をスキップ

                int npcID = int.Parse(values[0]);
                SpeakerType speaker = (SpeakerType)System.Enum.Parse(typeof(SpeakerType), values[1].Trim());
                string dialogueText = values[2].Trim();

                // NPCのIDに対応するリストがまだ存在しない場合、リストを作成
                if (!npcDialogues.ContainsKey(npcID))
                {
                    npcDialogues[npcID] = new List<Dialogue>();
                }

                // 会話データを作成してリストに追加
                Dialogue dialogue = new Dialogue(speaker, dialogueText);
                npcDialogues[npcID].Add(dialogue);

                // デバッグ用に読み込み内容を出力
                Debug.Log($"CSVデータ読み込み: NPC ID = {npcID}, 発言者 = {speaker}, 会話 = {dialogueText}");
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
    public List<Dialogue> GetDialoguesForNPC(int npcID)
    {
        if (npcDialogues.ContainsKey(npcID))
        {
            return npcDialogues[npcID];
        }
        // NPC ID が存在しない場合は空のリストを返す
        return new List<Dialogue>();
    }
}

