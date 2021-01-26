using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearClawController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyPattern", 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DestroyPattern()
    {
        Destroy(this.gameObject);
    }

}
