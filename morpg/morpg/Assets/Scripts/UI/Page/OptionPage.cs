using System.Collections;
using System.Collections.Generic;
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

    public override void EscapeKeyDown()
    {
        if (!Container.activeInHierarchy)
            return;

        OnClickClosePageButton();
    }
}
