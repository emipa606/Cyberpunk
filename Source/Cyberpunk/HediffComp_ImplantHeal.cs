using System.Linq;
using Verse;

namespace Cyberpunk;

public class HediffComp_ImplantHeal : HediffComp
{
    public int ticksSinceHeal;

    public HediffCompProperties_ImplantHeal Props => (HediffCompProperties_ImplantHeal)props;

    public override void CompExposeData()
    {
        Scribe_Values.Look(ref ticksSinceHeal, "ticksSinceHeal");
    }

    public override void CompPostTick(ref float severityAdjustment)
    {
        ticksSinceHeal++;
        if (ticksSinceHeal <= Props.healIntervalTicks || !Pawn.health.hediffSet.HasNaturallyHealingInjury())
        {
            return;
        }

        ticksSinceHeal = 0;
        var num = 8f;
        var hediff_Injury = (from hediff in Pawn.health.hediffSet.hediffs
            where hediff is Hediff_Injury && !hediff.IsPermanent()
            select hediff).RandomElement();
        hediff_Injury.Heal(num * Pawn.HealthScale * 0.01f);
    }
}