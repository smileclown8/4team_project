using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearFormController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        BearClaw_Pos1 = GameObject.Find("BearClaw_Pos1");
        BearClaw_Pos2 = GameObject.Find("BearClaw_Pos2");
        BearClaw_Pos3 = GameObject.Find("BearClaw_Pos3");
        BearClaw_Pos4 = GameObject.Find("BearClaw_Pos4");

        StartCoroutine(CreateClaws());

        Invoke("DestroyPattern", 5);

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //============================================================
    //베어폼 할퀴기 생성

    public GameObject Claw1;
    public GameObject Claw2;
    public GameObject Claw3;
    public GameObject Claw4;

    public GameObject BearClaw_Pos1;
    public GameObject BearClaw_Pos2;
    public GameObject BearClaw_Pos3;
    public GameObject BearClaw_Pos4;


    IEnumerator CreateClaws()
    {
        yield return new WaitForSeconds(3.0f);
        Instantiate(Claw4, BearClaw_Pos4.transform.position, BearClaw_Pos4.transform.rotation);
        yield return new WaitForSeconds(0.1f);
        Instantiate(Claw3, BearClaw_Pos3.transform.position, BearClaw_Pos3.transform.rotation);
        yield return new WaitForSeconds(0.1f);
        Instantiate(Claw2, BearClaw_Pos2.transform.position, BearClaw_Pos2.transform.rotation);
        yield return new WaitForSeconds(0.1f);
        Instantiate(Claw1, BearClaw_Pos1.transform.position, BearClaw_Pos1.transform.rotation);
        yield return new WaitForSeconds(0.1f);
    }


    // ======================================================
    // 패턴이 생성되고 일정시간이 지나고 패턴이 파괴되어야함
    void DestroyPattern()
    {
        Debug.Log("패턴 끝");
        Destroy(this.gameObject);
    }

}
