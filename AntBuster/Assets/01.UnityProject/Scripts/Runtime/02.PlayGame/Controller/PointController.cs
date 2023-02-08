using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointController : MonoBehaviour
{
    private Image pointImg = default;
    private Color enterColor = default;
    private Color exitColor = default;



    // Start is called before the first frame update
    void Start()
    {
        pointImg = GetComponent<Image>();
        ColorUtility.TryParseHtmlString("#767676", out enterColor);
        ColorUtility.TryParseHtmlString("#FFFFFF", out exitColor);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void PointEnterChangeImage()
    {
        pointImg.color = enterColor;
    }

    public void PointExitChangeImage()
    {
        //pointImg.color = exitColor;
        pointImg.color = new Color(1f, 1f, 1f, 0.5f);
    }


    //! 클릭한 곳에 포탑생성
    public void PointClickSetTurret()
    {
        SingletonManager.Instance.SetTurret(gameObject.GetRect().anchoredPosition);
    }



}
