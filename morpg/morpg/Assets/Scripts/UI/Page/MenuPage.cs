using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;




public class MenuPage : EventProcessor
{
    [SerializeField] private TextMeshProUGUI playTime;
    [SerializeField] private GameObject submenu_option;

    private MenuEnum menuEnum;

    public void OnClickOpenPageButton()
    {
        GameManager.Instance.SetGameState(GameStateEnum.Stop);
        base.OpenPage();
        OnClickOneButton();
    }

    public void OnClickClosePageButton()
    {
        GameManager.Instance.SetGameState(GameStateEnum.Play);
        base.ClosePage();
    }

    public override void EscapeKeyDown()
    {
        if (!Container.activeInHierarchy)
            return;

        OnClickClosePageButton();
    }

    private void SettingUI() 
    {
        subMenuActive(false);


        switch (menuEnum)
        {
            case MenuEnum.One:
                playTime.text = Util.GetTimer(GameManager.Instance.SaveData.getPlaytime());
                break;
            case MenuEnum.Two:
                break;
            case MenuEnum.Three:
                break;
            case MenuEnum.Option:
                UIPrefabManager.Instance.OptionPageProperty.OnClickOpenPageButton();
                break;
            case MenuEnum.Menu:
                subMenuActive(true);
                break;
            default:
                break;
        }
    }

    public void OnClickOneButton()
    {
        menuEnum = MenuEnum.One;
        SettingUI();
    }

    public void OnClickTwoButton()
    {
        menuEnum = MenuEnum.Two;
        SettingUI();
    }

    public void OnClickThreeButton()
    {
        menuEnum = MenuEnum.Three;
        SettingUI();
    }


    public void OnClickOptionButton()
    {
        menuEnum = MenuEnum.Option;
        SettingUI();
    }

    public void OnClickMenuButton()
    {
        menuEnum = MenuEnum.Menu;
        SettingUI();
    }

    private void subMenuActive(bool _active)
    {
        submenu_option.SetActive(_active);
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
