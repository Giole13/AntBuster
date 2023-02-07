using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointMaker : MonoBehaviour
{
    private GameObject pointObj = default;

    // Start is called before the first frame update

    private void Awake()
    {
        pointObj = gameObject.FindChildObj(GioleData.OBJ_NAME_POINT);
        GameObject startPointpositionObj = gameObject.FindChildObj("MapTilePool");
        for(int y = 0; y< 20; y++)
        {
            for(int x =0; x < 25; x++)
            {
                GameObject point_ =  Instantiate(pointObj, startPointpositionObj .transform);
                point_.name = $"Point({x},{y})";
                point_.GetRect().anchoredPosition = new Vector2(x * 50, y * -37);
            }
        }
        pointObj.SetActive(false);
        
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
