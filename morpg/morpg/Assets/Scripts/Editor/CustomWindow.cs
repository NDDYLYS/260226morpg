using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Unity.Jobs;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;





public class CustomWindow : EditorWindow
{
    private Vector2 ScrollPosition { get; set; }
    private Vector2 Position { get; set; }

    private int index { get; set; }
    private string textValue { get; set; }

    private SpeciesEnum species { get; set; }
    private JobEnum job { get; set; }
    private string encyclopedia {  get; set; }


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


        DrawHeader("Base");

        GUILayout.BeginHorizontal();

        Time.timeScale = EditorGUILayout.Slider(new GUIContent("TimeScale", $"인게임의 속도를 조절한다.(0~10)"), Time.timeScale, 0f, 10f);

        if (GUILayout.Button("Capture", GUILayout.ExpandWidth(false)))
            CaptureImage();

        if (GUILayout.Button("Go to CaptureFolder", GUILayout.ExpandWidth(false)))
            GotoCaptureFolder();

        if (GUILayout.Button("Go to BuildFolder", GUILayout.ExpandWidth(false)))
            GotoBuildFolder();

        GUILayout.EndHorizontal();

        EditorGUILayout.Space(10f);

        DrawHeader("Save&Load");

        GUILayout.BeginHorizontal();

        index = EditorGUILayout.IntField("index : ", index, GUILayout.ExpandWidth(true));
        if (index <= 1)
            index = 1;
        if (Constant.dataSlot < index)
            index = Constant.dataSlot;

        if (GUILayout.Button("Load", GUILayout.ExpandWidth(false)))
            UIPrefabManager.Instance.SaveLoadPageProperty.OnClickOpenPageButton(SaveLoadEnum.Load); //UGSManager.Instance.Load(index);

        if (GUILayout.Button("Save", GUILayout.ExpandWidth(false)))
            UIPrefabManager.Instance.SaveLoadPageProperty.OnClickOpenPageButton(SaveLoadEnum.Save);//UGSManager.Instance.Save(index);

        GUILayout.EndHorizontal();

        EditorGUILayout.Space(10f);

        DrawHeader("Debug");

        GUILayout.BeginHorizontal();
        textValue = EditorGUILayout.TextField("text : ", textValue, GUILayout.ExpandWidth(true));

        if (GUILayout.Button("Log", GUILayout.ExpandWidth(false)))
            Debug.Log(textValue);

        if (GUILayout.Button("LogWarning", GUILayout.ExpandWidth(false)))
            Debug.LogWarning(textValue);

        if (GUILayout.Button("LogError", GUILayout.ExpandWidth(false)))
            Debug.LogError(textValue);

        GUILayout.EndHorizontal();

        EditorGUILayout.Space(10f);

        DrawHeader("Species*Job");

        GUILayout.BeginHorizontal();

        species = (SpeciesEnum)EditorGUILayout.EnumPopup("species : ", species, GUILayout.ExpandWidth(true));

        if (GUILayout.Button("Add Species", GUILayout.ExpandWidth(false)))
        {
            if (GameManager.Instance?.SaveData != null)
                GameManager.Instance.SaveData.AddSpecies(species);
        }

        if (GUILayout.Button("Change Species", GUILayout.ExpandWidth(false)))
        {
            if (GameManager.Instance?.SaveData != null)
                GameManager.Instance.SaveData.Species = species;
        }

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();

        job = (JobEnum)EditorGUILayout.EnumPopup("job : ", job, GUILayout.ExpandWidth(true));

        if (GUILayout.Button("Add Job", GUILayout.ExpandWidth(false)))
        {
            if (GameManager.Instance?.SaveData != null)
                GameManager.Instance.SaveData.AddJob(job);
        }

        if (GUILayout.Button("Change Job", GUILayout.ExpandWidth(false)))
        {
            if (GameManager.Instance?.SaveData != null)
                GameManager.Instance.SaveData.Job = job;
        }

        GUILayout.EndHorizontal();

        DrawHeader("Encyclopedia");

        GUILayout.BeginHorizontal();

        encyclopedia = EditorGUILayout.TextField("encyclopedia : ", encyclopedia, GUILayout.ExpandWidth(true));

        if (GUILayout.Button("Add Encyclopedia", GUILayout.ExpandWidth(false)))
        {
            if (GameManager.Instance?.SaveData != null)
            {
                var encyclopediaT = TableDataManager.Instance.GetTableData<Table_Encyclopedia>(encyclopedia);
                if (encyclopediaT == null)
                    return;
                GameManager.Instance.SaveData.AddEncyclopedia(encyclopedia);
            }
        }

        if (GUILayout.Button("Add Random 5 Encyclopedia", GUILayout.ExpandWidth(false)))
        {
            AddRandom5Encyclopedia();
        }

        GUILayout.EndHorizontal();

        GUILayout.EndScrollView();
    }


    private void AddRandom5Encyclopedia()
    {
        var all = TableDataManager.Instance.GetTableDataList<Table_Encyclopedia>();
        all = Util.ShuffleAlgorithm(all, 10);

        for (var i = 0; i < 5; i++)
        {
            if (GameManager.Instance?.SaveData != null)
            {
                var noAdd = GameManager.Instance.SaveData.AddEncyclopedia(all[i].CodeName);
                if (!noAdd)
                {
                    all = Util.ShuffleAlgorithm(all, 10);
                    i--;
                }
            }
        }
    }

    public void CaptureImage()
    {
        string folderName = string.Format("D:/Capture/{0:yy-MM-dd}", DateTime.Now);
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
        string folderName = string.Format("D:/Capture/{0:yy-MM-dd}", DateTime.Now);
        if (!Directory.Exists(Path.GetFullPath(folderName)))
            Directory.CreateDirectory(Path.GetFullPath(folderName));

        Process.Start(folderName);
    }

    public void GotoBuildFolder()
    {
        string folderName = string.Format("D:/Build");
        if (!Directory.Exists(Path.GetFullPath(folderName)))
            Directory.CreateDirectory(Path.GetFullPath(folderName));

        Process.Start(folderName);
    }

    private void DrawHeader(string _title)
    {
        EditorGUILayout.Space(10);

        GUIStyle style = new GUIStyle(EditorStyles.boldLabel);
        style.fontSize = 14;

        EditorGUILayout.LabelField(_title, style);

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
    }
}
