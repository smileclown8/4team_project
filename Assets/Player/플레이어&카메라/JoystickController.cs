 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoystickController : MonoBehaviour, IPointerDownHandler,IPointerUpHandler,
    IDragHandler
{
    // 조이스틱을 이용해 플레이어를 이동시키는 스크립트
    // 좌,우 이동만 가능하게 해놨고
    // 조이스틱을 살살 움직이면 플레이어도 살살 움직이게 해놈 (근데 지금 힘을 되게 많이줘서 그냥 똑같을 수도 있음)
    // 왠만하면 건들지 말아주세요 저도 이거 잘모름. 건들때 뭘 하고싶으신지 말해주셈



    [SerializeField] private RectTransform rect_Background;
    [SerializeField] private RectTransform rect_Joystick;

    private float radius;

    private GameObject Player;
    private GameObject PlayerStatusManager;
    private Rigidbody2D playerRigidbody;
    private float joyStickDistance;
    private Vector2 joyStickvalue;


    private float maxMoveSpeed;
    private float moveSpeed;

    private bool isTouch = false;
    private Vector2 movePosition;

    public GameObject Dialogue;
    public bool isTalk;

    [HideInInspector]
    public int playerDir;

    [HideInInspector]
    Image JoystickBGImage;
    [HideInInspector]
    private GameObject Joystick;
    [HideInInspector]
    Image JoystickImage;


    void Awake()
    {
        Player = GameObject.Find("player");
        Joystick = GameObject.Find("Joystick");
        PlayerStatusManager = GameObject.Find("PlayerStatusManager");
        Dialogue = GameObject.Find("UI_Dialogue");
    }

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = Player.GetComponent<PlayerController>().playerRigidbody2D;
        moveSpeed = PlayerStatusManager.GetComponent<PlayerStatusManager>().moveSpeed;
        maxMoveSpeed = PlayerStatusManager.GetComponent<PlayerStatusManager>().maxMoveSpeed;
        radius = rect_Background.rect.width * 0.5f;

        JoystickBGImage = GetComponent<Image>();
        this.JoystickBGImage.color = new Color(1, 1, 1, 0.1f);
        JoystickImage = Joystick.GetComponent<Image>();
        JoystickImage.color = new Color(1, 1, 1, 0.1f);

    }

    // Update is called once per frame
    void Update()
    {
        isTalk = Dialogue.GetComponent<DialogueManager>().talking;

        if (isTalk == false)
        {
            if (playerDir == 1)
            {
                Player.transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (playerDir == -1)
            {
                Player.transform.eulerAngles = new Vector3(0, 180, 0);

            }
        }
        if (isTouch && isTalk == false)
        {
            playerRigidbody.AddForce(movePosition * 5, ForceMode2D.Impulse);
            //  Player.transform.position += movePosition;


            //플레이어 이동속도 최대 제한
            if (playerRigidbody.velocity.x > maxMoveSpeed)
                playerRigidbody.velocity = new Vector2(maxMoveSpeed, playerRigidbody.velocity.y);
            else if (playerRigidbody.velocity.x < maxMoveSpeed * -1)
                playerRigidbody.velocity = new Vector2(maxMoveSpeed * -1, playerRigidbody.velocity.y);

        }

    }

    //player의 Linear Drag(공기저항)을 1로 설정해서 미끄러지는걸 방지하기

    public void OnDrag(PointerEventData eventData)
    {
        joyStickvalue = eventData.position - (Vector2)rect_Background.position;

        joyStickvalue = Vector2.ClampMagnitude(joyStickvalue, radius);
        rect_Joystick.localPosition = joyStickvalue;

        joyStickDistance = Vector2.Distance(rect_Background.position,
            rect_Joystick.position) / radius;
        joyStickvalue = joyStickvalue.normalized;
        movePosition = new Vector3(joyStickvalue.x * joyStickDistance * moveSpeed * Time.deltaTime
          , 0, 0);

        if(joyStickvalue.x < 0)
        {
            playerDir = -1;
        }

        if (joyStickvalue.x > 0)
        {
            playerDir = 1;
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        this.JoystickBGImage.color = new Color(0, 0, 0, 1);
        JoystickImage.color = new Color(1, 1, 1, 1);
        isTouch = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        this.JoystickBGImage.color = new Color(0, 0, 0, 0.1f);
        JoystickImage.color = new Color(1, 1, 1, 0.1f);
        isTouch = false;
        rect_Joystick.localPosition = Vector3.zero;
        movePosition = Vector3.zero;
        playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.normalized.x * 0.5f, playerRigidbody.velocity.y);
    }

}
