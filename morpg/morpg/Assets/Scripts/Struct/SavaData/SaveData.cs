using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class SaveData
{
    //public string FileName { get; set; } // 저장된 파일명
    public float playTime;

    public List<string> encyclopediaList;
    public SpeciesEnum species;
    public JobEnum job;


    public SaveData()
    {
        playTime = 0f;

        encyclopediaList = new List<string>();
        species = SpeciesEnum.Human;
        job = JobEnum.notEmployed;
    }
}