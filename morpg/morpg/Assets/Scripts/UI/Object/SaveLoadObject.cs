using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveLoadObject : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI temp;
    [SerializeField] private TextMeshProUGUI playTIme;

    public void SettingUI(SaveData _saveData) 
    {
        if (_saveData == null)
            return;

        temp.text = _saveData.temp;
        playTIme.text = Util.GetTimer(_saveData.playTime);
    }
}