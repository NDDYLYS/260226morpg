using UnityEditor;
using Debug = UnityEngine.Debug;
using System.IO;
using System.Collections.Generic;
using System.Text;

public class TableMakerManager
{
    [MenuItem("TableMakerManager/Build")]
    static void Build()
    {
        CreateTableStruct(GetResourcesFolderPath(), true);

        CopyAll();
    }

    private static void CreateTableStruct(string _path, bool _isResource = false)
    {
        List<string> filesPath = GetTableDataList(_path);
        for (int i = 0; i < filesPath.Count; i++)
        {
            // 260307 xlsx 불러오는 기능으로 확장
            //string tableName = filesPath[i].Replace(".csv", "");
            var tableName = Path.GetFileNameWithoutExtension(filesPath[i]);

            var filePath = string.Format("{0}/{1}", _path, filesPath[i]);
            
            // 260307 xlsx 불러오는 기능으로 확장
            //string text = Util.LoadFile(filePath, Encoding.Unicode);
            //string[,] array = Util.PublicExcelReader(text);
            string[,] array = ExcelUtil.ReadXlsx(filePath);

            var completeStruct = new List<string>();
            completeStruct.Add(string.Format(""));

            completeStruct.Add(string.Format("[System.Serializable]"));
            completeStruct.Add(string.Format("public class {0}", tableName));

            completeStruct.Add("{");

            #region @.Field Sector
            bool isArray = false;
            Dictionary<string, int> ArrayCountDic = new Dictionary<string, int>();
            for (int j = 0; j < array.GetLength(1); j++)
            {
                List<string> lineField = new List<string>();

                if (!isArray)
                {
                    // 배열이 아닐 때

                    lineField.Add("\tpublic");
                    if (array[2, j].Contains("Array{"))
                    {
                        // 배열의 시작
                        isArray = true;
                        lineField.Add(string.Format("{0}[]", array[0, j]));
                        
                        if (!ArrayCountDic.ContainsKey(array[1, j]))
                        {
                            // 배열의 숫자를 카운트한다
                            ArrayCountDic.Add(array[1, j], 1);
                        }
                        else
                        {
                            Debug.Log(string.Format("<color=red>{0}의 같은 배열임에도 이름이 다르다!</color>", tableName));
                            break;
                        }
                    }
                    else
                    {
                        lineField.Add(array[0, j]);
                    }
                    
                    lineField.Add(array[1, j]);
                    lineField.Add(";");

                    string field = string.Join(" ", lineField.ToArray());
                    completeStruct.Add(string.Format("{0}", field));
                }
                else
                {
                    // 배열일 때
                    if (ArrayCountDic.ContainsKey(array[1, j]))
                    {
                        // 배열의 숫자를 카운트한다
                        ArrayCountDic[array[1, j]]++;
                    }
                    else
                    {
                        Debug.Log(string.Format("<color=red>{0}의 같은 배열임에도 이름이 다르다!</color>", tableName));
                        break;
                    }

                    if (array[2, j].Contains("}"))
                    {
                        // 배열의 끝
                        isArray = false;
                    }
                }
            }
            #endregion

            completeStruct.Add("\n");

            #region @.AutoLoadTable Sector
            completeStruct.Add("\tpublic static void AutoLoadTable()");
            completeStruct.Add("\t{");

            string boolText = _isResource.ToString().ToLower();
            completeStruct.Add(string.Format("\t\tstring[,] data = TableDataManager.Instance.PublicExcelReader(\"{0}\", {1});", tableName, boolText));
            completeStruct.Add("\t\tLoadTable(data);");
            completeStruct.Add("\t}");
            #endregion

            /*
            public static void AutoLoadTable()
            {
                string[,] data = TableDataManager.Instance.PublicExcelReader("Table_MessageBox", true);
                LoadTable(data);
            }
            */

            completeStruct.Add("\n");

            #region @.LoadTable Sector
            completeStruct.Add("\tpublic static void LoadTable(string[,] _data)");
            completeStruct.Add("\t{");

            completeStruct.Add("\t\tfor (int i = 4; i < _data.GetLength(0); i++)");
            completeStruct.Add("\t\t{");
            completeStruct.Add("\t\t\tint columnCount = 0;");
            completeStruct.Add(string.Format("\t\t\t{0} newData = new {0}();", tableName));

            isArray = false;
            int arrayCount = 0;
            List<string> fieldList = new List<string>();
            for (int j = 0; j < array.GetLength(1); j++)
            {
                List<string> lineField = new List<string>();
                if (!isArray)
                {
                    // 배열이 아닐 때
                    string temp = string.Format("newData.{0}", array[1, j]);
                    lineField.Add(temp);
                    fieldList.Add(temp);
                }
                else
                {
                    // 배열일 때
                    string temp = string.Format("newData.{0}[{1}]", array[1, j], arrayCount++);
                    lineField.Add(temp);
                    fieldList.Add(temp);
                }

                lineField.Add("=");

                if (array[2, j].Contains("Array{"))
                {
                    if (ArrayCountDic.ContainsKey(array[1, j]))
                    {
                        // 배열의 시작
                        isArray = true;
                        int count = ArrayCountDic[array[1, j]];
                        lineField.Add(string.Format("new {0}[{1}];", array[0, j], count));
                        
                        completeStruct.Add(string.Format("\t\t\t{0}", string.Join(" ", lineField.ToArray())));
                        lineField.Clear();

                        lineField.Add(string.Format("newData.{0}[{1}]", array[1, j], arrayCount++));
                        lineField.Add("=");

                        if (array[0, j].Contains("Enum"))
                        {
                            lineField.Add(string.Format("Util.GetEnumType<{0}>(_data[i, columnCount++]);", array[0, j]));
                        }
                        if (array[0, j].Contains("Custom"))
                        {
                            lineField.Add(string.Format("new {0}(_data[i, columnCount++]);", array[0, j]));
                        }
                        else
                        {
                            switch (array[0, j])
                            {
                                case "string":
                                    lineField.Add("_data[i, columnCount++];");
                                    break;

                                default:
                                    lineField.Add(string.Format("{0}.Parse(_data[i, columnCount++]);", array[0, j]));
                                    break;
                            }
                        }
                    }
                    else
                    {
                        Debug.Log(string.Format("<color=red>{0}의 같은 배열임에도 이름이 다르다!</color>", tableName));
                        break;
                    }
                }
                else
                {
                    if (array[0, j].Contains("Enum"))
                    {
                        lineField.Add(string.Format("Util.GetEnumType<{0}>(_data[i, columnCount++]);", array[0, j]));
                    }
                    else if (array[0, j].Contains("Custom"))
                    {
                        lineField.Add(string.Format("new {0}(_data[i, columnCount++]);", array[0, j]));
                    }
                    else
                    {
                        switch (array[0, j])
                        {
                            case "string":
                                lineField.Add("_data[i, columnCount++];");
                                break;

                            default:
                                lineField.Add(string.Format("{0}.Parse(_data[i, columnCount++]);", array[0, j]));
                                break;
                        }
                    }

                    if (array[2, j].Contains("}"))
                    {
                        // 배열의 끝
                        isArray = false;
                        arrayCount = 0;
                    }
                }

                string field = string.Join(" ", lineField.ToArray());
                completeStruct.Add(string.Format("\t\t\t{0}", field));
            }

            completeStruct.Add(string.Format("\t\t\tTableDataManager.Instance.SetDictinary<{0}>(newData.Index, newData.CodeName, newData);", tableName));
            completeStruct.Add("\t\t}");
            completeStruct.Add("\t}");
            #endregion

            completeStruct.Add("\n");

            //#region @.GetTableString Sector
            //completeStruct.Add(string.Format("\tpublic string GetTableString({0} _table)", tableName));
            //completeStruct.Add("\t{");

            //string getString = string.Empty;
            //List<string> format = new List<string>();
            //List<string> args = new List<string>();
            //for (int j = 0; j < fieldList.Count; j++)
            //{
            //    format.Add(string.Format("[{0}]", j));
            //    args.Add(string.Format("_table.{0}", fieldList[j].Replace("newData.", "")));
            //}

            //string formatS = string.Join(",", format.ToArray()); // {0},{1},{2},{3},{4}
            //formatS = formatS.Replace("[", "{");
            //formatS = formatS.Replace("]", "}");
            //string argsS = string.Join(", ", args.ToArray()); // _table.Index, _table.CodeName, _table.KoreanDesc, _table.KoreanConfirmButton, _table.KoreanCancelButton
            //string returnValue = string.Format("string.Format(\"{0}\", {1});", formatS, argsS);

            //completeStruct.Add(string.Format("\t\tstring returnValue = {0}", returnValue));
            //completeStruct.Add("\t\treturn returnValue;");

            //completeStruct.Add("\t}");
            //#endregion

            completeStruct.Add("}");

            var complete = string.Join("\n", completeStruct.ToArray());
            Util.SaveFile(string.Format("{0}/{1}.cs", GetStructPath(), tableName), complete, Encoding.UTF8);

            Debug.Log(string.Format("<color=yellow>{0}.cs</color> <color=blue>TableStruct Create Complete</color>", tableName));
        }

        AssetDatabase.Refresh();
        Debug.Log(string.Format("<color=blue>TableStruct Create Complete</color>"));
    }
    
