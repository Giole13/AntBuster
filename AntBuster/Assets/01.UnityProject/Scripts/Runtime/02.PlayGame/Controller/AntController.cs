using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntController : MonoBehaviour
{
    public float antSpeed;
    public bool isCakePicked;
    public int antLevel;
    public int antHp;


    public GameObject cakePointObj = default;
    public GameObject homePointObj = default;

    public float antMoveSpeed = 0.3f;




    private RectTransform antRect = default;

    private bool backMove = false;

    private Vector2 antDirection = default;
    private Vector2 cakePoint = default;
    private Vector2 homePoint = default;

    private AntPooling antPool = default;


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


        // 개미를 집에서 태어나게 만드는 함수
        gameObject.SetAnchoredPos(homePointObj.GetRect().anchoredPosition);


        antDirection =
        cakePointObj.GetRect().anchoredPosition -
        homePointObj.GetRect().anchoredPosition;

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
        }
        else if (antRect.anchoredPosition == homePointObj.GetRect().anchoredPosition)
        {
            //Debug.Log("집 도착 백무브 중단");
            backMove = false;
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

        if (antHp <= 0)
        {
            gameObject.SetActive(false);
            //gameObject.SetAnchoredPos(homePointObj.GetRect().anchoredPosition);
            //gameObject.SetActive(true);
            //SetAntStat();

        }
    }


    private void OnEnable()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Bullet")
        {
            antHp -= collision.transform.GetComponent<BulletController>().bulletDamage;
        }
    }       // OnTriggerEnter2D()

    public void SetAntStat()
    {
        antHp = 4;

    }

}       // class AntController

