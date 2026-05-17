using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.UI;

public class SpeciesObject : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI tmp;
    [SerializeField] private Button btn;
    private SpeciesEnum species;
    public void setting(SpeciesEnum _species)
    {
        //var prefab = TableDataManager.Instance.GetLoadedPrefab($"Units/{_species.ToString()}");
        //SpriteRenderer sr = prefab.GetComponent<SpriteRenderer>();
        //if (sr != null)
        //    image.sprite = sr.sprite;
        species = _species;
        var savedata = GameManager.Instance.SaveData;
        if (savedata.getSpecies(species))
            tmp.text = _species.ToString().GetTableText();
        else
            tmp.text = "Hidden001".GetTableText();
        btn.onClick.AddListener(() => click(_species));
    }

    private void click(SpeciesEnum _species)
    {
        var savedata = GameManager.Instance.SaveData;
        if (!savedata.getSpecies(species))
            return;
        GameManager.Instance.SaveData.Species = species;
    }
}
