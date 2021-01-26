using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMove : MonoBehaviour
{
    // 스탯
    int hp = 10;

    public float movePower = 1f;

    // 이동 및 추격용
    Vector3 movement;
    int movementFlag = 0;   // 0: 정지상태, 1: 좌이동, 2: 우이동
    bool isTracing = false;
    GameObject target;

    Animator animator;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    new CapsuleCollider2D collider;

    // 공격용
    float distanceFromPlayer;
    Transform playerPos;
    public float jumpHeight;


    // ===============================================================


    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<CapsuleCollider2D>();
        playerPos = GameObject.FindWithTag("Player").GetComponent<Transform>();

    }

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
        Move();
    }

    private void Update()
    {
            distanceFromPlayer = playerPos.position.x - transform.position.x;

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
        if (direction == "Left")
        {
            moveVelocity = Vector3.left;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (direction == "Right")
        {
            moveVelocity = Vector3.right;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        transform.position += moveVelocity * movePower * Time.deltaTime;
    }


    // 추격용 함수들
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
            if (distanceFromPlayer <= 2) // 안됨
            {
                Invoke("Jump", 2); // 안됨
                return; // 안됨
            }
            else
            {
                isTracing = true;
                animator.SetBool("isMoving", true);
            }
        }
    }
    void Jump() // 안됨
    {
        // 응용한 강좌 https://youtu.be/fviU0V6nivs
        rigid.AddForce(new Vector2(distanceFromPlayer, jumpHeight), ForceMode2D.Impulse);
        Debug.Log("점프!");
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isTracing = false;
            StartCoroutine("ChangeMovement");
        }
    }



    // 아래는 공격 피격용 아직 구현 안됨

    void JumpAttack()   // 몬스터의 공격
    {
        
    }
    

    // 체력 0 이하일 때 피격효과 내고 사라진다.
    public void OnDamaged(int damage)
    {
        if (hp > 0)
        {
            return;
        }
        else
        {
            spriteRenderer.color = new Color(1, 1, 1, 0.3f);        // 맞으면 반투명해짐
            collider.enabled = false;                               // 충돌 끄기
            rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);    // 위로 튀어오름
            Invoke("Destroy", 3);                                   // 3초 뒤 사라짐
        }
    }

    private void Destroy()
    {
        gameObject.SetActive(false);
    }
}
