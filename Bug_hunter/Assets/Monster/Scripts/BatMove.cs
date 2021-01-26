using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 상하 이동 강좌 https://youtu.be/4R_AdDK25kQ

public class BatMove : MonoBehaviour
{
    public float speed = 2f;        // 이동속도

    private Vector3 startPos;       // 시작점
    private Vector3 endPos;         // 끝점
    private Vector3 nextPos;        // 목적지
    
    [SerializeField]
    private Transform currentPos;   // 현재위치
    [SerializeField]
    private Transform desPos;       // 도달위치



    void Start()
    {
        startPos = currentPos.localPosition;
        endPos = desPos.localPosition;
        nextPos = endPos;
    }

    void Update()
    {
        Move();
    }

    public void Move() // 이동
    {
        currentPos.localPosition = Vector3.MoveTowards(currentPos.transform.localPosition, nextPos, speed * Time.deltaTime);
        if (Vector3.Distance(currentPos.localPosition, nextPos) <= 0.1)
        {
            ChangeDestination();
        }
    }
    private void ChangeDestination()    // 방향 전환
    {
        nextPos = nextPos != startPos ? startPos : endPos;
    }


}
