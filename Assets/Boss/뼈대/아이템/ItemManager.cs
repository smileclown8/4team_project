using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    //스킬 아이템 획득에 대한 매니저
    // 인스펙터창에서 item_SKill_ID를 변경해주는것으로 다른 스킬도 먹게할 수 있음
    // 이 매니저가 들어가있는, 스킬아이템 오브젝트는 콜라이더에 isTrigger 체크해주기

    private SpriteRenderer spriteRenderer;
    private GameObject playerStatus;
    private int item_Skill_ID = 2;


    private void Awake()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        playerStatus = GameObject.Find("PlayerStatusManager");
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    public float turnSpeed = 30f;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, turnSpeed * Time.deltaTime);

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerStatus.GetComponent<PlayerStatusManager>().skill_ID = item_Skill_ID;
            Debug.Log(item_Skill_ID + "번 스킬 아이템 획득");
            Debug.Log(playerStatus.GetComponent<PlayerStatusManager>().skill_ID);
            Destroy(this.gameObject);
        }
    }
}
