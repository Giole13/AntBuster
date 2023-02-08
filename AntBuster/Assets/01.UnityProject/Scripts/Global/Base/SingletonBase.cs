using UnityEngine;
using System;

public abstract class SingletonBase<T> : MonoBehaviour where T : SingletonBase<T>
{
    #region Lock Singleton Version
    // private static T _instance;
    // private static object syncRoot = new System.Object();
    // public static T Instance
    // {
    //     get
    //     {
    //         if (!_instance)
    //         {
    //             lock (syncRoot)
    //             {
    //                 _instance = FindObjectOfType(typeof(T)) as T;
    //                 if (!_instance)
    //                 {
    //                     GameObject obj = new GameObject("GameManager");
    //                     _instance = obj.AddComponent(typeof(T)) as T;
    //                 }
    //             }
    //         }
    //         DontDestroyOnLoad(_instance.gameObject);
    //         return _instance;

    //     }
    // }
    #endregion

    #region Lazy<T> Singleton Version
    private static readonly Lazy<T> _instance = new Lazy<T>(() =>   // readonly 값 변경 제한, Lazy<T> 클래스를 활용하여 Thread Safe하게 싱글톤 패턴 구현, 람다식으로 인스턴스 생성
    {
        T instance = FindObjectOfType(typeof(T)) as T;

        if (instance == null)
        {
            GameObject obj = new GameObject();
            instance = obj.AddComponent(typeof(T)) as T;
            DontDestroyOnLoad(obj);
        }
        else
        {
            DontDestroyOnLoad(instance.gameObject);
        }

        return instance;
    });


    public static T Instance
    {
        get
        {
            return _instance.Value;
        }
    }
    #endregion

    //protected void Awake()
    //{
    //    Instance.CreateInstance();
    //}
    //public virtual void CreateInstance() { }
}