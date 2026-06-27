
[System.Serializable]
public class Table_MessageBox
{
	public int Index ;
	public string CodeName ;
	public int ButtonCount ;
	public string KoreanDesc ;
	public string KoreanConfirmButton ;
	public string KoreanCancelButton ;
	public bool IsEscable ;


	public static void AutoLoadTable()
	{
		string[,] data = TableDataManager.Instance.PublicExcelReader("Table_MessageBox", true);
		LoadTable(data);
	}


	public static void LoadTable(string[,] _data)
	{
		for (int i = 4; i < _data.GetLength(0); i++)
		{
			int columnCount = 0;
			Table_MessageBox newData = new Table_MessageBox();
			newData.Index = int.Parse(_data[i, columnCount++]);
			newData.CodeName = _data[i, columnCount++];
			newData.ButtonCount = int.Parse(_data[i, columnCount++]);
			newData.KoreanDesc = _data[i, columnCount++];
			newData.KoreanConfirmButton = _data[i, columnCount++];
			newData.KoreanCancelButton = _data[i, columnCount++];
			newData.IsEscable = bool.Parse(_data[i, columnCount++]);
			TableDataManager.Instance.SetDictinary<Table_MessageBox>(newData.Index, newData.CodeName, newData);
		}
	}


}