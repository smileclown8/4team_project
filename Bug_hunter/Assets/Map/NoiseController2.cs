
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NoiseController2 : MonoBehaviour
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
            yield return new WaitForSeconds(2);//WaitForSeconds객체를 생성해서 반환
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
