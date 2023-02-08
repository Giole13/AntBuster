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
        // ������Ʈ ã�� ����
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
            //Debug.Log("�ɟ� ���� �鹫�� ����");
            // ���⼭ �ɟ� ���� bool �߰�
            cakePicked = true;
            SingletonManager.Instance.CakeAniControl(false);
        }
        else if (antRect.anchoredPosition == homePointObj.GetRect().anchoredPosition)
        {
            //Debug.Log("�� ���� �鹫�� �ߴ�");
            backMove = false;
            //�ɟ��� ��� �����Ѵٸ� ���--
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
            //Debug.Log("�� �þ�� �־�");
            distance += Time.deltaTime * antMoveSpeed;
        }
        else
        {
            //Debug.Log("�� �پ��� �־�");
            distance -= Time.deltaTime * antMoveSpeed;
        }

        // ü���� 0 ���ϰ� �Ǿ �����
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



    // ���̰� ������ �ڵ����� ������ ����!
    private void OnEnable()
    {
        distance = 0;
    }

    // �Ѿ��̶� �浹���� ��
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

