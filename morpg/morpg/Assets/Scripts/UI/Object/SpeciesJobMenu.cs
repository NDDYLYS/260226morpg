using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeciesJobMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI species;
    [SerializeField] private TextMeshProUGUI job;

    private void OnEnable()
    {
        var textList = new List<string>();
        var speciesS = TableDataManager.Instance.GetTableDataList<Table_Species>();
        foreach (var s in speciesS)
        {
            textList.Add(s.CodeName.GetTableText());
        }

        species.text = string.Join("\n", textList);
        textList.Clear();

        var jobS = TableDataManager.Instance.GetTableDataList<Table_Job>();
        foreach (var j in jobS)
        {
            textList.Add(j.CodeName.GetTableText());
        }

        job.text = string.Join("\n", textList);
    }
}
