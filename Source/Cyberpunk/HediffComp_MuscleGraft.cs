using Verse;

namespace Cyberpunk
{
    public class HediffComp_MuscleGraft : HediffComp
    {
        public override void CompPostMake()
        {
            //string msg = "HediffComp_MuscleGraft(CompPostMake)";

            //Log.Message(msg);
            base.CompPostMake();
            Patch.ChangeBodyType(base.Pawn, RimWorld.BodyType.Hulk);
        }

        public override void CompPostTick()
        {
            base.CompPostTick();
        }
    }
}
