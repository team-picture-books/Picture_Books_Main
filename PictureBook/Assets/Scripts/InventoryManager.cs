using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Transform inventoryUIParent; // インベントリUIの親オブジェクト
    public GameObject inventorySlotPrefab; // インベントリスロットのプレハブ

    private List<GameObject> inventory = new List<GameObject>(); // インベントリ

    public void AddItem(GameObject item)
    {
        inventory.Add(item);
        UpdateInventoryUI();
    }

    public void RemoveItem(GameObject item)
    {
        if (inventory.Contains(item))
        {
            inventory.Remove(item);
            UpdateInventoryUI();
        }
    }

    public GameObject GetFirstItem()
    {
        if (inventory.Count > 0)
        {
            return inventory[0];
        }
        return null;
    }


    private void UpdateInventoryUI()
    {
        // 既存のスロットを全削除
        foreach (Transform child in inventoryUIParent)
        {
            Destroy(child.gameObject);
        }

        // インベントリの内容に応じてスロットを生成
        foreach (var item in inventory)
        {
            GameObject slot = Instantiate(inventorySlotPrefab, inventoryUIParent);
            slot.GetComponentInChildren<Text>().text = item.name; // スロットにアイテム名を表示
        }
    }
}