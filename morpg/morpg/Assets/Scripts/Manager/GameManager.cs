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

    [SerializeField] private GameState gameState;
    public GameState GameState
    {
        get { return gameState; }
        set { gameState = value; }
    }

    public SaveData SaveData { get; set; }
    private string Path { get; set; }

    private List<EventProcessor> UIList = new List<EventProcessor>();

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        GameState = GameState.None;

        Path = string.Format("{0}/SaveData.dat", Application.persistentDataPath); // "F:/UnityProject/Roguelike/Assets/SaveData.dat"
    }

    public void Initialize()
    {
        Load();
        //ChangedGameState(GameState.Ready);
    }
    
    public void ChangedGameState(GameState _state, bool _isFirst = true)
    {
        GameState = _state;

        //switch (GameState)
        //{
        //    case GameState.Ready:
        //        break;
        //    case GameState.Pause:
        //        break;
        //    case GameState.Play:
        //        // 마을과 매 챕터의 첫번째 방에서 호출한다
        //        if (_isFirst)
        //        {
        //            OccurEvent(EventKind.GameStart);
        //            AllTurn(true);
        //        }
        //        break;
        //}
    }

    public void OccurEvent(EventKind _event)
    {
        if (EventAction != null)
            EventAction(_event);

        LogManager.Instance.DebugLogCategory(LogCategoryEnum.UI, string.Format("OccurEvent!!! {0}", _event.ToString()));
    }

    public void Save()
    {
        //var b = new BinaryFormatter();
        //Stream stream = new FileStream(Path, FileMode.Create, FileAccess.Write);
        //if (stream != null)
        //{
        //    if (SaveData != null && SaveData.InstantDungeon != null && Player != null)
        //        SaveData.InstantDungeon.Character = Player.CharacterStruct;
        //    b.Serialize(stream, SaveData);
        //    stream.Close();
        //    LogManager.Instance.DebugLogCategory(LogCategoryEnum.UI, "세이브파일을 저장했습니다.");
        //}
    }

    //public void Remove()
    //{
    //    string path = string.Format("{0}/SaveData.dat", Application.persistentDataPath);
    //    if (File.Exists(path))
    //    {
    //        LogManager.Instance.DebugLogCategory(LogCategoryEnum.UI, "세이브파일을 삭제했습니다.");
    //        File.Delete(path);
    //    }
    //}

    public void Load()
    {
        //if (File.Exists(Path))
        //{
        //    Stream stream = new FileStream(Path, FileMode.Open, FileAccess.Read);
        //    if (stream != null)
        //    {
        //        var b = new BinaryFormatter();
        //        SaveData = b.Deserialize(stream) as SaveData;
        //        SaveData = SaveData.Reset();
        //        LogManager.Instance.DebugLogCategory(LogCategoryEnum.UI, "세이브파일을 불러왔습니다.");
        //    }
        //}
        //else
        //{
        //    InitSaveData();
        //    Save();
        //    LogManager.Instance.DebugLogCategory(LogCategoryEnum.UI, "세이브파일이 존재하지 않아 새로 생성했습니다.");
        //}
    }

    public void InitSaveData()
    {
        SaveData = new SaveData();
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