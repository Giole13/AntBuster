using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntPooling : MonoBehaviour
{
    public int antQuantity = 6;


    private GameObject antObj = default;
    public List<GameObject> antPoolStack = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {

        GameObject gameObjs = GioleFunc.GetRootObj(GioleData.OBJ_NAME_GAMEOBJS);

        antObj = gameObjs.FindChildObj("Ant");
        antObj.SetActive(false);

        // 스택에 개미 저장
        for (int i = 0; i < antQuantity; ++i)
        {
            GameObject ant_ = Instantiate(antObj, transform);
            antPoolStack.Add(ant_);
            ant_.SetActive(false);
        }

        // 코루틴으로 계속 호출
        StartCoroutine(MakeAntToHome());

    }

    // Update is called once per frame
    void Update()
    {

    }



    IEnumerator MakeAntToHome()
    {
        while (true)
        {
            foreach (GameObject ant in antPoolStack)
            {
                if (ant.activeSelf == false)
                {
                    //ant.SetAnchoredPos(new Vector2(0, 0));
                    ant.SetActive(true);
                }
                yield return new WaitForSecondsRealtime(1f);
            }
            //GameObject ant_ = antPoolStack.Pop();
            //ant_.SetAnchoredPos(new Vector2(0, 0));

            //yield return new WaitForSecondsRealtime(1f);
        }

    }


    //public void RemakeAnt(GameObject )
    //{
    //    GameObject ant_ = antPoolStack.Pop();
    //    ant_.GetComponent<AntController>().SetAntStat();
    //    antPoolStack.Add(ant_);
    //    //ant_.SetAnchoredPos(new Vector2(1, 1));
    //    ant_.SetActive(true);
    //}

}
