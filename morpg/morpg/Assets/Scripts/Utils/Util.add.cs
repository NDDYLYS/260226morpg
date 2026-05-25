using System.Collections.Generic;

public static partial class Util
{
    //public static void CreateMessageBox(string _codename)
    //{
    //    var prefab = UIPrefabManager.Instance.MessageBoxProperty.OnClickOpenPageButton(_codename);
    //}

    public static void AddItem(string _codename)
    {
        var savedata = GameManager.Instance.SaveData;
        if (savedata == null)
            return;

        var tableItem = TableDataManager.Instance.GetTableData<Table_Item>(_codename);
        if (tableItem == null)
            return;

        var itemData = new ItemData();
        itemData.TableItem = tableItem;
        switch (tableItem.Category)
        {
            case CategoryEnum.Equipment:
                itemData.UniqueId = savedata.setUniqueId();
                itemData.Count = 1;
                break;
            case CategoryEnum.Consume:
            case CategoryEnum.Etc:
                itemData.UniqueId = 0;
                var count = savedata.getItemCount(_codename);
                if (count <= 0)
                    count = 1;
                else if (0 < count && count <= Constant.itemStackMaximum)
                    count++;
                itemData.Count = count;
                break;
        }
        savedata.AddItemList(itemData);
    }
}
