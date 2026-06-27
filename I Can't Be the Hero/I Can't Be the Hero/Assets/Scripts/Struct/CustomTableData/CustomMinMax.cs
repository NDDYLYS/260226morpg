using UnityEngine;

[System.Serializable]
public class CustomMinMax
{
    public int Min;
    public int Max;
    
    public CustomMinMax(string value)
    {
        var texts = value.Split('~');

        Min = int.Parse(texts[0]);
        Max = int.Parse(texts[1]);
    }

    public int GetRandom()
    {
        var random = Random.Range(Min, Max + 1);
        return random;
    }
}