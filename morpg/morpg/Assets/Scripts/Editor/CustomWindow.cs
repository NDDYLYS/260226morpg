using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;





public class CustomWindow : EditorWindow
{
    private Vector2 ScrollPosition { get; set; }
    private string CodeName { get; set; }
    private int Count { get; set; }
    private Vector2 Position { get; set; }

    private int BeginExp { get; set; }
    private int BeginGold { get; set; }
    private int IncreaseExp { get; set; }
    private int IncreaseGold { get; set; }
    private int IncreaseMaterial { get; set; }
    private int ClearCount { get; set; }
    private long Exp { get; set; }
    private int Gold { get; set; }
    private int Crystal { get; set; }

    private static string[] Stats = new string[(int)StatEnum.Max];
    private static string[] Abilitys = new string[(int)AbilityEnum.Max];
    private static string[] SpecialEffects = new string[2];
    private int Duration { get; set; }

    private DropGradeEnum DropGrade { get; set; }
    private string SkillCodeName { get; set; }
    private bool OverlapBuff { get; set; }

    private int Value { get; set; }

    [MenuItem("CustomWindow/Open Window %#q")]
    static void OpenWindow()
    {
        CustomWindow window = (CustomWindow)EditorWindow.GetWindow(typeof(CustomWindow));
        window.name = "CustomEditorWindow";        
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void FirstLoad()
    {
        //Application.runInBackground = false;
        Time.timeScale = 1f;
        SetGameViewScale();
        StaticClearConsole();
    }

    private static void SetGameViewScale()
    {
        // https://nickname.tistory.com/31

        System.Reflection.Assembly assembly = typeof(UnityEditor.EditorWindow).Assembly;
        System.Type type = assembly.GetType("UnityEditor.GameView");
        UnityEditor.EditorWindow v = UnityEditor.EditorWindow.GetWindow(type);

        var defScaleField = type.GetField("m_defaultScale", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

        //whatever scale you want when you click on play
        float defaultScale = 0.1f;

        var areaField = type.GetField("m_ZoomArea", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
        var areaObj = areaField.GetValue(v);

        var scaleField = areaObj.GetType().GetField("m_Scale", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
        scaleField.SetValue(areaObj, new Vector2(defaultScale, defaultScale));
    }

    private static void StaticClearConsole()
    {
        var logEntries = Type.GetType("UnityEditor.LogEntries, UnityEditor.dll");
        var clearMethod = logEntries.GetMethod("Clear", BindingFlags.Static | BindingFlags.Public);
        clearMethod.Invoke(null, null);
    }

    void OnGUI()
    {
        EditorGUILayout.InspectorTitlebar(true, this);

        ScrollPosition = GUILayout.BeginScrollView(ScrollPosition);

        Time.timeScale = EditorGUILayout.Slider(new GUIContent("TimeScale", $"인게임의 속도를 조절한다.(0~10)"), Time.timeScale, 0f, 10f);

        if (GUILayout.Button("Save SaveFile", GUILayout.ExpandWidth(false)))
            GameManager.Instance.Save();

        if (GUILayout.Button("Load SaveFile", GUILayout.ExpandWidth(false)))
            GameManager.Instance.Load();

        if (GUILayout.Button("Remove SaveFile", GUILayout.ExpandWidth(false)))
            Remove();

        EditorGUILayout.Space(10f);

        if (GUILayout.Button("Capture", GUILayout.ExpandWidth(false)))
            CaptureImage();

        if (GUILayout.Button("Go to CaptureFolder", GUILayout.ExpandWidth(false)))
            GotoCaptureFolder();

        if (GUILayout.Button("Go to BuildFolder", GUILayout.ExpandWidth(false)))
            GotoBuildFolder();

        EditorGUILayout.Space(10f);

        if (GUILayout.Button("Debug Stat", GUILayout.ExpandWidth(false)))
            DebugStat();

        if (GUILayout.Button("Debug Inventory", GUILayout.ExpandWidth(false)))
            DebugInventory();

        if (GUILayout.Button("Debug EnemyKillCount", GUILayout.ExpandWidth(false)))
            DebugEnemyKillCount(); // DebugEquipmentItem

        if (GUILayout.Button("Debug EquipmentItem", GUILayout.ExpandWidth(false)))
            DebugEquipmentItem();

        EditorGUILayout.Space(10f);

        CodeName = EditorGUILayout.TextField("CodeName : ", CodeName, GUILayout.ExpandWidth(true));
        Count = EditorGUILayout.IntField("Count : ", Count, GUILayout.ExpandWidth(true));
        Position = EditorGUILayout.Vector2Field("Position : ", Position, GUILayout.ExpandWidth(false));
        DropGrade = (DropGradeEnum)EditorGUILayout.EnumPopup("DropGrade : ", DropGrade);

        if (GUILayout.Button("Create Enemy", GUILayout.ExpandWidth(false)))
            CreateEnemy();

        if (GUILayout.Button("Create MaterialItem", GUILayout.ExpandWidth(false)))
            CreateMaterialItem();

        if (GUILayout.Button("Create NPC", GUILayout.ExpandWidth(false)))
            CreateNPC();

        if (GUILayout.Button("Create EquipmentItem", GUILayout.ExpandWidth(false)))
            CreateEquipmentItem();

        EditorGUILayout.Space(10f);

        BeginExp = EditorGUILayout.IntField("BeginExp : ", BeginExp, GUILayout.ExpandWidth(true));
        BeginGold = EditorGUILayout.IntField("BeginGold : ", BeginGold, GUILayout.ExpandWidth(true));
        IncreaseExp = EditorGUILayout.IntField("IncreaseExp : ", IncreaseExp, GUILayout.ExpandWidth(true));
        IncreaseGold = EditorGUILayout.IntField("IncreaseGold : ", IncreaseGold, GUILayout.ExpandWidth(true));
        IncreaseMaterial = EditorGUILayout.IntField("IncreaseMaterial : ", IncreaseMaterial, GUILayout.ExpandWidth(true));
        ClearCount = EditorGUILayout.IntField("ClearCount : ", ClearCount, GUILayout.ExpandWidth(true));
        Exp = EditorGUILayout.LongField("Exp : ", Exp, GUILayout.ExpandWidth(true));
        Gold = EditorGUILayout.IntField("Gold : ", Gold, GUILayout.ExpandWidth(true));
        Crystal = EditorGUILayout.IntField("Crystal : ", Crystal, GUILayout.ExpandWidth(true));

        if (GUILayout.Button("Add Data", GUILayout.ExpandWidth(false)))
            AddData();

        if (GUILayout.Button("All Get MaterialItems", GUILayout.ExpandWidth(false)))
            AllGetMaterialItems();

        SkillCodeName = EditorGUILayout.TextField("SkillCodeName : ", SkillCodeName, GUILayout.ExpandWidth(true));
        if (GUILayout.Button("Learn Skill", GUILayout.ExpandWidth(false)))
            LearnSkillList();

        EditorGUILayout.Space(10f);

        EditorGUILayout.LabelField("Fog of War.Team");
        EditorGUILayout.LabelField("0 == Player");
        EditorGUILayout.LabelField("1 == Enemy");
        EditorGUILayout.LabelField("2 == Item");
        EditorGUILayout.LabelField("3 == Wall");
        EditorGUILayout.LabelField("4 == Water");
        EditorGUILayout.LabelField("5 == Portal");
        EditorGUILayout.LabelField("6 == Entry");
        EditorGUILayout.LabelField("7 == NPC");
        EditorGUILayout.LabelField("8 == Summon");

        EditorGUILayout.Space(10f);
        
        if (GUILayout.Button("All Turn", GUILayout.ExpandWidth(false)))
            AllTurn();

        EditorGUILayout.Space(10f);

        CodeName = EditorGUILayout.TextField("CodeName : ", CodeName, GUILayout.ExpandWidth(true));
        foreach (StatEnum stat in Enum.GetValues(typeof(StatEnum)))
        {
            if (stat == StatEnum.None || stat == StatEnum.Max)
                continue;
            Stats[(int)stat] = EditorGUILayout.TextField(string.Format("{0} : ", stat.ToString()), Stats[(int)stat], GUILayout.ExpandWidth(true));
        }
        foreach (AbilityEnum ability in Enum.GetValues(typeof(AbilityEnum)))
        {
            if (ability == AbilityEnum.None || ability == AbilityEnum.Max)
                continue;
            Abilitys[(int)ability] = EditorGUILayout.TextField(string.Format("{0} : ", ability.ToString()), Abilitys[(int)ability], GUILayout.ExpandWidth(true));
        }
        for (int i = 0; i < 2; i++)
        {
            SpecialEffects[i] = EditorGUILayout.TextField(string.Format("SpecialEffect/Value : "), SpecialEffects[i], GUILayout.ExpandWidth(true));
        }

        Duration = EditorGUILayout.IntField("Duration : ", Duration, GUILayout.ExpandWidth(true));
        OverlapBuff = EditorGUILayout.Toggle(OverlapBuff, GUILayout.ExpandWidth(true));
        if (GUILayout.Button("Stat Buff", GUILayout.ExpandWidth(false)))
            StatBuff();

        if (GUILayout.Button("Stat Debuff", GUILayout.ExpandWidth(false)))
            StatDebuff();

        EditorGUILayout.Space(10f);

        Value = EditorGUILayout.IntField("Value : ", Value, GUILayout.ExpandWidth(true));
        if (GUILayout.Button("CharcaterDamage", GUILayout.ExpandWidth(false)))
            CharcaterDamage();
        if (GUILayout.Button("CharcaterHPHeal", GUILayout.ExpandWidth(false)))
            CharcaterHPHeal();
        if (GUILayout.Button("CharcaterMPHeal", GUILayout.ExpandWidth(false)))
            CharcaterMPHeal();

        EditorGUILayout.Space(10f);

        if (GUILayout.Button("Instant Dungeon Clear", GUILayout.ExpandWidth(false)))
            InstantDungeonClear();

        if (GUILayout.Button("Instant Dungeon Ouit", GUILayout.ExpandWidth(false)))
            InstantDungeonOuit();

        GUILayout.EndScrollView();
    }

    private List<Character> GetCharacterList()
    {
        var selects = Selection.gameObjects;
        if (selects == null || selects.Length <= 0)
            return null;

        var list = new List<Character>();
        foreach (var select in selects)
        {
            var character = select.GetComponent<Character>();
            if (character != null)
                list.Add(character);
        }
        return list;
    }

    public void CharcaterMPHeal()
    {
        var selects = GetCharacterList();
        if (selects == null)
            return;
        foreach (var select in selects)
        {
            select.Heal_MP(Value);
        }
    }

    public void CharcaterHPHeal()
    {
        var selects = GetCharacterList();
        if (selects == null)
            return;
        foreach (var select in selects)
        {
            select.Heal_HP(Value);
        }
    }

    public void CharcaterDamage()
    {
        var selects = GetCharacterList();
        if (selects == null)
            return;
        foreach (var select in selects)
        {
            select.HitDamage(FloatingEnum.Damage, Value);
        }
    }

    public void StatBuff()
    {
        var index = 0;
        var stats = new CustomBasePer[(int)StatEnum.Max];
        foreach (var Stat in Stats)
        {
            stats[index++] = new CustomBasePer(Stat);
        }
        index = 0;
        var abilitys = new CustomBasePer[(int)AbilityEnum.Max];
        foreach (var Ability in Abilitys)
        {
            abilitys[index++] = new CustomBasePer(Ability);
        }
        index = 0;
        var specialEffects = new CustomSpecialEffect[2];
        foreach (var SpecialEffect in SpecialEffects)
        {
            if (string.IsNullOrEmpty(SpecialEffect))
            {
                specialEffects[index++] = new CustomSpecialEffect(null);
            }
            else
                specialEffects[index++] = new CustomSpecialEffect(SpecialEffect);
        }

        var selects = GetCharacterList();
        if (selects == null)
            return;
        foreach (var select in selects)
        {
            var buff = Util.CreateBuff(CodeName, BuffTypeEnum.Buff, select, stats, abilitys, specialEffects, Duration, OverlapBuff);
            select.Buff(buff);
        }
    }

    public void StatDebuff()
    {
        var index = 0;
        var stats = new CustomBasePer[(int)StatEnum.Max];
        foreach (var Stat in Stats)
        {
            stats[index++] = new CustomBasePer(Stat);
        }
        index = 0;
        var abilitys = new CustomBasePer[(int)AbilityEnum.Max];
        foreach (var Ability in Abilitys)
        {
            abilitys[index++] = new CustomBasePer(Ability);
        }
        index = 0;
        var specialEffects = new CustomSpecialEffect[2];
        foreach (var SpecialEffect in SpecialEffects)
        {
            specialEffects[index++] = new CustomSpecialEffect(SpecialEffect);
        }        

        var selects = GetCharacterList();
        if (selects == null)
            return;
        foreach (var select in selects)
        {
            var debuff = Util.CreateBuff(CodeName, BuffTypeEnum.Debuff, select, stats, abilitys, specialEffects, Duration, OverlapBuff);
            select.Buff(debuff);
        }
    }

    public void InstantDungeonClear()
    {
        GameManager.Instance.SaveData.AddDungeonClearCount();

        UIPrefabManager.Instance.DungeonResultPageProperty.OnClickOpenPageButton();
        GameManager.Instance.OccurEvent(EventKind.PlayTimeStop);
    }

    public void InstantDungeonOuit()
    {
        UIPrefabManager.Instance.DungeonResultPageProperty.OnClickOpenPageButton();
    }

    public void LearnSkillList()
    {
        if (string.IsNullOrEmpty(SkillCodeName))
        {
            GameManager.Instance.DropSkill();
            return;
        }

        var codenames = SkillCodeName.Split('/');
        var skillList = new List<Table_Skill>();
        foreach (var codename in codenames)
        {
            var skill = TableDataManager.Instance.GetTableData<Table_Skill>(codename);
            if (skill != null)
                skillList.Add(skill);
        }

        UIPrefabManager.Instance.SelectSkillPageProperty.OnClickOpenPageButton(skillList);
    }

    public void AllGetMaterialItems()
    {
        List<Table_MaterialItem> items = TableDataManager.Instance.GetTableDataList<Table_MaterialItem>();
        for (int i = 0; i < items.Count; i++)
        {
            ItemStruct item = new ItemStruct(items[i].CodeName, 100, ItemState.Material);
            GameManager.Instance.SaveData.AddMaterialInventory(item);
        }
    }

    public void AllTurn()
    {
        GameManager.Instance.AllTurn();
    }

    public void AddData()
    {
        SaveData save = GameManager.Instance.SaveData;
        save.AddBeginExp(BeginExp);
        save.AddBeginGold(BeginGold);
        save.AddIncreaseExp(IncreaseExp);
        save.AddIncreaseGold(IncreaseGold);
        save.AddIncreaseMaterial(IncreaseMaterial);
        save.AddDungeonCheatClearCount(ClearCount);
        GameManager.Instance.AddExp(Exp);
        GameManager.Instance.AddGold(Gold);
        GameManager.Instance.AddCrystal(Crystal);

        GameManager.Instance.Save();
        GameManager.Instance.OccurEvent(EventKind.UpdateStat);
    }

    public void CreateEnemy()
    {
        Util.CreateEnemy(CodeName, Position);
    }

    public void CreateMaterialItem()
    {
        Util.CreateItem(CodeName, Count, Position, ItemState.Material);
    }

    public void CreateNPC()
    {
        Util.CreateNPC(CodeName, Position);
    }

    public void CreateEquipmentItem()
    {
        ItemStruct item = null;
        Table_EquipmentItem dropEquip = TableDataManager.Instance.GetEquipForGrade(DropGrade);
        if (dropEquip != null)
            item = new ItemStruct(dropEquip.CodeName, 1, ItemState.Equipment);
        if (item != null)
            Util.CreateItem(item.CodeName, item.Count, Position, item.ItemState);
    }

    public void DebugStat()
    {
        Character[] characters = FindObjectsOfType<Character>();
        List<string> list = new List<string>();
        for (int i = 0; i < characters.Length; i++)
        {
            list.Clear();
            list.Add(string.Format("Name : {0}", characters[i].name));
            if (characters[i].CharacterStruct != null)
            {
                list.Add(string.Format("Level : {0}", characters[i].CharacterStruct.Level));
                list.Add(string.Format("Exp : {0}", characters[i].CharacterStruct.Exp));
                list.Add(string.Format("AddStat : {0}", characters[i].CharacterStruct.AddStat));

                list.Add("\n");

                foreach (StatEnum stat in Enum.GetValues(typeof(StatEnum)))
                {
                    if (stat == StatEnum.None || stat == StatEnum.Max)
                        continue;
                    list.Add(string.Format("{0} : {1}", stat.ToString(), characters[i].CharacterStruct.GetTotalStat(stat)));
                }

                list.Add("\n");

                list.Add(string.Format("HP : {0}/{1}", characters[i].CharacterStruct.CurrentHP, characters[i].CharacterStruct.MaxHP));
                list.Add(string.Format("MP : {0}/{1}", characters[i].CharacterStruct.CurrentMP, characters[i].CharacterStruct.MaxMP));

                foreach (AbilityEnum ability in Enum.GetValues(typeof(AbilityEnum)))
                {
                    if (ability == AbilityEnum.None || ability == AbilityEnum.Max)
                        continue;
                    list.Add(string.Format("{0} : {1}", ability.ToString(), characters[i].CharacterStruct.GetTotalAbility(ability)));
                }

                list.Add("\n");

                var iter = characters[i].CharacterStruct.GetEquipItemDic().GetEnumerator();
                while (iter.MoveNext())
                {
                    EquipmentPartEnum part = iter.Current.Key;
                    Table_EquipmentItem value = iter.Current.Value;

                    list.Add(string.Format("{0} : {1}", part, (value == null) ? "null" : TableDataManager.Instance.GetTableText(value.Name)));
                }
            }

            Debug.Log(string.Join(" / ", list));
        }
    }

    public void DebugEquipmentItem()
    {
        List<string> list = new List<string>();

        Dictionary<string, bool> equipment = GameManager.Instance.SaveData.GetReciepeInventory();
        var iter = equipment.GetEnumerator();
        while (iter.MoveNext())
        {
            list.Add(string.Format("Equipment.Codename : {0} Have", iter.Current.Key));
        }

        Debug.Log(string.Join("\n", list));
    }

    public void DebugEnemyKillCount()
    {
        List<string> list = new List<string>();

        Dictionary<string, int> killCount = GameManager.Instance.SaveData.EnemyKillCount;
        var iter = killCount.GetEnumerator();
        while (iter.MoveNext())
        {
            list.Add(string.Format("Enemy.Codename : {0}, Count : {1}", iter.Current.Key, iter.Current.Value));
        }

        Debug.Log(string.Join("\n", list));
    }

    public void DebugInventory()
    {
        List<string> list = new List<string>();

        Dictionary<string, int> inventory = GameManager.Instance.SaveData.MaterialInventory;
        var iter = inventory.GetEnumerator();
        while (iter.MoveNext())
        {
            string key = iter.Current.Key;
            int count = iter.Current.Value;
            list.Add(string.Format("Item.CodeName : {0}, Count : {1}", key, count));
        }

        Debug.Log(string.Join("\n", list));
    }

    public void CaptureImage()
    {
        string folderName = string.Format("F:/Capture/{0:yy-MM-dd}", DateTime.Now);
        if (!Directory.Exists(Path.GetFullPath(folderName)))
            Directory.CreateDirectory(Path.GetFullPath(folderName));

        string time = string.Format("{0:H-mm-ss}", DateTime.Now);
        ScreenCapture.CaptureScreenshot(string.Format("{0}/{1}.png", folderName, time));
    }

    public void ClearConsole()
    {
        var logEntries = Type.GetType("UnityEditor.LogEntries, UnityEditor.dll");
        var clearMethod = logEntries.GetMethod("Clear", BindingFlags.Static | BindingFlags.Public);
        clearMethod.Invoke(null, null);
    }

    public void GotoCaptureFolder()
    {
        string folderName = string.Format("F:/Capture/{0:yy-MM-dd}", DateTime.Now);
        if (!Directory.Exists(Path.GetFullPath(folderName)))
            Directory.CreateDirectory(Path.GetFullPath(folderName));

        Process.Start(folderName);
    }

    public void GotoBuildFolder()
    {
        string folderName = string.Format("F:/Build");
        if (!Directory.Exists(Path.GetFullPath(folderName)))
            Directory.CreateDirectory(Path.GetFullPath(folderName));

        Process.Start(folderName);
    }

    private void Remove()
    {
        string path = string.Format("{0}/SaveData.dat", Application.persistentDataPath);
        if (File.Exists(path))
        {
            Debug.Log("세이브파일을 삭제했습니다.");
            File.Delete(path);
        }
    }
}
