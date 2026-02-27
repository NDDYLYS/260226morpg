
[System.Serializable]
public class Table_Text
{
	public int Index ;
	public string CodeName ;
	public string Korean ;
	public string English ;


	public static void AutoLoadTable()
	{
		string[,] data = TableDataManager.Instance.PublicExcelReader("Table_Text", true);
		LoadTable(data);
	}


	public static void LoadTable(string[,] _data)
	{
		for (int i = 4; i < _data.GetLength(0); i++)
		{
			int columnCount = 0;
			Table_Text newData = new Table_Text();
			newData.Index = int.Parse(_data[i, columnCount++]);
			newData.CodeName = _data[i, columnCount++];
			newData.Korean = _data[i, columnCount++];
			newData.English = _data[i, columnCount++];
			TableDataManager.Instance.SetDictinary<Table_Text>(newData.Index, newData.CodeName, newData);
		}
	}


}