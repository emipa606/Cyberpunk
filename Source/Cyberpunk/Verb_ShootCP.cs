using RimWorld;
using Verse;

namespace Cyberpunk;

public class Verb_ShootCP : Verb_LaunchProjectileCP
{
    protected override int ShotsPerBurst => verbProps.burstShotCount;

    public override void WarmupComplete()
    {
        base.WarmupComplete();
        if (!base.CasterIsPawn || base.CasterPawn.skills == null)
        {
            return;
        }

        var xp = 6f;
        if (currentTarget.Thing != null && currentTarget.Thing.def.category == ThingCategory.Pawn)
        {
            xp = !currentTarget.Thing.HostileTo(caster) ? 50f : 240f;
        }

        base.CasterPawn.skills.Learn(SkillDefOf.Shooting, xp);
    }

    protected override bool TryCastShot()
    {
        if (base.TryCastShot() && base.CasterIsPawn)
        {
            base.CasterPawn.records.Increment(RecordDefOf.ShotsFired);
        }

        return base.TryCastShot();
    }
}