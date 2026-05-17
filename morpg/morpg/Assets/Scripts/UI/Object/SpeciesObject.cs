using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SpeciesObject : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI tmp;
    [SerializeField] private Button btn;
    public void setting(SpeciesEnum _species)
    {
        //var prefab = TableDataManager.Instance.GetLoadedPrefab($"Units/{_species.ToString()}");
        //SpriteRenderer sr = prefab.GetComponent<SpriteRenderer>();
        //if (sr != null)
        //    image.sprite = sr.sprite;
        tmp.text = _species.ToString().GetTableText();
        btn.onClick.AddListener(() => click(_species));
    }

    private void click(SpeciesEnum _species)
    {

    }
}
