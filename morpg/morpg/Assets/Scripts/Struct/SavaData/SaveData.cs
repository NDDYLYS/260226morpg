using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class SaveData
{
    //public string FileName { get; set; } // 저장된 파일명
    public float playTime;

    private List<string> encyclopediaList;
    private SpeciesEnum species;
    private JobEnum job;


    public SaveData()
    {
        playTime = 0f;

        encyclopediaList = new List<string>();
        species = SpeciesEnum.Human;
        job = JobEnum.notEmployed;
    }

    public float getPlaytime()
    {
        return playTime;
    }

    public void setEncyclopedia(string _encyclopedia)
    {
        if (!encyclopediaList.Contains(_encyclopedia));
            encyclopediaList.Add(_encyclopedia);
    }

    public List<string> getEncyclopedia()
    {
        return encyclopediaList;
    }

    public void setSpecies(SpeciesEnum _species)
    {
        species = _species;
    }

    public SpeciesEnum getSpecies()
    {
        return species;
    }

    public void setJob(JobEnum _job)
    {
        job = _job;
    }

    public JobEnum getJob()
    {
        return job;
    }
}