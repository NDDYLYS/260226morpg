using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionPage : EventProcessor
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
