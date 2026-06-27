using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SpeciesJobMenu : MonoBehaviour
{
    [SerializeField] private ScrollRect speciesScroll;
    [SerializeField] private Transform speciesContent;
    [SerializeField] private GameObject speciesItem;
    private List<SpeciesObject> speciesObjects;
    private SpeciesEnum species;

    [SerializeField] private ScrollRect jobScroll;
    [SerializeField] private Transform jobContent;
    [SerializeField] private GameObject jobItem;
    private List<JobObject> jobObjects;

    private void Awake()
    {
        settingSpecies();
        settingJob();
    }

    private void settingSpecies()
    {
        SpeciesObject obj = null;
        speciesObjects = new List<SpeciesObject>();
        foreach (SpeciesEnum species in System.Enum.GetValues(typeof(SpeciesEnum)))
        {
            if (species == SpeciesEnum.None || species == SpeciesEnum.Max || species == SpeciesEnum.Unknown)
                continue;

            obj = Util.CreateObject(speciesItem.gameObject, speciesContent, Vector2.zero, Vector2.one).GetComponent<SpeciesObject>();
            obj.gameObject.SetActive(true);
            obj.setting(species, this);
            speciesObjects.Add(obj);
        }
    }

    private void settingJob()
    {
        JobObject obj = null;
        jobObjects = new List<JobObject>();
        foreach (JobEnum job in System.Enum.GetValues(typeof(JobEnum)))
        {
            if (job == JobEnum.None || job == JobEnum.Max || job == JobEnum.notEmployed)
                continue;

            obj = Util.CreateObject(jobItem.gameObject, jobContent, Vector2.zero, Vector2.one).GetComponent<JobObject>();
            obj.gameObject.SetActive(true);
            obj.setting(job, this);
            jobObjects.Add(obj);
        }
    }

    public void refresh_species(SpeciesEnum _species)
    {
        foreach (var species in speciesObjects)
        {
            species.refresh();
        }
        species = _species;
    }

    public void refresh_job()
    {
        foreach (var jobObject in jobObjects)
        {
            jobObject.refresh();
        }
    }

    public SpeciesEnum getSpecies()
    {
        return species;
    }
}