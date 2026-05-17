using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class JobObject : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmp;
    [SerializeField] private Button btn;
    public void setting(JobEnum _job)
    {
        tmp.text = _job.ToString().GetTableText();
        btn.onClick.AddListener(() => click(_job));
    }

    private void click(JobEnum _job)
    {

    }
}
