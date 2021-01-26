using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreRotater : MonoBehaviour
{
    [SerializeField]
    public GameObject Boss_CORE;

    // Start is called before the first frame update
    void Start()
    {
    }

    public float turnSpeed = 15f;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, turnSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {

            Debug.Log("충돌");
            Boss_CORE.gameObject.SetActive(true);
            this.gameObject.SetActive(false);


        }
    }
}
