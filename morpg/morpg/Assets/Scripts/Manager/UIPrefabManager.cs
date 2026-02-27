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
                MessageBox = GetPrefab("UI/MessageBox").GetComponent<MessageBox>();
            MessageBox.transform.SetAsLastSibling();
            return MessageBox;
        }
    }

    public GameObject GetPrefab(string _path, Transform _parent = null, bool _isAssetBundle = false)
    {
        GameObject prefab = TableDataManager.Instance.GetLoadedPrefab(_path, _isAssetBundle);
        GameObject obj = Util.CreateObject(prefab, (_parent == null) ? Root : _parent, Vector3.zero, Vector3.one);
        return obj;
    }

    public void UIPage(bool enable)
    {
        Dimmed.gameObject.SetActive(enable);
        if (enable)
            Dimmed.SetAsLastSibling();
    }
}