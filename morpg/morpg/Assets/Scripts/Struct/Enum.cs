public enum EventKind
{
    None = -1,
    GameStart, // 게임이 시작되었다
    MonsterAppear, // 몬스터가 등장했다, 문이 닫힌다
    MonsterClear, // 몬스터가 전부 죽었다, 문이 열린다
    CreatedMap, // 맵을 생성한다
    UpdateStat, // 스탯을 갱신한다
    PlayShadow, // 쉐도우를 실행한다
    AddStat, // 스탯포인트가 추가되었다
    PlayTimeStart, // 플레이타임이 흐르기 시작한다
    PlayTimeStop,
    TurnEnd, // 턴이 경과하였다
    CreatedEquipmentItem, // 장비 아이템을 만들었다 == 제작 보유 여부가 바뀐다, 재료 개수가 바뀐다
    EquiporRelease, // 장비 아이템을 장착하거나 해제했다
    UpdateSkillList, // 230207 UsingSkill가 변경되었다
    AddBuffIcon, // 230228 버프 추가
    UpdateUI, // UI를 업데이트한다
    Max
}

public enum GameState
{
    None = -1,
    Ready, // 준비 중, 몬스터 전멸 후 대기 중
    Pause, // 일시 정지
    Play, // 플레이 중
    Max
}
public enum MessageBoxClick
{
    None = -1,
    Confirm,
    Cancel,
    Max
}

public enum SwitchEnum
{
    None = -1,
    Move,
    Attack,
    Max
}

public enum FloatingEnum
{
    None = -1,
    Miss,
    Damage,
    Heal,
    Mana,
    Buff,
    Debuff,
    Critical,
    Max
}

public enum StatEnum
{
    None = -1,
    Str = 0,
    Agi = 1,
    Vit = 2,
    Int = 3,
    Dex = 4,
    Luk = 5,
    Max
}

public enum CharacterTypeEnum
{
    None = -1,
    Player,
    Enemy,
    Boss,
    Summon,
    Max
}

public enum SkillTargetEnum
{
    None = -1,
    Ally, // 아군
    Enemy, // 적군
    Max
}

public enum SubSkillTargetEnum
{
    None = -1,
    Self, // 자기 자신
    Land, // 지형
    Max
}

public enum VisibleState
{
    None = -1,
    Visible, // 보이는 상태
    Invisible, // 안 보이는 상태
    Max
}

public enum ItemState
{
    None = -1,
    Material, // 재료 아이템
    Equipment, // 장비 아이템
    Use, // 소비 아이템
    Max
}

public enum EquipmentPartEnum
{
    None = -1,
    All, // 전체
    RightHand, // 오른손
    LeftHand, // 왼손
    Head, // 머리
    Body, // 몸
    Legs, // 다리
    Max
}

public enum EquipmentKindEnum
{
    None = -1,
    // Hand
    OneHandSword,
    OneHandDagger,
    TwoHandSword,
    OneHandShield,
    TwoHandShield,
    OneHandMace,
    TwoHandSpear,
    TwoHandBow,
    OneHandStaff,
    TwoHandStaff,
    // Head
    Hat,
    Helmet,
    // Body
    Cloth,
    Armor,
    // Legs
    Shoes,
    Boots,

    Max
}

public enum AbilityEnum
{
    None = -1,
    Atk = 0,
    Matk = 1,
    Hit = 2,
    Flee = 3,
    Def = 4,
    Mdef = 5,
    CriProb = 6,
    CriPower = 7,
    Max
}

public enum AttributeEnum
{
    None = -1,
    EachTurn, // 매 n턴마다 발동
    Miss, // 회피
    HitBefore, // 맞기 전
    HitAfter, // 맞은 후
    AttackTry, // 공격 시도
    Kill, // 공격 후 적군 사망
    Death, // 자신의 사망
    Revive, // 자신의 부활
    Eqiup, // 장착
    Release, // 해제
    Levelup, // 230307 레벨업
    Max
}

public enum BuffTypeEnum
{
    None = -1,
    Buff,
    Debuff,
    Max
}

public enum LogCategoryEnum
{
    None = -1,
    UI,
    Battle,
    Error,
    Etc,
    Data,
    PathFinder,
    All,
    Max
}

public enum IngameLogCategoryEnum
{
    None = -1,
    UI,
    Battle,
    All,
    Max
}

public enum DifficultyEnum
{
    None = -1,
    Easy,
    Normal,
    Hard,
    Nightmare,
    Hell,
    Chaos,
    Max
}

public enum DropGradeEnum
{
    None = -1,
    C,
    B,
    A,
    S,
    SS,
    Max
}

