using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;




public class MenuPage : EventProcessor
{
    [SerializeField] private TextMeshProUGUI playTime;
    [SerializeField] private GameObject submenu;
    [SerializeField] private GameObject equipment_menu;
    [SerializeField] private GameObject speciesjob_menu;
    [SerializeField] private GameObject encyclopedia_menu;


    private MenuEnum menuEnum;
    private List<ClickMenu> menuList;

    private void Awake()
    {
        menuList = GetComponentsInChildren<ClickMenu>().ToList();
    }

    public void OnClickOpenPageButton()
    {
        GameManager.Instance.SetGameState(GameStateEnum.Stop);
        base.OpenPage();
        OnClickMenuButton();
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
        menuHIde();
        subMenuActive(false);

        foreach (var clicked in menuList) 
        {
            clicked.menuSelect(menuEnum);
        }

        switch (menuEnum)
        {
            case MenuEnum.Menu:
                playTime.text = Util.GetTimer(GameManager.Instance.SaveData.playTime);
                break;
            case MenuEnum.Equipment:
                equipment_menu.SetActive(true);
                break;
            case MenuEnum.SpeciesJob:
                speciesjob_menu.SetActive(true);
                break;
            case MenuEnum.Encyclopedia:
                encyclopedia_menu.SetActive(true);
                break;
            case MenuEnum.Option:
                UIPrefabManager.Instance.OptionPageProperty.OnClickOpenPageButton();
                break;
            case MenuEnum.SubMenu:
                subMenuActive(true);
                break;
            default:
                break;
        }
    }

    public void OnClickMenuButton()
    {
        menuEnum = MenuEnum.Menu;
        SettingUI();
    }

    public void OnClickEquipmentButton()
    {
        menuEnum = MenuEnum.Equipment;
        SettingUI();
    }

    public void OnClickSpeciesJobButton()
    {
        menuEnum = MenuEnum.SpeciesJob;
        SettingUI();
    }

    public void OnClickEncyclopediaButton()
    {
        menuEnum = MenuEnum.Encyclopedia;
        SettingUI();
    }


    public void OnClickOptionButton()
    {
        menuEnum = MenuEnum.Option;
        SettingUI();
    }

    public void OnClickSubMenuButton()
    {
        menuEnum = MenuEnum.SubMenu;
        SettingUI();
    }

    private void menuHIde()
    {
        equipment_menu.SetActive(false);
        speciesjob_menu.SetActive(false);
        encyclopedia_menu.SetActive(false);
    }

    private void subMenuActive(bool _active)
    {
        submenu.SetActive(_active);
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
