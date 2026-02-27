[System.Serializable]
public class CustomBasePer
{
    public int Base;
    public int Per;
    
    public CustomBasePer(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            Base = 0;
            Per = 0;
            return;
        }

        var texts = value.Split('+');

        Base = int.Parse(texts[0]);
        Per = int.Parse(texts[1].Replace("%", ""));
    }

    public static CustomBasePer operator +(CustomBasePer c1, CustomBasePer c2)
    {
        if (c1 == null)
            return c2;
        if (c2 == null)
            return c1;
        CustomBasePer value = new CustomBasePer("0+0%");
        value.Base = c1.Base + c2.Base;
        value.Per = c1.Per + c2.Per;
        return value;
    }
}