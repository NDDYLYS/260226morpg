using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EncyclopediaObject : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmp;
    private string title;
    private EncyclopediaMenu menu;

    public void SettingUI(string _title, EncyclopediaMenu _menu) 
    {
        title = _title;
        if (menu == null)
            menu = _menu;

        tmp.text = title.GetTableText();
    }

    public void OnClickDescButton()
    {
        var textList = new List<string>();
        var list = TableDataManager.Instance.getEncyclopediaDesc(title);
        foreach (var item in list) 
        {
            textList.Add(item.GetTableText());
        }

        var text = string.Join("\n\n", textList.ToArray());

        menu.SettingDesc(text);
    }
}