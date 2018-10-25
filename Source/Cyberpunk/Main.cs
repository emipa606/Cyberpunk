using Verse;
using Harmony;
using RimWorld;
using System.Linq;
using System.IO;
using UnityEngine;
using System.Collections.Generic;
using System;

namespace Cyberpunk
{
    [StaticConstructorOnStartup]
    class Main
    {
        public static HarmonyInstance harmony;

        static Main()
        {
            var id = "Cyberpunk";
            harmony = HarmonyInstance.Create(id);
            harmony.PatchAll(typeof(Main).Assembly);
        }

        static string GetHarmonyConfigFile()
        {
            var myModule = typeof(Main).Module;
            ModContentPack modInfo = LoadedModManager.RunningMods
                .FirstOrDefault(mcp => mcp.assemblies.loadedAssemblies
                    .ToList().SelectMany(a => a.GetLoadedModules()).Contains(myModule));
            if (modInfo == null) throw new InvalidDataException("Cannot find mod for module " + myModule);
            var sep = Path.DirectorySeparatorChar;
            return string.Concat(new object[] { modInfo.RootDir, sep, "About", sep, "Harmony.txt" });
        }
    }

    //[HarmonyPatch(typeof(FloatMenuMakerMap))]
    //[HarmonyPatch("AddHumanlikeOrders")]
    internal static class Patch
    {
        public static void ChangeBodyType(Pawn pawn, BodyTypeDef bt)
        {
            var storyTrv = Traverse.Create(pawn.story);
            var newStory = new Pawn_StoryTracker(pawn);
            var newStoryTrv = Traverse.Create(newStory);
            AccessTools.GetFieldNames(typeof(Pawn_StoryTracker))
                    .ForEach(f => newStoryTrv.Field(f).SetValue(storyTrv.Field(f).GetValue()));
            newStory.bodyType = bt;
            pawn.story = newStory;
            pawn.Drawer.renderer.graphics.ResolveAllGraphics();
        }

        //static void AddMenuOptions(Pawn pawn, List<FloatMenuOption> opts)
        //{
        //    opts.Add(new FloatMenuOption("Make Thin", delegate () { ChangeBodyType(pawn, BodyType.Thin); },
        //        MenuOptionPriority.Default, null, null, 0f, null, null));

        //    opts.Add(new FloatMenuOption("Make Hulk", delegate () { ChangeBodyType(pawn, BodyType.Hulk); },
        //        MenuOptionPriority.Default, null, null, 0f, null, null));
        //}

        //static void Postfix(ref Vector3 clickPos, ref Pawn pawn, ref List<FloatMenuOption> opts)
        //{
        //    AddMenuOptions(pawn, opts);
        //}
    }
}