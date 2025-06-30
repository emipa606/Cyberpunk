using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace Cyberpunk;

public class BulletCP : Bullet
{
    protected override void Impact(Thing hitThing, bool blockedByShield = false)
    {
        var map = Map;
        base.Impact(hitThing, blockedByShield);
        var battleLogEntryRangedImpact = new BattleLogEntry_RangedImpact(launcher, hitThing, intendedTarget.Thing,
            equipmentDef, def, targetCoverDef);
        Find.BattleLog.Add(battleLogEntryRangedImpact);
        if (hitThing != null)
        {
            var damageDef = def.projectile.damageDef;
            float max = DamageAmount;
            var num = Rand.Range(0f, max);
            var armorPenetration = ArmorPenetration;
            var y = ExactRotation.eulerAngles.y;
            var thing = launcher;
            var thingDef = equipmentDef;
            hitThing.TakeDamage(new DamageInfo(damageDef, num, armorPenetration, y, thing, null, thingDef,
                    DamageInfo.SourceCategory.ThingOrUnknown, intendedTarget.Thing))
                .AssociateWithLog(battleLogEntryRangedImpact);
            if (hitThing is Pawn { stances: not null } pawn && pawn.BodySize <= def.projectile.stoppingPower + 0.001f)
            {
                pawn.stances.stagger.StaggerFor(95);
            }

            return;
        }

        SoundDefOf.BulletImpact_Ground.PlayOneShot(new TargetInfo(Position, map));
        FleckMaker.Static(ExactPosition, map, FleckDefOf.ShotHit_Dirt);
        if (Position.GetTerrain(map).takeSplashes)
        {
            FleckMaker.WaterSplash(ExactPosition, map, Mathf.Sqrt(DamageAmount) * 1f, 4f);
        }
    }
}