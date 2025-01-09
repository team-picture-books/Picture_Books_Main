using UnityEngine;
using System.Collections;



public class ToggleObject : MonoBehaviour

{
   
    public GameObject targetObject; // 対象のGameObject
    public AudioSource seSource;
    private void Awake()
    {
        

        targetObject.SetActive(false);

    }
    public void toggleobeject()
    {
        IEnumerator DelayedProcess()
        {
            Debug.Log("1行目の処理: すぐに実行");

            // 1秒待機
            yield return new WaitForSeconds(2f);

            Debug.Log("2行目の処理: 1秒後に実行");
            targetObject.SetActive(false);
        }
        if(seSource != null)
        {
            seSource.Play();

        }
        targetObject.SetActive(true);

        StartCoroutine(DelayedProcess());





    }

}
