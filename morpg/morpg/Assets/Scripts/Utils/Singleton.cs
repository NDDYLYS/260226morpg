using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : class, new()
{
    private static T _instance = null;

    public static T Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new T();
            }

            return _instance;
        }
    }
     
}

public class SingletonGameObject<T> : MonoBehaviour where T : class, new()
{
    private static T _instance = null;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(T)) as T;

                if (_instance == null)
                {
                    GameObject container = new GameObject();
                    //container.name = "Singleton";
                    container.name = typeof(T).Name;
                    _instance = container.AddComponent(typeof(T)) as T;
                }
            }

            return _instance;
        }
    }

    //protected virtual void Awake()
    //{
    //    DontDestroyOnLoad(this.gameObject); //씬 변경되어도 삭제 안됨
    //}
}
