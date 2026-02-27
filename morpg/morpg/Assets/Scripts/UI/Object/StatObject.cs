using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatObject : MonoBehaviour
{
//    [SerializeField] private Text Text;

//    public void Setting(string _text, string _base)
//    {
//        Text.text = string.Format("{0} : {1}", _text, _base);
//    }

//    public void Setting(StatEnum _stat, StatPage _statPage)
//    {
//        var text = GetStatText(_stat, _statPage.GetStat(_stat));
//        Text.text = text;
//    }

//    private string GetStatText(StatEnum _stat, int[] _stats)
//    {
//        var totalStat = _stats[0];
//        var baseStat = _stats[1];
//        var equipmentStat = _stats[2];
//        var PassiveStat = _stats[3];
//        var buffStat = _stats[4];
//        var textList = new List<string>();
//        textList.Add(string.Format("<color={0}>{1}</color>", Util.GetColor(_stat), _stat.ToString()));
//        textList.Add($" : {Util.GetComma(totalStat)} = ");
//        textList.Add($"{Util.GetComma(baseStat)}");
//        textList.Add($"<color=#323232>{GetAdd(equipmentStat)}</color>");
//        textList.Add($"<color=#323232>{GetAdd(PassiveStat)}</color>");
//        textList.Add($"<color=#323232>{GetAdd(buffStat)}</color>");
//        var text = string.Join("", textList);
//        return text;
//    }

//    private string GetAdd(int value)
//    {
//        if (value == 0)
//            return string.Empty;
//        return (0 < value) ? $" + {Util.GetComma(Mathf.Abs(value))}" : $" - {Util.GetComma(Mathf.Abs(value))}";
//    }

//    public void Setting(AbilityEnum _ability, StatPage _statPage)
//    {
//        var text = GetAbilityText(_ability, _statPage.GetAbility(_ability));
//        Text.text = text;
//    }

//    private string GetAbilityText(AbilityEnum _ability, int[] _abilitys)
//    {
//        var cri = string.Empty;
//        var criPower = 0;
//        if (_ability == AbilityEnum.CriProb || _ability == AbilityEnum.CriPower)
//            cri = "%";
//        if (_ability == AbilityEnum.CriPower)
//            criPower = 100;

//        var totalAbility = _abilitys[0] + criPower;
//        var baseAbility = _abilitys[1] + criPower;
//        var equipmentAbility = _abilitys[2];
//        var passiveAbility = _abilitys[3];
//        var buffAbility = _abilitys[4];
//        var speciesAbility = _abilitys[5];
//        var textList = new List<string>();
//        textList.Add(string.Format("<color={0}>{1}</color>", Util.GetColor(_ability), _ability.ToString()));
//        textList.Add($" : {Util.GetComma(totalAbility)}{cri} = ");
//        textList.Add($"({Util.GetComma(baseAbility)}");
//        textList.Add($"<color=#323232>{GetAdd(equipmentAbility)}</color>");
//        textList.Add($"<color=#323232>{GetAdd(passiveAbility)}</color>");
//        textList.Add($"<color=#323232>{GetAdd(buffAbility)}</color>");
//        textList.Add($") * {Util.GetComma(speciesAbility)}%");
//        var text = string.Join("", textList);
//        return text;
//    }
}
