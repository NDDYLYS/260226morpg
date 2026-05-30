using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EquipmentItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI name; 
    [SerializeField] private TextMeshProUGUI count;
    private ItemData itemData;

    public void SettingUI(ItemData _itemData) 
    {
        itemData = _itemData;
        name.text = $"{itemData.TableItem.CodeName}_Name".GetTableText();
        count.text = $"x{itemData.Count}"; 
    }

    public void OnClickItemButton()
    {
        Debug.Log($"item({itemData.TableItem.CodeName}) click");
    }
}