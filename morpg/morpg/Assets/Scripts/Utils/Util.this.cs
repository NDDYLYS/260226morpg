using System.Collections.Generic;

public static partial class Util
{
    public static string GetTableText(this string _codename, params string[] param)
    {
        return TableDataManager.Instance.GetTableText(_codename, param);
    }

    public static string GetTableText(this string _codename)
    {
        return TableDataManager.Instance.GetTableText(_codename);
    }

    public static string GetToString(this EquipmentKindEnum _enum)
    {
        return $"UI_{_enum.ToString()}".GetTableText();
    }

    public static string GetToString(this EquipmentTypeEnum _enum)
    {
        return $"UI_{_enum.ToString()}".GetTableText();
    }

    public static string GetToString(this List<EquipmentPartEnum> _enumList, string _separator)
    {
        var list = new List<string>();
        foreach (var tempEnum in _enumList)
        {
            list.Add($"UI_{tempEnum.ToString()}".GetTableText());
        }
        return string.Join(_separator, list);
    }

    public static int GetSpecialEffectValue(this Character _useCharacter, SpecialEffectTypeEnum _specialEffectType, int _value = 0)
    {
        switch (_specialEffectType)
        {
            case SpecialEffectTypeEnum.MeleeDamage:
                var increaseMeleeDamage = _useCharacter.CharacterStruct.GetTotalSpecialEffect(SpecialEffectTypeEnum.MeleeDamage, _value);
                _value += increaseMeleeDamage;
                return _value;

            case SpecialEffectTypeEnum.MagicDamage:
                var increaseMagicDamage = _useCharacter.CharacterStruct.GetTotalSpecialEffect(SpecialEffectTypeEnum.MagicDamage, _value);
                _value += increaseMagicDamage;
                return _value;

            case SpecialEffectTypeEnum.DamageReduce:
                var damageReduce = _useCharacter.CharacterStruct.GetTotalSpecialEffect(SpecialEffectTypeEnum.DamageReduce, _value);
                _value -= damageReduce;
                return _value;

            case SpecialEffectTypeEnum.IncreaseRecovery:
                var recovery = _useCharacter.CharacterStruct.GetTotalSpecialEffect(SpecialEffectTypeEnum.IncreaseRecovery, _value);
                _value += recovery;
                return _value;

            case SpecialEffectTypeEnum.DrainHP:
                var drainHP = _useCharacter.CharacterStruct.GetTotalSpecialEffect(SpecialEffectTypeEnum.DrainHP, _value);
                return drainHP;

            case SpecialEffectTypeEnum.DrainMP:
                var drainMP = _useCharacter.CharacterStruct.GetTotalSpecialEffect(SpecialEffectTypeEnum.DrainMP, _value);
                return drainMP;

            case SpecialEffectTypeEnum.ConsumeMP:
                var decreaseMP = _useCharacter.CharacterStruct.GetTotalSpecialEffect(SpecialEffectTypeEnum.ConsumeMP, _value);
                _value -= decreaseMP;
                return _value;

            case SpecialEffectTypeEnum.Reflection:
                var reflection = _useCharacter.CharacterStruct.GetTotalSpecialEffect(SpecialEffectTypeEnum.Reflection, _value);
                return reflection;

            case SpecialEffectTypeEnum.ImmuneMagic:
                break;
            case SpecialEffectTypeEnum.Maximize:
                var value0 = _useCharacter.CharacterStruct.GetTotalSpecialEffect(SpecialEffectTypeEnum.Maximize);
                return value0;

            case SpecialEffectTypeEnum.Double:
                var value1 = _useCharacter.CharacterStruct.GetTotalSpecialEffect(SpecialEffectTypeEnum.Double);
                return value1;

            case SpecialEffectTypeEnum.Triple:
                var value2 = _useCharacter.CharacterStruct.GetTotalSpecialEffect(SpecialEffectTypeEnum.Triple);
                return value2;

            case SpecialEffectTypeEnum.Shield:
                break;
            case SpecialEffectTypeEnum.ManaShield:
                var manaShield = _useCharacter.CharacterStruct.GetTotalSpecialEffect(SpecialEffectTypeEnum.ManaShield);
                if (0 < manaShield)
                {
                    var damage = (int)(_value * 0.25f);
                    _useCharacter.ConsumeMP(damage);
                    if (damage <= _useCharacter.CharacterStruct.CurrentMP)
                    {
                        // 230313 마나가 더 크면 전체 흡수
                        _value = 0;
                    }
                    else
                    {
                        // 230313 마나가 부족하면 일부만 흡수
                        _value -= damage;
                    }
                }
                return _value;

            case SpecialEffectTypeEnum.MagicFixedDamage:
                var value3 = _useCharacter.CharacterStruct.GetTotalSpecialEffect(SpecialEffectTypeEnum.MagicFixedDamage);
                return value3;

            case SpecialEffectTypeEnum.AdvancedNormal:
            case SpecialEffectTypeEnum.AdvancedWater:
            case SpecialEffectTypeEnum.AdvancedWind:
            case SpecialEffectTypeEnum.AdvancedEarth:
            case SpecialEffectTypeEnum.AdvancedFire:
            case SpecialEffectTypeEnum.AdvancedPoison:
            case SpecialEffectTypeEnum.AdvancedIce:
            case SpecialEffectTypeEnum.AdvancedLightning:
            case SpecialEffectTypeEnum.AdvancedLight:
            case SpecialEffectTypeEnum.AdvancedDark:
            case SpecialEffectTypeEnum.AdvancedAll:
                var advancedElement = _useCharacter.CharacterStruct.GetTotalSpecialEffect(_specialEffectType, _value);
                return advancedElement;

            case SpecialEffectTypeEnum.ImmuneNormal:
            case SpecialEffectTypeEnum.ImmuneWater:
            case SpecialEffectTypeEnum.ImmuneWind:
            case SpecialEffectTypeEnum.ImmuneEarth:
            case SpecialEffectTypeEnum.ImmuneFire:
            case SpecialEffectTypeEnum.ImmunePoison:
            case SpecialEffectTypeEnum.ImmuneIce:
            case SpecialEffectTypeEnum.ImmuneLightning:
            case SpecialEffectTypeEnum.ImmuneLight:
            case SpecialEffectTypeEnum.ImmuneDark:
            case SpecialEffectTypeEnum.ImmuneAll:
                var immuneElement = _useCharacter.CharacterStruct.GetTotalSpecialEffect(_specialEffectType, _value);
                return immuneElement;
        }

        return 0;
    }

