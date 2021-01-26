using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 기본적으로 슬라임과 같음

public class SnakeManager : MonoBehaviour
{
    // 스탯
    int hp = 10;
    int snakeDamage = 15;


    // 이동 및 추격용
    public int movementFlag = 0;   // 0: 정지상태, 1: 좌이동, 2: 우이동
    public bool isTracing = false;
    public float movePower = 1f;
    GameObject target;

    Animator animator;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    new CapsuleCollider2D collider;
    public Collider2D col;

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
        playerPos = GameObject.FindWithTag("Player").GetComponent<Transform>();
        col = GetComponent<BoxCollider2D>();
        animator = gameObject.GetComponentInChildren<Animator>();

    }

    void Start()
    {
        StartCoroutine("ChangeMovement");
    }


    void FixedUpdate()
    {
        Move();
        //if (distanceFromPlayer < 0.1f)
        //InvokeRepeating("SnakeAttack", 1, 1);
        Invoke("MovingSlow", 0.2f);
        Invoke("MovingFast", 3);
    }


    private void Update()
    {
        distanceFromPlayer = playerPos.position.x - transform.position.x;
    }


    // 이동용 함수
    public void Move()
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

    IEnumerator MoveSlow(Vector3 velo)
    {
        yield return new WaitForSeconds(0.5f);
        movePower = 1f;
        transform.position += velo * movePower * Time.deltaTime;
    }
    IEnumerator MoveFast(Vector3 velo)
    {
        yield return new WaitForSeconds(3);
        movePower = 4f;
        transform.position += velo * movePower * Time.deltaTime;
    }

    void MovingSlow()
    {
        movePower = 1f;
        new WaitForSeconds(0.2f);
    }
    void MovingFast()
    {
        movePower = 4f;
        new WaitForSeconds(3);
    }




    // 추격용 함수들
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            target = other.gameObject;
            StopCoroutine("ChangeMovement");
        }
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isTracing = true;
            animator.SetBool("isMoving", true);
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isTracing = false;
            StartCoroutine("ChangeMovement");
        }
    }


    public IEnumerator ChangeMovement()
    {
        movementFlag = Random.Range(0, 3);

        if (movementFlag == 0)
            animator.SetBool("isMoving", false);
        else
            animator.SetBool("isMoving", true);

        yield return new WaitForSeconds(3f);

        StartCoroutine("ChangeMovement");
    }



    // 충돌하면 초당 15씩 체력 깎기
    private void OnCollisionEnter2D(Collision2D col)
    {
        StopCoroutine("ChangeMovement");
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
    }
    void SnakeAttack()
    {
        // GameObject.FindWithTag("Player").hp -= snakeDamage;
        Debug.Log("콰삭!");
    }



    // 체력 0 이하일 때 피격효과 내고 사라진다.
    public void OnDamaged(int damage)
    {
        if (hp <= 0)
        {
            spriteRenderer.color = new Color(1, 1, 1, 0.3f);        // 맞으면 반투명해짐
            collider.enabled = false;                               // 충돌 끄기
            rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);    // 위로 튀어오름
            Invoke("Destroy", 1.5f);                                // 1.5초 뒤 사라짐
        }
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
