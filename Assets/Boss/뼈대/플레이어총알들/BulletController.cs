using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    //직선으로 나가는 탄환, 노말공격의 컨트롤러
    // 직선으로 나가는 특성상 적이 맞으면 뒤로 밀려날 수 있다.
    // 그것을 방지하기 위해서 탄이 충돌할때 사라지는 조건을, 
    // 각각의 탄 자체에서 Ray로 앞으로 광선을 쏴서
    // 그 광선에 적이 닿으면 사라지게 하고 데미지를 주기 가능.

    public float speed;

    void Start()
    {
        Invoke("DestroyBullet", 4); //총알이 만들어지고 4초 후 파괴됨
    }


    void Update()
    {
        if (transform.rotation.y == 0) //플레이어가 오른쪽바라보면 오른쪽발사
        {
            transform.Translate(transform.right * speed * Time.deltaTime);
        }
        else //그 반대면 왼쪽 발사
        {
            transform.Translate(transform.right * -1 * speed * Time.deltaTime);
        }
    }


    /* 폐기 처리 
    public float speed;

    public float distance = 1;
    // 탄과 부딪히는 오브젝트 사이의 거리, 즉 ray의 거리

    public LayerMask isLayer; //레이어 감지

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyBullet", 4);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, transform.right, distance, isLayer);
        //인스펙터창에서 isLayer에 탄이 부딪히면 파괴되는 레이어들을 설정해주기
        //플랫폼, 몬스터 , 까시 등등.. 원하는대로 해주자. 여러개 선택가능함

        if(ray.collider != null)
        {
            if(ray.collider.gameObject.tag == "Enemy")  //광선이 닿은 적의 태그가 Enemy면  명중을 출력한다. 이때, 데미지를 줄 수 있음.
                                                // 데미지 주는방법 : ray.collider.gameObject.GetComponet<몬스터정보가담긴스크립트>.몬스터HP를 직접감소
                                                    // or 감소시키는 함수호출..
            {
                Debug.Log("명중");
            }
                DestroyBullet(); 
        }

        if (transform.rotation.y == 0) //플레이어가 오른쪽바라보면 오른쪽발사
        {
            transform.Translate(transform.right * speed * Time.deltaTime);
        }
        else //그 반대면 왼쪽 발사
        {
            transform.Translate(transform.right * -1 * speed * Time.deltaTime);
        }

    }


    */

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            DestroyBullet();
        }
    }


    void DestroyBullet()
    {
        Destroy(this.gameObject);
    }
}
