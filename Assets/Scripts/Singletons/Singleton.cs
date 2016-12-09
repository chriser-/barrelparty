using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField]
    private bool m_DontDestroyOnLoad = true;
    private static T _instance;
    private static object _lock = new object();
    public static T Instance
    {
        get
        {
            MakeInstance();
            return _instance;
        }
    }

    protected void Awake()
    {
        if (_instance == null)
        {
            MakeInstance();
            OnAwake();
        }
        else if (_instance == this)
            OnAwake();
        else if (_instance != this)
            Destroy(gameObject);
    }

    protected virtual void OnAwake()
    {

    }

    public static void MakeInstance()
    {
        lock (_lock)
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    _instance = new GameObject(typeof(T).Name + "::Singleton").AddComponent<T>();
                }
                if(_instance.GetComponent<Singleton<T>>().m_DontDestroyOnLoad)
                    DontDestroyOnLoad(_instance.gameObject);
            }
        }
    }
}