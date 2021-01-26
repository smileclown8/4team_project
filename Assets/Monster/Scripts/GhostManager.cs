using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GhostManager : MonoBehaviour
{
    int hp = 15;            // 체력
    bool isDamaged;         // 공격을 받았는가?
    bool trace;             // 추격을 시작했는가?
    Rigidbody2D rigid;
    Animator animator;
    public GameObject target;

    int playerHP;           // 플레이어의 HP. 나중에 플레이어와 연결할 것

    // 따라가기용
    // https://youtu.be/jvtFUfJ6CP8
    public AIPath aiPath;


    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        shoottime = 3f;
        nextshoot = Time.time;
        GetComponentInParent<AIDestinationSetter>().enabled = false;
    }

    private void Update()
    {
        if (hp < 0)
            Destroy(this.gameObject);

        if (Input.GetMouseButtonDown(0))    // 테스트용 임시함수. 지울 것***************
            isDamaged = true;

        if (isDamaged)
            TakeDamage();

    }



    public void TakeDamage()
    {
        // 여기에 피격 사운드 넣기 audio.clip=제목; audio.Play();

        GetComponentInParent<AIDestinationSetter>().enabled = true;
        target = GameObject.FindWithTag("Player");                      // 타깃을 플레이어로 세팅
        
        while (playerHP > 0)
        {
            if (hp > 0)
            {
                Attack();
            }
            else
            {
                animator.SetTrigger("Death");
                Destroy(this.gameObject);
            }
        }

    }


    [SerializeField] public GameObject bullet;
    float shoottime;
    float nextshoot;

    void Attack()
    {
        Trace();

        if (Time.time > nextshoot)      // 플레이어가 죽을 때까지 3초마다 한 번씩 탄알 발사
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            nextshoot = Time.time + shoottime;
        }
    }


    void Trace()
    {
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
