using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public class TurretController : MonoBehaviour, IPointerClickHandler
{
    public float attackSpeed = 1f;
    public int bulletDamage = default;
    //public int bulletCount = 10;
    public float bulletSpeed = 1.0f;



    private GameObject gunObj = default;
    private GameObject bulletObj = default;

    private GameObject bullet = default;


    private bool isGunAtcive = false;

    private float range = 0f;

    private List<GameObject> enemyList = default;
    private GameObject enemyObj = default;


    // Start is called before the first frame update
    void Start()
    {
        // ������Ʈ ã�� ����
        gunObj = gameObject.FindChildObj("Gun");
        bulletDamage = 1;

        // �ʱ�ȭ ����
        enemyList = new List<GameObject>();
        range = GetComponent<CircleCollider2D>().radius;


        StartCoroutine(GunActive());
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == GioleData.OBJ_TAG_NAME_ANT)
        {
            enemyList.Add(collision.gameObject);
            enemyObj = collision.gameObject;

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == GioleData.OBJ_TAG_NAME_ANT)
        {
            if (collision.transform.GetComponent<AntController>().antHp <= 0)
            {
                enemyList.Remove(collision.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("�� ��Ȱ��ȭ");
        // ���� ģ���� Ant�� ���
        if (collision.transform.tag == GioleData.OBJ_TAG_NAME_ANT)
        {
            enemyList.Remove(collision.gameObject);
        }
    }

    //! ��ӵ��ư��ٰ� �� �߽߰ÿ� ���
    IEnumerator GunActive()
    {
        while (true)
        {
            if (0 < enemyList.Count)
            {

                //Debug.Log("�� Ȱ��ȭ!");
                // �ѿ� ���ư��� ����
                Vector3 relative = transform.InverseTransformPoint
                (enemyObj.transform.position);
                float angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
                gunObj.transform.rotation = Quaternion.Euler(0, 0, -angle);

                // ���ư� ���� ���ϴ� ����
                Vector2 bulletDir =
                (enemyObj.gameObject.GetRect().anchoredPosition -
                gameObject.GetRect().anchoredPosition).normalized;


                SingletonManager.Instance.UseBulletPool(gameObject, bulletDir, bulletSpeed);

                yield return new WaitForSecondsRealtime(attackSpeed);
                continue;
            }


            yield return new WaitForSecondsRealtime(1f);
        }
    }


    // �ͷ��� Ŭ������ �� ����Ǵ� �Լ�
    public void OnPointerClick(PointerEventData eventData)
    {
        // �ͷ��� ������ ����ִ� �Լ�
        SingletonManager.Instance.
            SetTurretInterface(attackSpeed, range, bulletSpeed, bulletDamage);
    }
}
