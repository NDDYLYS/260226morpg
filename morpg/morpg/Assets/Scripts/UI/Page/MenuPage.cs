using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;




public class MenuPage : EventProcessor
{
    public void OnClickOpenPageButton()
    {
        base.OpenPage();
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

    public void OnClickOptionButton()
    {
        UIPrefabManager.Instance.OptionPageProperty.OnClickOpenPageButton();
    }

    public void OnClickMenuButton()
    {
    }

    public void OnClickTitleButton() 
    {
        GameManager.Instance.SaveData = null;
        GameManager.Instance.MovingScene("1Start");
    }

    public void OnClickLoadButton()
    {
        UIPrefabManager.Instance.SaveLoadPageProperty.OnClickOpenPageButton(SaveLoadEnum.Load);
    }

    public void OnClickExitButton()
    {
        UIPrefabManager.Instance.MessageBoxProperty.OnClickOpenPageButton("GameExit", delegate (MessageBoxClick click)
        {
            if (click == MessageBoxClick.Confirm)
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
            }
        });
    }
}
