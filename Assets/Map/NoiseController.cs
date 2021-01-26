//==================================================================================================================
// Invoke의 주요 내용
// - Invoke를 이용한 지연CoroutineSample
// - 일정한 간격을 이용해 반복, 반복의 멈춤
//==================================================================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NoiseController : MonoBehaviour
{
    public GameObject targetObject;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Nosing());
    }

    IEnumerator Nosing()
    {
        while (true)
        {
            OnEnableSettingEnvent();
            yield return new WaitForSeconds(3);//WaitForSeconds객체를 생성해서 반환


        }
    }

    public void OnEnableSettingEnvent()
    {
        if (targetObject.activeInHierarchy)
            targetObject.SetActive(false);
        else
            targetObject.SetActive(true);
    }


    // Update is called once per frame
    void Update()
    {
 
    }
}
