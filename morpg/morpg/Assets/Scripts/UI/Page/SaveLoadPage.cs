using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;




public class SaveLoadPage : EventProcessor
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private RectTransform content;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject prefab;

    private List<SaveLoadObject> objects;

    public void OnClickOpenPageButton()
    {
        scrollRect.verticalNormalizedPosition = 0;
        base.OpenPage();

        settingUI();
    }

    private void settingUI() 
    {
        if (objects == null)
            objects = new List<SaveLoadObject>();
        else 
            objects.Clear();

        if (1 <= content.transform.childCount)
        {
            var children = content.GetComponentsInChildren<Transform>();
            for (var i = 1; i < children.Length; i++)
            {
                Destroy(children[i].gameObject);
            }
        }


        if (prefab == null)
            return;

        SaveData save = new SaveData();

        var slot = Constant.dataSlot;

        for (var i = 0; i < slot; i++) 
        {
            var obj = Util.CreateObject(prefab, content, Vector2.zero, Vector2.one);
            obj.SetActive(true);

            var saveload = obj.GetComponent<SaveLoadObject>();
            saveload.SettingUI(i+1);
            objects.Add(saveload);
        }
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
