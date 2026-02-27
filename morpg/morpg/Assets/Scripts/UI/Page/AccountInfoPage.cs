using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class AccountInfoPage : EventProcessor
{
    //[Header("Common")]
    //public ScrollRect ScrollRect;
    //public RectTransform Content;
    //public Text Prefab;
    
    //private List<Text> TextList = new List<Text>(); // 사용 중인 객체

    //public void OnClickOpenPageButton()
    //{
    //    ScrollRect.verticalNormalizedPosition = 1f;
    //    ResetUI();
    //    base.OpenPage();
    //}

    //private void ResetUI()
    //{
    //    List<string> list = GetAccountInfoTextList();
    //    for (int i = 0; i < list.Count; i++)
    //    {
    //        Text text = AddText(i);
    //        text.text = list[i];
    //    }
    //}

    //private List<string> GetAccountInfoTextList()
    //{
    //    SaveData save = GameManager.Instance.SaveData;
    //    List<string> list = new List<string>();
    //    list.Add(string.Format("UI_TotalPlayTime".GetTableText(), Util.GetTimer(save.TotalPlayTime)));
    //    list.Add(string.Format("UI_TotalTurnCount".GetTableText(), save.TotalTurnCount));

    //    list.Add(string.Empty);
    //    var difficultyIndex = 0;
    //    var tryCounts = save.GetDungeonTryCounts();
    //    foreach (var tryCount in tryCounts)
    //    {
    //        var difficulty = $"UI_{((DifficultyEnum)difficultyIndex++).ToString()}".GetTableText();
    //        list.Add(string.Format("UI_DungeonTryCount".GetTableText(), difficulty, tryCount));
    //    }
    //    difficultyIndex = 0;
    //    var clearCounts = save.GetDungeonClearCounts();
    //    foreach (var clearCount in clearCounts)
    //    {
    //        var difficulty = $"UI_{((DifficultyEnum)difficultyIndex++).ToString()}".GetTableText();
    //        list.Add(string.Format("UI_DungeonClearCount".GetTableText(), difficulty, clearCount));
    //    }
    //    list.Add(string.Empty); 

    //    list.Add(string.Format("UI_BeginExp".GetTableText(), save.GetBeginExp()));
    //    list.Add(string.Format("UI_BeginGold".GetTableText(), save.GetBeginGold()));
    //    list.Add(string.Empty); 
    //    list.Add(string.Format("UI_IncreaseExp".GetTableText(), save.GetIncreaseExp()));
    //    list.Add(string.Format("UI_IncreaseGold".GetTableText(), save.GetIncreaseGold()));
    //    list.Add(string.Format("UI_IncreaseMaterial".GetTableText(), save.GetIncreaseMaterial()));

    //    return list;
    //}

    //public void OnClickClosePageButton()
    //{
    //    base.ClosePage();
    //}

    //public override void EscapeKeyDown()
    //{
    //    if (!Container.activeInHierarchy)
    //        return;

    //    OnClickClosePageButton();
    //}

    //private Text AddText(int _index)
    //{
    //    Text text = null;
    //    if (_index < TextList.Count)
    //    {
    //        text = TextList[_index];
    //    }
    //    else
    //    {
    //        GameObject obj = Util.CreateObject(Prefab.gameObject, Content, Vector3.zero, Vector3.one);
    //        text = obj.GetComponent<Text>();
    //        TextList.Add(text);
    //    }

    //    text.gameObject.SetActive(true);
    //    return text;
    //}
}
