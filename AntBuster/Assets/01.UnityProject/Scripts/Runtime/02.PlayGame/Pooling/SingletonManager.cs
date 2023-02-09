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


    /// <summary>
    /// �÷��̾� ��� ���� �Լ�
    /// </summary>
    /// <param name="typeBool">true�� ����, false�� ����</param>
    public void PlayerLifeControl(bool typeBool)
    {
        GameManager manager = GioleFunc.GetRootObj("GameManager").
            GetComponent<GameManager>();
        if (typeBool == true)
        {
            manager.PlayerLifePlus();
        }
        else if (typeBool == false)
        {
            manager.PlayerLifeMinus();
        }
    }


    /// <summary>
    /// ����ũ�� �ִϸ��̼� ���� �Լ�
    /// </summary>
    /// <param name="typeBool">true�� ����, false�� ����</param>
    public void CakeAniControl(bool typeBool)
    {
        GameManager manager = GioleFunc.GetRootObj("GameManager").
            GetComponent<GameManager>();
        if (typeBool == true)
        {
            manager.CakeAnimatorPlus();
        }
        else if (typeBool == false)
        {
            manager.CakeAnimatorMinus();
        }
    }

    /// <summary>
    /// �����ϸ� ��ž�� ������ �����ִ� �Լ�
    /// </summary>
    /// ���ݼӵ�, ��Ÿ�, ����ü �ӵ�, ���ݷ�
    public void SetTurretInterface(float attackSpeed_, float range_,
        float bulletSpeed_, int bulletDamage_)
    {
        GameObject turretInterfaceObj = GioleFunc.GetRootObj(GioleData.OBJ_NAME_UI).
            FindChildObj(GioleData.OBJ_NAME_InformationWindow);

        turretInterfaceObj.SetActive(true);
        turretInterfaceObj.SetTmpText(
            $"���� �ӵ� : {attackSpeed_}\t��Ÿ� : {range_}\n" +
            $"����ü �ӵ� : {bulletSpeed_}\t���ݷ� : {bulletDamage_}"
            );
    }


    /// <summary>
    /// �����ϸ� ������ ������ �����ִ� �Լ�
    /// </summary>
    public void SetAntInterface(int level_, int antMaxHp_,int antCurrentHp_, float antSpeed_)
    {
        GameObject turretInterfaceObj = GioleFunc.GetRootObj(GioleData.OBJ_NAME_UI).
            FindChildObj(GioleData.OBJ_NAME_InformationWindow);

        turretInterfaceObj.SetActive(true);
        turretInterfaceObj.SetTmpText(
            $"���� : {level_}\n" +
            $"ü�� : {antCurrentHp_} / {antMaxHp_}\t" +
            $"�̵��ӵ� : {antSpeed_}"
            );
    }


    /// <summary>
    /// ������ �÷����ִ� �Լ�
    /// </summary>
    public void SetLevel(int level_)
    {
        GameObject levelObj = GioleFunc.GetRootObj(GioleData.OBJ_NAME_UI).
            FindChildObj("Level");

        levelObj.SetTmpText($"{level_}");

    }
}
