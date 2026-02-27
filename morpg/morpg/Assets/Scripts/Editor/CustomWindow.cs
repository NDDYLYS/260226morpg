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

        //if (GUILayout.Button("Save SaveFile", GUILayout.ExpandWidth(false)))
        //    GameManager.Instance.Save();

        //if (GUILayout.Button("Load SaveFile", GUILayout.ExpandWidth(false)))
        //    GameManager.Instance.Load();

        //if (GUILayout.Button("Remove SaveFile", GUILayout.ExpandWidth(false)))
        //    Remove();

        //EditorGUILayout.Space(10f);

        //if (GUILayout.Button("Capture", GUILayout.ExpandWidth(false)))
        //    CaptureImage();

        //if (GUILayout.Button("Go to CaptureFolder", GUILayout.ExpandWidth(false)))
        //    GotoCaptureFolder();

        //if (GUILayout.Button("Go to BuildFolder", GUILayout.ExpandWidth(false)))
        //    GotoBuildFolder();

        GUILayout.EndScrollView();
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