public enum EquipmentTypeEnum
{
    None = -1,
    Cloth, // 천
    Leather, // 가죽
    Metalic, // 금속
    Max
}

public enum AIJudgeSkillTargetStandardEnum // 230224 AI가 스킬의 대상을 정하는 기준
{
    None = -1,
    MinimumDistance, // 최소거리
    MaximumDistance, // 최대거리
    MinimumLevel, // 최소레벨
    MaximumLevel, // 최대레벨
    MinimumCurrentHP, // 최소현재체력
    MaximumCurrentHP, // 최대현재체력
    MinimumCurrentMP, // 최소현재마나
    MaximumCurrentMP, // 최대현재마나
    MinimumMaxHP, // 최소최대체력
    MaximumMaxHP, // 최대최대체력
    MinimumMaxMP, // 최소최대마나
    MaximumMaxMP, // 최대최대마나
    MinimumStr, // 최소Str
    MaximumStr, // 최대Str
    MinimumAgi, // 최소Agi
    MaximumAgi, // 최대Agi
    MinimumVit, // 최소Vit
    MaximumVit, // 최대Vit
    MinimumInt, // 최소Int
    MaximumInt, // 최대Int
    MinimumDex, // 최소Dex
    MaximumDex, // 최대Dex
    MinimumLuk, // 최소Luk
    MaximumLuk, // 최대Luk
    MinimumAtk, // 최소Atk
    MaximumAtk, // 최대Atk
    MinimumMatk, // 최소Matk
    MaximumMatk, // 최대Matk
    MinimumHit, // 최소Hit
    MaximumHit, // 최대Hit
    MinimumFlee, // 최소Flee
    MaximumFlee, // 최대Flee
    MinimumDef, // 최소Def
    MaximumDef, // 최대Def
    MinimumMdef, // 최소Mdef
    MaximumMdef, // 최대Mdef
    MinimumCriProb, // 최소CriProb
    MaximumCriProb, // 최대CriProb
    MinimumCriPower, // 최소CriPower
    MaximumCriPower, // 최대CriPower
    MinimumBuffCount, // 최소버프숫자
    MaximumBuffCount, // 최대버프숫자
    Max
}

public enum SpecialEffectTypeEnum
{
    None = -1,
    MeleeDamage, // 근접 대미지 증가 -
    MagicDamage, // 마법 대미지 증가 -
    DamageReduce, // 대미지 감소 -
    IncreaseRecovery, // 회복량 증가 -
    DrainHP, // 체력 흡수 -
    DrainMP, // 마나 흡수 -
    ConsumeMP, // 마나 소모량 감소 -
    Reflection, // 대미지 반사 -
    ImmuneMagic, // 마법 면역
    Maximize, // 마법 최대화 -
    Double, // 마법 이중화 -
    Triple, // 마법 삼중화 -
    Shield, // 쉴드
    ManaShield, // 마나쉴드
    MagicFixedDamage, // 마법 고정 피해 -
    WearedFall, // 230316 위아디폴
    PhoenixForm, // 230316 피닉스폼
    AdvancedNormal, // 230320 속성 강화
    AdvancedWater, // 230317 속성 강화
    AdvancedWind, // 230317 속성 강화
    AdvancedEarth, // 230317 속성 강화
    AdvancedFire, // 230317 속성 강화
    AdvancedPoison, // 230317 속성 강화
    AdvancedIce, // 230317 속성 강화
    AdvancedLightning, // 230317 속성 강화
    AdvancedLight, // 230317 속성 강화
    AdvancedDark, // 230317 속성 강화
    AdvancedAll, // 230317 속성 강화
    ImmuneNormal, // 230320 속성 내성
    ImmuneWater, // 230317 속성 내성
    ImmuneWind, // 230317 속성 내성
    ImmuneEarth, // 230317 속성 내성
    ImmuneFire, // 230317 속성 내성
    ImmunePoison, // 230317 속성 내성
    ImmuneIce, // 230317 속성 내성
    ImmuneLightning, // 230317 속성 내성
    ImmuneLight, // 230317 속성 내성
    ImmuneDark, // 230317 속성 내성
    ImmuneAll, // 230317 속성 내성
    Max
}

public enum CrowdControlTypeEnum
{
    None = -1,
    Stun, // 기절. 행동 불가
    Slience, // 침묵. 스킬 사용불가
    Deaf, // 귀먹어리. 음악 계열 버프/디버프 무시
    Paralysis, // 마비. 행동 불가
    Burn, // 화상. 지속 대미지
    Poison, // 중독. 지속 대미지
    Paton, // 페이튼. 극대 지속 대미지
    Max
}