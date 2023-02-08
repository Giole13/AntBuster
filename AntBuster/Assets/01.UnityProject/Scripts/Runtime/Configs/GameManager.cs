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
        // ������Ʈ ã�� ����
        scorePoints = GioleFunc.GetRootObj(GioleData.OBJ_NAME_UI).
            FindChildObj("ScorePoints");



        // �Ѿ� �ʱ�ȭ �����ִ� ����
        SingletonManager.Instance.SetBulletPool();
        GioleFunc.GetRootObj(GioleData.OBJ_NAME_GAMEOBJS).FindChildObj("MapTilePool").
            SetActive(false);

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


}
