using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClickEn : MonoBehaviour
{
    [SerializeField] private EncyclopediaEnum menu;
    private TextMeshProUGUI tmp;

    private void Awake()
    {
        tmp = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void menuSelect(EncyclopediaEnum _menu)
    {
        if (menu == _menu)
            tmp.color = Color.white;
        else
            tmp.color = Color.black;
    }
}
