using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondSkillBulletController : MonoBehaviour
{

    // 자연스럽게하기위해서는 중력값 조절이 필요
    // 중력값이 크고 speed를 높이면 빠른 곡사포 탄환을 만들 수 있다.
    // shoootAngle의 값으로 각도를 조절한다
    // 탄환에 physical material2D를 넣고, 그 탄성값(bounce)를 조절함으로써 통통튀게할 수 있다.

    Rigidbody2D SecondSkillBulletRigid;


    public Vector2 shootAngle = new Vector2 (1,2); //60도 정도 
    public float speed;


    private void Awake()
    {
        SecondSkillBulletRigid = this.GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (transform.rotation.y == 0)
        {
            shootAngle.x = Mathf.Abs(shootAngle.x);
            SecondSkillBulletRigid.AddForce(shootAngle * speed, ForceMode2D.Impulse);
        }
        else
        {
            shootAngle.x = Mathf.Abs(shootAngle.x) * -1;
            SecondSkillBulletRigid.AddForce(shootAngle * speed, ForceMode2D.Impulse);
        }

        Invoke("DestroyBullet", 6);
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            DestroyBullet();
        }
    }


    void DestroyBullet()
    {
        Destroy(this.gameObject);
    }

}
