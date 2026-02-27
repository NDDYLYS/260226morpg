using UnityEngine;
using System;
using System.IO;

using System.Collections.Generic;
using LitJson;
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

        LogManager.Instance.DebugLogCategory(LogCategoryEnum.Data, string.Format("not found column (Type : {0} , code : {1})", (typeof(T)).ToString(), p_code));
        return default(T);
    }

    public static string GetComma(long _value)
    {
        return string.Format("{0:#,##0}", _value);
    }

    public static void CreateFolder(string _path)
    {
        if (!Directory.Exists(_path))
        {
            Directory.CreateDirectory(_path);
            LogManager.Instance.DebugLogCategory(LogCategoryEnum.Data, string.Format("<color=yellow>CreateFolder : {0}</color>", _path));
        }
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

    public static string JsonToClass<T>(T _data)
    {
        return JsonMapper.ToJson(_data);
    }

    public static T ClassToJson<T>(string _json)
    {
        return JsonMapper.ToObject<T>(_json);
    }

    public static byte[] BytesToClass<T>(T _data)
    {
        string json = JsonToClass<T>(_data);
        return Encoding.UTF8.GetBytes(json);
    }

    public static T ClassToBytes<T>(byte[] _bytes)
    {
        string json = Encoding.UTF8.GetString(_bytes);
        return ClassToJson<T>(json);
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

    public static MapStruct GetRandomMapStruct(List<MapStruct> _list)
    {
        int index = (int)Random.Range(0, _list.Count);
        return _list[index];
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

    public static void CreateEnemy(string _codename, Vector3 _position)
    {
        GameObject obj = TableDataManager.Instance.GetLoadedPrefab(string.Format("Character/Dungeon/Enemy"));
        obj = CreateObject(obj, Map.Instance.ObjectGenerator, _position, Vector3.one);
        Enemy enemy = obj.GetComponent<Enemy>();
        if (enemy != null)
            enemy.SetEnemyStat(_codename);
    }
    
    public static void CreateItem(string _codename, int _count, Vector3 _position, ItemState _state)
    {
        GameObject obj = TableDataManager.Instance.GetLoadedPrefab(string.Format("Item/Item"));
        obj = CreateObject(obj, Map.Instance.ObjectGenerator, _position, Vector3.one);
        Item item = obj.GetComponent<Item>();
        if (item != null)
            item.SetItem(_codename, _count, _state);
    }

    public static void CreateSummon(string _codename, int _addLevel, Vector3 _position)
    {
        GameObject obj = TableDataManager.Instance.GetLoadedPrefab(string.Format("Character/Dungeon/Summon"));
        obj = CreateObject(obj, Map.Instance.ObjectGenerator, _position, Vector3.one);
        Summon summon = obj.GetComponent<Summon>();
        if (summon != null)
            summon.SetSummonStat(_codename, _addLevel);
    }

    public static ItemStruct GetDropItem(string _dropItem, ItemState _state)
    {
        ItemStruct item = null;
        Table_DropItem drop = TableDataManager.Instance.GetTableData<Table_DropItem>(_dropItem);
        if (drop != null)
        {
            int max = GetMax(drop);
            int minSum = 0;
            int prob = Random.Range(0, max);

            string[] codename = drop.ItemCodename.Split('.');
            string[] probs = drop.Prob.Split('.');
            string[] mins = drop.MinCount.Split('.');
            string[] maxs = drop.MaxCount.Split('.');

            for (int i = 0; i < probs.Length; i++)
            {
                int maxSum = minSum + int.Parse(probs[i]);
                if (minSum <= prob && prob < maxSum)
                {
                    switch (_state)
                    {
                        case ItemState.Material:
                            int count = Random.Range(int.Parse(mins[i]), int.Parse(maxs[i]) + 1);
                            item = new ItemStruct(codename[i], count, _state);
                            break;
                        case ItemState.Use:
                            break;
                    }
                    return item;
                }

                minSum = maxSum;
            }
        }

        return item;
    }

    private static int GetMax(Table_DropItem _drop)
    {
        int max = 0;
        string[] value = _drop.Prob.Split('.');
        for (int i = 0; i < value.Length; i++)
        {
            max += int.Parse(value[i]);
        }

        return max;
    }

    public static DamageFloating CreateFloating(FloatingStruct _floating)
    {
        DamageFloating obj = MainManager.Instance.PopDamageFloating();
        obj.OnFloatingStart(_floating.FloatingType, _floating.Value, _floating.Target);
        return obj;
    }

    public static NameFloating CreateFloating(string _name, Transform _target)
    {
        NameFloating obj = MainManager.Instance.PopNameFloating();
        obj.OnFloatingStart(_name, _target);
        return obj;
    }

    public static NameFloating CreateFloating(CharacterStruct _character, Transform _target)
    {
        NameFloating obj = MainManager.Instance.PopNameFloating();
        obj.OnFloatingStart(_character.Level, _character.Name, _target);
        return obj;
    }

    public static void CreateNPC(string _codename, Vector3 _position)
    {
        GameObject obj = TableDataManager.Instance.GetLoadedPrefab(string.Format("Character/Village/NPC"));
        obj = CreateObject(obj, Map.Instance.ObjectGenerator, _position, Vector3.one);
        NPC npc = obj.GetComponent<NPC>();
        if (npc != null)
            npc.SetNPC(_codename);
    }

    public static Player CreatePlayer(SpeciesTypeEnum _species, Vector3 _position)
    {
        string prefab = (GameManager.Instance.CurrentScene == "3Dungeon") ? "Dungeon" : "Village";
        GameObject obj = TableDataManager.Instance.GetLoadedPrefab($"Character/{prefab}/Player");
        obj = CreateObject(obj, Map.Instance.ObjectGenerator, _position, Vector3.one);
        Player player = obj.GetComponent<Player>();
        if (player != null)
            player.SetPlayer(_species);
        return player;
    }

    public static Texture2D GetTexture(string _image)
    {
        Texture2D tex = Resources.Load<Texture2D>(string.Format("Image/{0}", _image));
        return tex;
    }

    public static Sprite GetSprite(string _image)
    {
        Texture2D tex = GetTexture(_image);
        if (tex == null)
            return null;
        Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 32f);
        return sprite;
    }

    public static Color GetColorForGrade(DropGradeEnum _dropGrade)
    {
        Color value = Color.white;
        switch (_dropGrade)
        {
            case DropGradeEnum.C:
                value = new Color32(225, 225, 225, 255);
                break;
            case DropGradeEnum.B:
                value = new Color32(150, 200, 255, 255);
                break;
            case DropGradeEnum.A:
                value = new Color32(255, 255, 200, 255);
                break;
            case DropGradeEnum.S:
                value = new Color32(255, 200, 0, 255);
                break;
            case DropGradeEnum.SS:
                value = new Color32(255, 150, 255, 255);
                break;
        }

        return value;
    }

    public static string GetColorTextForGrade(DropGradeEnum _dropGrade, string _text)
    {
        var value = "";
        switch (_dropGrade)
        {
            case DropGradeEnum.C:
                value = $"<color=#FFFFFF>{_text}</color>";
                break;
            case DropGradeEnum.B:
                value = $"<color=#96C8FF>{_text}</color>";
                break;
            case DropGradeEnum.A:
                value = $"<color=#FFFFC8>{_text}</color>";
                break;
            case DropGradeEnum.S:
                value = $"<color=#FFC800>{_text}</color>";
                break;
            case DropGradeEnum.SS:
                value = $"<color=#FF96FF>{_text}</color>";
                break;
        }

        return value;
    }

    /// <summary>
    /// Dictionary 2개를 합친다. 재료 아이템과 몬스터 처치 수에 쓴다
    /// </summary>
    /// <param name="_origin"></param>
    /// <param name="_add"></param>
    /// <returns></returns>
    public static Dictionary<string, int> SumDictionary(Dictionary<string, int> _origin, Dictionary<string, int> _add)
    {
        Dictionary<string, int> origin = _origin;
        Dictionary<string, int>.Enumerator add = _add.GetEnumerator();
        while (add.MoveNext())
        {
            string key = add.Current.Key;
            int value = add.Current.Value;

            if (!origin.ContainsKey(key))
                origin.Add(key, value);
            else
                origin[key] += value;
        }

        return origin;
    }

    /// <summary>
    /// 장착하려는 장비아이템의 부위에 따라 이미 장착하고 있는 해당 부분의 아이템을 벗는다
    /// </summary>
    /// <param name="_kind"></param>
    /// <returns></returns>
    public static List<EquipmentPartEnum> GetEquipmentKindForParts(EquipmentKindEnum _kind)
    {
        List<EquipmentPartEnum> value = new List<EquipmentPartEnum>();
        switch (_kind)
        {
            case EquipmentKindEnum.OneHandSword:
            case EquipmentKindEnum.OneHandDagger:
            case EquipmentKindEnum.OneHandMace:
            case EquipmentKindEnum.OneHandStaff:
                value.Add(EquipmentPartEnum.RightHand);
                break;
            case EquipmentKindEnum.TwoHandSword:
            case EquipmentKindEnum.TwoHandSpear:
            case EquipmentKindEnum.TwoHandBow:
            case EquipmentKindEnum.TwoHandStaff:
            case EquipmentKindEnum.TwoHandShield:
                value.Add(EquipmentPartEnum.RightHand);
                value.Add(EquipmentPartEnum.LeftHand);
                break;
            case EquipmentKindEnum.OneHandShield:
                value.Add(EquipmentPartEnum.LeftHand);
                break;
            case EquipmentKindEnum.Hat:
            case EquipmentKindEnum.Helmet:
                value.Add(EquipmentPartEnum.Head);
                break;
            case EquipmentKindEnum.Cloth:
            case EquipmentKindEnum.Armor:
                value.Add(EquipmentPartEnum.Body);
                break;
            case EquipmentKindEnum.Shoes:
            case EquipmentKindEnum.Boots:
                value.Add(EquipmentPartEnum.Legs);
                break;
        }

        return value;
    }

    /// <summary>
    /// 210210 방패를 장착하려는데, 오른손에 양손무기를 끼고 있다면 그것도 벗는다
    /// </summary>
    /// <param name="_kind"></param>
    /// <returns></returns>
    public static EquipmentPartEnum GetEquipmentTwoHandForSheild(EquipmentKindEnum _kind)
    {
        var parts = GetEquipmentKindForParts(_kind);
        if (parts[0] == EquipmentPartEnum.LeftHand)
        {
            var rightHandEquip = GameManager.Instance.Player.CharacterStruct.GetEquipItem(EquipmentPartEnum.RightHand);
            if (rightHandEquip == null)
                return EquipmentPartEnum.None;

            switch (rightHandEquip.EquipKind)
            {
                case EquipmentKindEnum.TwoHandSword:
                case EquipmentKindEnum.TwoHandSpear:
                case EquipmentKindEnum.TwoHandBow:
                case EquipmentKindEnum.TwoHandStaff:
                case EquipmentKindEnum.TwoHandShield:
                    return EquipmentPartEnum.RightHand;
            }
        }

        return EquipmentPartEnum.None;
    }

    /// <summary>
    /// 230214 무기를 장착하려는데, 왼손에 양손무기를 끼고 있다면 그것도 벗는다
    /// </summary>
    /// <param name="_kind"></param>
    /// <returns></returns>
    public static EquipmentPartEnum GetEquipmentTwoHandForWeapon(EquipmentKindEnum _kind)
    {
        var parts = GetEquipmentKindForParts(_kind);
        if (parts[0] == EquipmentPartEnum.RightHand)
        {
            var leftHandEquip = GameManager.Instance.Player.CharacterStruct.GetEquipItem(EquipmentPartEnum.LeftHand);
            if (leftHandEquip == null)
                return EquipmentPartEnum.None;

            switch (leftHandEquip.EquipKind)
            {
                case EquipmentKindEnum.TwoHandSword:
                case EquipmentKindEnum.TwoHandSpear:
                case EquipmentKindEnum.TwoHandBow:
                case EquipmentKindEnum.TwoHandStaff:
                case EquipmentKindEnum.TwoHandShield:
                    return EquipmentPartEnum.LeftHand;
            }
        }

        return EquipmentPartEnum.None;
    }

    public static List<EquipmentKindEnum> GetEquipmentKindForPart(EquipmentPartEnum _kind)
    {
        List<EquipmentKindEnum> value = new List<EquipmentKindEnum>();
        switch (_kind)
        {
            case EquipmentPartEnum.All:
                value.Add(EquipmentKindEnum.OneHandSword);
                value.Add(EquipmentKindEnum.OneHandDagger);
                value.Add(EquipmentKindEnum.OneHandMace);
                value.Add(EquipmentKindEnum.OneHandStaff);

                value.Add(EquipmentKindEnum.OneHandShield);

                value.Add(EquipmentKindEnum.TwoHandSword);
                value.Add(EquipmentKindEnum.TwoHandSpear);
                value.Add(EquipmentKindEnum.TwoHandBow);
                value.Add(EquipmentKindEnum.TwoHandStaff);
                value.Add(EquipmentKindEnum.TwoHandShield);

                value.Add(EquipmentKindEnum.Hat);
                value.Add(EquipmentKindEnum.Helmet);

                value.Add(EquipmentKindEnum.Cloth);
                value.Add(EquipmentKindEnum.Armor);

                value.Add(EquipmentKindEnum.Shoes);
                value.Add(EquipmentKindEnum.Boots);
                break;
            case EquipmentPartEnum.RightHand:
                value.Add(EquipmentKindEnum.OneHandSword);
                value.Add(EquipmentKindEnum.OneHandDagger);
                value.Add(EquipmentKindEnum.OneHandMace);
                value.Add(EquipmentKindEnum.OneHandStaff);

                value.Add(EquipmentKindEnum.TwoHandSword);
                value.Add(EquipmentKindEnum.TwoHandSpear);
                value.Add(EquipmentKindEnum.TwoHandBow);
                value.Add(EquipmentKindEnum.TwoHandStaff);
                value.Add(EquipmentKindEnum.TwoHandShield);
                break;
            case EquipmentPartEnum.LeftHand:
                value.Add(EquipmentKindEnum.OneHandShield);

                value.Add(EquipmentKindEnum.TwoHandSword);
                value.Add(EquipmentKindEnum.TwoHandSpear);
                value.Add(EquipmentKindEnum.TwoHandBow);
                value.Add(EquipmentKindEnum.TwoHandStaff);
                value.Add(EquipmentKindEnum.TwoHandShield);
                break;
            case EquipmentPartEnum.Head:
                value.Add(EquipmentKindEnum.Hat);
                value.Add(EquipmentKindEnum.Helmet);
                break;
            case EquipmentPartEnum.Body:
                value.Add(EquipmentKindEnum.Cloth);
                value.Add(EquipmentKindEnum.Armor);
                break;
            case EquipmentPartEnum.Legs:
                value.Add(EquipmentKindEnum.Shoes);
                value.Add(EquipmentKindEnum.Boots);
                break;
        }

        return value;
    }

    public static DropGradeEnum DownGrade(DropGradeEnum _dropGrade)
    {
        DropGradeEnum value = DropGradeEnum.C;
        switch (_dropGrade)
        {
            case DropGradeEnum.B:
                value = DropGradeEnum.C;
                break;
            case DropGradeEnum.A:
                value = DropGradeEnum.B;
                break;
            case DropGradeEnum.S:
                value = DropGradeEnum.A;
                break;
            case DropGradeEnum.SS:
                value = DropGradeEnum.S;
                break;
            default:
                value = DropGradeEnum.C;
                break;
        }

        return value;
    }

    public static void AddAttribute(Character _obj, string _attribute)
    {
        if (string.IsNullOrEmpty(_attribute))
            return;
        
        GameObject obj = _obj.gameObject;
        Type component = Type.GetType(_attribute);

        Attribute attribute = (Attribute)obj.AddComponent(component);
        if (_obj.AttributeList == null)
            _obj.AttributeList = new List<Attribute>();
        if (!_obj.AttributeList.Contains(attribute))
            _obj.AttributeList.Add(attribute);
    }

    public static bool RemoveAttribute(Character _obj, string _attribute)
    {
        if (string.IsNullOrEmpty(_attribute))
            return false;

        GameObject obj = _obj.gameObject;
        Type component = Type.GetType(_attribute);

        Attribute attribute = (Attribute)obj.GetComponent(component);
        if (_obj.AttributeList == null)
            _obj.AttributeList = new List<Attribute>();
        if (_obj.AttributeList.Contains(attribute))
        {
            _obj.AttributeList.Remove(attribute);
            GameObject.Destroy(attribute);
            return true;
        }

        return false;
    }

    public static void RemoveAttribute(Character _obj)
    {
        GameObject obj = _obj.gameObject;

        Attribute[] attributes = obj.GetComponents<Attribute>();
        if (_obj.AttributeList == null)
            _obj.AttributeList = new List<Attribute>();
        int count = attributes.Length;
        for (int i = 0; i < count; i++)
        {
            GameObject.Destroy(attributes[i]);
        }
        _obj.AttributeList.Clear();
    }

    public static bool IsAttack(Vector2Int _position)
    {
        Collider2D[] obstacles = Physics2D.OverlapCircleAll(_position, 0.4f);
        foreach (var obstacle in obstacles)
        {
            if (obstacle != null)
            {
                var viewsight = obstacle.gameObject.GetComponent<Viewsight>();
                if (viewsight != null)
                    continue;

                var character = obstacle.gameObject.GetComponent<Character>();
                if (character != null)
                {
                    var attackable = character.GetAttackable();
                    if (attackable)
                        return character.IsAlive;
                    else
                        return false;
                }
                else
                    return false;
            }
        }

        return false;
    }

    private static CharacterTypeEnum GetReverseType(CharacterTypeEnum _type)
    {
        switch (_type)
        {
            case CharacterTypeEnum.Player:
            case CharacterTypeEnum.Summon:
                return CharacterTypeEnum.Enemy;
            case CharacterTypeEnum.Enemy:
            case CharacterTypeEnum.Boss:
                return CharacterTypeEnum.Player;
        }

        return CharacterTypeEnum.None;
    }

    public static bool Attack(Character _user, Vector3 _target, Table_Skill _skill, Vector2 _position, Action _callback = null)
    {
        Vector2Int vector = Round(_target);
        
        bool isAttack = false;
        if (_skill.SkillTarget == SkillTargetEnum.Enemy)
            isAttack = IsAttack(vector); // 해당 좌표에 적이 있다면
        else if (_skill.SkillTarget == SkillTargetEnum.Ally)
            isAttack = true;

        float distance = Vector2.Distance(_position, vector); // 적과 나의 거리
        if (isAttack)
        {
            // 해당 좌표에 적이 있다면 IsLand의 여부에 상관이 없다
            // 210124 힐은 해당 좌표에 적이 없어도 가능하다
            return CheckSkillRange(_user, _skill, vector, distance, _callback);
        }
        else
        {
            if (_skill.SubSkillTarget == SubSkillTargetEnum.Land)
            {
                // 해당 좌표에 적이 없어도 Land는 사용할 수 있다
                return CheckSkillRange(_user, _skill, vector, distance, _callback);
            }
            else
            {
                // 210207 땅에 쓸 수 없는 스킬인데 땅에 겨냥했으면 턴이 지나지 않는다
                // 230224 이곳에서 문제가 발생하여 일단은 턴이 지나도록 한다
                if (_callback != null)
                    _callback();
                return false;
            }
        }

        return false;
    }

    /// <summary>
    /// 스킬의 사정거리를 체크한다
    /// </summary>
    /// <param name="_user"></param>
    /// <param name="_skill"></param>
    /// <param name="_vector"></param>
    /// <param name="_distance"></param>
    /// <returns></returns>
    private static bool CheckSkillRange(Character _user, Table_Skill _skill, Vector2Int _vector, float _distance, Action _callback = null)
    {
        if (_distance <= _skill.Range)
            return CheckSkillMana(_user, _skill, _vector, _callback);
        if (_callback != null)
            _callback();
        return false;
    }

    /// <summary>
    /// 스킬의 마나소비를 체크한다
    /// </summary>
    /// <param name="_user"></param>
    /// <param name="_skill"></param>
    /// <param name="_vector"></param>
    /// <returns></returns>
    private static bool CheckSkillMana(Character _user, Table_Skill _skill, Vector2Int _vector, Action _callback = null)
    {
        bool value = false;        
        var consumeMP = _user.GetSpecialEffectValue(SpecialEffectTypeEnum.ConsumeMP, _skill.ConsumeMP);

        if (consumeMP <= _user.CharacterStruct.CurrentMP)
        {
            BattleWay.UseSkill(_user, _skill, _vector, _callback);
            value = true;
        }
        else
        {
            if (_callback != null)
                _callback();
        }
        return value;
    }

    public static string GetColor(StatEnum _stat)
    {
        string color = string.Empty;
        switch (_stat)
        {
            case StatEnum.Str:
                color = "#ff0000ff";
                break;
            case StatEnum.Agi:
                color = "#ff00ffff";
                break;
            case StatEnum.Vit:
                color = "#ff6400ff";
                break;
            case StatEnum.Int:
                color = "#0000ffff";
                break;
            case StatEnum.Dex:
                color = "#00ff00ff";
                break;
            case StatEnum.Luk:
                color = "#ffff00ff";
                break;
        }
        return color;
    }

    public static string GetColor(AbilityEnum _ability)
    {
        string color = string.Empty;
        switch (_ability)
        {
            case AbilityEnum.Atk:
                color = "#ff0000ff";
                break;
            case AbilityEnum.Matk:
                color = "#0000ffff";
                break;
            case AbilityEnum.Hit:
                color = "#00ff00ff";
                break;
            case AbilityEnum.Flee:
                color = "#ff00ffff";
                break;
            case AbilityEnum.Def:
                color = "#ff6400ff";
                break;
            case AbilityEnum.Mdef:
                color = "#0000ffff";
                break;
            case AbilityEnum.CriProb:
                color = "#ffff00ff";
                break;
            case AbilityEnum.CriPower:
                color = "#ffff00ff";
                break;
        }
        return color;
    }

    public static string GetColor(bool _isSet)
    {
        if (_isSet)
            return "#ffff00ff";
        return "#2F4F4Fff";
    }

    public static BuffStruct CreateBuff(string _codename, BuffTypeEnum _type, Character _target, 
        CustomBasePer[] _stats, 
        CustomBasePer[] _abilitys,
        CustomSpecialEffect[] _specialEffects,
        int _duration, bool _overlap)
    {
        var sign = (_type == BuffTypeEnum.Buff) ? 1 : -1;

        BuffStruct buff = null;
        var stat = new int[(int)StatEnum.Max];
        var ability = new int[(int)AbilityEnum.Max];

        var index = 0;
        foreach (var _stat in _stats)
        {
            var @base = _stat.Base;
            var per = (int)((_target.CharacterStruct.GetPlayerStat((StatEnum)index) * _stat.Per * 0.01f));
            stat[index] = sign * (@base + per);
            index++;
        }

        index = 0;
        foreach (var _ability in _abilitys)
        {
            var @base = _ability.Base;
            var per = (int)((_target.CharacterStruct.GetPlayerAbility((AbilityEnum)index) * _ability.Per * 0.01f));
            ability[index] = sign * (@base + per);
            index++;
        }

        buff = new BuffStruct(_codename, stat, ability, _specialEffects, _duration, _overlap);
        return buff;
    }

    public static List<string> GetBuffDebug(BuffStruct _buff)
    {
        List<string> list = new List<string>();

        int count = _buff.Stats.Length;
        for (int i = 0; i < count; i++)
        {
            int value = _buff.Stats[i];
            if (0 < value)
            {
                var text = string.Format(TableDataManager.Instance.GetTableText("UI_BuffIcrease"), ((StatEnum)i).ToString());
                list.Add(text);
            }
        }

        count = _buff.Abilitys.Length;
        for (int i = 0; i < count; i++)
        {
            int value = _buff.Abilitys[i];
            if (0 < value)
            {
                var text = string.Format(TableDataManager.Instance.GetTableText("UI_BuffIcrease"), ((AbilityEnum)i).ToString());
                list.Add(text);
            }
        }

        count = _buff.SpecialEffects.Length;
        for (int i = 0; i < count; i++)
        {
            var value = _buff.SpecialEffects[i];
            if (0 < value.Values.Base || 0 < value.Values.Per)
            {
                switch (value.SpecialEffectType)
                {
                    case SpecialEffectTypeEnum.MeleeDamage:
                        list.Add("물리 공격력 증가");
                        break;
                    case SpecialEffectTypeEnum.MagicDamage:
                        list.Add("마법 공격력 증가");
                        break;
                    case SpecialEffectTypeEnum.DamageReduce:
                        list.Add("대미지 감소율 증가");
                        break;
                    case SpecialEffectTypeEnum.IncreaseRecovery:
                        list.Add("회복량 증가");
                        break;
                    case SpecialEffectTypeEnum.DrainHP:
                        list.Add("체력 흡수율 증가");
                        break;
                    case SpecialEffectTypeEnum.DrainMP:
                        list.Add("마나 흡수율 증가");
                        break;
                    case SpecialEffectTypeEnum.ConsumeMP:
                        list.Add("마나 소모량 감소");
                        break;
                    case SpecialEffectTypeEnum.Reflection:
                        list.Add("피해 반사 증가");
                        break;
                    case SpecialEffectTypeEnum.ImmuneMagic:
                        list.Add("마법면역");
                        break;
                    case SpecialEffectTypeEnum.Maximize:
                        list.Add("마법 최대화");
                        break;
                    case SpecialEffectTypeEnum.Double:
                        list.Add("마법 이중화");
                        break;
                    case SpecialEffectTypeEnum.Triple:
                        list.Add("마법 삼중화");
                        break;
                    case SpecialEffectTypeEnum.Shield:
                        list.Add("쉴드");
                        break;
                    case SpecialEffectTypeEnum.ManaShield:
                        list.Add("마나쉴드");
                        break;
                }
            }
        }

        return list;
    }

    public static List<string> GetDebuffDebug(BuffStruct _buff)
    {
        List<string> list = new List<string>();

        int count = _buff.Stats.Length;
        for (int i = 0; i < count; i++)
        {
            int value = _buff.Stats[i];
            if (value < 0)
            {
                var text = string.Format(TableDataManager.Instance.GetTableText("UI_DebuffDecrease"), ((StatEnum)i).ToString());
                list.Add(text);
            }
        }

        count = _buff.Abilitys.Length;
        for (int i = 0; i < count; i++)
        {
            int value = _buff.Abilitys[i];
            if (value < 0)
            {
                var text = string.Format(TableDataManager.Instance.GetTableText("UI_DebuffDecrease"), ((AbilityEnum)i).ToString());
                list.Add(text);
            }
        }

        return list;
    }

    public static PassiveStruct CreatePassive(string _codename, Character _target)
    {
        var passiveSkill = TableDataManager.Instance.GetTableData<Table_Skill_Passive>(_codename);
        if (passiveSkill == null)
            return null;

        PassiveStruct passive = null;
        var stat = new int[(int)StatEnum.Max];
        var ability = new int[(int)AbilityEnum.Max];

        var index = 0;
        foreach (var _stat in passiveSkill.PassiveStat)
        {
            var @base = _stat.Base;
            var per = (int)((_target.CharacterStruct.GetPlayerStat((StatEnum)index) * _stat.Per * 0.01f));
            stat[index] += (@base + per);
            index++;
        }

        index = 0;
        foreach (var _ability in passiveSkill.PassiveAbility)
        {
            var @base = _ability.Base;
            var per = (int)((_target.CharacterStruct.GetPlayerAbility((AbilityEnum)index) * _ability.Per * 0.01f));
            ability[index] += (@base + per);
            index++;
        }

        passive = new PassiveStruct(_codename, stat, ability, passiveSkill.PassiveSpecialEffect);
        return passive;
    }

    public static CharacterStruct GetEnemyStruct(Character _character, Table_Character _enemy)
    {
        CharacterStruct enemy = new CharacterStruct();

        enemy.Level = _enemy.Level.GetRandom();
        enemy.ViewSight = _enemy.ViewSight;

        enemy.SpeciesType = _enemy.SpeciesType;

        enemy.Image = _enemy.Image;
        enemy.Name = _enemy.Name;
        enemy.DefenceElement = _enemy.DefenceElement;

        enemy.HP = _enemy.DefaultHP;
        enemy.MP = _enemy.DefaultMP;

        var index = 0;
        var stats = _enemy.DefaultStat;
        foreach (var growth in _enemy.GrowthStat)
        {
            stats[index++] += Mathf.FloorToInt(growth * (enemy.Level - 1));
        }

        var difficultyBonus = TableDataManager.Instance.GetDungeonDifficulty();
        if (difficultyBonus == null)
            difficultyBonus = TableDataManager.Instance.GetTableData<Table_Difficulty>("Normal");

        for (int i = 0; i < stats.Length; i++)
        {
            stats[i] = (int)(stats[i] * (difficultyBonus.MonsterRate * 0.01f));
            if (stats[i] <= 0)
                stats[i] = 1;
        }

        enemy.Stat = stats;
        enemy.UpdateStat();

        foreach (var equipment in _enemy.Equipments)
        {
            enemy.EquipItem(_character, equipment);
        }

        return enemy;
    }

    public static CharacterStruct GetSummonStruct(Character _character, Table_Character _summon, int _addLevel)
    {
        CharacterStruct summon = new CharacterStruct();

        summon.Level = _summon.Level.GetRandom() + _addLevel;
        summon.ViewSight = _summon.ViewSight;

        summon.SpeciesType = _summon.SpeciesType;

        summon.Image = _summon.Image;
        summon.Name = _summon.Name;
        summon.DefenceElement = _summon.DefenceElement;

        summon.HP = _summon.DefaultHP;
        summon.MP = _summon.DefaultMP;

        var index = 0;
        var stats = _summon.DefaultStat;
        foreach (var growth in _summon.GrowthStat)
        {
            stats[index++] += Mathf.FloorToInt(growth * (summon.Level - 1));
        }

        summon.Stat = stats;
        summon.UpdateStat();

        foreach (var equipment in _summon.Equipments)
        {
            summon.EquipItem(_character, equipment);
        }

        return summon;
    }

    public static List<Table_EquipmentItem> GetEquipmentInventory(List<string> _equipmentInventoryList, List<EquipmentKindEnum> _viewPart)
    {
        List<Table_EquipmentItem> value = new List<Table_EquipmentItem>();
        foreach (var element in _equipmentInventoryList)
        {
            Table_EquipmentItem equip = TableDataManager.Instance.GetTableData<Table_EquipmentItem>(element);
            if (_viewPart.Contains(equip.EquipKind))
                value.Add(equip);
        }

        return value;
    }

    public static List<Table_EquipmentItem> GetEquipmentInventory(List<Table_EquipmentItem> _equipmentInventoryList, List<EquipmentKindEnum> _viewPart)
    {
        List<Table_EquipmentItem> value = new List<Table_EquipmentItem>();
        foreach (var element in _equipmentInventoryList)
        {
            Table_EquipmentItem equip = element;
            if (_viewPart.Contains(equip.EquipKind))
                value.Add(equip);
        }

        return value;
    }

    public static void CheckSetItem(Character _character)
    {
        List<string> alreadySetItem = new List<string>();
        var iter = _character.CharacterStruct.GetEquipItemDic().GetEnumerator();
        while (iter.MoveNext())
        {
            var equip = iter.Current.Value;
            if (equip != null)
            {
                if (!alreadySetItem.Contains(equip.SetItem))
                {
                    bool ischeck = _character.CharacterStruct.CheckSetItem(_character, equip.SetItem);
                    if (ischeck)
                        alreadySetItem.Add(equip.SetItem);
                }
            }
        }
    }

    public static List<string> GetDescEquipment(Table_EquipmentItem _tableSkillEquipment)
    {
        var list = new List<string>();
        list.AddRange(GetDescEquipmentOption(_tableSkillEquipment));
        list.AddRange(GetDescEquipmentStat(_tableSkillEquipment));
        list.AddRange(GetDescEquipmentAbility(_tableSkillEquipment));
        list.AddRange(GetDescEquipmentSet(_tableSkillEquipment));
        list.AddRange(GetDescEquipmentDesc(_tableSkillEquipment));
        return list;
    }

    private static List<string> GetDescEquipmentOption(Table_EquipmentItem _tableSkillEquipment)
    {
        var list = new List<string>();

        var parts = string.Format("UI_Parts".GetTableText(), GetEquipmentKindForParts(_tableSkillEquipment.EquipKind).GetToString("/"));
        list.Add(string.Format("<color=#000000ff>{0}</color>", parts));
        //var kind = string.Format("UI_Kind".GetTableText(), _tableSkillEquipment.EquipKind.GetToString());
        //list.Add(string.Format("<color=#000000ff>{0}</color>", kind));

        if (_tableSkillEquipment.EquipmentType != EquipmentTypeEnum.None)
        {
            var type = string.Format("UI_Type".GetTableText(), _tableSkillEquipment.EquipmentType.GetToString());
            list.Add(string.Format("<color=#000000ff>{0}</color>", type));
        }

        list.Add("");
        return list;
    }

    private static List<string> GetDescEquipmentAbility(Table_EquipmentItem _tableSkillEquipment)
    {
        var list = new List<string>();
        foreach (AbilityEnum ability in Enum.GetValues(typeof(AbilityEnum)))
        {
            if (ability == AbilityEnum.None || ability == AbilityEnum.Max)
                continue;
            int value = _tableSkillEquipment.Ability[(int)ability];
            if (value != 0)
                list.Add(string.Format("<color={0}>{1} : </color><color=#000000ff>{2}</color>", GetColor(ability), ability.ToString(), value));
        }

        list.Add("");
        return list;
    }

    private static List<string> GetDescEquipmentStat(Table_EquipmentItem _tableSkillEquipment)
    {
        var list = new List<string>();
        foreach (StatEnum stat in Enum.GetValues(typeof(StatEnum)))
        {
            if (stat == StatEnum.None || stat == StatEnum.Max)
                continue;
            int value = _tableSkillEquipment.Stat[(int)stat];
            if (value != 0)
                list.Add(string.Format("<color={0}>{1} : </color><color=#000000ff>{2}</color>", GetColor(stat), stat.ToString(), value));
        }

        list.Add("");
        return list;
    }

    private static List<string> GetDescEquipmentSet(Table_EquipmentItem _tableSkillEquipment)
    {
        var list = new List<string>();
        var set = TableDataManager.Instance.GetTableData<Table_SetItem>(_tableSkillEquipment.SetItem);
        if (set != null)
        {
            list.Add(string.Format("<color={0}>{1}</color>", GetColor(true), set.Name.GetTableText()));
            list.Add(string.Format("<color={0}>{1}</color>", GetColor(true), set.Desc.GetTableText()));

            foreach (var tempEquip in set.Equipment)
            {
                var equip = TableDataManager.Instance.GetTableData<Table_EquipmentItem>(tempEquip);
                if (equip != null)
                {
                    bool isDungeon = (GameManager.Instance.CurrentScene == "3Dungeon") ? GameManager.Instance.Player.CharacterStruct.GetAlreadyEquipment(equip) : false;
                    list.Add(string.Format("<color={0}>{1}</color>", GetColor(isDungeon), $"(({equip.DropGrade}){equip.Name.GetTableText()})"));
                }
            }
        }

        list.Add("");
        return list;
    }

    private static List<string> GetDescEquipmentDesc(Table_EquipmentItem _tableSkillEquipment)
    {
        var list = new List<string>();
        var desc = TableDataManager.Instance.GetTableText(_tableSkillEquipment.Desc);
        if (!string.IsNullOrEmpty(desc))
        {
            list.Add("");
            list.Add(desc);
        }
        return list;
    }

    public static List<string> GetDescSkill(Table_Skill _tableSkill)
    {
        var list = new List<string>();
        list.Add($"{_tableSkill.CodeName} : {_tableSkill.DropGrade}");
        list.Add($"SkillTarget : {_tableSkill.SkillTarget}");
        if (0 < _tableSkill.Range)
            list.Add($"Range : {_tableSkill.Range}");
        if (!string.IsNullOrEmpty(_tableSkill.AttackSkill))
            list.Add($"Attack : {_tableSkill.AttackSkill}");
        if (!string.IsNullOrEmpty(_tableSkill.HealSkill))
            list.Add($"Heal : {_tableSkill.HealSkill}");
        if (!string.IsNullOrEmpty(_tableSkill.BuffSkill))
            list.Add($"Buff : {_tableSkill.BuffSkill}");
        if (!string.IsNullOrEmpty(_tableSkill.SummonSkill))
            list.Add($"Summon : {_tableSkill.SummonSkill}");
        if (!string.IsNullOrEmpty(_tableSkill.PassiveSkill))
            list.Add($"Passive : {_tableSkill.PassiveSkill}");
        if (0 < _tableSkill.ConsumeMP)
            list.Add($"ConsumeMP : {_tableSkill.ConsumeMP}");
        if (0 < _tableSkill.CoolTime)
            list.Add($"CoolTime : {_tableSkill.CoolTime}");
        return list;
    }
}

public static class CustomClass
{
    public static SaveData Reset(this SaveData _data)
    {
        if (_data.MaterialInventory == null)
            _data.MaterialInventory = new Dictionary<string, int>();
        if (_data.EnemyKillCount == null)
            _data.EnemyKillCount = new Dictionary<string, int>();
        if (_data.ReciepeInventory == null)
            _data.ReciepeInventory = new Dictionary<string, bool>();

        if (_data.InstantDungeon != null)
        {
            if (_data.InstantDungeon.MaterialInventory == null)
                _data.InstantDungeon.MaterialInventory = new Dictionary<string, int>();
            if (_data.InstantDungeon.EnemyKillCount == null)
                _data.InstantDungeon.EnemyKillCount = new Dictionary<string, int>();

            if (_data.InstantDungeon.EquipInventory == null)
                _data.InstantDungeon.EquipInventory = new List<string>();

            if (_data.InstantDungeon.Character.EquipItemDic == null)
                _data.InstantDungeon.Character.EquipItemDic = new Dictionary<EquipmentPartEnum, Table_EquipmentItem>();
            if (_data.InstantDungeon.Character.BuffDic == null)
                _data.InstantDungeon.Character.BuffDic = new Dictionary<string, BuffStruct>();
            if (_data.InstantDungeon.Character.SkillDic == null)
                _data.InstantDungeon.Character.SkillDic = new Dictionary<string, SkillStruct>();
        }

        return _data;
    }
}