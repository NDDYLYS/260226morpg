
[System.Serializable]
public class Table_Job
{
	public int Index ;
	public string CodeName ;
	public JobEnum Job ;
	public HiddenEnum Hidden ;
	public int Consume ;


	public static void AutoLoadTable()
	{
		string[,] data = TableDataManager.Instance.PublicExcelReader("Table_Job", true);
		LoadTable(data);
	}


	public static void LoadTable(string[,] _data)
	{
		for (int i = 4; i < _data.GetLength(0); i++)
		{
			int columnCount = 0;
			Table_Job newData = new Table_Job();
			newData.Index = int.Parse(_data[i, columnCount++]);
			newData.CodeName = _data[i, columnCount++];
			newData.Job = Util.GetEnumType<JobEnum>(_data[i, columnCount++]);
			newData.Hidden = Util.GetEnumType<HiddenEnum>(_data[i, columnCount++]);
			newData.Consume = int.Parse(_data[i, columnCount++]);
			TableDataManager.Instance.SetDictinary<Table_Job>(newData.Index, newData.CodeName, newData);
		}
	}


}