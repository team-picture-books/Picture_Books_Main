using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class titlescript : MonoBehaviour
{
    public string nextSceneName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Bbutton"))  // Fire2はBボタンに対応
        {
            // シーンを切り替える
            SceneManager.LoadScene(nextSceneName);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
