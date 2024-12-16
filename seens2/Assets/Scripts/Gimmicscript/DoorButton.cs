using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    public GameObject doorObject; // ドアのオブジェクト
    private IDoor door; // 共通インターフェースで制御
    private bool playerNearby = false;

    void Start()
    {
        door = doorObject.GetComponent<IDoor>(); // ドアのインターフェースを取得
        if (door == null)
        {
            Debug.LogError("DoorButton: IDoor を実装したスクリプトがアタッチされていません。");
        }
    }

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            door?.ToggleDoor(); // ドアを開閉
        }
        if (playerNearby && Input.GetButtonDown("Bbutton"))
        {
            door?.ToggleDoor(); // ドアを開閉
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }
}
