using UnityEngine;
using System;
using System.IO;

using System.Collections.Generic;
using System.Text;

using System.Reflection;
using System.Linq;

using Random = UnityEngine.Random;
using Object = UnityEngine.Object;

public partial class Util
{
    /// <summary>
    /// 스트링을 열거형으로 변환한다
    /// </summary>
    public static T GetEnumType<T>(string p_code)
    {
        foreach (var objvalue in Enum.GetValues(typeof(T)))
        {
            if (objvalue.ToString().ToUpper() == p_code.ToUpper())
            {
                return (T)objvalue;
            }
        }

        return default(T);
    }

    public static string GetComma(long _value)
    {
        return string.Format("{0:#,##0}", _value);
    }

    /// <summary>
    /// 셔플 알고리즘
    /// </summary>
    public static List<T> ShuffleAlgorithm<T>(List<T> _list, int _shuffleCount)
    {
        List<T> returnList = _list;
        for (int i = 0; i < _shuffleCount; i++)
        {
            int ran1 = Random.Range(0, returnList.Count);
            int ran2 = Random.Range(0, returnList.Count);
            T swapElement = returnList[ran1];
            returnList[ran1] = returnList[ran2];
            returnList[ran2] = swapElement;
        }

        return returnList;
    }

    /// <summary>
    /// 셔플 알고리즘
    /// </summary>
    public static T[] ShuffleAlgorithm<T>(T[] _array, int _shuffleCount)
    {
        T[] returnArray = _array;
        for (int i = 0; i < _shuffleCount; i++)
        {
            int ran1 = Random.Range(0, returnArray.Length);
            int ran2 = Random.Range(0, returnArray.Length);
            T swapElement = returnArray[ran1];
            returnArray[ran1] = returnArray[ran2];
            returnArray[ran2] = swapElement;
        }

        return returnArray;
    }

    public static string LoadFile(string _path, Encoding _encoding = null)
    {
        if (_encoding == null)
            return File.ReadAllText(_path);
        return File.ReadAllText(_path, _encoding);
    }

    public static void SaveFile(string _path, string _text, Encoding _encoding)
    {
        File.WriteAllText(_path, _text, _encoding);
    }

    public static string[,] PublicExcelReader(string _text)
    {
        string[] lines = _text.Split('\n');
        int lineCount = (!string.IsNullOrEmpty(lines[lines.Length - 1])) ? lines.Length : lines.Length - 1;

        string[] naming = lines[0].Split(',');
        string[,] returnArray = new string[lineCount, naming.Length];

        for (int i = 0; i < lineCount; i++)
        {
            string[] nowLine = lines[i].Split(',');
            for (int j = 0; j < nowLine.Length; j++)
            {
                try
                {
                    nowLine[j] = nowLine[j].Replace("\r", "");
                    nowLine[j] = nowLine[j].Replace(';', ',');
                    nowLine[j] = nowLine[j].Replace("\\n", "\n");
                    returnArray[i, j] = nowLine[j];
                }
                catch (Exception _ex)
                {

                }
            }
        }

        return returnArray;
    }

    public static string GetFullPath()
    {
        return Path.GetFullPath(string.Format("."));
    }

    public static string GetReceipt(string _receipt)
    {
        //int index = _receipt.IndexOf("GPA.");

        //string text = string.Empty;
        //while (!_receipt[index].Equals('\\'))
        //{
        //    text += _receipt[index++];
        //}
        //return text;

        string returnValue = string.Empty;
        string[] line = _receipt.Split(',');
        for (int i = 0; i < line.Length; i++)
        {
            if (line[i].Contains("TransactionID"))
            {
                returnValue = line[i].Replace("TransactionID", "");
                returnValue = returnValue.Replace(":", "");
                returnValue = returnValue.Replace("\"", "");
                break;
            }
        }

        return returnValue;
    }

    public static Component CopyComponent(Component _original, GameObject _destination)
    {
        Type type = _original.GetType();
        Component copy = _destination.GetComponent(type);
        FieldInfo[] fields = type.GetFields();
        foreach (FieldInfo field in fields)
        {
            field.SetValue(copy, field.GetValue(_original));
        }
        return copy;
    }

    /// <summary>
    /// string을 숫자만 남긴 다음 숫자로 반환한다
    /// </summary>
    /// <param name="_text"></param>
    /// <returns></returns>
    public static int GetNumber(string _text)
    {
        //System.Text.RegularExpressions.Regex.Replace(TestString, "[^0-9a-zA-Zㄱ-ㅎㅏ-ㅣ가-힗あ-んァ-ソ一-龥]+", "")
        string number = System.Text.RegularExpressions.Regex.Replace(_text, "[^0-9]+", "");
        int value = 0;
        int.TryParse(number, out value);
        return value;
    }

    public static T GetRandomIndex<T>(List<T> _list)
    {
        int index = (int)Random.Range(0, _list.Count);
        return _list[index];
    }

    public static T GetRandomIndex<T>(T[] _array)
    {
        int index = (int)Random.Range(0, _array.Length);
        return _array[index];
    }

    /// <summary>
    /// 자식을 삭제한다
    /// </summary>
    /// <param name="_obj"></param>
    public static void DestroyChild(GameObject _obj)
    {
        Transform[] childList = _obj.GetComponentsInChildren<Transform>(true);
        if (childList != null)
        {
            for (int i = 1; i < childList.Length; i++)
            {
                if (childList[i] != _obj.transform)
                    GameObject.DestroyImmediate(childList[i].gameObject, true);
            }
        }
    }

    /// <summary>
    /// 타일의 위치를 반올림한다(소숫점을 없앤다)
    /// </summary>
    /// <param name="_obj"></param>
    public static void TilePosition_Round(GameObject _obj)
    {
        Transform[] childList = _obj.GetComponentsInChildren<Transform>(true);
        if (childList != null)
        {
            for (int i = 1; i < childList.Length; i++)
            {
                int x = Mathf.RoundToInt(childList[i].position.x);
                int y = Mathf.RoundToInt(childList[i].position.y);
                childList[i].position = new Vector2(x, y);
            }
        }
    }

    public static int Round(float _value)
    {
        return Mathf.RoundToInt(_value);
    }

    public static Vector2Int Round(Vector3 _value)
    {
        return new Vector2Int(Round(_value.x), Round(_value.y));
    }

    public static string GetTimer(long _time)
    {
        int second = (int)(_time % 60);
        int minute = (int)((_time / 60) % 60);
        int hour = (int)(_time / (60 * 60));

        return string.Format("{0:00}:{1:00}:{2:00}", hour, minute, second);
    }

    public static GameObject CreateObject(GameObject _obj, Transform _parent, Vector3 _position, Vector3 _scale)
    {
        GameObject obj = Object.Instantiate(_obj);
        obj.transform.SetParent(_parent);
        obj.transform.localPosition = _position;
        obj.transform.localScale = _scale;

        return obj;
    }

    public static void CreateFolder(string _path)
    {
        if (!Directory.Exists(_path))
        {
            Directory.CreateDirectory(_path);
        }
    }

}