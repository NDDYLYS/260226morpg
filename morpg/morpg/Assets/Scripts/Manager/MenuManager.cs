using Unity.VisualScripting;
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

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        createUnit();
    }

    public void createUnit()
    {
        var species = GameManager.Instance.SaveData.Species;
        var prefab = TableDataManager.Instance.GetLoadedPrefab($"Units/{species.ToString()}");
        var unit = Util.CreateObject(prefab, null, Vector3.zero, Vector3.one);
        GameManager.Instance.setPlayer(unit, species);

        var animator = unit.transform.GetChild(0).AddComponent<player>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!UIPrefabManager.Instance.MenuPageProperty.Container.activeSelf)
                UIPrefabManager.Instance.MenuPageProperty.OnClickOpenPageButton();
            else
                UIPrefabManager.Instance.MenuPageProperty.OnClickClosePageButton();
        }
    }
}