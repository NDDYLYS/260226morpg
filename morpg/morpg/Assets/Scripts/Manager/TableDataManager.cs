using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using System;

using System.IO;
using System.Linq;




public class TableDataManager : SingletonGameObject<TableDataManager>
{
    public SystemLanguage Language;
    public bool IsUseAssetBundle;

    private Dictionary<string, Dictionary<string, object>> Table_CodenameDic = new Dictionary<string, Dictionary<string, object>>();
    private Dictionary<string, Dictionary<int, object>> Table_IndexDic = new Dictionary<string, Dictionary<int, object>>();

    private Dictionary<string, GameObject> LoadedPrefabDic = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject[]> LoadedPrefabsDic = new Dictionary<string, GameObject[]>(); // 챕터당 맵을 저장한다

    //private Dictionary<string, AssetBundle> LoadedAssetBundles = new Dictionary<string, AssetBundle>();
    //private Dictionary<string, Texture> LoadedTextures = new Dictionary<string, Texture>();

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void SettingLanguage(SystemLanguage _language)
    {
        if (_language == SystemLanguage.Unknown)
            Language = Application.systemLanguage;
        else 
            Language = _language;
    }

    public void ResourcesTableLoad()
    {
        TextAsset[] textAssets = Resources.LoadAll<TextAsset>(string.Format("TableData/"));
        for (int i = 0; i < textAssets.Length; i++)
        {
            Type type = Type.GetType(textAssets[i].name);
            MethodInfo info = type.GetMethod("AutoLoadTable");
            
            if (info != null)
                info.Invoke(null, null);
        }
    }

    //public void BundleTableLoad()
    //{
    //    TextAsset[] textAssets = LoadAllAssets<TextAsset>("tabledata", string.Format("Assets/AssetBundle/TableData/"));
    //    for (int i = 0; i < textAssets.Length; i++)
    //    {
    //        Type type = Type.GetType(textAssets[i].name);
    //        MethodInfo info = type.GetMethod("AutoLoadTable");

    //        if (info != null)
    //            info.Invoke(null, null);
    //    }
    //}

    public void SetDictinary<T>(int _index, string _codename, T _data)
    {
        string tablename = typeof(T).Name;

        if (!Table_CodenameDic.ContainsKey(tablename))
        {
            Dictionary<string, object> codenameDic = new Dictionary<string, object>();
            codenameDic.Add(_codename, _data);
            Table_CodenameDic.Add(tablename, codenameDic);
        }
        else
        {
            if (!Table_CodenameDic[tablename].ContainsKey(_codename))
                Table_CodenameDic[tablename].Add(_codename, _data);
            else
            {

            }
        }

        if (!Table_IndexDic.ContainsKey(tablename))
        {
            Dictionary<int, object> indexDic = new Dictionary<int, object>();
            indexDic.Add(_index, _data);
            Table_IndexDic.Add(tablename, indexDic);
        }
        else
        {
            if (!Table_IndexDic[tablename].ContainsKey(_index))
                Table_IndexDic[tablename].Add(_index, _data);
            else
            {

            }
        }
    }

    public string GetTableText(string _codename, params string[] param)
    {
        return string.Format(GetTableText(_codename), param);
    }

    //public string GetTableText(string _codename)
    //{
    //    Table_Text text = GetTableData<Table_Text>(_codename);
    //    if (text == null)
    //        return string.Format("{0}", _codename);

    //    if (Language == SystemLanguage.Korean)
    //        return text.Korean;
    //    else if (Language == SystemLanguage.English)
    //        return text.English;

    //    return string.Format("None Text({0})", _codename);
    //}

    //public string GetEditeGameText(string _codename, SystemLanguage _language)
    //{
    //    TableTextLoad();

    //    string tablename = "Table_Text";
    //    if (Table_CodenameDic.ContainsKey(tablename))
    //    {
    //        Dictionary<string, object> codenameDic = Table_CodenameDic[tablename];
    //        if (codenameDic.ContainsKey(_codename))
    //        {
    //            if (_language == SystemLanguage.Korean)
    //                return ((Table_Text)codenameDic[_codename]).Korean;
    //        }
    //    }

    //    return string.Format("None Text({0})", _codename);
    //}

    public void TableTextLoad()
    {
        Type type = Type.GetType("Table_Text");
        MethodInfo info = type.GetMethod("AutoLoadTable");

        if (info != null)
            info.Invoke(null, null);
    }
    
    public List<T> GetTableDataList<T>()
    {
        List<T> returnList = new List<T>();

        string tablename = typeof(T).Name;
        if (Table_CodenameDic.ContainsKey(tablename))
        {
            Dictionary<string, object> codenameDic = Table_CodenameDic[tablename];

            foreach (string key in codenameDic.Keys)
            {
                returnList.Add((T)codenameDic[key]);
            }
        }

        return returnList;
    }

    //private static string GetFieldName(string _fieldname)
    //{
    //    return string.Format("<{0}>k__BackingField", _fieldname);
    //}

