using Verse;

namespace Cyberpunk
{
    public class HediffComp_MuscleGraft : HediffComp
    {
        public override void CompPostMake()
        {
            string msg = "(CompPostMake) Body Size Factor " + Pawn.ageTracker.CurLifeStage.bodySizeFactor + "->";
            base.CompPostMake();
            
            msg = msg + Pawn.ageTracker.CurLifeStage.bodySizeFactor;
            if (Pawn.gender == Gender.Female)
                Pawn.story.adulthood.bodyTypeMale = RimWorld.BodyType.Hulk;
            else
                Pawn.story.adulthood.bodyTypeMale = RimWorld.BodyType.Hulk;
            Log.Message(msg);
        }

        public override void CompPostTick()
        {
            base.CompPostTick();
        }
    }
}
