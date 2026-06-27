using System.Collections;
using UnityEngine;
using UnityEngine.UI;



public class PlayTime : EventProcessor
{
    private Text Text { get; set; }
    private string PlayTimeValue { get; set; }
    private SaveData SaveData { get; set; }
    private string TextFormat { get; set; }

    void Awake()
    {
        Text = GetComponent<Text>();
        SaveData = GameManager.Instance.SaveData;
    }

    private void Start()
    {
        TextFormat = "UI_TurnPlayTime".GetTableText();
    }

    void OnEnable()
    {
        base.AddAction();
    }

    void OnDisable()
    {
        base.RemoveAction();
    }

    //public override void EventProcessorMethod(EventKind _eventType)
    //{
    //    if (_eventType == EventKind.PlayTimeStart)
    //    {
    //        StartCoroutine("PlayTimerCoroutine");
    //    }
    //    else if (_eventType == EventKind.PlayTimeStop)
    //    {
    //        StopCoroutine("PlayTimerCoroutine");
    //    }
    //    else if (_eventType == EventKind.TurnEnd)
    //    {
    //        string value = GetTotalString();
    //        Text.text = value;
    //    }
    //}

    //private IEnumerator PlayTimerCoroutine()
    //{
    //    while (true)
    //    {
    //        if (GameManager.Instance.GameState != GameState.Pause)
    //        {
    //            if (SaveData.InstantDungeon == null)
    //            {
    //                SaveData.TotalPlayTime += 1;
    //                string value = string.Format("{0}\n{1}턴 경과", Util.GetTimer(SaveData.TotalPlayTime), SaveData.TotalTurnCount);

    //                Text.text = value;
    //            }
    //            else
    //            {
    //                SaveData.InstantDungeon.PlayTime += 1;
    //                PlayTimeValue = string.Format("{0}", Util.GetTimer(SaveData.InstantDungeon.PlayTime));
    //                string value = GetTotalString();
    //                Text.text = value;
    //            }
    //        }

    //        yield return new WaitForSeconds(1f);
    //    }
    //}

    //private string GetTotalString()
    //{
    //    string value = string.Format(TextFormat, PlayTimeValue, SaveData.InstantDungeon.TurnCount);
    //    return value;
    //}
}
