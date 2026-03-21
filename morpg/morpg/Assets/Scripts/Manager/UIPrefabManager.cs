using UnityEngine;



public class UIPrefabManager : SingletonGameObject<UIPrefabManager>
{
    private Transform Root { get; set; }
    private Transform Dimmed { get; set; }
    

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetRoot(Transform _root, Transform _dimmed)
    {
        Root = _root;
        Dimmed = _dimmed;
        UIPage(false);
    }

    private MessageBox MessageBox { get; set; }
    public MessageBox MessageBoxProperty
    {
        get
        {
            if (MessageBox == null)
                MessageBox = GetPrefab("UIs/MessageBox").GetComponent<MessageBox>();
            MessageBox.transform.SetAsLastSibling();
            return MessageBox;
        }
    }

    private OptionPage OptionPage { get; set; }
    public OptionPage OptionPageProperty
    {
        get
        {
            if (OptionPage == null)
                OptionPage = GetPrefab("UIs/OptionPage").GetComponent<OptionPage>();
            OptionPage.transform.SetAsLastSibling();
            return OptionPage;
        }
    }

    private SaveLoadPage SaveLoadPage { get; set; }
    public SaveLoadPage SaveLoadPageProperty
    {
        get
        {
            if (SaveLoadPage == null)
                SaveLoadPage = GetPrefab("UIs/SaveLoadPage").GetComponent<SaveLoadPage>();
            SaveLoadPage.transform.SetAsLastSibling();
            return SaveLoadPage;
        }
    }

    private MenuPage MenuPage { get; set; }
    public MenuPage MenuPageProperty
    {
        get
        {
            if (MenuPage == null)
                MenuPage = GetPrefab("UIs/MenuPage").GetComponent<MenuPage>();
            MenuPage.transform.SetAsLastSibling();
            return MenuPage;
        }
    }

    public GameObject GetPrefab(string _path, Transform _parent = null, bool _isAssetBundle = false)
    {
        var prefab = TableDataManager.Instance.GetLoadedPrefab(_path, _isAssetBundle);
        var obj = Util.CreateObject(prefab, (_parent == null) ? Root : _parent, Vector3.zero, Vector3.one);
        return obj;
    }

    public void UIPage(bool enable)
    {
        Dimmed.gameObject.SetActive(enable);
        if (enable)
            Dimmed.SetAsLastSibling();
    }
}