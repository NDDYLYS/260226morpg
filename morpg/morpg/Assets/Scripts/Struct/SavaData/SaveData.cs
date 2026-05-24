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

    [SerializeField] private List<string> existItemList = new();
    public IReadOnlyList<string> ExistItemList => existItemList;
    public void AddItemList(string _item)
    {
        existItemList.Add(_item);
    }
    public void AddItemList(List<string> _itemList)
    {
        foreach (var item in _itemList)
        {
            AddItemList(item);
        }
    }
    public Dictionary<string, int>  getItemList(CategoryEnum _category)
    {
        switch (_category)
        {
            case CategoryEnum.Consume:
            case CategoryEnum.Etc:
                break;
            case CategoryEnum.Equipment:
                break;
            default:
                break;
        }

        return new Dictionary<string, int>();
    }

    public SaveData()
    {
        playTime = 0f;

        encyclopediaList = new List<string>();
        species = SpeciesEnum.Human;
        job = JobEnum.notEmployed;

        AddSpeciesList(TableDataManager.Instance.getSpeciesList());
        AddJobList(TableDataManager.Instance.getJobList());
    }
}