using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class ItemData
{
    [SerializeField] private Table_Item tableItem;
    public Table_Item TableItem
    {
        get { return tableItem; }
        set { tableItem = value; }
    }
    [SerializeField] private long uniqueId;
    public long UniqueId
    {
        get { return uniqueId; }
        set { uniqueId = value; }
    }
    [SerializeField] private int count;
    public int Count
    {
        get { return count; }
        set { count = value; }
    }


    public ItemData()
    {
        TableItem = null;
        UniqueId = 0;
        Count = 0;
    }
}