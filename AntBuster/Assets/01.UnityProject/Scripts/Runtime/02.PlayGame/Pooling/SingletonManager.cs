using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonManager : SingletonBase<SingletonManager>
{

    private Stack<GameObject> bulletPool = default;     // ��� ���̴� Ǯ������ ����


    /// <summary>
    /// ������Ʈ Ǯ���� �Ҹ� ����
    /// </summary>
    public void SetBulletPool()
    {
        int bulletQuantity = 20;    // �Ѿ� ����
        GameObject Objs = GioleFunc.GetRootObj(GioleData.OBJ_NAME_GAMEOBJS);

        bulletPool = new Stack<GameObject>();
        GameObject bulletObj = Objs.FindChildObj("Bullet");
        GameObject bullePoolObj = Objs.FindChildObj("BulletPool");

        GameObject bullet_ = default;

        for (int i = 0; i < bulletQuantity; ++i)
        {
            bullet_ = Instantiate(bulletObj, bullePoolObj.transform);
            bullet_.name = $"Bullet {i}";

            // �Ҹ� Ǯ ���ÿ� �Ҹ� Ǯ �ֱ�
            bulletPool.Push(bullet_);
        }
    }


    /// <summary>
    /// Ǯ������ ���� ���� �Լ�
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
    /// �ͷ� �����ͼ� ���� ��ġ���ٰ� �־��ִ� �Լ�
    /// </summary>
    public void SetTurret(Vector2 position_)
    {
        GioleFunc.GetRootObj(GioleData.OBJ_NAME_UI).FindChildObj("BasicTurretIcon").
            GetComponent<BasicTurretIconController>().SetTurretToClick(position_);
    }


    /// <summary>
    /// ���ھ ������Ű�� �Լ�
    /// </summary>
    public void PlusScoreNum(int score_)
    {
        GioleFunc.GetRootObj("GameManager").GetComponent<GameManager>().
            PlusScoreNum(score_);
    }

}
