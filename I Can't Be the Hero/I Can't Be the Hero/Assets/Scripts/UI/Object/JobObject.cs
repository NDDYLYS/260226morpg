using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class JobObject : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmp;
    [SerializeField] private Button btn;
    private JobEnum job;
    private SpeciesJobMenu manager;
    public void setting(JobEnum _job, SpeciesJobMenu _manager)
    {
        job = _job;
        manager = _manager;
        btn.onClick.AddListener(() => click(_job));
        refresh();
    }

    private void click(JobEnum _job)
    {
        GameManager.Instance.SaveData.Job = job;
        if (manager != null)
            manager.refresh_job();
    }

    public void refresh()
    {
        var table = TableDataManager.Instance;
        tmp.text = table.getJobText(job);
        tmp.color = table.getJobColor(job);
    }
}
