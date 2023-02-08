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


    // 켜질 때 1번 실행
    private void OnEnable()
    {
        // 총알 움직임 + 날아가다가 꺼지는 코루틴 +
        // 불릿 풀에 자신 넣어주기

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
    /// 설정한 방향으로 날아가는 함수
    /// </summary>
    public void SetBulletDirection(Vector2 dir_,
        float bulletSpeed_)
    {
        bulletSpeed = bulletSpeed_;
        dir = dir_;
        gameObject.SetActive(true);
        //Debug.Log("총알 활성화!");


    }

    IEnumerator RunBulletObj()
    {
        bulletFire = true;
        yield return new WaitForSecondsRealtime(0.5f);
        gameObject.SetActive(false);
        SingletonManager.Instance.ReloadBullet(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Ant")
        {
            gameObject.SetActive(false);
            SingletonManager.Instance.ReloadBullet(gameObject);
        }
    }       // OnTriggerEnter2D()
}

