using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonManager : SingletonBase<SingletonManager>
{

    private Stack<GameObject> bulletPool = default;     // 계속 쓰이는 풀링전용 스택


    /// <summary>
    /// 오브젝트 풀링할 불릿 선택
    /// </summary>
    public void SetBulletPool()
    {
        int bulletQuantity = 20;    // 총알 갯수
        GameObject Objs = GioleFunc.GetRootObj(GioleData.OBJ_NAME_GAMEOBJS);

        bulletPool = new Stack<GameObject>();
        GameObject bulletObj = Objs.FindChildObj("Bullet");
        GameObject bullePoolObj = Objs.FindChildObj("BulletPool");

        GameObject bullet_ = default;

        for (int i = 0; i < bulletQuantity; ++i)
        {
            bullet_ = Instantiate(bulletObj, bullePoolObj.transform);
            bullet_.name = $"Bullet {i}";

            // 불릿 풀 스택에 불릿 풀 넣기
            bulletPool.Push(bullet_);
        }
    }


    /// <summary>
    /// 풀링에서 꺼내 쓰는 함수
    /// </summary>
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


    /// <summary>
    /// 터렛 가져와서 지정 위치에다가 넣어주는 함수
    /// </summary>
    public void SetTurret(Vector2 position_)
    {
        GioleFunc.GetRootObj(GioleData.OBJ_NAME_UI).FindChildObj("BasicTurretIcon").
            GetComponent<BasicTurretIconController>().SetTurretToClick(position_);
    }


    /// <summary>
    /// 스코어를 증가시키는 함수
    /// </summary>
    public void PlusScoreNum(int score_)
    {
        GioleFunc.GetRootObj("GameManager").GetComponent<GameManager>().
            PlusScoreNum(score_);
    }

}
