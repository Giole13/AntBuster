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
        // ������Ʈ ã�� ����
        GameObject objs = GioleFunc.GetRootObj(GioleData.OBJ_NAME_GAMEOBJS);
        antRect = gameObject.GetRect();
        cakePointObj = objs.FindChildObj(GioleData.OBJ_NAME_MAPTILEPOOL).
            FindChildObj("Point(23,18)");
        homePointObj = objs.FindChildObj(GioleData.OBJ_NAME_MAPTILEPOOL).
            FindChildObj("Point(1,1)");
        antPool = objs.FindChildObj("AntPool").GetComponent<AntPooling>();

        // ���� �ʱ�ȭ �ϴ� ����
        antLevel = 1;
        deathCount = 0;
        antMove = true;
        backMove = false;

        cakePoint = cakePointObj.GetRect().anchoredPosition;
        homePoint = homePointObj.GetRect().anchoredPosition;

        // ���� ���
        SingletonManager.Instance.SetLevel(antLevel);


        // ���� ���� �ʱ�ȭ
        SetAntStat();

        //StartCoroutine(AntHpCheck());
    }

    // Update is called once per frame
    void Update()
    {
        // �����̴� ����
        if (antMove)
        {
            antRect.anchoredPosition =
                Vector2.Lerp(homePoint, cakePoint, distance);
        }
        antPoint = antRect.anchoredPosition;


        // �ɟ� ����
        if (Vector2.Distance(antPoint, cakePoint) == 0)
        {
            backMove = true;
            //Debug.Log("�ɟ� ���� �鹫�� ����");
            //! ����ũ ���
            cakePicked = true;
            SingletonManager.Instance.CakeAniControl(false);
        }
        // �� ����
        else if (Vector2.Distance(antPoint, homePoint) == 0)
        {
            //Debug.Log("�� ���� �鹫�� �ߴ�");
            backMove = false;
            //�ɟ��� ��� �����Ѵٸ� ���--
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



    // ���̰� ������ �ڵ����� ������ ����!
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


    //! ���̸� Ŭ������ ��
    public void OnPointerClick(PointerEventData eventData)
    {
        // ������ ������ ǥ���ϰ�ʹ�.
        SingletonManager.Instance.SetAntInterface(antLevel, maxHp, antHp, antMoveSpeed);
    }



    IEnumerator AntHpCheck()
    {
        while (true)
        {
            // ü���� 0 ���ϰ� �Ǿ �����
            if (antHp <= 0)
            {
                // �̵� ����
                antMove = false;

                // ������ �ð����� ��ٸ�
                yield return new WaitForSecondsRealtime(delayTime);

                // �ɟ��� ��� ������ �ɟ� ���ϸ��̼� ȸ��
                if (cakePicked == true)
                {
                    Debug.Log("����ũ ȸ��?");
                    SingletonManager.Instance.CakeAniControl(true);
                }
                gameObject.SetActive(false);

                SingletonManager.Instance.PlusScoreNum(50);
                deathCount++;
                if (deathCount % 6 == 0)
                {
                    Debug.Log("���⼭ ������ ���");
                    antLevel++;
                    SingletonManager.Instance.SetLevel(antLevel);
                }


                // ���� �ʱ�ȭ
                SetAntStat();
            }
            yield return new WaitForSecondsRealtime(1f);
        }       // loop :��ӵ�
    }
}       // class AntController

