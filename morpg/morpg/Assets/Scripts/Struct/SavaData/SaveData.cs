using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

[Serializable]
public class SaveData
{
    //public string FileName { get; set; } // 저장된 파일명
    public string temp { get; set; }
    public long playTime { get; set; }


    public SaveData()
    {
        temp = "";
        playTime = 0;
    }
}