using UnityEngine;
using System.Collections;



public class ToggleObject : MonoBehaviour

{
   
    public GameObject targetObject; // �Ώۂ�GameObject
    public AudioSource seSource;
    private void Awake()
    {
        

        targetObject.SetActive(false);

    }
    public void toggleobeject()
    {
        IEnumerator DelayedProcess()
        {
            Debug.Log("1�s�ڂ̏���: �����Ɏ��s");

            // 1�b�ҋ@
            yield return new WaitForSeconds(2f);

            Debug.Log("2�s�ڂ̏���: 1�b��Ɏ��s");
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
