using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DialogueLoader : MonoBehaviour
{
    // NPC���Ƃ̉�b���e��ێ����鎫��
    public Dictionary<int, List<string>> npcDialogues = new Dictionary<int, List<string>>();

    // CSV�t�@�C���̃p�X�iResources�t�H���_�ɔz�u�j
    public string csvFilePath = "Assets/Resources/NPC_Dialogue.csv";

    void Start()
    {
        LoadDialogueFromCSV();
    }

    // CSV�t�@�C����ǂݍ��ރ��\�b�h
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

                    // �w�b�_�[�s���X�L�b�v
                    if (isFirstLine)
                    {
                        isFirstLine = false;
                        continue;
                    }

                    // �J���}�ŕ�������NPC_ID�Ɖ�b���e���擾
                    string[] values = line.Split(',');
                    int npcID = int.Parse(values[0]);
                    string dialogue = values[1];

                    // NPC��ID�ɑΉ����郊�X�g���܂����݂��Ȃ��ꍇ�A���X�g���쐬
                    if (!npcDialogues.ContainsKey(npcID))
                    {
                        npcDialogues[npcID] = new List<string>();
                    }

                    // NPC�̉�b���X�g�ɒǉ�
                    npcDialogues[npcID].Add(dialogue);
                }
            }
        }
        catch (IOException e)
        {
            Debug.LogError("CSV�t�@�C���̓ǂݍ��ݒ��ɃG���[���������܂���: " + e.Message);
        }
    }

    // �����NPC�̉�b���X�g���擾
    public List<string> GetDialoguesForNPC(int npcID)
    {
        if (npcDialogues.ContainsKey(npcID))
        {
            return npcDialogues[npcID];
        }
        else
        {
            return new List<string> { "��b��������܂���B" };
        }
    }
}
