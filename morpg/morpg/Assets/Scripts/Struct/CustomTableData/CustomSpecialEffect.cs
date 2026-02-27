//[System.Serializable]
//public class CustomSpecialEffect
//{
//    public SpecialEffectTypeEnum SpecialEffectType;
//    public CustomBasePer Values;
    
//    public CustomSpecialEffect(string value)
//    {
//        if (string.IsNullOrEmpty(value))
//        {
//            SpecialEffectType = SpecialEffectTypeEnum.None;
//            Values = new CustomBasePer(null);
//            return;
//        }

//        var texts = value.Split('/');

//        SpecialEffectType = Util.GetEnumType<SpecialEffectTypeEnum>(texts[0]);
//        Values = new CustomBasePer(texts[1]);
//    }
//}