using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;

public class EncyclopediaMenu : MonoBehaviour
{
    private EncyclopediaEnum encyclopediaEnum;
    private List<ClickEn> menuList;

    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private Transform content;
    [SerializeField] private EncyclopediaObject item;
    [SerializeField] private TextMeshProUGUI desc;

    private Stack<EncyclopediaObject> objectPool; // 미사용
    private List<EncyclopediaObject> objectList; // 사용 중

    private void Awake()
    {
        menuList = GetComponentsInChildren<ClickEn>().ToList();
        objectPool = new Stack<EncyclopediaObject>();
        objectList = new List<EncyclopediaObject>();
    }

    private void OnEnable()
    {
        encyclopediaEnum = EncyclopediaEnum.None;
    }

    public void Refresh()
    {
        encyclopediaEnum = EncyclopediaEnum.Cocruwa;
        clickedMenuFocus();
        changeList(encyclopediaEnum);
    }

    public void OnClickCocruwaButton() 
    {
        encyclopediaEnum = EncyclopediaEnum.Cocruwa;
        clickedMenuFocus();
        changeList(encyclopediaEnum);
    }

    public void OnClickTermButton()
    {
        encyclopediaEnum = EncyclopediaEnum.Term;
        clickedMenuFocus();
        changeList(encyclopediaEnum);
    }

    public void OnClickHistoryButton()
    {
        encyclopediaEnum = EncyclopediaEnum.History;
        clickedMenuFocus();
        changeList(encyclopediaEnum);
    }

    private void clickedMenuFocus() 
    {
        foreach (var clicked in menuList)
        {
            clicked.menuSelect(encyclopediaEnum);
        }
    }

    private void changeList(EncyclopediaEnum _enum) 
    {
        objectPoolOff();

        var list = TableDataManager.Instance.getEncyclopediaList(_enum);
        foreach (var element in list) 
        {
            var obj = objectPoolOn();
            obj.SettingUI(element, this);
        }
    }

    private void objectPoolOff() 
    {
        foreach (var obj in objectList)
        {
            obj.gameObject.SetActive(false);
            objectPool.Push(obj);
        }
        objectList.Clear();
    }

    private EncyclopediaObject objectPoolOn() 
    {
        EncyclopediaObject obj = null;
        if (1 <= objectPool.Count)
            obj = objectPool.Pop();
        else
            obj = Util.CreateObject(item.gameObject, content, Vector2.zero, Vector2.one).GetComponent<EncyclopediaObject>();

        obj.gameObject.SetActive(true);
        objectList.Add(obj);

        obj.transform.SetAsLastSibling();
        return obj;
    }

    public void SettingDesc(string _desc) 
    {
        desc.text = _desc;
    }
}
