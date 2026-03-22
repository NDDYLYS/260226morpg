using UnityEngine;




public class MenuManager : MonoBehaviour
{
    private static MenuManager _Instance = null;
    public static MenuManager Instance
    {
        get
        {
            if (_Instance == null)
                _Instance = FindObjectOfType(typeof(MenuManager)) as MenuManager;
            return _Instance;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            UIPrefabManager.Instance.MenuPageProperty.OnClickOpenPageButton();
        }
    }
}