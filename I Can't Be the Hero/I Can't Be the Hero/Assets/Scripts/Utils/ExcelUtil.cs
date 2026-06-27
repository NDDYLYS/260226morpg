using System.Data;
using System.IO;
using ExcelDataReader;

public static class ExcelUtil
{
    public static string[,] ReadXlsx(string filePath)
    {
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

        using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
        using (var reader = ExcelReaderFactory.CreateReader(stream))
        {
            DataSet dataSet = reader.AsDataSet();
            DataTable table = dataSet.Tables[0]; // Ã¹ ½ÃÆ®

            int rowCount = table.Rows.Count;
            int colCount = table.Columns.Count;

            string[,] result = new string[rowCount, colCount];

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    result[i, j] = table.Rows[i][j]?.ToString() ?? "";
                }
            }

            return result;
        }
    }
}