using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using System.Text;

public partial class TableDataManager : SingletonGameObject<TableDataManager>
{
    public List<string> getEncyclopediaList(EncyclopediaEnum _enum)
    {
        var list = new List<string>();
        var encyclopedia = GetTableDataList<Table_Encyclopedia>();

        foreach (var element in encyclopedia)
        {
            if (element.Encyclopedia == _enum)
            {
                if (!list.Contains(element.Title))
                    list.Add(element.Title);
            }
        }

        return list;
    }

    public List<string> getEncyclopediaDesc(string _title)
    {
        var list = new List<string>();
        var encyclopedia = GetTableDataList<Table_Encyclopedia>();

        foreach (var element in encyclopedia)
        {
            if (element.Title.Equals(_title))
            {
                list.Add(element.Desc);
            }
        }

        return list;
    }
}