    public static int GetAdvancedElementSpecialEffectValue(this Character _useCharacter, ElementTypeEnum _elementType, int _value = 0)
    {
        var all = _useCharacter.GetSpecialEffectValue(SpecialEffectTypeEnum.AdvancedAll, _value);
        var element = 0;

        switch (_elementType)
        {
            case ElementTypeEnum.Normal:
                element = _useCharacter.GetSpecialEffectValue(SpecialEffectTypeEnum.AdvancedNormal, _value);
                break;

            case ElementTypeEnum.Water:
                element = _useCharacter.GetSpecialEffectValue(SpecialEffectTypeEnum.AdvancedWater, _value);
                break;

            case ElementTypeEnum.Wind:
                element = _useCharacter.GetSpecialEffectValue(SpecialEffectTypeEnum.AdvancedWind, _value);
                break;

            case ElementTypeEnum.Earth:
                element = _useCharacter.GetSpecialEffectValue(SpecialEffectTypeEnum.AdvancedEarth, _value);
                break;

            case ElementTypeEnum.Fire:
                element = _useCharacter.GetSpecialEffectValue(SpecialEffectTypeEnum.AdvancedFire, _value);
                break;

            case ElementTypeEnum.Poison:
                element = _useCharacter.GetSpecialEffectValue(SpecialEffectTypeEnum.AdvancedPoison, _value);
                break;

            case ElementTypeEnum.Ice:
                element = _useCharacter.GetSpecialEffectValue(SpecialEffectTypeEnum.AdvancedIce, _value);
                break;

            case ElementTypeEnum.Lightning:
                element = _useCharacter.GetSpecialEffectValue(SpecialEffectTypeEnum.AdvancedLightning, _value);
                break;

            case ElementTypeEnum.Light:
                element = _useCharacter.GetSpecialEffectValue(SpecialEffectTypeEnum.AdvancedLight, _value);
                break;

            case ElementTypeEnum.Dark:
                element = _useCharacter.GetSpecialEffectValue(SpecialEffectTypeEnum.AdvancedDark, _value);
                break;
        }

        return all + element;
    }

    public static int GetImmuneElementSpecialEffectValue(this Character _useCharacter, ElementTypeEnum _elementType, int _value = 0)
    {
        var all = _useCharacter.GetSpecialEffectValue(SpecialEffectTypeEnum.ImmuneAll, _value);
        var element = 0;

        switch (_elementType)
        {
            case ElementTypeEnum.Normal:
                element = _useCharacter.GetSpecialEffectValue(SpecialEffectTypeEnum.ImmuneNormal, _value);
                break;

            case ElementTypeEnum.Water:
                element = _useCharacter.GetSpecialEffectValue(SpecialEffectTypeEnum.ImmuneWater, _value);
                break;

            case ElementTypeEnum.Wind:
                element = _useCharacter.GetSpecialEffectValue(SpecialEffectTypeEnum.ImmuneWind, _value);
                break;

            case ElementTypeEnum.Earth:
                element = _useCharacter.GetSpecialEffectValue(SpecialEffectTypeEnum.ImmuneEarth, _value);
                break;

            case ElementTypeEnum.Fire:
                element = _useCharacter.GetSpecialEffectValue(SpecialEffectTypeEnum.ImmuneFire, _value);
                break;

            case ElementTypeEnum.Poison:
                element = _useCharacter.GetSpecialEffectValue(SpecialEffectTypeEnum.ImmunePoison, _value);
                break;

            case ElementTypeEnum.Ice:
                element = _useCharacter.GetSpecialEffectValue(SpecialEffectTypeEnum.ImmuneIce, _value);
                break;

            case ElementTypeEnum.Lightning:
                element = _useCharacter.GetSpecialEffectValue(SpecialEffectTypeEnum.ImmuneLightning, _value);
                break;

            case ElementTypeEnum.Light:
                element = _useCharacter.GetSpecialEffectValue(SpecialEffectTypeEnum.ImmuneLight, _value);
                break;

            case ElementTypeEnum.Dark:
                element = _useCharacter.GetSpecialEffectValue(SpecialEffectTypeEnum.ImmuneDark, _value);
                break;
        }

        return all + element;
    }
}
