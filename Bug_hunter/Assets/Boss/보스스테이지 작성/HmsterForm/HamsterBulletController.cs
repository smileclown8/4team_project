using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamsterBulletController : MonoBehaviour
{
    public GameObject Target;
    Vector3 trackingDir;
    float angle;
    public float speed;


    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.Find("player");
        trackingDir = (Target.transform.position - this.transform.position).normalized;
        Invoke("DestroyBullet", 7);
    }

    // Update is called once per frame
    void Update()
    {  
        angle = Mathf.Atan2(trackingDir.y, trackingDir.x) * Mathf.Rad2Deg;

        this.transform.rotation = Quaternion.AngleAxis(angle + 90f, Vector3.forward);

        this.transform.position += trackingDir * speed * Time.deltaTime;
    
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            DestroyBullet();
        }
    }
    void DestroyBullet()
    {
        Destroy(this.gameObject);
    }
}
