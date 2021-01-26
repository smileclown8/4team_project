using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alpaca_LeftController : MonoBehaviour
{



    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();


        Invoke("DestroyPattern", 10);
    }

    // Update is called once per frame
    void Update()
    {
        AlpacaMoveLeft_To_Right();
        moveSpeed += 5;
    }





    // ======================================================
    // 알파카, 우측에서 왼쪽으로 힘주기


    public Rigidbody2D rigid;

    public float moveSpeed;

    void AlpacaMoveLeft_To_Right()
    {
        rigid.AddForce(Vector2.right * Time.deltaTime * moveSpeed, ForceMode2D.Impulse) ;

    }



    // ======================================================
    // 패턴이 생성되고 일정시간이 지나고 패턴이 파괴되어야함

    void DestroyPattern()
    {
        Destroy(this.gameObject);
    }

}
