using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPooling : MonoBehaviour
{
    public int bulletCount = 10;

    Stack<GameObject> bulletPool = default;



    //! 여기서 오브젝트 풀링을 해준다!

    // Start is called before the first frame update
    void Start()
    {
        GameObject bulletObj = gameObject.FindChildObj("Bullet");

        bulletPool = new Stack<GameObject>();

        GameObject bullet = default;
        for (int i = 0; i < bulletCount; ++i)
        {
            bullet = Instantiate(bulletObj, gameObject.FindChildObj
                (GioleData.OBJ_NAME_BASICTURRET).transform);
            bullet.name = $"Bullet({i})";
            bulletPool.Push(bullet);
        }



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
