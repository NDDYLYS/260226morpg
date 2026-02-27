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


    private DungeonResultPage DungeonResultPage { get; set; }
    public DungeonResultPage DungeonResultPageProperty
    {
        get
        {
            if (DungeonResultPage == null)
                DungeonResultPage = GetPrefab("UI/DungeonResultPage").GetComponent<DungeonResultPage>();
            DungeonResultPage.transform.SetAsLastSibling();
            return DungeonResultPage;
        }
    }

    private EnemyKillCountPage EnemyKillCountPage { get; set; }
    public EnemyKillCountPage EnemyKillCountPageProperty
    {
        get
        {
            if (EnemyKillCountPage == null)
                EnemyKillCountPage = GetPrefab("UI/EnemyKillCountPage").GetComponent<EnemyKillCountPage>();
            EnemyKillCountPage.transform.SetAsLastSibling();
            return EnemyKillCountPage;
        }
    }

    private StatUpPage StatUpPage { get; set; }
    public StatUpPage StatUpPageProperty
    {
        get
        {
            if (StatUpPage == null)
                StatUpPage = GetPrefab("UI/StatUpPage").GetComponent<StatUpPage>();
            StatUpPage.transform.SetAsLastSibling();
            return StatUpPage;
        }
    }

    private LogViewPage LogViewPage { get; set; }
    public LogViewPage LogViewPageProperty
    {
        get
        {
            if (LogViewPage == null)
                LogViewPage = GetPrefab("UI/LogViewPage").GetComponent<LogViewPage>();
            LogViewPage.transform.SetAsLastSibling();
            return LogViewPage;
        }
    }

    private MinimapPage MinimapPage { get; set; }
    public MinimapPage MinimapPageProperty
    {
        get
        {
            if (MinimapPage == null)
                MinimapPage = GetPrefab("UI/MinimapPage").GetComponent<MinimapPage>();
            MinimapPage.transform.SetAsLastSibling();
            return MinimapPage;
        }
    }

    private CreateEquipmentPage CreateEquipmentPage { get; set; }
    public CreateEquipmentPage CreateEquipmentPageProperty
    {
        get
        {
            if (CreateEquipmentPage == null)
                CreateEquipmentPage = GetPrefab("UI/CreateEquipmentPage").GetComponent<CreateEquipmentPage>();
            CreateEquipmentPage.transform.SetAsLastSibling();
            return CreateEquipmentPage;
        }
    }

    private EquipmentInfoPage EquipmentInfoPage { get; set; }
    public EquipmentInfoPage EquipmentInfoPageProperty
    {
        get
        {
            if (EquipmentInfoPage == null)
                EquipmentInfoPage = GetPrefab("UI/EquipmentInfoPage").GetComponent<EquipmentInfoPage>();
            EquipmentInfoPage.transform.SetAsLastSibling();
            return EquipmentInfoPage;
        }
    }

    private ReciepeInfoPage ReciepeInfoPage { get; set; }
    public ReciepeInfoPage ReciepeInfoPageProperty
    {
        get
        {
            if (ReciepeInfoPage == null)
                ReciepeInfoPage = GetPrefab("UI/ReciepeInfoPage").GetComponent<ReciepeInfoPage>();
            ReciepeInfoPage.transform.SetAsLastSibling();
            return ReciepeInfoPage;
        }
    }

    private MaterialItemPage MaterialItemPage { get; set; }
    public MaterialItemPage MaterialItemPageProperty
    {
        get
        {
            if (MaterialItemPage == null)
                MaterialItemPage = GetPrefab("UI/MaterialItemPage").GetComponent<MaterialItemPage>();
            MaterialItemPage.transform.SetAsLastSibling();
            return MaterialItemPage;
        }
    }

    private EquipmentItemPage EquipmentItemPage { get; set; }
    public EquipmentItemPage EquipmentItemPageProperty
    {
        get
        {
            if (EquipmentItemPage == null)
                EquipmentItemPage = GetPrefab("UI/EquipmentItemPage").GetComponent<EquipmentItemPage>();
            EquipmentItemPage.transform.SetAsLastSibling();
            return EquipmentItemPage;
        }
    }

    private StatPage StatPage { get; set; }
    public StatPage StatPageProperty
    {
        get
        {
            if (StatPage == null)
                StatPage = GetPrefab("UI/StatPage").GetComponent<StatPage>();
            StatPage.transform.SetAsLastSibling();
            return StatPage;
        }
    }

    private SkillPage SkillPage { get; set; }
    public SkillPage SkillPageProperty
    {
        get
        {
            if (SkillPage == null)
                SkillPage = GetPrefab("UI/SkillPage").GetComponent<SkillPage>();
            SkillPage.transform.SetAsLastSibling();
            return SkillPage;
        }
    }

    private SelectSpeciesPage SelectSpeciesPage { get; set; }
    public SelectSpeciesPage SelectSpeciesPageProperty
    {
        get
        {
            if (SelectSpeciesPage == null)
                SelectSpeciesPage = GetPrefab("UI/SelectSpeciesPage").GetComponent<SelectSpeciesPage>();
            SelectSpeciesPage.transform.SetAsLastSibling();
            return SelectSpeciesPage;
        }
    }

    private AccountInfoPage AccountInfoPage { get; set; }
    public AccountInfoPage AccountInfoPageProperty
    {
        get
        {
            if (AccountInfoPage == null)
                AccountInfoPage = GetPrefab("UI/AccountInfoPage").GetComponent<AccountInfoPage>();
            AccountInfoPage.transform.SetAsLastSibling();
            return AccountInfoPage;
        }
    }

    private ObjectInfoPage ObjectInfoPage { get; set; }
    public ObjectInfoPage ObjectInfoPageProperty
    {
        get
        {
            if (ObjectInfoPage == null)
                ObjectInfoPage = GetPrefab("UI/ObjectInfoPage").GetComponent<ObjectInfoPage>();
            ObjectInfoPage.transform.SetAsLastSibling();
            return ObjectInfoPage;
        }
    }

    private SelectDifficultyPage SelectDifficultyPage { get; set; }
    public SelectDifficultyPage SelectDifficultyPageProperty
    {
        get
        {
            if (SelectDifficultyPage == null)
                SelectDifficultyPage = GetPrefab("UI/SelectDifficultyPage").GetComponent<SelectDifficultyPage>();
            SelectDifficultyPage.transform.SetAsLastSibling();
            return SelectDifficultyPage;
        }
    }

    private SelectSkillPage SelectSkillPage { get; set; }
    public SelectSkillPage SelectSkillPageProperty
    {
        get
        {
            if (SelectSkillPage == null)
                SelectSkillPage = GetPrefab("UI/SelectSkillPage").GetComponent<SelectSkillPage>();
            SelectSkillPage.transform.SetAsLastSibling();
            return SelectSkillPage;
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