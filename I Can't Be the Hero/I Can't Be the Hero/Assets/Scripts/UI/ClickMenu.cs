using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickMenu : MonoBehaviour
{
    [SerializeField] private MenuEnum menu;
    private TextMeshProUGUI tmp;

    private void Awake()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }

    public void menuSelect(MenuEnum _menu) 
    {
        if (menu == _menu)
            tmp.color = Color.white;
        else 
            tmp.color = Color.black;
    }
}
