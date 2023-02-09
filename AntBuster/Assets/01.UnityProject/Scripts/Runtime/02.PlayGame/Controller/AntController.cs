using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class AntController : MonoBehaviour, IPointerClickHandler
{
    public int antHp;
    public int maxHp;
    public float antMoveSpeed = 0.3f;
    public float delayTime = 3f;

    public GameObject cakePointObj = default;
    public GameObject homePointObj = default;


    private RectTransform antRect = default;
    private AntPooling antPool = default;

    private bool backMove = false;
    private bool cakePicked = false;
    private bool antMove = false;


    private Vector2 cakePoint = default;
    private Vector2 homePoint = default;
    private Vector2 antPoint = default;

    private int antLevel;
    private int deathCount = default;
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

        // 변수 초기화 하는 공간
        antLevel = 1;
        deathCount = 0;
        antMove = true;
        backMove = false;

        cakePoint = cakePointObj.GetRect().anchoredPosition;
        homePoint = homePointObj.GetRect().anchoredPosition;

        // 레벨 출력
        SingletonManager.Instance.SetLevel(antLevel);


        // 개미 스탯 초기화
        SetAntStat();

        //StartCoroutine(AntHpCheck());
    }

    // Update is called once per frame
    void Update()
    {
        // 움직이는 로직
        if (antMove)
        {
            antRect.anchoredPosition =
                Vector2.Lerp(homePoint, cakePoint, distance);
        }
        antPoint = antRect.anchoredPosition;


        // 케잌 도착
        if (Vector2.Distance(antPoint, cakePoint) == 0)
        {
            backMove = true;
            //Debug.Log("케잌 도착 백무브 실행");
            //! 케이크 들기
            cakePicked = true;
            SingletonManager.Instance.CakeAniControl(false);
        }
        // 집 도착
        else if (Vector2.Distance(antPoint, homePoint) == 0)
        {
            //Debug.Log("집 도착 백무브 중단");
            backMove = false;
            //케잌을 들고 도착한다면 목숨--
            if (cakePicked)
            {
                SingletonManager.Instance.PlayerLifeControl(false);
                cakePicked = false;
            }
        }


        if (backMove)
        {
            distance -= Time.deltaTime * antMoveSpeed;
        }
        else if (backMove == false)
        {
            distance += Time.deltaTime * antMoveSpeed;
        }
    }



    // 개미가 켜지면 자동으로 집으로 간다!
    private void OnEnable()
    {
        distance = 0;
        antMove = true;
        StartCoroutine(AntHpCheck());
    }

    private void OnDisable()
    {
        StopCoroutine(AntHpCheck());
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


    //! 개미를 클릭했을 때
    public void OnPointerClick(PointerEventData eventData)
    {
        // 개미의 정보를 표시하고싶다.
        SingletonManager.Instance.SetAntInterface(antLevel, maxHp, antHp, antMoveSpeed);
    }



    IEnumerator AntHpCheck()
    {
        while (true)
        {
            // 체력이 0 이하가 되어서 사라짐
            if (antHp <= 0)
            {
                // 이동 멈춤
                antMove = false;

                // 딜레이 시간동안 기다림
                yield return new WaitForSecondsRealtime(delayTime);

                // 케잌을 들고 죽으면 케잌 에니메이션 회복
                if (cakePicked == true)
                {
                    Debug.Log("케이크 회복?");
                    SingletonManager.Instance.CakeAniControl(true);
                }
                gameObject.SetActive(false);

                SingletonManager.Instance.PlusScoreNum(50);
                deathCount++;
                if (deathCount % 6 == 0)
                {
                    Debug.Log("여기서 레벨이 상승");
                    antLevel++;
                    SingletonManager.Instance.SetLevel(antLevel);
                }


                // 스텟 초기화
                SetAntStat();
            }
            yield return new WaitForSecondsRealtime(1f);
        }       // loop :계속돎
    }
}       // class AntController

