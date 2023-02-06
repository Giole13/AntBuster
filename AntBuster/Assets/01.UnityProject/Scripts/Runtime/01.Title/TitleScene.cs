using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : MonoBehaviour
{
    //! 타이틀 화면에서 버튼의 조작을 위한 스크립트

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickPlayGame()
    {
        GioleFunc.LoadScene(GioleData.SCENE_NAME_PLAY);
    }
}
