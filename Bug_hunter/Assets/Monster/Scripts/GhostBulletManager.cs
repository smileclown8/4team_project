using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBulletManager : MonoBehaviour
{
    GameObject target;
    Vector3 trackingDir; // 대상 타겟으로 항상 쫓아가게 해주는 좌표 설정
    float angle;

    public float speed;

    void Start()
    {
        target = GameObject.FindWithTag("Player");
        Invoke("DestroyBullet", 4);                         // 플레이어에 닿지 않아도 4초가 지나면 총알 없어짐
    }

    void Update()
    {
        trackingDir = (target.transform.position - this.transform.position).normalized;

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

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Hit!");
            Destroy(gameObject);
        }
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
