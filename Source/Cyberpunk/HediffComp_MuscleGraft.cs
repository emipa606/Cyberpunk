using RimWorld;
using Verse;

namespace Cyberpunk;

public class HediffComp_MuscleGraft : HediffComp
{
    public override void CompPostMake()
    {
        base.CompPostMake();
        Patch.ChangeBodyType(Pawn, BodyTypeDefOf.Hulk);
    }
}