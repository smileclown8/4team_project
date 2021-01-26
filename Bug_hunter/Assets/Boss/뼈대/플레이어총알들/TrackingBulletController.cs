using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingBulletController : MonoBehaviour
{
    //유도탄 컨트롤러
    // 인스펙터창의 Target에 유도탄이 쫓아가야할 대상오브젝트를 넣어주거나, Start문에서 대상을 설정한다.
    // speed를 제외하곤 건드릴 곳 없음!

    public GameObject Target;
        
    Vector3 trackingDir; // 대상 타겟으로 항상쫓아가게 해주는 좌표 설정
    float angle;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.Find("player");
        Invoke("DestroyBullet", 4);
    }

    // Update is called once per frame
    void Update()
    {
        trackingDir = (Target.transform.position - this.transform.position).normalized;

        angle = Mathf.Atan2(trackingDir.y, trackingDir.y) * Mathf.Rad2Deg;

        Vector3 cross = Vector3.Cross(transform.up, trackingDir);

        if (cross.z < 0)
        {
            angle = transform.rotation.eulerAngles.z - Mathf.Min(10, angle);
        }
        else
        {
            angle = transform.rotation.eulerAngles.z + Mathf.Min(10, angle);
        }

        this.transform.Translate(trackingDir * speed * Time.deltaTime);
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
