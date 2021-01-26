using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatManager : MonoBehaviour
{
    // 스탯
    int hp = 10;
    int batDamage = 2;


    [SerializeField] public GameObject bullet;
    float shoottime;
    float nextshoot;
    Animator animator;
    int movementFlag = 0;   // 0: 정지상태, 1: 좌이동, 2: 우이동


    void Start()
    {
        shoottime = 2f;
        nextshoot = Time.time;
    }


    // 사망 시 사라지기
    private void Update()
    {
        if (hp <= 0)
            Destroy(transform.parent.gameObject);       // 사망하면 부모 오브젝트부터 모두 삭제
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
    }


    GameObject target;
    GameObject bat;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")   // 플레이어가 닿으면
        {
            target = other.gameObject;          // 타깃을 플레이어로 세팅
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject.Find("BatMoving").GetComponent<BatMove>().enabled = false;       // 움직임을 멈추고
            Attack();                                                                   // 공격

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")                                           // 트리거 반경에서 벗어나면
        {
            movementFlag = 0;
            GameObject.Find("BatMoving").GetComponent<BatMove>().enabled = true;        // 다시 움직이기 시작
        }
    }


    void Attack()
    {
        if (Time.time > nextshoot)
        {
            // 왜 두번까지만 되는지 모르겠지만 두 번까지만 발사되니까 두 번 발사였던 것처럼 하자
            Invoke("GenerateBullet", 0.3f);
            GenerateBullet();
            nextshoot = Time.time + shoottime;
        }
    }

    void GenerateBullet()
    {
        Instantiate(bullet, transform.position, Quaternion.identity);
    }

}
