using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_IF_CORE_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //=======================================
        //보스 상하 이동 위치 초기화
        transform.position = startPos.position;
        desPos = endPos;

        //=======================================
        //상하 이동 코루틴 시작
        StartCoroutine(MovingCore());

        //=======================================
        //플레이어 찾기
        Player = GameObject.Find("player");
        PlayerStatusManager = GameObject.Find("PlayerStatusManager");


        //=======================================
        //패턴생성 코루틴 시작
        StartCoroutine(PatternDecide());


    }

    public float turnSpeed = -360f;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, turnSpeed * Time.deltaTime);

     //   Debug.Log(nowPattern);
    }


    // =======================================================================
    // 보스 체력, 공격력 관리

    public float Boss_IF_HP;
    public float Boss_IF_MaxHP;

    GameObject Player;
    GameObject PlayerStatusManager;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "PlayerBullet")
        {
            Boss_IF_HP -= collision.gameObject.GetComponent<BulletDamage>().damage;
        }
    }



    // =======================================================================
    // 코어 상, 하 이동 부분

    public Transform startPos; //한쪽 끝
    public Transform endPos; // 다른쪽 끝
    public Transform desPos; //목적지
    public float CoreMovespeed = 5f;
    IEnumerator MovingCore()
    {
        yield return new WaitForSeconds(3.0f);

        while (true)
        {
            transform.position = Vector2.MoveTowards(transform.position, desPos.position, Time.deltaTime * CoreMovespeed);
            yield return null;

            if (Vector2.Distance(transform.position, desPos.position) <= 0.05f)
            {
                if (desPos == endPos)
                {
                    desPos = startPos;
                }
                else
                {
                    desPos = endPos;
                }
            }
        }
    }
    // =======================================================================
    // 보스 패턴 결정 부분

    public bool isPattern = false;
    public int nowPattern;

    public GameObject FirstPattern_Hamster;
    public GameObject FirsPattern_StartPos;


    public GameObject SecondPattern_Alpaca_1;
    public GameObject SecondPattern_Alpaca_2;
    public GameObject SecondPattern_RightPos;
    public GameObject SecondPattern_LeftPos;

    public GameObject ThirdPattern_Dove;
    public GameObject ThirdPattern_DovePos;

    public GameObject FourthPattern_Bear;
    public GameObject FourthPattern_BearPos;

    IEnumerator PatternDecide()
    {
        yield return new WaitForSeconds(5.0f); //보스 패턴 시작하기전까지 시간
        
        int postPattern =0;            

        while (true)
        {
            isPattern = false;

            nowPattern = Random.Range(1, 4 + 1);


            while (true)
            {
                if( nowPattern == postPattern)
                {
                    nowPattern = Random.Range(1, 4 + 1);
                }
                if (nowPattern != postPattern)
                    break;
            }
            switch (nowPattern)
            {
                case 1:;

                    // 1번 패턴 생성
                    Instantiate(FirstPattern_Hamster, FirsPattern_StartPos.transform.position, FirsPattern_StartPos.transform.rotation);
                    postPattern = nowPattern;
                    Debug.Log("패턴1");
                    yield return new WaitForSeconds(13.0f); // 1번 패턴이 끝날때까지 걸리는 시간
                    break;
                case 2:;

                    // 2번 패턴 생성

                    Instantiate(SecondPattern_Alpaca_1, SecondPattern_RightPos.transform.position, SecondPattern_RightPos.transform.rotation);

                    yield return new WaitForSeconds(5.0f);

                    Instantiate(SecondPattern_Alpaca_2, SecondPattern_LeftPos.transform.position, SecondPattern_LeftPos.transform.rotation);

                    postPattern = nowPattern;
                    Debug.Log("패턴2");
                    yield return new WaitForSeconds(10.0f); // 2번 패턴이 끝날때까지 걸리는 시간
                    break;
                case 3:;

                    // 3번 패턴 생성
                    Instantiate(ThirdPattern_Dove, ThirdPattern_DovePos.transform.position, ThirdPattern_DovePos.transform.rotation);
  
                    postPattern = nowPattern;
                    Debug.Log("패턴3");
                    yield return new WaitForSeconds(10.0f); // 3번 패턴이 끝날때까지 걸리는 시간
                    isPattern = true;
                    break;
                case 4:;
                    // 4번 패턴 생성
                    Instantiate(FourthPattern_Bear, FourthPattern_BearPos.transform.position, FourthPattern_BearPos.transform.rotation);


                    postPattern = nowPattern;
                    Debug.Log("패턴4");
                    yield return new WaitForSeconds(6.0f); // 4번 패턴이 끝날때까지 걸리는 시간
                    break;
            }

            yield return new WaitForSeconds(3.0f);
            
        }
    }





    // ======================================================================
    // 보스 대기


    // ======================================================================

    }
