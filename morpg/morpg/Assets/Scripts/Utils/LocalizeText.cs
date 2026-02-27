using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;


[RequireComponent(typeof(Text))]
public class LocalizeText : MonoBehaviour
{
    public string Codename;
    private Text Text { get; set; }
    public Text TextProperty
    {
        get
        {
            if (Text == null)
                Text = GetComponent<Text>();
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