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
    private SpeciesJobMenu manager;
    public void setting(SpeciesEnum _species, SpeciesJobMenu _manager)
    {
        species = _species;
        manager = _manager;
        btn.onClick.AddListener(() => click(_species));
        refresh();
    }

    private void click(SpeciesEnum _species)
    {
        GameManager.Instance.SaveData.Species = species;
        if (manager != null)
            manager.refresh_species(species);
    }

    public void refresh()
    {
        var table = TableDataManager.Instance;
        tmp.text = table.getSpeciesText(species);
        tmp.color = table.getSpeciesColor(species);
    }
}
