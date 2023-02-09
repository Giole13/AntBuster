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
        // 오브젝트 찾는 공간
        gunObj = gameObject.FindChildObj("Gun");
        bulletDamage = 1;

        // 초기화 공간
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
        //Debug.Log("건 비활성화");
        // 나간 친구가 Ant일 경우
        if (collision.transform.tag == GioleData.OBJ_TAG_NAME_ANT)
        {
            enemyList.Remove(collision.gameObject);
        }
    }

    //! 계속돌아가다가 적 발견시에 사격
    IEnumerator GunActive()
    {
        while (true)
        {
            if (0 < enemyList.Count)
            {

                //Debug.Log("건 활성화!");
                // 총열 돌아가는 로직
                Vector3 relative = transform.InverseTransformPoint
                (enemyObj.transform.position);
                float angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
                gunObj.transform.rotation = Quaternion.Euler(0, 0, -angle);

                // 날아갈 방향 정하는 로직
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


    // 터렛을 클릭했을 때 실행되는 함수
    public void OnPointerClick(PointerEventData eventData)
    {
        // 터렛의 정보를 띄워주는 함수
        SingletonManager.Instance.
            SetTurretInterface(attackSpeed, range, bulletSpeed, bulletDamage);
    }
}
