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

    private Dictionary<string, AssetBundle> LoadedAssetBundles = new Dictionary<string, AssetBundle>();
    private Dictionary<string, Texture> LoadedTextures = new Dictionary<string, Texture>();

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

        LogManager.Instance.DebugLogCategory(LogCategoryEnum.Data, "테이블 로딩 완료!");
    }

    public void BundleTableLoad()
    {
        TextAsset[] textAssets = LoadAllAssets<TextAsset>("tabledata", string.Format("Assets/AssetBundle/TableData/"));
        for (int i = 0; i < textAssets.Length; i++)
        {
            Type type = Type.GetType(textAssets[i].name);
            MethodInfo info = type.GetMethod("AutoLoadTable");

            if (info != null)
                info.Invoke(null, null);
        }
    }

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

    public string GetTableText(string _codename)
    {
        Table_Text text = GetTableData<Table_Text>(_codename);
        if (text == null)
            return string.Format("{0}", _codename);

        if (Language == SystemLanguage.Korean)
            return text.Korean;
        else if (Language == SystemLanguage.English)
            return text.English;

        return string.Format("None Text({0})", _codename);
    }

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

    public string[,] PublicExcelReader(string _csvFileName, bool _isResource = false)
    {
        string content = string.Empty;
        if (!_isResource)
        {
            string bundleName = "tabledata";
            string bundlePath = string.Format("Assets/AssetBundle/TableData/{0}.csv", _csvFileName);
            TextAsset csvFile = LoadAsset<TextAsset>(bundleName, bundlePath);
            content = csvFile.text;
        }
        else
        {
            TextAsset csvFile = Resources.Load<TextAsset>(string.Format("TableData/{0}", _csvFileName));
            content = csvFile.text;
        }

        return Util.PublicExcelReader(content);
    }

    public T LoadAsset<T>(string bundleName, string path) where T : UnityEngine.Object
    {
#if UNITY_EDITOR
        if (IsUseAssetBundle == false)
        {
            T asset = UnityEditor.AssetDatabase.LoadAssetAtPath<T>(path);
            return asset;
        }
        else
#endif
        {
            AssetBundle bundle = GetAssetBundle(bundleName);
            if (bundle != null)
            {
                T asset = bundle.LoadAsset<T>(path);
                if (asset == null)
                {
                    //LogManager.Instance.DebugLogCategory(LogCategoryEnum.IsBundle, string.Format("{0}' {1}을 불러오는 데에 실패하였다!!!", bundleName, path));
                }

                return asset;
            }
        }
        
        return default(T);
    }

    public T[] LoadAllAssets<T>(string bundleName, string path) where T : UnityEngine.Object
    {
        if (IsUseAssetBundle == false)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            FileInfo[] fis = di.GetFiles();

            if (fis != null)
            {
                List<T> list = new List<T>();
                int count = 0;
                foreach (FileInfo info in fis)
                {
                    T asset = LoadAsset<T>(bundleName, path + info.Name);
                    if (asset != null)
                    {
                        list.Add(asset);
                        count++;
                    }
                }
                return list.ToArray();
            }
            else
            {
                Debug.LogError("LoadAllAssets Fail : " + path);
            }
        }
        else
        {
            AssetBundle bundle = GetAssetBundle(bundleName);
            if (bundle != null)
            {
                T[] assets = bundle.LoadAllAssets<T>();
                return assets;
            }
        }
        return null;
    }

    public AssetBundle GetAssetBundle(string _bundleName)
    {
        if (LoadedAssetBundles.ContainsKey(_bundleName))
            return LoadedAssetBundles[_bundleName];
        return null;
    }

    public void AddAssetBundle(AssetBundle _bundle)
    {
        if (_bundle == null)
            return;
        if (!LoadedAssetBundles.ContainsKey(_bundle.name))
        {
            //LogManager.Instance.DebugLogCategory(LogCategoryEnum.IsBundle, string.Format("{0}'s Load.", _bundle));
            LoadedAssetBundles.Add(_bundle.name, _bundle);
        }
    }

    public bool HasLoadedAssetBundle(string _key)
    {
        if (LoadedAssetBundles.ContainsKey(_key))
        {
            //LogManager.Instance.DebugLogCategory(LogCategoryEnum.IsBundle, string.Format("{0}'s Already Load.", _key));
            return true;
        }
        //LogManager.Instance.DebugLogCategory(LogCategoryEnum.IsBundle, string.Format("{0}'s Not Load.", _key));
        return false;
    }

    public Texture GetBundleTextures(string _textureName)
    {
        if (LoadedTextures.ContainsKey(_textureName))
        {
            return LoadedTextures[_textureName];
        }

        Texture texture = null;
        string bundleName = "textures";
        string bundlePath = string.Format("Assets/AssetBundle/Textures/{0}.png", _textureName);
        texture = LoadAsset<Texture>(bundleName, bundlePath);

        if (texture != null)
            LoadedTextures.Add(_textureName, texture);
        return texture;
    }

    public long GetExpForLevel(int _level)
    {
        Table_Level exp = GetTableData<Table_Level>(_level + 1);
        if (exp != null)
            return exp.Exp;

        List<Table_Level> list = GetTableDataList<Table_Level>();
        return list[list.Count - 1].Exp;
    }

    public long GetStackExpForLevel(int _level)
    {
        Table_Level exp = GetTableData<Table_Level>(_level);
        if (exp != null)
            return exp.StackExp;

        List<Table_Level> list = GetTableDataList<Table_Level>();
        return list[list.Count - 1].StackExp;
    }

    public long GetStackExpForMaxLevel()
    {
        List<Table_Level> list = GetTableDataList<Table_Level>();
        Table_Level maxLevel = list[list.Count - 1];

        return maxLevel.Exp + maxLevel.StackExp;
    }

    public Table_Map GetMapForChater()
    {
        return GetMapForChater(GameManager.Instance.SaveData.InstantDungeon.Chapter);
    }

    public Table_Map GetMapForChater(int _chapter)
    {
        List<Table_Map> list = GetTableDataList<Table_Map>();
        if (list != null)
        {
            foreach (var element in list)
            {
                if (element.Chapter == _chapter)
                    return element;
            }
        }

        return null;
    }

    public Table_EquipmentItem GetEquipForGrade(DropGradeEnum _dropGrade)
    {
        var value = new List<Table_EquipmentItem>();
        var list = GetTableDataList<Table_EquipmentItem>();
        foreach (var element in list)
        {
            bool haveReciepe = GameManager.Instance.SaveData.GetReciepeInventory(element.CodeName);
            if (haveReciepe)
            {
                // 210207 레시피를 가지고 있는 장비일 경우에만 드랍한다
                if (element.DropGrade == _dropGrade)
                {
                    if (!GameManager.Instance.SaveData.InstantDungeon.GetDropEquipOverlap(element.CodeName))
                    {
                        // 210208 장비 아이템의 중복 드랍을 막는다
                        value.Add(element);
                    }
                }
            }
        }

        try
        {
            if (0 < value.Count)
            {
                value = Util.ShuffleAlgorithm<Table_EquipmentItem>(value, 50);
                Table_EquipmentItem drop = value[0];
                GameManager.Instance.SaveData.InstantDungeon.SetDropEquipOverlap(drop.CodeName);

                return drop;
            }
            else
            {
                if (_dropGrade == DropGradeEnum.C)
                {
                    Debug.LogWarning(string.Format("원래 {0}등급에서는 더 이상 드랍할 아이템이 없다!", _dropGrade));
                    return null;
                }
                else
                {
                    Debug.LogWarning(string.Format("원래 {0}등급이 나와야 하는데 열린 레시피가 없어서 {1} 등급이 나왔다!", _dropGrade, Util.DownGrade(_dropGrade)));
                    return GetEquipForGrade(Util.DownGrade(_dropGrade));
                }
            }
        }
        catch (Exception _ex)
        {
            Debug.LogWarning(string.Format("Tm.GetEquipmentForGrade() Error!!! {0}'s Grade", _dropGrade));
        }

        return null;
    }

    public Table_Species GetSpecies(SpeciesTypeEnum _species)
    {
        var list = GetTableDataList<Table_Species>();
        return list.Where(species => species.SpeciesType == _species).FirstOrDefault();
    }

    public string GetDropGrade(int[] _dropGrades)
    {
        var text = GetTableText("UI_DropGrade");
        var gradeList = new List<string>();
        var index = 0;
        foreach (var _dropGrade in _dropGrades)
        {
            var dropGrade = (DropGradeEnum)index;
            if (0 < _dropGrade)
                gradeList.Add(Util.GetColorTextForGrade(dropGrade, $"{dropGrade.ToString()}({_dropGrade * 0.01f}%)"));
            index++;
        }

        return string.Format(text, string.Join("\n", gradeList));
    }

    public Table_Character GetPlayerSpecies(SpeciesTypeEnum _species)
    {
        var player = GetTableDataList<Table_Character>().FindAll(character => character.CharacterType == CharacterTypeEnum.Player).Find(character => character.SpeciesType == _species);
        return player;
    }

    public Table_Difficulty GetDungeonDifficulty()
    {
        var difficulty = GameManager.Instance.SaveData.GetDifficulty();
        return GetTableDataList<Table_Difficulty>().Find(dif => dif.Difficulty == difficulty);
    }
    
    // 230203 드랍하는 스킬의 등급을 정한다
    public List<DropGradeEnum> GetDropGradeList()
    {
        var dropGradeList = new List<DropGradeEnum>();
        var difficultyBonus = GetDungeonDifficulty();

        for (int i = 0; i < 3; i++)
        {
            var grade = 0;
            var prob = UnityEngine.Random.Range(0, 10000);
            foreach (var dropProb in difficultyBonus.DropGrade)
            {
                if (dropProb <= 0)
                {
                    grade++;
                    continue;
                }
                if (prob < dropProb)
                {
                    dropGradeList.Add((DropGradeEnum)(grade));
                    break;
                }
                prob -= dropProb;
                grade++;
            }
        }

        return dropGradeList;
    }

    // 230206 드랍하는 스킬을 정한다
    public Table_Skill GetSkillForGrade(DropGradeEnum _dropGrade, List<Table_Skill> _dropSkillList = null)
    {
        var value = new List<Table_Skill>();
        var list = GetTableDataList<Table_Skill>();
        foreach (var element in list)
        {
            bool IsBeginningProduct = element.IsBeginningProduct;
            if (IsBeginningProduct)
            {
                // 230207 배울 수 있는 스킬일 경우에만 드랍한다
                if (element.DropGrade == _dropGrade)
                {
                    if (!GameManager.Instance.SaveData.InstantDungeon.GetDropSkillOverlap(element.CodeName))
                    {
                        // 230206 스킬의 중복 드랍을 막는다
                        if (_dropSkillList != null && !_dropSkillList.Contains(element))
                        {
                            // 230207 스킬획득 페이지에서 중복을 막는다
                            value.Add(element);
                        }
                    }
                }
            }
        }

        try
        {
            if (0 < value.Count)
            {
                value = Util.ShuffleAlgorithm<Table_Skill>(value, 50);
                var drop = value[0];

                return drop;
            }
            else
            {
                return LoopDropSkill(_dropGrade);
            }
        }
        catch (Exception _ex)
        {
            Debug.LogWarning(string.Format("Tm.GetSkillForGrade() Error!!! {0}'s Grade", _dropGrade));
        }

        return null;
    }

    private Table_Skill LoopDropSkill(DropGradeEnum _dropGrade)
    {
        if (_dropGrade == DropGradeEnum.C)
        {
            Debug.LogWarning(string.Format("원래 {0}등급에서는 더 이상 드랍할 스킬이 없다!", _dropGrade));
            return null;
        }
        else
        {
            Debug.LogWarning(string.Format("원래 {0}등급이 나와야 하는데 열린 스킬이 없어서 {1} 등급이 나왔다!", _dropGrade, Util.DownGrade(_dropGrade)));
            return GetSkillForGrade(Util.DownGrade(_dropGrade));
        }
    }

    // 230206 드랍하는 장비의 등급을 정한다
    public DropGradeEnum GetDropGrade()
    {
        var difficultyBonus = GetDungeonDifficulty();
        
        var grade = 0;
        var prob = UnityEngine.Random.Range(0, 10000);
        foreach (var dropProb in difficultyBonus.DropGrade)
        {
            if (dropProb <= 0)
            {
                grade++;
                continue;
            }
            if (prob < dropProb)
                return (DropGradeEnum)grade;
            prob -= dropProb;
            grade++;
        }

        return DropGradeEnum.None;
    }
}