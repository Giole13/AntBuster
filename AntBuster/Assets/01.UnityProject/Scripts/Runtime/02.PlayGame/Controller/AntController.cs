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



    private float distance = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // ������Ʈ ã�� ����
        antRect = gameObject.GetRect();
        cakePointObj = GioleFunc.GetRootObj(GioleData.OBJ_NAME_GAMEOBJS).
            FindChildObj(GioleData.OBJ_NAME_MAPTILEPOOL).FindChildObj("Point(23,18)");
        homePointObj = GioleFunc.GetRootObj(GioleData.OBJ_NAME_GAMEOBJS).
            FindChildObj(GioleData.OBJ_NAME_MAPTILEPOOL).FindChildObj("Point(1,1)");


        // ���̸� ������ �¾�� ����� �Լ�
        gameObject.SetAnchoredPos(homePointObj.GetRect().anchoredPosition);

        
        antDirection =
        cakePointObj.GetRect().anchoredPosition -
        homePointObj.GetRect().anchoredPosition;

        cakePoint = cakePointObj.GetRect().anchoredPosition;
        homePoint = homePointObj.GetRect().anchoredPosition;


        antHp = 4;

    }

    // Update is called once per frame
    void Update()
    {
        if (antRect.anchoredPosition == cakePointObj.GetRect().anchoredPosition)
        {
            backMove = true;
            //Debug.Log("�ɟ� ���� �鹫�� ����");
        }
        else if (antRect.anchoredPosition == homePointObj.GetRect().anchoredPosition)
        {
            //Debug.Log("�� ���� �鹫�� �ߴ�");
            backMove = false;
        }



        gameObject.GetRect().anchoredPosition =
            Vector2.Lerp(homePoint, cakePoint, distance);

        if (backMove == false)
        {
            //Debug.Log("�� �þ�� �־�");
            distance += Time.deltaTime * antMoveSpeed;
        }
        else
        {
            //Debug.Log("�� �پ��� �־�");
            distance -= Time.deltaTime * antMoveSpeed;
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Bullet")
        {
            antHp -= collision.transform.parent.GetComponent<TurretController>().bulletDamage;
        }
    }       // OnTriggerEnter2D()



}       // class AntController

