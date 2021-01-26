using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusManager : MonoBehaviour
{
    // 플레이어의 각종 스테이터스값들을 관리해주는 매니저
    // 버프,피격, 등등.. 별별 상황이 발생했을때 이 곳의 값을 바꿔준다.

    //플레이어의 최대 이동속도
    public float maxMoveSpeed = 10f;

    //이름은 moveSpeed로 했지만, 실제로는 플레이어가 이동(좌,우)할때 받는 힘의 양을 조절해줌
    //클수록 maxMoveSpeed에 도달하는 시간이 짧아짐. 자동차의 제로백이라고 생각하면 된다.
    //값이 클수록 이동이 부드러워지는걸 확인. 60~80정도를 추천
    public float moveSpeed = 80f;


    //플레이어가 점프를 할때 위로 올려주는 힘
    //35정도 추천
    //플레이어가 점프를 하고 나면, 다시 떨어져야하는데 너무 천천히 떨어질 수 있다.
    //그러니 플레이어의 리지드바디2D의 그래비티 스케일을 조절해주자. ( 8 추천 )
    public float jumpPower = 35f;


    public float player_MaxHP = 100;
    public float player_HP = 100;
    public int skill_ID;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
