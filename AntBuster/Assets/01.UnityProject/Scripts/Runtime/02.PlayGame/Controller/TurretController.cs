using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TurretController : MonoBehaviour
{
    public float attackSpeed = 1f;
    public int bulletDamage = default;
    //public int bulletCount = 10;
    public float bulletSpeed = 1.0f;

    private GameObject gunObj = default;
    private GameObject bulletObj = default;

    private GameObject bullet = default;

    public Stack<GameObject> bulletPool = default;

    private bool isGunAtcive = false;


    private Queue<GameObject> enemyQueue = default;
    private GameObject enemyObj = default;


    // Start is called before the first frame update
    void Start()
    {
        // ������Ʈ ã�� ����
        gunObj = gameObject.FindChildObj("Gun");
        bulletDamage = 1;


        bulletPool = transform.parent.GetComponent<BulletPooling>().
            SetBulletPool();

        enemyQueue = new Queue<GameObject>();

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
            enemyQueue.Enqueue(collision.gameObject);
            enemyObj = collision.gameObject;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("�� ��Ȱ��ȭ");
        // ���� ģ���� Ant�� ���
        if (collision.transform.tag == GioleData.OBJ_TAG_NAME_ANT)
        {
            enemyQueue.Dequeue();
        }
    }

    IEnumerator GunActive()
    {
        while (true)
        {
            if (0 < enemyQueue.Count)
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


                SingletonBulletPooling.Instance.UseBulletPool(gameObject, bulletDir, bulletSpeed);

                yield return new WaitForSecondsRealtime(attackSpeed);
                continue;
            }


            yield return new WaitForSecondsRealtime(1f);
        }
    }









}
