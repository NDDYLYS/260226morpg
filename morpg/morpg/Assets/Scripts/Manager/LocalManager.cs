using UnityEngine;




public class LocalManager : MonoBehaviour
{
    private static LocalManager _Instance = null;
    public static LocalManager Instance
    {
        get
        {
            if (_Instance == null)
                _Instance = FindObjectOfType(typeof(LocalManager)) as LocalManager;
            return _Instance;
        }
    }
    
    public string BundleVersion;
    public int BundleVersionCode;

    public string UUID;
    public string UUIDProperty
    {
        get
        {
            if (string.IsNullOrEmpty(UUID))
                return "UNITY_EDITOR";
            return UUID;
        }
    }

    //public ServerCategoryEnum ServerCategory;
    public SystemLanguage Language;
    public bool IsUseAssetBundle;
    
    public string AppPackageName;

    void Awake()
    {
        DontDestroyOnLoad(this);
        UUID = UUIDProperty;

        //#if !UNITY_EDITOR
        //        UUID = string.Empty;

        //        IsUseAssetBundle = true;
        //        Reporter.SetActive(true);
        //        GPGSManager.Instance.Init();

        //#if DEVSERVER
        //        ServerCategory = ServerCategoryEnum.DevServer;
        //#elif LOCALSERVER
        //        ServerCategory = ServerCategoryEnum.LocalServer;
        //#elif LIVESERVER
        //        ServerCategory = ServerCategoryEnum.LiveServer;
        //#endif

        //#endif

        //#if UNITY_ANDROID
        //        GameManager.Instance.Platform = "Android";
        //#elif UNITY_IOS
        //        GameManager.Instance.Platform = "iOS";
        //#endif

        //FirebaseManager.Instance.Init();

        TableDataManager.Instance.IsUseAssetBundle = IsUseAssetBundle;

        TableDataManager.Instance.SettingLanguage(Language);
        TableDataManager.Instance.ResourcesTableLoad();

        GameManager.Instance.Initialize();
        
        //string scene = string.Empty;
        //if (GameManager.Instance.SaveData.InstantDungeon == null)
        //{
        //    scene = "2Village";
        //}
        //else
        //{
        //    scene = "3Dungeon";
        //}

        //GameManager.Instance.MovingScene(scene);
    }
}