using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentMenu : MonoBehaviour
{
    private CategoryEnum category;
    [SerializeField] private List<ClickEn> menuList;

    public void onClickAllButton()
    {
        category = CategoryEnum.Max;
        clickedMenuFocus();

        var itemList = TableDataManager.Instance.getItemList(CategoryEnum.Max);
        var textList = new List<string>();
        foreach (var item in itemList)
        {
            textList.Add($"{item.TableItem.CodeName}({item.UniqueId})x{item.Count}");
        }
        Debug.Log(string.Join("/", textList));
    }

    public void onClickConsumeButton()
    {
        category = CategoryEnum.Consume;
        clickedMenuFocus();

        var itemList = TableDataManager.Instance.getItemList(CategoryEnum.Consume); 
        var textList = new List<string>();
        foreach (var item in itemList)
        {
            textList.Add($"{item.TableItem.CodeName}({item.UniqueId})x{item.Count}");
        }
        Debug.Log(string.Join("/", textList));
    }

    public void onClickEquipmentButton()
    {
        category = CategoryEnum.Equipment;
        clickedMenuFocus();

        var itemList = TableDataManager.Instance.getItemList(CategoryEnum.Equipment); 
        var textList = new List<string>();
        foreach (var item in itemList)
        {
            textList.Add($"{item.TableItem.CodeName}({item.UniqueId})x{item.Count}");
        }
        Debug.Log(string.Join("/", textList));
    }

    public void onClickEtcButton()
    {
        category = CategoryEnum.Etc;
        clickedMenuFocus();

        var itemList = TableDataManager.Instance.getItemList(CategoryEnum.Etc); 
        var textList = new List<string>();
        foreach (var item in itemList)
        {
            textList.Add($"{item.TableItem.CodeName}({item.UniqueId})x{item.Count}");
        }
        Debug.Log(string.Join("/", textList));
    }

    private void clickedMenuFocus()
    {
        foreach (var clicked in menuList)
        {
            clicked.menuSelect(category);
        }
    }
}
