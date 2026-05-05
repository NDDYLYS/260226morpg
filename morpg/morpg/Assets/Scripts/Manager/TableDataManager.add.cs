using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using System.Text;

public partial class TableDataManager : SingletonGameObject<TableDataManager>
{
    private Dictionary<string, string> titleDic;
    private Dictionary<string, string> descDic;

    public List<string> getEncyclopediaList(EncyclopediaEnum _enum) 
    {
        var list = new List<string>();
        var encyclopedia = GetTableDataList<Table_Encyclopedia>();

        foreach (var element in encyclopedia)
        {
            if (element.Encyclopedia == _enum) 
            { 
                if (!list.Contains(element.Title))
                    list.Add(getSavedata_TitleDic(element.CodeName));
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
                list.Add(getSavedata_DescDic(element.CodeName));
            }
        }

        return list;
    }

    public void getSavedata_Encyclopedia()
    {
        var savedata = GameManager.Instance.SaveData;
        if (savedata.encyclopediaList == null || savedata.encyclopediaList.Count <= 0)
            return;

        if (titleDic == null)
            titleDic = new Dictionary<string, string>();
        else 
            titleDic.Clear();
        if (descDic == null)
            descDic = new Dictionary<string, string>();
        else
            descDic.Clear();

        Table_Encyclopedia table = null;
        foreach (var element in savedata.encyclopediaList) 
        {
            table = GetTableData<Table_Encyclopedia>(element);

            var codenames = getCodenameEqualTitle(table.Title);
            foreach (var codename in codenames)
            {
                titleDic.Add(codename, table.Title);
            }
            if (!descDic.ContainsKey(table.CodeName))
                descDic.Add(table.CodeName, table.Desc);
        }
    }

    public string getSavedata_TitleDic(string _codename) 
    {
        if (titleDic.ContainsKey(_codename))
            return titleDic[_codename];
        return "Hidden001";
    }

    public string getSavedata_DescDic(string _codename)
    {
        if (descDic.ContainsKey(_codename))
            return descDic[_codename];
        return "Hidden002";
    }

    private List<string> getCodenameEqualTitle(string _title) 
    {
        var codenames = new List<string>();
        var all = GetTableDataList<Table_Encyclopedia>();
        foreach (var element in all) 
        {
            if (element.Title.Equals(_title)) 
                codenames.Add(element.CodeName);  
        }
        return codenames;
    }
}