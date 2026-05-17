using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class JobObject : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmp;
    [SerializeField] private Button btn;
    private JobEnum job;
    public void setting(JobEnum _job)
    {
        job = _job;

        var savedata = GameManager.Instance.SaveData;
        if (savedata.getJob(job))
            tmp.text = job.ToString().GetTableText();
        else
            tmp.text = "Hidden001".GetTableText();
        btn.onClick.AddListener(() => click(_job));
    }

    private void click(JobEnum _job)
    {
        var savedata = GameManager.Instance.SaveData;
        if (!savedata.getJob(job))
            return;
        GameManager.Instance.SaveData.Job = job;
    }
}
