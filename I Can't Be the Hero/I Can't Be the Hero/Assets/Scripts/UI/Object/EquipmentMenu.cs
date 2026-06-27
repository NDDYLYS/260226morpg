using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentMenu : MonoBehaviour
{
    private CategoryEnum category;
    [SerializeField] private List<ClickEn> menuList;

    [Header("part")]
    [SerializeField] private ScrollRect partScrollRect;
    [SerializeField] private Transform partContent;
    [SerializeField] private EquipmentObject partItem;


    [Header("item")]
    [SerializeField] private ScrollRect itemScrollRect;
    [SerializeField] private Transform itemContent;
    [SerializeField] private EquipmentItem itemItem;

    private Stack<EquipmentObject> objectPool; // 미사용
    private List<EquipmentObject> objectList; // 사용 중

    private Stack<EquipmentItem> objectPool2; // 미사용
    private List<EquipmentItem> objectList2; // 사용 중

    private void Awake()
    {
        objectPool = new Stack<EquipmentObject>();
        objectList = new List<EquipmentObject>();

        objectPool2 = new Stack<EquipmentItem>();
        objectList2 = new List<EquipmentItem>();
    }

    public void onClickAllButton()
    {
        category = CategoryEnum.Max;
        clickedMenuFocus();
        changeList();
    }

    public void onClickConsumeButton()
    {
        category = CategoryEnum.Consume;
        clickedMenuFocus();
        changeList();
    }

    public void onClickEquipmentButton()
    {
        category = CategoryEnum.Equipment;
        clickedMenuFocus();
        changeList();
    }

    public void onClickEtcButton()
    {
        category = CategoryEnum.Etc;
        clickedMenuFocus();
        changeList();

        //var itemList = TableDataManager.Instance.getItemList(CategoryEnum.Etc); 
        //var textList = new List<string>();
        //foreach (var item in itemList)
        //{
        //    textList.Add($"{item.TableItem.CodeName}({item.UniqueId})x{item.Count}");
        //}
        //Debug.Log(string.Join("/", textList));
    }

    private void clickedMenuFocus()
    {
        foreach (var clicked in menuList)
        {
            clicked.menuSelect(category);
        }
    }

    private void changeList()
    {
        objectPoolOff();

        if (category == CategoryEnum.Equipment)
        {
            foreach (EquipmentEnum equipment in EquipmentEnum.GetValues(typeof(EquipmentEnum)))
            {
                if (equipment == EquipmentEnum.None || equipment == EquipmentEnum.Max)
                    continue;

                var obj = objectPoolOn();
                obj.SettingUI(equipment.ToString(), this);
            }
        }
        else
        {
            var items = TableDataManager.Instance.getItemList(category);
            changedItemList(items);
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

    private EquipmentObject objectPoolOn()
    {
        EquipmentObject obj = null;
        if (1 <= objectPool.Count)
            obj = objectPool.Pop();
        else
            obj = Util.CreateObject(partItem.gameObject, partContent, Vector2.zero, Vector2.one).GetComponent<EquipmentObject>();

        obj.gameObject.SetActive(true);
        objectList.Add(obj);

        obj.transform.SetAsLastSibling();
        return obj;
    }

    private void objectPool2Off()
    {
        foreach (var obj in objectList2)
        {
            obj.gameObject.SetActive(false);
            objectPool2.Push(obj);
        }
        objectList2.Clear();
    }

    private EquipmentItem objectPool2On()
    {
        EquipmentItem obj = null;
        if (1 <= objectPool2.Count)
            obj = objectPool2.Pop();
        else
            obj = Util.CreateObject(itemItem.gameObject, itemContent, Vector2.zero, Vector2.one).GetComponent<EquipmentItem>();

        obj.gameObject.SetActive(true);
        objectList2.Add(obj);

        obj.transform.SetAsLastSibling();
        return obj;
    }

    public void changedItemList(List<ItemData> _itemList)
    {
        objectPool2Off();

        foreach (var item in _itemList)
        {
            var obj = objectPool2On();
            obj.SettingUI(item);
        }
    }
}
