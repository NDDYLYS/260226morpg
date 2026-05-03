using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EncyclopediaMenu : MonoBehaviour
{
    private EncyclopediaEnum encyclopediaEnum;
    private List<ClickEn> menuList;

    private void Awake()
    {
        menuList = GetComponentsInChildren<ClickEn>().ToList();
    }

    private void OnEnable()
    {
        encyclopediaEnum = EncyclopediaEnum.None;
    }

    public void OnClickCocruwaButton() 
    {
        encyclopediaEnum = EncyclopediaEnum.Cocruwa;

        foreach (var clicked in menuList)
        {
            clicked.menuSelect(encyclopediaEnum);
        }
    }

    public void OnClickTermButton()
    {
        encyclopediaEnum = EncyclopediaEnum.Term;

        foreach (var clicked in menuList)
        {
            clicked.menuSelect(encyclopediaEnum);
        }
    }

    public void OnClickHistoryButton()
    {
        encyclopediaEnum = EncyclopediaEnum.History;

        foreach (var clicked in menuList)
        {
            clicked.menuSelect(encyclopediaEnum);
        }
    }
}
