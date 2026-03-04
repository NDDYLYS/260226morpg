using Unity.Services.Analytics;

public class AttackEvent : Event
{
    public AttackEvent(int damage, string weapon, bool critical)
        : base("attack")
    {
        SetParameter("damage", damage);
        SetParameter("weapon", weapon);
        SetParameter("critical", critical);
    }
}