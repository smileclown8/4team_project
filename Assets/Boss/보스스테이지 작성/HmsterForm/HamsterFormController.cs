using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamsterFormController : MonoBehaviour
{
    public Rigidbody2D rigid;


    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        StartCoroutine(HamsterFormMove());
        
        StartCoroutine(HamsterBulletStop());

        endPos = GameObject.Find("hamsterEndPos").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(isHamsterBulletStop);

        if(isHamsterBulletStop == false)
        {
            CreateHamsterBullet();
        }
    }

    //============================================================
    //햄스터폼, 왼쪽에서 오른쪽으로 이동
    public Transform endPos; // 오른쪽 끝
    public float HamsterMoveSpeed = 5f;

    IEnumerator HamsterFormMove()
    {
        yield return new WaitForSeconds(2.0f);
        isHamsterBulletStop = false;
        while (true)
        {
            transform.position = Vector2.MoveTowards(transform.position, endPos.position, Time.deltaTime * HamsterMoveSpeed);
            yield return null;
        }
    }

    //============================================================
    //햄스터폼 총알 생성

    public GameObject hamsterBullet;
    private float curTime;
    private float coolTime = 0.5f;
    void CreateHamsterBullet()
    {
        if (curTime <= 0)
        {
            Instantiate(hamsterBullet, this.transform.position, transform.rotation);
            curTime = coolTime;
        }
        curTime -= Time.deltaTime;
    }

    //=============1===============================================
    //일정 시간 후 햄스터폼 총알 생성 멈추게하기

    private bool isHamsterBulletStop = true;
    IEnumerator HamsterBulletStop()
    {
        yield return new WaitForSeconds(8.0f);
        isHamsterBulletStop = true;
        Destroy(this.gameObject);
    }

}
