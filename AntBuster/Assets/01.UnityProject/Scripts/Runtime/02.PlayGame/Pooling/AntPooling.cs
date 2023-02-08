using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntPooling : MonoBehaviour
{
    public int antQuantity = 6;


    private GameObject antObj = default;
    private Stack<GameObject> antPoolStack = new Stack<GameObject>();


    // Start is called before the first frame update
    void Start()
    {

        GameObject gameObjs = GioleFunc.GetRootObj(GioleData.OBJ_NAME_GAMEOBJS);

        antObj = gameObjs.FindChildObj("Ant");
        antObj.SetActive(false);

        StartCoroutine(MakeAnt());


    }

    // Update is called once per frame
    void Update()
    {

    }



    IEnumerator MakeAnt()
    {
        for (int i = 0; i < antQuantity; ++i)
        {
            GameObject ant_ = Instantiate(antObj, transform);
            ant_.SetAnchoredPos(new Vector2(1, 1));
            antPoolStack.Push(ant_);
            ant_.SetActive(true);

            yield return new WaitForSecondsRealtime(1f);
        }
    }

    public void RemakeAnt()
    {
        GameObject ant_ = antPoolStack.Pop();
        ant_.GetComponent<AntController>().SetAntStat();
        antPoolStack.Push(ant_);
        //ant_.SetAnchoredPos(new Vector2(1, 1));
        ant_.SetActive(true);
    }

}