    private static List<string> GetTableDataList(string _path)
    {
        var directory = new DirectoryInfo(_path);
        var files = directory.GetFiles();
        List<string> filesPath = new List<string>();
        for (int i = 0; i < files.Length; i++)
        {
            // 260307 xlsx 불러오는 기능으로 확장
            // if (!files[i].Name.Contains(".meta"))
            if (files[i].Extension == ".xlsx")
            {
                filesPath.Add(files[i].Name);
            }
        }

        filesPath.Sort();
        return filesPath;
    }

    /// <summary>
    /// 번들에 있는 테이블을 불러오는 경로
    /// </summary>
    /// <returns></returns>
    private static string GetResourcesFolderPath()
    {
        return Path.GetFullPath("../morpg/Assets/TableData");
    }

    /// <summary>
    /// 구조체를 저장하는 경로
    /// </summary>
    /// <returns></returns>
    private static string GetStructPath()
    {
        return Path.GetFullPath("../morpg/Assets/Scripts/Struct/TableData");
    }


    public static void CopyAll()
    {
        var sourceFolder = "Assets/TableData";
        var targetFolder = "Assets/Resources/TableData";

        if (!Directory.Exists(sourceFolder))
        {
            Debug.LogError("Source 폴더가 없습니다: " + sourceFolder);
            return;
        }

        if (!Directory.Exists(targetFolder))
        {
            Directory.CreateDirectory(targetFolder);
        }

        var files = Directory.GetFiles(sourceFolder, "*.xlsx", SearchOption.TopDirectoryOnly);

        foreach (var file in files)
        {
            var fileName = Path.GetFileNameWithoutExtension(file);
            var targetPath = Path.Combine(targetFolder, fileName + ".bytes");

            File.Copy(file, targetPath, true);
        }

        AssetDatabase.Refresh();
    }
}