using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContol : MonoBehaviour
{
    //ÉJÉÅÉâäiî[
    private GameObject CameraNorth;
    private GameObject CameraEast;
    private GameObject CameraSouth;
    private GameObject CameraWest;
    // Start is called before the first frame update
    void Start()
    {
        CameraNorth = GameObject.Find("North");
        CameraEast = GameObject.Find("East");
        CameraSouth = GameObject.Find("South");
        CameraWest = GameObject.Find("West");
        CameraNorth.SetActive(true);
        CameraEast.SetActive(false);
        CameraSouth.SetActive(false);
        CameraWest.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            CameraNorth.SetActive(true);
            CameraEast.SetActive(false);
            CameraSouth.SetActive(false);
            CameraWest.SetActive(false);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            CameraNorth.SetActive(false);
            CameraEast.SetActive(true);
            CameraSouth.SetActive(false);
            CameraWest.SetActive(false);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            CameraNorth.SetActive(false);
            CameraEast.SetActive(false);
            CameraSouth.SetActive(true);
            CameraWest.SetActive(false);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            CameraNorth.SetActive(false);
            CameraEast.SetActive(false);
            CameraSouth.SetActive(false);
            CameraWest.SetActive(true);
        }
    }
}
