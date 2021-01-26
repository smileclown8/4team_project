using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dove_TearsController : MonoBehaviour
{
    public GameObject Boss_IF;
    public bool isPatternEnd;

    // Start is called before the first frame update
    void Start()
    {
        Boss_IF = GameObject.Find("Boss_CORE");
    }

    // Update is called once per frame
    void Update()
    {
        isPatternEnd = Boss_IF.GetComponent<Boss_IF_CORE_Controller>().isPattern;     
        
        if(isPatternEnd == true)
        {
            Destroy(this.gameObject);
        }
    }
}
