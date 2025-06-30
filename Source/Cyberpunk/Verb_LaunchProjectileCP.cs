using RimWorld;
using Verse;

namespace Cyberpunk;

public class Verb_LaunchProjectileCP : Verb_LaunchProjectile
{
    protected virtual float reliable => EquipmentSource.GetStatValue(StatDefOf_CP.reliability);

    public VerbPropertiesCP verbPropsCP => verbProps as VerbPropertiesCP;

    protected override bool TryCastShot()
    {
        if (!CyberpunkMod.Instance.Settings.Durability)
        {
            return base.TryCastShot();
        }

        var thingDefGunCp = EquipmentSource as ThingDef_GunCP;
        StatPart_Reliability.GetReliability(thingDefGunCp, out _, out var jamsOn);
        var num = Rand.Range(0, 1000) / 10f;
        if (!(num < jamsOn))
        {
            return base.TryCastShot();
        }

        var text = $"{caster.LabelCap}'s {thingDefGunCp?.LabelCap} had a weapon jam. ({num}/{jamsOn})";
        Messages.Message(text, MessageTypeDefOf.SilentInput);
        if (thingDefGunCp != null)
        {
            thingDefGunCp.HitPoints--;
        }

        return false;
    }
}