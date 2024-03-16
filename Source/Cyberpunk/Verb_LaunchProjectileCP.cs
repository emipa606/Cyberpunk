using RimWorld;
using Verse;

namespace Cyberpunk;

public class Verb_LaunchProjectileCP : Verb_LaunchProjectile
{
    protected virtual float reliable => EquipmentSource.GetStatValue(StatDefOf_CP.reliability);

    public VerbPropertiesCP verbPropsCP => verbProps as VerbPropertiesCP;

    protected override bool TryCastShot()
    {
        if (!CyberpunkMod.instance.Settings.Durability)
        {
            return base.TryCastShot();
        }

        var thingDef_GunCP = EquipmentSource as ThingDef_GunCP;
        StatPart_Reliability.GetReliability(thingDef_GunCP, out _, out var jamsOn);
        var num = Rand.Range(0, 1000) / 10f;
        if (!(num < jamsOn))
        {
            return base.TryCastShot();
        }

        var text = $"{caster.LabelCap}'s {thingDef_GunCP?.LabelCap} had a weapon jam. ({num}/{jamsOn})";
        Messages.Message(text, MessageTypeDefOf.SilentInput);
        if (thingDef_GunCP != null)
        {
            thingDef_GunCP.HitPoints--;
        }

        return false;
    }
}