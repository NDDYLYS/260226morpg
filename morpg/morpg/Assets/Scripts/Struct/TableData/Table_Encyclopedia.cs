
[System.Serializable]
public class Table_Encyclopedia
{
	public int Index ;
	public string CodeName ;
	public EncyclopediaEnum Encyclopedia ;
	public string Title ;
	public string Desc ;


	public static void AutoLoadTable()
	{
		string[,] data = TableDataManager.Instance.PublicExcelReader("Table_Encyclopedia", true);
		LoadTable(data);
	}


	public static void LoadTable(string[,] _data)
	{
		for (int i = 4; i < _data.GetLength(0); i++)
		{
			int columnCount = 0;
			Table_Encyclopedia newData = new Table_Encyclopedia();
			newData.Index = int.Parse(_data[i, columnCount++]);
			newData.CodeName = _data[i, columnCount++];
			newData.Encyclopedia = Util.GetEnumType<EncyclopediaEnum>(_data[i, columnCount++]);
			newData.Title = _data[i, columnCount++];
			newData.Desc = _data[i, columnCount++];
			TableDataManager.Instance.SetDictinary<Table_Encyclopedia>(newData.Index, newData.CodeName, newData);
		}
	}


}