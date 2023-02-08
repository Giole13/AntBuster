using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject scorePoints = default;
    private int scorePointInt = 0;


    // Start is called before the first frame update
    void Start()
    {
        // 오브젝트 찾는 구문
        scorePoints = GioleFunc.GetRootObj(GioleData.OBJ_NAME_UI).
            FindChildObj("ScorePoints");



        // 총알 초기화 시켜주는 구문
        SingletonManager.Instance.SetBulletPool();
        GioleFunc.GetRootObj(GioleData.OBJ_NAME_GAMEOBJS).FindChildObj("MapTilePool").
            SetActive(false);

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


}
