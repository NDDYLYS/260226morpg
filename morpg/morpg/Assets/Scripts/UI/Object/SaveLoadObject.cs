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

    private SaveLoadEnum saveLoadEnum;
    private SaveData saveData;
    private int Index;

    public void SettingUI(int _index, SaveLoadEnum _saveLoad) 
    {
        Index = _index;
        index.text = _index.ToString();

        saveLoadEnum = _saveLoad;
        saveData = UGSManager.Instance.GetSaveData(_index);

        if (saveData != null)
        {
            temp.text = $"{saveData.species.ToString().GetTableText()}/{saveData.job.ToString().GetTableText()}";
            playTIme.text = Util.GetTimer(saveData.playTime);
        }
        else
        {
            temp.text = "No SaveData";
            playTIme.text = string.Empty;
        }
    }

    public void OnClickSaveLoadButton() 
    {
        switch (saveLoadEnum)
        {
            case SaveLoadEnum.Save:
                Save();
                break;
            case SaveLoadEnum.Load:
                Load();
                break;
            default:
                break;
        }
    }

    private void Save() 
    {
        UGSManager.Instance.Save(Index);
    }

    private void Load() 
    {
        if (saveData == null)
            return;

        UGSManager.Instance.Load(Index);
    }
}