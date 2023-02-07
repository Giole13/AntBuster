using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.ParticleSystem;

public class BasicTurretIconController : MonoBehaviour, IPointerClickHandler
    
{

    private bool isClicked = false;
    private PointerEventData mouseData = default;
    private RectTransform turretObjRect = default;

    private GameObject uiObj = default;
    private GameObject basicTurretObj = default;

    // Start is called before the first frame update
    void Start()
    {
        // 오브젝트 찾는 공간
        basicTurretObj = GioleFunc.GetRootObj(GioleData.OBJ_NAME_GAMEOBJS)
            .FindChildObj(GioleData.OBJ_NAME_BASICTURRET);
        uiObj = GioleFunc.GetRootObj(GioleData.OBJ_NAME_UI);

        // 컴포넌트 찾는 공간
        turretObjRect = basicTurretObj.GetRect();





        // 기본 셋팅
        isClicked = false;
        basicTurretObj.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (isClicked == true)
        {
            basicTurretObj.AddAnchoredPos(
                mouseData.delta / 
                uiObj.GetComponentMust<Canvas>().scaleFactor);

            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(basicTurretObj,
                    basicTurretObj.transform.parent);

                isClicked = false;
                basicTurretObj.SetActive(false);
            }
        }
    }



    //! 마우스로 클릭했을 때 실행
    public void OnPointerClick(PointerEventData eventData)
    {
        basicTurretObj.SetActive(true);
        mouseData = eventData;
        turretObjRect.anchoredPosition = gameObject.GetRect().anchoredPosition;
        isClicked = !isClicked;
    }


    // { LAGACY
    //// 드래그 함수
    //public void OnDrag(PointerEventData eventData)
    //{
    //    Debug.Log("온드레그!");
    //}
    // } LAGACY
}
