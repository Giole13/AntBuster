using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntController : MonoBehaviour
{
    public float antSpeed;
    public bool isCakePicked;
    public int antLevel;
    public int antHp;
    public int maxHp;

    public GameObject cakePointObj = default;
    public GameObject homePointObj = default;


    private RectTransform antRect = default;
    private AntPooling antPool = default;

    private bool backMove = false;
    private bool cakePicked = false;

    private Vector2 cakePoint = default;
    private Vector2 homePoint = default;

    public float antMoveSpeed = 0.3f;
    private float distance = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // 오브젝트 찾는 공간
        GameObject objs = GioleFunc.GetRootObj(GioleData.OBJ_NAME_GAMEOBJS);
        antRect = gameObject.GetRect();
        cakePointObj = objs.FindChildObj(GioleData.OBJ_NAME_MAPTILEPOOL).
            FindChildObj("Point(23,18)");
        homePointObj = objs.FindChildObj(GioleData.OBJ_NAME_MAPTILEPOOL).
            FindChildObj("Point(1,1)");
        antPool = objs.FindChildObj("AntPool").GetComponent<AntPooling>();

        cakePoint = cakePointObj.GetRect().anchoredPosition;
        homePoint = homePointObj.GetRect().anchoredPosition;

        SetAntStat();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetRect().anchoredPosition =
            Vector2.Lerp(homePoint, cakePoint, distance);
        if (antRect.anchoredPosition == cakePointObj.GetRect().anchoredPosition)
        {
            backMove = true;
            //Debug.Log("케잌 도착 백무브 실행");
            // 여기서 케잌 집는 bool 추가
            cakePicked = true;
            SingletonManager.Instance.CakeAniControl(false);
        }
        else if (antRect.anchoredPosition == homePointObj.GetRect().anchoredPosition)
        {
            //Debug.Log("집 도착 백무브 중단");
            backMove = false;
            //케잌을 들고 도착한다면 목숨--
            if (cakePicked)
            {
                SingletonManager.Instance.PlayerLifeControl(false);
                cakePicked= false;
            }
            else
            {
                /* Do nothing */
            }
        }

        if (backMove == false)
        {
            //Debug.Log("난 늘어나고 있어");
            distance += Time.deltaTime * antMoveSpeed;
        }
        else
        {
            //Debug.Log("난 줄어들고 있어");
            distance -= Time.deltaTime * antMoveSpeed;
        }

        // 체력이 0 이하가 되어서 사라짐
        if (antHp <= 0)
        {
            if(cakePicked == true)
            {
                SingletonManager.Instance.CakeAniControl(true   );
            }
            gameObject.SetActive(false);
            distance = 0;
            SetAntStat();
            SingletonManager.Instance.PlusScoreNum(100);
        }
    }



    // 개미가 켜지면 자동으로 집으로 간다!
    private void OnEnable()
    {
        distance = 0;
    }

    // 총알이랑 충돌했을 때
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Bullet")
        {
            antHp -= collision.transform.GetComponent<BulletController>().bulletDamage;
        }
    }       // OnTriggerEnter2D()


    public void SetAntStat()
    {
        antHp = maxHp;

    }

}       // class AntController

