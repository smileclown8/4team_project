using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_IF_TypingEffect : MonoBehaviour
{
    public Text tx;
    private string m_text = "IF { ME == HAMSTER }";

    void Start()
    {
        StartCoroutine(_typing());
    }

    void Update()
    {
        
    }

    IEnumerator _typing()
    {
        yield return new WaitForSeconds(2f);
        for(int i = 0; i <=m_text.Length; i++)
        {
            tx.text = m_text.Substring(0,i);
            yield return new WaitForSeconds(0.15f);
        }
    }
}
