using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoveFormController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyPattern", 10);
        DoveTearsPos = GameObject.Find("DoveTearsPos").transform;
    }

    // Update is called once per frame
    void Update()
    {
        CreateHamsterBullet();
    }

    //============================================================
    //비둘기폼 눈물 생성


    public GameObject DoveTears;
    public Transform DoveTearsPos;
    private float curTime;
    private float coolTime = 0.03f;
    void CreateHamsterBullet()
    {
        float createPos_Random = Random.Range(-0.5f, 0.5f);
        Vector2 decideTearPos = new Vector2(this.DoveTearsPos.position.x + createPos_Random, this.DoveTearsPos.position.y);
        if (curTime <= 0)
        {
            Instantiate(DoveTears, decideTearPos, DoveTearsPos.rotation);
            curTime = coolTime;
        }
        curTime -= Time.deltaTime;
    }


    // ======================================================
    // 패턴이 생성되고 일정시간이 지나고 패턴이 파괴되어야함

    void DestroyPattern()
    {
        Debug.Log("패턴 끝");
        Destroy(this.gameObject);
 
    }

}
