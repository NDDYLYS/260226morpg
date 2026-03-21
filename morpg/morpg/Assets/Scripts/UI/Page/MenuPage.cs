using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;




public class MenuPage : EventProcessor
{
    public void OnClickOpenPageButton()
    {
        base.OpenPage();
    }

    public void OnClickClosePageButton()
    {
        base.ClosePage();
    }

    public override void EscapeKeyDown()
    {
        if (!Container.activeInHierarchy)
            return;

        OnClickClosePageButton();
    }
}
