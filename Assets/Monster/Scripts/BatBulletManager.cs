using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBulletManager : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;

    GameObject target;
    Vector2 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player");
        moveDirection = (target.transform.position - transform.position).normalized * speed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Hit!");
            Destroy(gameObject);
        }
    }


    void Update()
    {

    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
