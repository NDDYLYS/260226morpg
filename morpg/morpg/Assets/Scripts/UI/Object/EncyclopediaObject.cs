using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EncyclopediaObject : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmp;

    public void SettingUI(string _title) 
    {
        tmp.text = _title.GetTableText();
    }
}
