using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 구현해야 할 것
 * 1. 몬스터가 플랫폼 위에서 이동 (완료)
 * 2. 몬스터가 플레이어를 인식하면 추격 : 좌측 추격은 됐음 우측 추격 필요
 * 3. 몬스터가 플레이어 주변에 있으면 공격 << 안되고있음
 * 4. 다 구현됐을 때 애니메이션 좌우반전
 */

public class MonsterMove : MonoBehaviour
{
    // 스탯
    int hp = 10;


    public float movePower = 1f;

    // 이동용
    Animator animator;
    Vector3 movement;
    int movementFlag = 0;   // 0: 정지상태, 1: 좌이동, 2: 우이동

    bool isMoving = true;
    bool isTracing = false;
    GameObject target;

    // Ray로 추격용
    public float distance;
    public float atkDistance;       // 공격거리
    public LayerMask isLayer;       // 탐색할 레이어
    public float speed;             // 추격 속도 배율

    // 투사체
    // https://youtu.be/YFESOLMbB6Q
    public GameObject bullet;
    public Transform pos;           // 투사체 생성 위치
    public float cooltime;          // 발사 딜레이 타임
    private float currenttime;


    // =======================================================


    void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();

        StartCoroutine("ChangeMovement");
    }

    IEnumerator ChangeMovement()
    {
        movementFlag = Random.Range(0, 3);

        if (movementFlag == 0)
            animator.SetBool("isMoving", false);
        else
            animator.SetBool("isMoving", true);

        yield return new WaitForSeconds(3f);

        StartCoroutine("ChangeMovement");
    }


    void FixedUpdate()
    {

        RaycastHit2D raycast = Physics2D.Raycast(transform.position, transform.right * -1, distance, isLayer);
        if(raycast.collider != null) // 충돌이 있으면
        {
            if (Vector2.Distance(transform.position, raycast.collider.transform.position) < atkDistance) // Ray에 닿는 범위 안이면 공격
            {
                if (currenttime <= 0) // 발사 시간이 되면 발사
                {
                    GameObject bulletPrefab = Instantiate(bullet, pos.position, transform.rotation);
                    currenttime = cooltime;
                }
            }
            else // 공격범위 밖이면 추격
            {
                transform.position = Vector3.MoveTowards(transform.position, raycast.collider.transform.position, Time.deltaTime * speed);
            }
            currenttime -= Time.deltaTime;
        }
        else // 충돌이 없으면 이동
            Move();

    }


    // 이동용 함수
    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;
        string direction = "";

        if (isTracing)
        {
            Vector3 playerPos = target.transform.position;

            if (playerPos.x < transform.position.x)
                direction = "Left";
            else if (playerPos.x > transform.position.x)
                direction = "Right";
        }
        else
        {
            if (movementFlag == 1)
                direction = "Left";
            else if (movementFlag == 2)
                direction = "Right";
        }


        // 왼쪽 오른쪽 이동
        if (movementFlag == 1)
        {
            moveVelocity = Vector3.left;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (movementFlag == 2)
        {
            moveVelocity = Vector3.right;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        transform.position += moveVelocity * movePower * Time.deltaTime;
    }


    // 추적용 함수들
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            target = other.gameObject;
            StopCoroutine("ChangeMovement");
        }
        else
        {
            return;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isTracing = true;
            animator.SetBool("isMoving", true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isTracing = false;
            StartCoroutine("ChangeMovement");
        }
    }


    // 사망 시 사라지기
    private void Update()
    {
        if (hp < 0)
            Destroy(this.gameObject);
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
    }

}
