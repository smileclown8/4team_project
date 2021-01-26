using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float distance;
    public LayerMask isLayer;

    void Start()
    {
        Invoke("DestroyBullet", 2);
    }

    void Update()
    {
        transform.Translate(transform.right * -1f * speed * Time.deltaTime);

        RaycastHit2D raycast = Physics2D.Raycast(transform.position, transform.right * -1, distance, isLayer);
        if (raycast.collider.tag == "Player")
        {
            Debug.Log("탕탕");
        }
        DestroyBullet();
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
