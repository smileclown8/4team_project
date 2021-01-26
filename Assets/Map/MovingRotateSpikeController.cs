using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingRotateSpikeController : MonoBehaviour
{

    //움직이는 플랫폼을 관리해주는 컨트롤러

    // 빈 오브젝트로 MovingPlatform을 만들고,
    //그 자식으로 실제로 플랫폼이미지+리지드바디+콜라이더가 들어가는 찐 MovingPlatform과 시작,끝의 위치를 가르키는 빈오브젝트 start,end를 만든다.
    // 인스펙터창의 startPos에는 오브젝트 start를, endPos에는 오브젝트 end를 넣어준다.
    // 그러면 플랫폼이 start위치로갔다가 다시 end위치로 갔다가.. 반복운동함
    // 이동속도는 PlatfromMovespeed를 건드리면된다.
    // 플랫폼이 끝에서 멈추는 시간은 맨아랫줄의 waitrforseconds()안의 값을 바꿔준다.

    // 움직이는 플랫포머 스크립트에 톱날의 회전값만 업데이트에 추가
    
    public Transform startPos; //한쪽 끝
    public Transform endPos; // 다른쪽 끝

    public Transform desPos; //목적지

    public float PlatformMovespeed;
    void Start()
    {
        transform.position = startPos.position;
        desPos = endPos;

        StartCoroutine(MovingPlatform());
    }
    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, 1) * 180 * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }


    IEnumerator MovingPlatform()
    {
        while (true)
        {
            transform.position = Vector2.MoveTowards(transform.position, desPos.position, Time.deltaTime * PlatformMovespeed);
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
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

}
