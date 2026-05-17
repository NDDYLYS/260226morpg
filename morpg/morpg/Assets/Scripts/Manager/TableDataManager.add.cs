using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using System.Text;

public partial class TableDataManager : SingletonGameObject<TableDataManager>
{
    private List<string> titleList = new List<string>();
    private List<string> descList = new List<string>();

    public List<string> getEncyclopediaList(EncyclopediaEnum _enum)
    {
        var list = new List<string>();
        var encyclopedia = GetTableDataList<Table_Encyclopedia>();

        foreach (var element in encyclopedia)
        {
            if (element.Encyclopedia == _enum)
            {
                if (element.Order == 1)
                    list.Add(getTitle(element.Title));
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
                list.Add(getDesc(element.Desc));
            }
        }

        return list;
    }

    public void updateSavedata(SaveData _savedata) 
    {
        titleList.Clear();
        descList.Clear();

        var list = _savedata.EncyclopediaList;
        foreach (var element in list) 
        {
            var table = GetTableData<Table_Encyclopedia>(element);
            if (table == null)
                return;
            titleList.Add(table.Title);
            descList.Add(table.Desc);
        }
    }

    public string getTitle(string _title) 
    {
        if (titleList.Contains(_title))
            return _title;
        return "Hidden001";
    }

    public string getDesc(string _desc)
    {
        if (descList.Contains(_desc))
            return _desc;
        return "Hidden002";
    }

    public List<SpeciesEnum> getSpeciesList()
    {
        var result = new List<SpeciesEnum>();
        var list = GetTableDataList<Table_Species>();
        foreach (var element in list) 
        {
            if (element.Species == SpeciesEnum.Unknown)
                continue;
            if (element.Hidden == HiddenEnum.Default)
                result.Add(element.Species);
        }
        return result;
    }

    public List<JobEnum> getJobList() 
    {
        var result = new List<JobEnum>();
        var list = GetTableDataList<Table_Job>();
        foreach (var element in list)
        {
            if (element.Job == JobEnum.notEmployed)
                continue;
            if (element.Hidden == HiddenEnum.Default)
                result.Add(element.Job);
        }
        return result;
    }
}