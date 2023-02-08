using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonBulletPooling : SingletonBase<SingletonBulletPooling>
{

    private Stack<GameObject> bulletPool = default;
    private int bulletQuantity = 20;


    // 오브젝트 풀링할 불릿 선택
    public void SetBulletPool()
    {
        GameObject Objs = GioleFunc.GetRootObj(GioleData.OBJ_NAME_GAMEOBJS);

        bulletPool = new Stack<GameObject>();
        GameObject bulletObj = Objs.FindChildObj("Bullet");
        GameObject bullePoolObj = Objs.FindChildObj("BulletPool");

        GameObject bullet_ = default;

        for(int i  =0; i < bulletQuantity; ++i)
        {
            bullet_ = Instantiate(bulletObj, bullePoolObj.transform);
            bullet_.name = $"Bullet {i}";

            // 불릿 풀 스택에 불릿 풀 넣기
            bulletPool.Push(bullet_);
        }
    }



    // 풀링에서 꺼내 쓰는 함수
    public void UseBulletPool(GameObject myselfObj, Vector2 dir, float bulletSpeed)
    {
        myselfObj.GetRect();
        GameObject bullet_ = bulletPool.Pop();
        bullet_.SetAnchoredPos(myselfObj.GetRect().anchoredPosition);
        bullet_.GetComponent<BulletController>().
            SetBulletDirection(dir, bulletSpeed);
    }

    public void ReloadBullet(GameObject bullet_)
    {
        bulletPool.Push(bullet_);
    }



}
