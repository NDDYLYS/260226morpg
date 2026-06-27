using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class OptionPage : EventProcessor
{
    [SerializeField] private TextMeshProUGUI playerId;

    public void OnClickOpenPageButton()
    {
        settingUI();
        base.OpenPage();
    }

    public void OnClickClosePageButton()
    {
        base.ClosePage();
    }

    private void settingUI() 
    {
        playerId.text = LocalManager.Instance.playerId;
    }

    public void OnClickCopyButton() 
    {
        GUIUtility.systemCopyBuffer = LocalManager.Instance.playerId;
    }

    public void OnClickBugReportFolderButton()
    {
        string path = Application.persistentDataPath;

        Process.Start(path);
    }

    public override void EscapeKeyDown()
    {
        if (!Container.activeInHierarchy)
            return;

        OnClickClosePageButton();
    }
}
