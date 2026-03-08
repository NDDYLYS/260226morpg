using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveLoadObject : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI index;
    [SerializeField] private TextMeshProUGUI temp;
    [SerializeField] private TextMeshProUGUI playTIme;

    public void SettingUI(int _index) 
    {
        index.text = _index.ToString();

        //if (save == null)
        //{
        //    temp.text = "notSaveData".GetTableText();
        //    playTIme.text = string.Empty;
        //}
        //else 
        //{ 
        //    temp.text = save.temp;
        //    playTIme.text = Util.GetTimer(save.playTime);
        //}
    }
}