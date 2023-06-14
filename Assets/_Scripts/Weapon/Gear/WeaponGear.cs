
public class WeaponGear : Weapon
{
    public enum EGearType
    {
        Damage,
        Speed,
        AttackSpeed,
        Magnet,
        EXP,
    }

    public EGearType gearType;

    public override void Init(WeaponData data)
    {
        base.Init(data);

        updatePlayerStat(data.counts[0]);
    }
    public override void LevelUP(WeaponData data, int level)
    {
        updatePlayerStat(data.counts[level]);
    }

    private void updatePlayerStat(float value)
    {
        switch (gearType)
        {
            case EGearType.Damage:
                Player.Instance.Stat.damageRatio += value;
                break;
            case EGearType.Speed:
                Player.Instance.Stat.speedRatio += value;
                break;
            case EGearType.AttackSpeed:
                Player.Instance.Stat.attackSpeedRatio += value;
                break;
            case EGearType.Magnet:
                Player.Instance.Stat.magnetRatio += value;
                break;
            default:
                break;
        }
    }
}