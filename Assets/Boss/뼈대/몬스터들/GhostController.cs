using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{

    void Awake()
    {
        bulletPos = GameObject.Find("GhostBulletPos");
    }
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("GhostShoot", 0, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject bullet;

    public GameObject bulletPos;
    //bulletPos도 ShootPos로 이름 바꿔야함
    public Transform pos;



    //bullet의 이름과 해당 총알의 스크립트이름도 바꿔야함
    public void GhostShoot()
    {
        pos = bulletPos.transform;
        Instantiate(bullet, pos.position, transform.rotation);
    }
}
