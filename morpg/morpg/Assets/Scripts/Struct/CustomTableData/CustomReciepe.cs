[System.Serializable]
public class CustomReciepe
{
    public string MaterialCodename;
    public int Count;
    
    public CustomReciepe(string value)
    {
        if (string.IsNullOrEmpty(value))
            return;

        var texts = value.Split('/');

        MaterialCodename = texts[0];
        Count = int.Parse(texts[1]);
    }
}