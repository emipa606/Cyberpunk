using HarmonyLib;
using RimWorld;
using Verse;

namespace Cyberpunk;

internal static class Patch
{
    public static void ChangeBodyType(Pawn pawn, BodyTypeDef bt)
    {
        var storyTrv = Traverse.Create(pawn.story);
        var pawnStoryTracker = new Pawn_StoryTracker(pawn);
        var newStoryTrv = Traverse.Create(pawnStoryTracker);
        AccessTools.GetFieldNames(typeof(Pawn_StoryTracker)).ForEach(delegate(string f)
        {
            newStoryTrv.Field(f).SetValue(storyTrv.Field(f).GetValue());
        });
        pawnStoryTracker.bodyType = bt;
        pawn.story = pawnStoryTracker;
        pawn.Drawer.renderer.SetAllGraphicsDirty();
    }
}