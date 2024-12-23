using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene3Transferscript : MonoBehaviour
{
    // Start is called before the first frame update
    public bool item1flag = false;
    public bool item2flag = false;
    public bool item3flag = false;

    public bool posterflag = false;
    public bool textflag = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (item1flag == true &&item2flag == true && item3flag == true)
        {
            textflag = true;
        }


    }
    public void Scenetransfer()
    {
        
    }
    public void ChangeScene(string arasuzi3)
    {
        SceneManager.LoadScene(arasuzi3);
    }
}
