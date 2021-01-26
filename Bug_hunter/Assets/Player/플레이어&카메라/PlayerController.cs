using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //플레이어의 이동, 점프, 점프공격, 총발싸등 온갖 활동을 다 관장하는 컨트롤러
    //기능중 일부를 따로 빼서 따로 컨트롤러스크립트를 만들어도 된다.
    // -> 01.25 수정 : 각 스테이터스값들을 PlayerStatusManager로 이관
    // ====로 기능별로 구분해놨음
    // == 구분별로 변수가 따로 설정되어있기때문에, 변수가 위에만 있는게 아니라 곳곳에 있으니 잘 확인하세요




    public Rigidbody2D playerRigidbody2D;
    SpriteRenderer spriteRenderer;
    Animator anim;
    

    //플레이어의 최대 이동속도
    public float maxMoveSpeed;

    //이름은 moveSpeed로 했지만, 실제로는 플레이어가 이동(좌,우)할때 받는 힘의 양을 조절해줌
    //클수록 maxMoveSpeed에 도달하는 시간이 짧아짐. 자동차의 제로백이라고 생각하면 된다.
    //값이 클수록 이동이 부드러워지는걸 확인. 60정도를 추천
    public float moveSpeed;





    //플레이어가 점프중인가를 확인해주는 bool 변수
    //점프중에는 다시 점프를 못하게 막아준다.
    public bool isJumping = false;

    //PlayerStatusManager의 jumpPower값을 읽어옴
    public float jumpPower;
    public GameObject PlayerStatusManager;

    public GameObject Dialogue;
    public bool isTalk;

    void Awake()
    {
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        PlayerStatusManager = GameObject.Find("PlayerStatusManager");
        jumpPower = PlayerStatusManager.GetComponent<PlayerStatusManager>().jumpPower;
        Dialogue = GameObject.Find("UI_Dialogue");
    }



    // Start is called before the first frame update
    void Start()
    {
        bulletPos = GameObject.Find("bulletPos"); //자식인 bulletPos 오브젝트를 찾아서 그 좌표값을 총알발사 좌표값으로 사용한다.
    }

    // Update is called once per frame
    void Update()
    {
        skill_ID = PlayerStatusManager.GetComponent<PlayerStatusManager>().skill_ID;
        isTalk = Dialogue.GetComponent<DialogueManager>().talking;

        Jump();
        Land();
        if (Input.GetKey(KeyCode.F)) // 유니티내에서 
            //테스트용으로 버튼대신 키입력받기. 나중에는 update에서 Jump()와 Shoot을하는게 아니라 따로 버튼입력 받을거임
            //즉, Update문은 Land를 제외하고 싹 비워버릴꺼니 신경ㄴㄴ
        {
            switch (skill_ID)
            {
                case 0:
                    PlayerNormalShoot() ;
                    break;
                case 1:
                    PlayerFirstSkillShoot();
                    break;
                case 2:
                    PlayerSecondSkillShoot();
                    break;

            }
        }
    }

    public void Jump() //플레이어 점프
    {
        //if (!isJumping)
        if (Input.GetKeyDown(KeyCode.Space)&& !isJumping
            && isTalk==false) // GetKeydown은 테스트용으로 넣은것, 나중에 버튼입력으로 바꿀거라 if(!isJumping)만 남을거
        {
            playerRigidbody2D.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            playerRigidbody2D.AddForce(Vector2.right * this.playerRigidbody2D.velocity.x * 0.5f, ForceMode2D.Impulse);
            //플레이어가 대각선으로 점프할때, 너무 앞으로 나아가는 힘이 없어서 좀 더 앞으로 밀어줌
            isJumping = true;
        }

        //점프중일때 점프하는 모습의 애니메이션 추가해주세요 anim.SetBool("isJumping",true);
        

        if(playerRigidbody2D.velocity.y > 40.0f)
        {
            playerRigidbody2D.AddForce(Vector2.down * jumpPower / 5, ForceMode2D.Impulse);
        }
        // 플레이어가 점프힘을 지나치게 많이받아서 하늘로 날아가고 그러길래, 플레이어의 점프속도가 어느 정도를 넘어가면 강제로
        // 아래로 내려꽂히는 힘을 추가로 가해줌.

    }


    public LayerMask isLayer;
    public void Land() //플레이어 착륙 감지 함수. 이 함수 때문에 레이어설정이 중요하다
    {
        if (playerRigidbody2D.velocity.y <= 0)
        {
            Debug.DrawRay(playerRigidbody2D.position, Vector3.down, new Color(0, 1, 0));

            RaycastHit2D rayHit = Physics2D.Raycast(playerRigidbody2D.position, Vector3.down
                , 1, isLayer);  // 밟는 땅이 platform 레이어가 아니라면 다시 점프가 안됨! 반드시 
                                                      // 점프가 가능한 땅은 platform을 설정해줄것

            if (rayHit.collider != null)
            {
                if (rayHit.distance < 1.0f) // 플레이어의 중심에서 발끝까지의 거리(플레이어절반크기) 즉, 바닥에 닿았으면
                {
                    isJumping = false;

                    //점프가 끝나고 착지하는 애니메이션 anim.SetBool("isJumping",false);
                }
            }
        }

    }


    // ==================================================================================================
    // 여기서부터 플레이어 + 오브젝트 충돌
    void OnCollisionEnter2D(Collision2D collision)   // 이 스크립트가 들어가있는 오브젝트가 콜라이더를 가지고 있는 무언가와 충돌했을때 실행됨
    {
      if(collision.gameObject.tag == "Enemy")  // tag가 Enemy인것과 충돌할때.
                                               // 현재 까시와 몬스터가 모두 태그를 Enemy로 했음.
                                               // 까시를 따로 태그 설정한다면
                                               // if(collision.gameObject.tag =="까시"){ OnDamaged( ... ) } 이런 내용을 추가해야함
        {
            // 몬스터 밟기
            if (this.transform.position.y > collision.transform.position.y) //몬스터와 충돌할때, 몬스터 위에서 충돌하면.
            {
                OnJumpAttack(collision.transform);  //점프 어택을 하고,
            }
            else
            {
              OnDamaged(collision.transform.position);  // 만약 몬스터 위가 아닌곳에서 몬스터와 충돌하면 데미지를 입는다.
            }
        }  
    }


    void OnJumpAttack(Transform monster)  // 아직 미완성부분
    {
        //Point


        isJumping = false;
        playerRigidbody2D.AddForce(Vector2.up * 15, ForceMode2D.Impulse); // 밟았을때 통~ 하고 튀게 위로 힘을 준다.

        //Enemy Die

        // BE2 6분대 근처 참고하면서 마저작성하기
    }
    void OnDamaged(Vector2 targetPos) //메이플마냥 데미지를 받으면 뒤로 약간 밀려나고 투명해지며 몬스터를 통과하게 함
    {
        //피격시 PlayerDamaged로 플레이어의 레이어 변화
        this.gameObject.layer = 10; // 레이어 PlayerDamaged 가 들어가있는 레이어 번호

        //피격당하면 색변화(투명도를 변화시킴)
        this.spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        // 피격당했을때 튕겨나가는거
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1 ;  //몬스터에게 피격당해서 날아갈 방향
        isJumping = true;
        playerRigidbody2D.AddForce(new Vector2(dirc, 1) * 8, ForceMode2D.Impulse);

        Invoke("OffDamaged", 2); //레이어와 투명해진거를 원상복귀

    }
    void OffDamaged() //원상복귀
    {
        this.gameObject.layer = 9; // 레이어 Player가 들어가있는 레이어 번호
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    // =======================================================================================
    // 여기서부터 플레이어 슈팅

    public int skill_ID; //초기값, 일반공격

    public GameObject NormalBullet; // 일반 공격의 bullet(탄환,투사체). 
    public GameObject FirstSkillBullet; // 첫번째 스킬의 bullet
    public GameObject SecondSkillBullet; // 두번째스킬의 bullet.

    public GameObject bulletPos;
    //bulletPos도 ShootPos로 이름 바꿔야함
    public Transform pos;

    public float coolTime;

    //나중에 밸런싱할때 각각의 공격마다의 쿨타임을 따로 정해주고,
    // 각 공격마다의 쿨타임을 다시, 플레이어 자체의 공격쿨타임(coolTime)과 곱해준다.
    // 왜그러냐면, 공격속도 강화 아이템을 먹었을때 coolTime을 강화시켜주면 (실제론 coolTIme의 값이 작아질것)
    //모든 스킬의 공격속도가 빨라지게됨
    public float coolTime_NormalShoot = 1;
    public float coolTime_FirstSkill = 1;
    public float coolTime_SecondSKill= 1;
    //임시로 모두 초기값을 1로 통일

    private float curTime;

    //bullet의 이름과 해당 총알의 스크립트이름도 바꿔야함
    public void PlayerNormalShoot()
    {
        pos = bulletPos.transform;

        if (curTime <= 0)
        {
            Instantiate(NormalBullet, pos.position, transform.rotation);
            curTime = coolTime * coolTime_NormalShoot;
        }
        curTime -= Time.deltaTime;
    }
    public void PlayerFirstSkillShoot()
    {
        pos = bulletPos.transform;

        if (curTime <= 0)
        {
            Instantiate(FirstSkillBullet, pos.position, transform.rotation);
            curTime = coolTime * coolTime_FirstSkill;
        }
        curTime -= Time.deltaTime;
    }

    public void PlayerSecondSkillShoot()
        //곡사포? 일단 대충넣어놈.
    {
        pos = bulletPos.transform;

        if (curTime <= 0)
        {
            Instantiate(SecondSkillBullet, pos.position, transform.rotation);
            curTime = coolTime * coolTime_SecondSKill;
        }
        curTime -= Time.deltaTime;
    }
}
