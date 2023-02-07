using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private RectTransform bulletRect = default;

    private Vector2 dir = default;
    private bool bulletFire = false;
    private float bulletSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        bulletRect = gameObject.GetRect();
        gameObject.GetRect().anchoredPosition =
            new Vector2(0, 0);

        StartCoroutine(RunBulletObj());

    }

    // Update is called once per frame
    void Update()
    {
        if (bulletFire)
        {
            bulletRect.anchoredPosition += 
                dir/* * bulletSpeed * Time.deltaTime*/;
        }
    }



    public void SetBulletDirection(Vector2 dir_,
        float bulletSpeed_)
    {
        bulletSpeed = bulletSpeed_;
        dir = dir_;
        gameObject.SetActive(true);
    }

    IEnumerator RunBulletObj()
    {
        bulletFire= true;
        yield return new WaitForSecondsRealtime(3f);
        gameObject.SetActive(false);
    }
}
