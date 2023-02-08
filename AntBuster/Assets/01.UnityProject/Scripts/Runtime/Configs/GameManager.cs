using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject scorePoints = default;
    private int scorePointInt = 0;
    private int playerLife = 0;
    private int playerNumInt = 0;
    private GameObject gameOverText = default;
    private Animator cakeAnimator = default;


    // Start is called before the first frame update
    void Start()
    {
        // ������Ʈ ã�� ����
        scorePoints = GioleFunc.GetRootObj(GioleData.OBJ_NAME_UI).
            FindChildObj("ScorePoints");


        // �Ѿ� �ʱ�ȭ �����ִ� ����
        SingletonManager.Instance.SetBulletPool();
        GioleFunc.GetRootObj(GioleData.OBJ_NAME_GAMEOBJS).FindChildObj("MapTilePool").
            SetActive(false);
        gameOverText = GioleFunc.GetRootObj(GioleData.OBJ_NAME_UI).
            FindChildObj("GameOverText");
        cakeAnimator = GioleFunc.GetRootObj(GioleData.OBJ_NAME_GAMEOBJS).
            FindChildObj("Cake").GetComponent<Animator>();



        // �ν��Ͻ� ����
        gameOverText.SetActive(false);


        // ���� �ʱ�ȭ
        playerLife = 8;
        playerNumInt = 8;


        // �ִϸ����� �Ķ���� ����
        cakeAnimator.SetInteger("CakeNum", playerLife);
    }

    // Update is called once per frame
    void Update()
    {

    }


    // ���ھ� ���� �Լ�
    public void PlusScoreNum(int score_)
    {
        scorePointInt += score_;
        GioleFunc.SetTmpText(scorePoints, $"{scorePointInt}");
    }


    // �÷��̾� ������ ����
    public void PlayerLifeMinus()
    {
        playerLife--;

        if (playerLife <= 0)
        {
            // ���ӿ���
            gameOverText.SetActive(true);
        }
    }

    // �÷��̾� ���̺� ȸ��
    public void PlayerLifePlus()
    {
        playerLife++;
    }


    // ����ũ�� ���� �ִϸ��̼��� �ٿ��ִ� �Լ�
    public void CakeAnimatorMinus()
    {
        cakeAnimator.SetInteger("CakeNum", playerNumInt--);
    }

    public void CakeAnimatorPlus()
    {
        cakeAnimator.SetInteger("CakeNum", playerNumInt++);
    }

}
