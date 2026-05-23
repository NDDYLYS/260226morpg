using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameManager : SingletonGameObject<GameManager>
{
    public event Action<EventKind> EventAction;

    [SerializeField] private string currentScene;
    public string CurrentScene
    {
        get { return currentScene; }
        set { currentScene = value; }
    }

    public SaveData SaveData;
    public GameStateEnum GameState;
    private GameObject Player;
    private SpeciesEnum beforeSpecies;


    private List<EventProcessor> UIList = new List<EventProcessor>();

    public void create()
    {
    }

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

    public bool ContainUI(EventProcessor _ui)
    {
        return UIList.Contains(_ui);
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
        updatePlayTime();

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

    private void updatePlayTime()
    {
        if (SaveData == null)
            return;
        if (EqualsGameState(GameStateEnum.Stop) == true)
            return;
        SaveData.PlayTime += Time.fixedDeltaTime;
    }

    public void SetGameState(GameStateEnum _state)
    {
        GameState = _state;
    }

    public bool EqualsGameState(GameStateEnum _state)
    {
        return GameState == _state;
    }

    /// <summary>
    /// 씬 이동
    /// </summary>
    /// <param name="_scene"></param>
    public void MovingScene(string _scene)
    {
        StartCoroutine(LoadAsynchronously(_scene));
    }

    private IEnumerator LoadAsynchronously(string sceneIndex)
    {
        ClearUI();
        CurrentScene = sceneIndex;
        AsyncOperation operation = SceneManager.LoadSceneAsync(CurrentScene);

        //loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            //slider.value - progress;
            //text.text = progress * 100f;

            yield return null;
        }
    }

    public void setPlayer(GameObject _obj, SpeciesEnum _species)
    {
        Player = _obj;
        beforeSpecies = _species;
    }

    public GameObject getPlayer() 
    { 
        return Player; 
    }

    public void changePlayer(SpeciesEnum _species)
    {
        if (beforeSpecies != _species)
        {
            var player = getPlayer();
            var position = player.transform.position;
            var rotation = player.transform.rotation;
            GameObject.Destroy(player.gameObject);

            var prefab = TableDataManager.Instance.GetLoadedPrefab($"Units/{_species.ToString()}");
            var unit = Util.CreateObject(prefab, null, Vector3.zero, Vector3.one);
            var animator = unit.transform.GetChild(0).AddComponent<player>();
            setPlayer(unit.gameObject, _species);
            unit.transform.position = position;
            unit.transform.rotation = rotation;
        }
    }

    private async Task<DateTime> GetNetworkTime()
    {
        using UnityWebRequest req =
            UnityWebRequest.Head("https://www.google.com");

        var operation = req.SendWebRequest();

        while (!operation.isDone)
            await Task.Yield();

        string date = req.GetResponseHeader("date");

        if (!string.IsNullOrEmpty(date))
        {
            return DateTime.Parse(date).ToUniversalTime();
        }

        return DateTime.MinValue;
    }

    [Button]
    public async Task<string> getTime(TimeEnum _time)
    {
        var utc = await GetNetworkTime();
        var localTime = utc.ToLocalTime();

        var text = string.Empty;
        switch (_time)
        {
            case TimeEnum.All:
                text = localTime.ToString();
                break;
            case TimeEnum.Year:
                text = localTime.Year.ToString();
                break;
            case TimeEnum.Month:
                text = localTime.Month.ToString();
                break;
            case TimeEnum.Day:
                text = localTime.Day.ToString();
                break;
            case TimeEnum.DayofWeek:
                text = localTime.DayOfWeek.ToString();
                break;
            case TimeEnum.Hours:
                text = localTime.Hour.ToString();
                break;
            case TimeEnum.Minute:
                text = localTime.Minute.ToString();
                break;
            case TimeEnum.Second:
                text = localTime.Second.ToString();
                break;
            default:
                break;
        }

        //int year = localTime.Year;
        //int month = localTime.Month;
        //int day = localTime.Day;
        //DayOfWeek dayOfWeek = localTime.DayOfWeek;
        //string koreanDay = localTime.ToString("dddd");

        return text;
    }

    //[Button]
    //public async void getTime()
    //{
    //    var utc = await GetNetworkTime();
    //    var localTime = utc.ToLocalTime();

    //    Debug.Log(localTime);
    //}
}