using HarmonyLib;
using RimWorld;
using Verse;

namespace Cyberpunk;

internal static class Patch
{
    public static void ChangeBodyType(Pawn pawn, BodyTypeDef bt)
    {
        var storyTrv = Traverse.Create(pawn.story);
        var pawn_StoryTracker = new Pawn_StoryTracker(pawn);
        var newStoryTrv = Traverse.Create(pawn_StoryTracker);
        AccessTools.GetFieldNames(typeof(Pawn_StoryTracker)).ForEach(delegate(string f)
        {
            newStoryTrv.Field(f).SetValue(storyTrv.Field(f).GetValue());
        });
        pawn_StoryTracker.bodyType = bt;
        pawn.story = pawn_StoryTracker;
        pawn.Drawer.renderer.SetAllGraphicsDirty();
    }
}