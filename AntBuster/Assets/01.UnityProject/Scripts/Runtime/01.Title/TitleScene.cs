using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : MonoBehaviour
{
    //! Ÿ��Ʋ ȭ�鿡�� ��ư�� ������ ���� ��ũ��Ʈ

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
