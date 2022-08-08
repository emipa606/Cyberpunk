using Verse;

namespace Cyberpunk;

public class HediffCompProperties_ImplantHeal : HediffCompProperties
{
    public readonly int healIntervalTicks = 60;

    public HediffCompProperties_ImplantHeal()
    {
        compClass = typeof(HediffComp_ImplantHeal);
    }
}