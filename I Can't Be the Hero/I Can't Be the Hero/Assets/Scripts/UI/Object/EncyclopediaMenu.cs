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

    [Header("menu")]
    [SerializeField] private ScrollRect menuScrollRect;
    [SerializeField] private Transform menuContent;
    [SerializeField] private ClickEn menuItem;

    [Header("title")]
    [SerializeField] private ScrollRect titleScrollRect;
    [SerializeField] private Transform titleContent;
    [SerializeField] private EncyclopediaObject titleItem;

    [Header("desc")]
    [SerializeField] private TextMeshProUGUI desc;

    private Stack<EncyclopediaObject> objectPool; // 미사용
    private List<EncyclopediaObject> objectList; // 사용 중

    private void Awake()
    {
        objectPool = new Stack<EncyclopediaObject>();
        objectList = new List<EncyclopediaObject>();

        menuObj();
    }

    private void OnEnable()
    {
        encyclopediaEnum = EncyclopediaEnum.None;
    }

    public void Refresh()
    {
        OnClickEnumButton(EncyclopediaEnum.Cocruwa);
    }

    public void OnClickEnumButton(EncyclopediaEnum _menu)
    {
        encyclopediaEnum = _menu;
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
            obj = Util.CreateObject(titleItem.gameObject, titleContent, Vector2.zero, Vector2.one).GetComponent<EncyclopediaObject>();

        obj.gameObject.SetActive(true);
        objectList.Add(obj);

        obj.transform.SetAsLastSibling();
        return obj;
    }

    public void SettingDesc(string _desc) 
    {
        desc.text = _desc;
    }

    private void menuObj()
    {
        menuList = new List<ClickEn>();
        ClickEn menuClass = null;
        foreach (EncyclopediaEnum menu in System.Enum.GetValues(typeof(EncyclopediaEnum)))
        {
            if (menu == EncyclopediaEnum.None || menu == EncyclopediaEnum.Max)
                continue;

            menuClass = Util.CreateObject(menuItem.gameObject, menuContent, Vector2.zero, Vector2.one).GetComponent<ClickEn>();
            menuClass.gameObject.SetActive(true);
            menuClass.setting(menu);

            menuList.Add(menuClass);
        }
    }
}
