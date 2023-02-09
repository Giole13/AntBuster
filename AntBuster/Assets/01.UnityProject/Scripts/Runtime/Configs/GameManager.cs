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
    private GameObject informationWindowObj = default;

    // Start is called before the first frame update
    void Start()
    {
        // 오브젝트 찾는 공간
        scorePoints = GioleFunc.GetRootObj(GioleData.OBJ_NAME_UI).
            FindChildObj("ScorePoints");
        informationWindowObj = GioleFunc.GetRootObj(GioleData.OBJ_NAME_UI).
            FindChildObj(GioleData.OBJ_NAME_InformationWindow);

        informationWindowObj.SetActive(false);

        // 총알 초기화 시켜주는 공간
        SingletonManager.Instance.SetBulletPool();
        GioleFunc.GetRootObj(GioleData.OBJ_NAME_GAMEOBJS).FindChildObj("MapTilePool").
            SetActive(false);
        gameOverText = GioleFunc.GetRootObj(GioleData.OBJ_NAME_UI).
            FindChildObj("GameOverText");
        cakeAnimator = GioleFunc.GetRootObj(GioleData.OBJ_NAME_GAMEOBJS).
            FindChildObj("Cake").GetComponent<Animator>();



        // 인스턴스 설정
        gameOverText.SetActive(false);


        // 변수 초기화
        playerLife = 8;
        playerNumInt = 8;


        // 애니메이터 파라미터 조정
        cakeAnimator.SetInteger("CakeNum", playerLife);
    }

    // Update is called once per frame
    void Update()
    {

    }


    // 스코어 증가 함수
    public void PlusScoreNum(int score_)
    {
        scorePointInt += score_;
        GioleFunc.SetTmpText(scorePoints, $"{scorePointInt}");
    }


    // 플레이어 라이프 감소
    public void PlayerLifeMinus()
    {
        --playerLife;

        if (playerLife <= 0)
        {
            // 게임오버
            gameOverText.SetActive(true);
        }
    }

    // 플레이어 라이브 회복
    public void PlayerLifePlus()
    {
        ++playerLife;
    }


    // 케이크의 개수 애니메이션을 줄여주는 함수
    public void CakeAnimatorMinus()
    {
        cakeAnimator.SetInteger("CakeNum", --playerNumInt);
    }

    // 케이크의 개수 애니메이션을 올려주는 함수
    public void CakeAnimatorPlus()
    {
        cakeAnimator.SetInteger("CakeNum", ++playerNumInt);
    }

}
