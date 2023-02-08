using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private RectTransform bulletRect = default;

    private Vector2 dir = default;
    private bool bulletFire = false;
    public float bulletSpeed = 1f;
    public int bulletDamage = 1;

    // Start is called before the first frame update
    void Start()
    {

        bulletRect = gameObject.GetRect();
        gameObject.SetActive(false);


    }


    // ���� �� 1�� ����
    private void OnEnable()
    {
        // �Ѿ� ������ + ���ư��ٰ� ������ �ڷ�ƾ +
        // �Ҹ� Ǯ�� �ڽ� �־��ֱ�

        StartCoroutine(RunBulletObj());

        
    }

    // Update is called once per frame
    void Update()
    {
        if (bulletFire)
        {
            bulletRect.anchoredPosition +=
                dir * bulletSpeed /* Time.deltaTime*/;
        }
    }


    /// <summary>
    /// ������ �������� ���ư��� �Լ�
    /// </summary>
    public void SetBulletDirection(Vector2 dir_,
        float bulletSpeed_)
    {
        bulletSpeed = bulletSpeed_;
        dir = dir_;
        gameObject.SetActive(true);
        //Debug.Log("�Ѿ� Ȱ��ȭ!");


    }

    IEnumerator RunBulletObj()
    {
        bulletFire = true;
        yield return new WaitForSecondsRealtime(0.5f);
        gameObject.SetActive(false);
        SingletonBulletPooling.Instance.ReloadBullet(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Ant")
        {
            gameObject.SetActive(false);
            SingletonBulletPooling.Instance.ReloadBullet(gameObject);
        }
    }       // OnTriggerEnter2D()
}

