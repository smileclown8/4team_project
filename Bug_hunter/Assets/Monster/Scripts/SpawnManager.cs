using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://youtu.be/0RNOrEh4T4E

public class SpawnManager : MonoBehaviour
{
    public float spawnTime;
    public float curTime;
    public Transform[] spawnPoints;
    public GameObject monster;


    void Update()
    {
        if (curTime >= spawnTime)
        {
            int x = Random.Range(0, spawnPoints.Length);
            SpawnMonster(x);
        }
        curTime += Time.deltaTime;
    }

    public void SpawnMonster(int ranNum)
    {
        curTime = 0;
        Instantiate(monster, spawnPoints[ranNum]); // 숫자에 따라 그 숫자가 있는 오브젝트 위치에 몬스터 스폰
    }

}
