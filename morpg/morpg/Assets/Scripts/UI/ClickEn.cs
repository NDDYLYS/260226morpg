using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClickEn : MonoBehaviour
{
    [SerializeField] private EncyclopediaEnum menu;
    [SerializeField] private Button btn;
    [SerializeField] private TextMeshProUGUI tmp;
    [SerializeField] private EncyclopediaMenu manager;

    public void menuSelect(EncyclopediaEnum _menu)
    {
        if (menu == _menu)
            tmp.color = Color.white;
        else
            tmp.color = Color.black;
    }

    public void setting(EncyclopediaEnum _menu) 
    {
        menu = _menu;
        btn.onClick.AddListener(() => OnClickButton(_menu));
        tmp.text = _menu.ToString().GetTableText();
    }

    public void OnClickButton(EncyclopediaEnum _menu)
    {
        manager.OnClickEnumButton(_menu);
    }
}
