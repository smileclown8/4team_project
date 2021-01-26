using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Damage : MonoBehaviour
{
    [SerializeField]
    public float damage;

    public GameObject PlayerStatusManager;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerStatusManager = GameObject.Find("PlayerStatusManager");
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerStatusManager.GetComponent<PlayerStatusManager>().player_HP -= damage;
        }   
    }
}
