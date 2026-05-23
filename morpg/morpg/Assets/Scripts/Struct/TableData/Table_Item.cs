
[System.Serializable]
public class Table_Item
{
	public int Index ;
	public string CodeName ;
	public CategoryEnum Category ;
	public EquipmentEnum Equipment ;


	public static void AutoLoadTable()
	{
		string[,] data = TableDataManager.Instance.PublicExcelReader("Table_Item", true);
		LoadTable(data);
	}


	public static void LoadTable(string[,] _data)
	{
		for (int i = 4; i < _data.GetLength(0); i++)
		{
			int columnCount = 0;
			Table_Item newData = new Table_Item();
			newData.Index = int.Parse(_data[i, columnCount++]);
			newData.CodeName = _data[i, columnCount++];
			newData.Category = Util.GetEnumType<CategoryEnum>(_data[i, columnCount++]);
			newData.Equipment = Util.GetEnumType<EquipmentEnum>(_data[i, columnCount++]);
			TableDataManager.Instance.SetDictinary<Table_Item>(newData.Index, newData.CodeName, newData);
		}
	}


}