using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;
using System.Linq;

public class GameManager : SingletonGameObject<GameManager>
{
    public event Action<EventKind> EventAction;

    [SerializeField] private string currentScene;
    public string CurrentScene
    {
        get { return currentScene; }
        set { currentScene = value; }
    }

    public SaveData SaveData { get; set; }
    private string Path { get; set; }

    private List<EventProcessor> UIList = new List<EventProcessor>();

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
  
    public void OccurEvent(EventKind _event)
    {
        if (EventAction != null)
            EventAction(_event);

        LogManager.Instance.DebugLogCategory(LogCategoryEnum.UI, string.Format("OccurEvent!!! {0}", _event.ToString()));
    }

    public void GenerateToast(string _message)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        var activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
        activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
        {
            AndroidJavaObject toast = new AndroidJavaClass("android.widget.Toast").CallStatic<AndroidJavaObject>("makeText", activity, _message, 1);
            toast.Call("show");
        }));
#endif
    }

    public void AddUI(EventProcessor _ui)
    {
        if (!UIList.Contains(_ui))
            UIList.Add(_ui);
    }

    public void RemoveUI(EventProcessor _ui)
    {
        if (UIList.Contains(_ui))
            UIList.Remove(_ui);
    }

    public bool UICount()
    {
        if (0 < UIList.Count)
            return true;
        return false;
    }

    public void ClearUI()
    {
        UIList.Clear();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (UICount())
            {
                EventProcessor ui = UIList[UIList.Count - 1];
                if (ui != null)
                    ui.EscapeKeyDown();
            }
            //else
            //{
            //    UsedSkill = null;
            //    SkillTargetList = null;
            //}
        }
    }
}