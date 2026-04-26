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

public enum SaveLoadEnum 
{
    None = -1,
    Save,
    Load,
    Max
}

public enum MenuEnum 
{
    None = -1,
    One,
    Two,
    Three,
    Option,
    Menu,
    Max
}

public enum TilemapEnum
{
    None = -1,
    Move,
    NotMove,
    Deco,
    Max
}

public enum GameStateEnum
{
    None = -1,
    Stop,
    Play,
    Max
}

public enum EncyclopediaEnum
{
    None = -1,
    Cocruwa,
    Term,
    History,
    Max
}

public enum CocruwaEnum
{
    None = -1,
    Renod,
    Tartaros,
    Redinin,
    Perth,
    Ehrlone,
    Gingarsion,
    GrenFomos,
    NarosaFomos,
    IreanSorin,
    BultonKajon,
    KaroukWingent,
    RoutonWinner,
    GaremenTooskaDin,
    Max
}

public enum HiddenEnum
{
    None = -1,
    Default,
    Hidden,
    Max
}

public enum SpeciesEnum
{
    None = -1,
    Human,
    Rodec,
    Toorka,
    Teemole,
    Selena,
    Badrak,
    Machine,
    Sandora,
    Kalapok,
    Unknown,
    Max
}

public enum JobEnum
{
    None = -1,
    notEmployed,
    Warrior,
    Archer,
    Mage,
    Priest,
    Assasin,
    Rogue,
    Summoner,
    Alchemist,
    Necromancer,
    Demon,
    Archmage,
    Max
}