using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class SaveData
{
    //public string FileName { get; set; } // 저장된 파일명
    public string temp;
    public long playTime;


    public SaveData()
    {
        temp = "";
        playTime = 0;
    }
}