using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TurretController : MonoBehaviour
{
    public float attackSpeed = 0.1f;
    public int bulletDamage = default;
    //public int bulletCount = 10;
    public float bulletSpeed = 1.0f;

    private GameObject gunObj = default;
    private GameObject bulletObj = default;



    public Stack<GameObject> bulletPool = default;

    private bool gunAtive = false;

    // Start is called before the first frame update
    void Start()
    {
        // 오브젝트 찾는 공간
        gunObj = gameObject.FindChildObj("Gun");
        bulletDamage = 1;

        //bulletPool = new Stack<GameObject>();


        bulletPool = transform.parent.GetComponent<BulletPooling>().
            SetBulletPool();


        //bulletObj = gameObject.FindChildObj("Bullet");

        //GameObject bullet = default;
        //for (int i = 0; i < bulletCount; ++i)
        //{
        //    bullet = Instantiate(bulletObj, transform);
        //    bullet.name = $"Bullet({i})";
        //    bulletPool.Push(bullet);
        //}


    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == GioleData.OBJ_TAG_NAME_ANT)
        {
            gunAtive = true;
            StartCoroutine(GunActive(collision));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("건 비활성화");
        // 나간 친구가 Ant일 경우
        if (collision.transform.tag == GioleData.OBJ_TAG_NAME_ANT)
        {
            gunAtive = false;
        }
    }

    IEnumerator GunActive(Collider2D collision)
    {

        while (gunAtive)
        {
            Debug.Log("건 활성화!");
            // 총열 돌아가는 로직
            Vector3 relative = transform.InverseTransformPoint
            (collision.transform.position);
            float angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
            gunObj.transform.rotation = Quaternion.Euler(0, 0, -angle);

            // 날아갈 방향 정하는 로직
            Vector2 bulletDir =
            (collision.gameObject.GetRect().anchoredPosition -
            gameObject.GetRect().anchoredPosition).normalized;

            //Debug.Log("불릿 폴링 이다! "+ bulletPool);
            // 총알 꺼내서 발사하는 로직
            GameObject bullet = bulletPool.Pop();
            bullet.GetComponent<BulletController>().
                SetBulletDirection(bulletDir, bulletSpeed);
            
            //Debug.Log("총알 발사하고 나옴!");


            yield return new WaitForSeconds(attackSpeed);
        }
    }

    







}
