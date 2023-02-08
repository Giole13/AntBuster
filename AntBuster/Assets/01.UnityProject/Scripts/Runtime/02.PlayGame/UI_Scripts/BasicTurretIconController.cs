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
    private GameObject mapTilePoolObj = default;

    // Start is called before the first frame update
    void Start()
    {
        // ������Ʈ ã�� ����
        basicTurretObj = GioleFunc.GetRootObj(GioleData.OBJ_NAME_GAMEOBJS).
            FindChildObj(GioleData.OBJ_NAME_BASICTURRET);
        uiObj = GioleFunc.GetRootObj(GioleData.OBJ_NAME_UI);
        mapTilePoolObj = GioleFunc.GetRootObj(GioleData.OBJ_NAME_GAMEOBJS).
            FindChildObj("MapTilePool");


        // ������Ʈ ã�� ����
        turretObjRect = basicTurretObj.GetRect();

        // �⺻ ����
        isClicked = false;
        basicTurretObj.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        // { LAGACY
        //if (isClicked == true)
        //{
        //    basicTurretObj.AddAnchoredPos(
        //        mouseData.delta /
        //        uiObj.GetComponentMust<Canvas>().scaleFactor);

        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        Instantiate(basicTurretObj,
        //            basicTurretObj.transform.parent);

        //        isClicked = false;
        //        basicTurretObj.SetActive(false);
        //    }
        //}
        // } LAGACY
    }

    //! ���콺�� Ŭ������ �� ����
    public void OnPointerClick(PointerEventData eventData)
    {
        basicTurretObj.SetActive(true);
        mouseData = eventData;
        turretObjRect.anchoredPosition = gameObject.GetRect().anchoredPosition;
        isClicked = !isClicked;
        mapTilePoolObj.SetActive(true);
    }

    public void SetTurretToClick(Vector2 position_)
    {
        if (isClicked == true)
        {
            basicTurretObj.SetAnchoredPos(position_);
            Instantiate(basicTurretObj, basicTurretObj.transform.parent);
            isClicked = false;
            basicTurretObj.SetActive(false);
            mapTilePoolObj.SetActive(false);
        }
    }

    // { LAGACY
    //// �巡�� �Լ�
    //public void OnDrag(PointerEventData eventData)
    //{
    //    Debug.Log("�µ巹��!");
    //}
    // } LAGACY
}
