using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Transform inventoryUIParent; // �C���x���g��UI�̐e�I�u�W�F�N�g
    public GameObject inventorySlotPrefab; // �C���x���g���X���b�g�̃v���n�u

    private List<GameObject> inventory = new List<GameObject>(); // �C���x���g��

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
        // �����̃X���b�g��S�폜
        foreach (Transform child in inventoryUIParent)
        {
            Destroy(child.gameObject);
        }

        // �C���x���g���̓��e�ɉ����ăX���b�g�𐶐�
        foreach (var item in inventory)
        {
            GameObject slot = Instantiate(inventorySlotPrefab, inventoryUIParent);
            slot.GetComponentInChildren<Text>().text = item.name; // �X���b�g�ɃA�C�e������\��
        }
    }
}