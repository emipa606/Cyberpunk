using System.Linq;
using Verse;

namespace Cyberpunk;

public class HediffComp_ImplantHeal : HediffComp
{
    private int ticksSinceHeal;

    private HediffCompProperties_ImplantHeal Props => (HediffCompProperties_ImplantHeal)props;

    public override void CompExposeData()
    {
        Scribe_Values.Look(ref ticksSinceHeal, "ticksSinceHeal");
    }

    public override void CompPostTick(ref float severityAdjustment)
    {
        ticksSinceHeal++;
        if (ticksSinceHeal <= HediffCompProperties_ImplantHeal.HealIntervalTicks ||
            !Pawn.health.hediffSet.HasNaturallyHealingInjury())
        {
            return;
        }

        ticksSinceHeal = 0;
        const float num = 8f;
        var hediffInjury = (from hediff in Pawn.health.hediffSet.hediffs
            where hediff is Hediff_Injury && !hediff.IsPermanent()
            select hediff).RandomElement();
        hediffInjury.Heal(num * Pawn.HealthScale * 0.01f);
    }
}