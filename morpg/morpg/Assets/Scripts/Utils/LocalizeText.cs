using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using TMPro;


[RequireComponent(typeof(Text))]
public class LocalizeText : MonoBehaviour
{
    public string Codename;
    private TextMeshProUGUI Text { get; set; }
    public TextMeshProUGUI TextProperty
    {
        get
        {
            if (Text == null)
                Text = GetComponent<TextMeshProUGUI>();
            return Text;
        }
    }

    void Start()
    {
        SettingText();
    }

    [Button]
    public void SettingText()
    {
        TextProperty.text = TableDataManager.Instance.GetTableText(Codename);
    }
}