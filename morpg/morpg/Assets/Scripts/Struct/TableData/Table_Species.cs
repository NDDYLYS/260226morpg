
[System.Serializable]
public class Table_Species
{
	public int Index ;
	public string CodeName ;
	public SpeciesEnum Species ;
	public HiddenEnum Hidden ;
	public JobEnum Required ;


	public static void AutoLoadTable()
	{
		string[,] data = TableDataManager.Instance.PublicExcelReader("Table_Species", true);
		LoadTable(data);
	}


	public static void LoadTable(string[,] _data)
	{
		for (int i = 4; i < _data.GetLength(0); i++)
		{
			int columnCount = 0;
			Table_Species newData = new Table_Species();
			newData.Index = int.Parse(_data[i, columnCount++]);
			newData.CodeName = _data[i, columnCount++];
			newData.Species = Util.GetEnumType<SpeciesEnum>(_data[i, columnCount++]);
			newData.Hidden = Util.GetEnumType<HiddenEnum>(_data[i, columnCount++]);
			newData.Required = Util.GetEnumType<JobEnum>(_data[i, columnCount++]);
			TableDataManager.Instance.SetDictinary<Table_Species>(newData.Index, newData.CodeName, newData);
		}
	}


}