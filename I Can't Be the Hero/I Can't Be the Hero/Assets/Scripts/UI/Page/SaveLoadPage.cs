using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;




public class SaveLoadPage : EventProcessor
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private List<SaveLoadObject> list;
    [SerializeField] private TextMeshProUGUI text;

    private SaveLoadEnum saveLoadEnum;

    public void OnClickOpenPageButton(SaveLoadEnum _saveLoad)
    {
        saveLoadEnum = _saveLoad;

        scrollRect.verticalNormalizedPosition = 1;
        base.OpenPage();

        settingUI();
    }

    private void settingUI() 
    {
        text.text = saveLoadEnum.ToString().ToLower().GetTableText();

        var index = 1;
        foreach (var save in list)
        {
            save.SettingUI(index++, saveLoadEnum);
        }
    }

    public void OnClickClosePageButton()
    {
        base.ClosePage();
    }

    public override void EscapeKeyDown()
    {
        if (!Container.activeInHierarchy)
            return;

        OnClickClosePageButton();
    }
}