    //public List<T> GetTableDataList<T>(int index, string fieldname)
    //{
    //    string tablename = typeof(T).Name;
    //    List<T> resultList = new List<T>();
    //    if (Table_CodenameDic.ContainsKey(tablename))
    //    {
    //        Dictionary<string, object> codenameDic = Table_CodenameDic[tablename];
    //        foreach (object obj in codenameDic.Values)
    //        {
    //            Type type = obj.GetType();
    //            FieldInfo field = type.GetField(GetFieldName(fieldname), BindingFlags.NonPublic | BindingFlags.Instance);
    //            if (field != null)
    //            {
    //                int objIndex = (int)field.GetValue(obj);
    //                if (objIndex == index)
    //                {
    //                    resultList.Add((T)obj);
    //                }
    //            }
    //        }
    //    }
    //    return resultList;
    //}

    public T GetTableData<T>(string _codename)
    {
        string tablename = typeof(T).Name;
        if (Table_CodenameDic.ContainsKey(tablename))
        {
            Dictionary<string, object> codenameDic = Table_CodenameDic[tablename];
            if (codenameDic.ContainsKey(_codename))
            {
                //return ((T)codenameDic[_codename]);
                T obj = default(T);
                obj = (T)codenameDic[_codename];
                return obj;
            }
        }

        return default(T);
    }

    public T GetTableData<T>(int _index)
    {
        string tablename = typeof(T).Name;
        if (Table_CodenameDic.ContainsKey(tablename))
        {
            Dictionary<int, object> indexDic = Table_IndexDic[tablename];
            if (indexDic.ContainsKey(_index))
            {
                //return ((T)indexDic[_index]);
                T obj = default(T);
                obj = (T)indexDic[_index];
                return obj;
            }
        }

        return default(T);
    }

    public GameObject GetLoadedPrefab(string _path, bool _isAssetBundle = false)
    {
        if (LoadedPrefabDic.ContainsKey(_path))
        {
            return LoadedPrefabDic[_path];
        }

        GameObject prefab = null;
        if (!_isAssetBundle)
        {
            prefab = Resources.Load<GameObject>(string.Format("Prefabs/{0}", _path));
        }
        else
        {
            string bundleName = "prefabs";
            string bundlePath = string.Format("Assets/AssetBundle/Prefabs/{0}.prefab", _path);
            prefab = LoadAsset<GameObject>(bundleName, bundlePath);
        }
        if (prefab != null)
            LoadedPrefabDic.Add(_path, prefab);
        return prefab;
    }

    public GameObject[] GetLoadedPrefabs(string _path)
    {
        if (LoadedPrefabsDic.ContainsKey(_path))
        {
            return LoadedPrefabsDic[_path];
        }

        GameObject[] prefabs = null;
        prefabs = Resources.LoadAll<GameObject>(string.Format("Prefabs/{0}", _path));

        if (prefabs != null)
            LoadedPrefabsDic.Add(_path, prefabs);
        return prefabs;
    }

    //public string[,] PublicExcelReader(string _csvFileName, bool _isResource = false)
    //{
    //    string content = string.Empty;
    //    if (!_isResource)
    //    {
    //        string bundleName = "tabledata";
    //        string bundlePath = string.Format("Assets/AssetBundle/TableData/{0}.csv", _csvFileName);
    //        TextAsset csvFile = LoadAsset<TextAsset>(bundleName, bundlePath);
    //        content = csvFile.text;
    //    }
    //    else
    //    {
    //        TextAsset csvFile = Resources.Load<TextAsset>(string.Format("TableData/{0}", _csvFileName));
    //        content = csvFile.text;
    //    }

    //    return Util.PublicExcelReader(content);
    //}

    public T LoadAsset<T>(string bundleName, string path) where T : UnityEngine.Object
    {
#if UNITY_EDITOR
        if (IsUseAssetBundle == false)
        {
            T asset = UnityEditor.AssetDatabase.LoadAssetAtPath<T>(path);
            return asset;
        }
        //else
#endif
        //{
        //    AssetBundle bundle = GetAssetBundle(bundleName);
        //    if (bundle != null)
        //    {
        //        T asset = bundle.LoadAsset<T>(path);
        //        if (asset == null)
        //        {
        //            //LogManager.Instance.DebugLogCategory(LogCategoryEnum.IsBundle, string.Format("{0}' {1}을 불러오는 데에 실패하였다!!!", bundleName, path));
        //        }

        //        return asset;
        //    }
        //}

        return default(T);
    }

    //public T[] LoadAllAssets<T>(string bundleName, string path) where T : UnityEngine.Object
    //{
    //    if (IsUseAssetBundle == false)
    //    {
    //        DirectoryInfo di = new DirectoryInfo(path);
    //        FileInfo[] fis = di.GetFiles();

    //        if (fis != null)
    //        {
    //            List<T> list = new List<T>();
    //            int count = 0;
    //            foreach (FileInfo info in fis)
    //            {
    //                T asset = LoadAsset<T>(bundleName, path + info.Name);
    //                if (asset != null)
    //                {
    //                    list.Add(asset);
    //                    count++;
    //                }
    //            }
    //            return list.ToArray();
    //        }
    //        else
    //        {
    //            Debug.LogError("LoadAllAssets Fail : " + path);
    //        }
    //    }
    //    else
    //    {
    //        AssetBundle bundle = GetAssetBundle(bundleName);
    //        if (bundle != null)
    //        {
    //            T[] assets = bundle.LoadAllAssets<T>();
    //            return assets;
    //        }
    //    }
    //    return null;
    //}
}