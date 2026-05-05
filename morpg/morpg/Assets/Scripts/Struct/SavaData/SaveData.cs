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

    public SaveData()
    {
        playTime = 0f;

        encyclopediaList = new List<string>();
        species = SpeciesEnum.Human;
        job = JobEnum.notEmployed;
    }


}