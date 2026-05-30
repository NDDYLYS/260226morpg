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

    public string getSpeciesText(SpeciesEnum _species)
    {
        var text = string.Empty;
        var species = GetTableData<Table_Species>(_species.ToString());
        if (species.Hidden == HiddenEnum.Default)
            text = _species.ToString().GetTableText();
        else if (species.Hidden == HiddenEnum.Hidden)
            text = "Hidden001".GetTableText();
        return text;
    }

    public Color getSpeciesColor(SpeciesEnum _species)
    {
        var color = Color.black;
        var savedata = GameManager.Instance.SaveData;
        if (savedata.Species == _species)
            color = Color.yellow;
        else
        {
            if (!savedata.getSpecies(_species))
                color = Color.gray;
            else
                color = Color.black;
        }
        return color;
    }

    public string getJobText(JobEnum _job)
    {
        var text = string.Empty;
        var job = GetTableData<Table_Job>(_job.ToString());
        if (job.Hidden == HiddenEnum.Default)
            text = _job.ToString().GetTableText();
        else if (job.Hidden == HiddenEnum.Hidden)
            text = "Hidden001".GetTableText();
        return text;
    }

    public Color getJobColor(JobEnum _job)
    {
        var color = Color.black;
        var savedata = GameManager.Instance.SaveData;
        if (savedata.Job == _job)
            color = Color.yellow;
        else
        {
            if (!savedata.getJob(_job))
                color = Color.gray;
            else
                color = Color.black;
        }
        return color;
    }

    public List<ItemData> getItemList(CategoryEnum _category)
    {
        var savedata = GameManager.Instance.SaveData;
        List<ItemData> list = savedata.ItemList;
        List<ItemData> list2 = new List<ItemData>();
        if (_category == CategoryEnum.Max)
            return list;
        else
        {
            foreach (var element in list)
            {
                if (element.TableItem.Category == _category)
                {
                    list2.Add(element);
                }
            }
        }

        return list2;
    }
}