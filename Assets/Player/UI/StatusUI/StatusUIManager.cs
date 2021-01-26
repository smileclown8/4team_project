using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusUIManager : MonoBehaviour
{
    public GameObject PlayerStatusManager;
    public GameObject HpBar;

    public GameObject Boss_IF_CORE_Controller;
    public GameObject BossHpBar;

    // Start is called before the first frame update
    void Start()
    {
        PlayerStatusManager = GameObject.Find("PlayerStatusManager");
        HpBar = GameObject.Find("HpBar");

        BossHpBar = GameObject.Find("BossHpBar");
    }

    // Update is called once per frame
    void Update()
    {
        Boss_IF_CORE_Controller = GameObject.Find("Boss_CORE");

        HpBar.GetComponent<Image>().fillAmount = PlayerStatusManager.GetComponent<PlayerStatusManager>().player_HP /
            PlayerStatusManager.GetComponent<PlayerStatusManager>().player_MaxHP;


        BossHpBar.GetComponent<Image>().fillAmount =
            Boss_IF_CORE_Controller.GetComponent<Boss_IF_CORE_Controller>().Boss_IF_HP /
             Boss_IF_CORE_Controller.GetComponent<Boss_IF_CORE_Controller>().Boss_IF_MaxHP;

    }
}
