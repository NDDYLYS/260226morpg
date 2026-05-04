using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using System.Text;

public partial class TableDataManager : SingletonGameObject<TableDataManager>
{
    //[Button]
    //public List<Table_Encyclopedia> GetEncyclopedia(EncyclopediaEnum _enum, CocruwaEnum _cocruwa = CocruwaEnum.None)
    //{
    //    var all = GetTableDataList<Table_Encyclopedia>();
    //    List<Table_Encyclopedia> list = null;
    //    if (_enum != EncyclopediaEnum.Cocruwa)
    //        list = all.Where(x => x.Encyclopedia == _enum).ToList();
    //    else 
    //        list = all.Where(x => x.Encyclopedia == EncyclopediaEnum.Cocruwa).ToList().Where(x => x.Cocruwa == _cocruwa).ToList();

    //    var texts = new StringBuilder();
    //    foreach (var data in list)
    //    {
    //        texts.Append($"{data.Encyclopedia}-{data.CodeName}");
    //        texts.Append("\n");
    //    }

    //    //Debug.Log(texts);

    //    return list;
    //}
}