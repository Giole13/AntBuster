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
        // ������Ʈ ã�� ����
        gunObj = gameObject.FindChildObj("Gun");
        bulletDamage = 1;

        bulletPool = new Stack<GameObject>();





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
        Debug.Log("�� ��Ȱ��ȭ");
        // ���� ģ���� Ant�� ���
        if (collision.transform.tag == GioleData.OBJ_TAG_NAME_ANT)
        {
            gunAtive = false;
        }
    }

    IEnumerator GunActive(Collider2D collision)
    {
        while (gunAtive)
        {
            Debug.Log("�� Ȱ��ȭ!");
            // �ѿ� ���ư��� ����
            Vector3 relative = transform.InverseTransformPoint
            (collision.transform.position);
            float angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
            gunObj.transform.rotation = Quaternion.Euler(0, 0, -angle);

            Vector2 bulletDir =
            (collision.gameObject.GetRect().anchoredPosition -
            gameObject.GetRect().anchoredPosition).normalized;

            GameObject bullet = bulletPool.Pop();
            bullet.GetComponent<BulletController>().
                SetBulletDirection(bulletDir, bulletSpeed);


            yield return new WaitForSeconds(attackSpeed);
        }
    }

    public void SetBulletPool(Stack<GameObject> bulletPool_)
    {
        bulletPool = bulletPool_;
    }







}
