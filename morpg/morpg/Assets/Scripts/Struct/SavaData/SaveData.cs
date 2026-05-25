using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class SaveData
{
    //public string FileName { get; set; } // 저장된 파일명
    [SerializeField] private float playTime;
    public float PlayTime
    {
        get => playTime;
        set
        {
            playTime = value;
        }
    }
    [SerializeField] private List<string> encyclopediaList;
    public IReadOnlyList<string> EncyclopediaList => encyclopediaList;
    public bool AddEncyclopedia(string value)
    {
        if (encyclopediaList.Contains(value))
            return false; // 이미 있음

        encyclopediaList.Add(value);
        TableDataManager.Instance.updateSavedata(this);
        UIPrefabManager.Instance.MenuPageProperty.Refresh();
        return true;
    }
    [SerializeField] private SpeciesEnum species;
    public SpeciesEnum Species
    {
        get => species;
        set
        {
            species = value;
        }
    }
    [SerializeField] private JobEnum job;
    public JobEnum Job
    {
        get => job;
        set
        {
            job = value;
        }
    }

    [SerializeField] private List<SpeciesEnum> existSpeciesList = new();
    public IReadOnlyList<SpeciesEnum> ExistSpeciesList => existSpeciesList;
    public void AddSpecies(SpeciesEnum _species)
    {
        if (!existSpeciesList.Contains(_species))
            existSpeciesList.Add(_species);
    }
    public void AddSpeciesList(List<SpeciesEnum> _speciesList)
    {
        foreach (SpeciesEnum species in _speciesList)
        {
            AddSpecies(species);
        }
    }
    public bool getSpecies(SpeciesEnum _species)
    {
        return existSpeciesList.Contains(_species);
    }

    [SerializeField] private List<JobEnum> existJobList = new();
    public IReadOnlyList<JobEnum> ExistJobList => existJobList;

    public void AddJob(JobEnum job)
    {
        if (!existJobList.Contains(job))
            existJobList.Add(job);
    }
    public void AddJobList(List<JobEnum> _jobList) 
    { 
        foreach (JobEnum job in _jobList)
        {
            AddJob(job);
        }
    }
    public bool getJob(JobEnum _job)
    {
        return existJobList.Contains(_job);
    }

    [SerializeField] private long uniqueId;
    public long UniqueId
    {
        get => uniqueId;
        set
        {
            uniqueId = value;
        }
    }
    public long setUniqueId() 
    { 
        return uniqueId++;
    }

    [SerializeField] private List<ItemData> itemList = new();
    public List<ItemData> ItemList
    {
        get => itemList;
        set => itemList = value;
    }

    public int getItemCount(string _codename)
    {
        var count = 0;
        foreach (var item in ItemList)
        {
            if (item.TableItem.CodeName == _codename)
                count = item.Count;
        }

        return count;
    }
    public void AddItemList(ItemData _item)
    {
        var tableItem = TableDataManager.Instance.GetTableData<Table_Item>(_item.TableItem.CodeName);
        if (tableItem == null)
            return;
        var index = itemList.FindIndex(x => x.TableItem.CodeName == _item.TableItem.CodeName);

        if (0 <= index)
            itemList[index] = _item;
        else
            itemList.Add(_item);
    }


    public SaveData()
    {
        playTime = 0f;

        encyclopediaList = new List<string>();
        species = SpeciesEnum.Human;
        job = JobEnum.notEmployed;

        AddSpeciesList(TableDataManager.Instance.getSpeciesList());
        AddJobList(TableDataManager.Instance.getJobList());

        itemList = new List<ItemData>();
        uniqueId = 1;
    }
}