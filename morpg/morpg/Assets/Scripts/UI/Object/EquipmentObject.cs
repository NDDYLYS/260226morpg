using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EquipmentObject : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmp;
    private string part;
    private EquipmentMenu menu;

    public void SettingUI(string _part, EquipmentMenu _menu) 
    {
        part = _part;
        if (menu == null)
            menu = _menu;

        tmp.text = part.GetTableText();
    }

    public void OnClickPartButton()
    {
        Debug.Log($"item-equipment-{part}");
    }
}