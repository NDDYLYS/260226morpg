using System.Collections.Generic;

public static partial class Util
{
    public static string GetTableText(this string _codename, params string[] param)
    {
        return TableDataManager.Instance.GetTableText(_codename, param);
    }

    public static string GetTableText(this string _codename)
    {
        return TableDataManager.Instance.GetTableText(_codename);
    }
}
