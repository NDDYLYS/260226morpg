using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


//UIPrefabManager.Instance.UIMessageBoxProperty.OnClickOpenPageButton("HappenedEvent", delegate (MessageBoxClick click)
//{
//    if (click == MessageBoxClick.Confirm)
//    {
//        MovingScene("4EventScene");
//    }
//}, EventType.ToString(), EventType.ToString());

public class MessageBox : EventProcessor
{
    public delegate void CallbackMethod(MessageBoxClick _click);
    private CallbackMethod Callback { get; set; }

    public TextMeshProUGUI UIDesc;
    public GameObject OneButton;
    public TextMeshProUGUI OneConfirm;

    public GameObject TwoButton;
    public TextMeshProUGUI TwoConfirm;
    public TextMeshProUGUI TwoCancel;

    private Table_MessageBox Table_MessageBox { get; set; }
    
    public void OnClickOpenPageButton(string _codename, CallbackMethod _click, params string[] _parameters)
    {
        Table_MessageBox messageBox = TableDataManager.Instance.GetTableData<Table_MessageBox>(_codename);
        Table_MessageBox = messageBox;
        if (Table_MessageBox != null)
        {
            UIDesc.text = string.Format(messageBox.KoreanDesc, _parameters);
            if (Table_MessageBox.ButtonCount == 1)
            {
                // 1버튼
                OneButton.SetActive(true);
                TwoButton.SetActive(false);
                OneConfirm.text = messageBox.KoreanConfirmButton;

            }
            else if (Table_MessageBox.ButtonCount == 2)
            {
                // 2버튼
                OneButton.SetActive(false);
                TwoButton.SetActive(true);
                TwoConfirm.text = messageBox.KoreanConfirmButton;
                TwoCancel.text = messageBox.KoreanCancelButton;
            }

            Callback = _click;
        }

        base.OpenPage();
    }

    public void OnClickConfirmButton()
    {
        OnClickClosePageButton();
        if (Callback != null)
            Callback(MessageBoxClick.Confirm);
    }

    public void OnClickCancelButton()
    {
        OnClickClosePageButton();
        if (Callback != null)
            Callback(MessageBoxClick.Cancel);
    }

    public void OnClickClosePageButton()
    {
        base.ClosePage();
    }

    public override void EscapeKeyDown()
    {
        if (!Container.activeInHierarchy)
            return;

        if (Table_MessageBox != null)
        {
            if (!Table_MessageBox.IsEscable)
                return;
            OnClickClosePageButton();
            if (Callback != null)
                Callback(MessageBoxClick.Cancel);
        }
    }
}