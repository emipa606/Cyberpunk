using System.IO;
using System.Linq;
using HarmonyLib;
using Verse;

namespace Cyberpunk;

[StaticConstructorOnStartup]
internal class Main
{
    public static Harmony harmony;

    static Main()
    {
        harmony = new Harmony("net.MDarque.cyberpunk");
        harmony.PatchAll(typeof(Main).Assembly);
    }

    private static string GetHarmonyConfigFile()
    {
        var myModule = typeof(Main).Module;
        var modContentPack = LoadedModManager.RunningMods.FirstOrDefault(mcp =>
            mcp.assemblies.loadedAssemblies.ToList().SelectMany(a => a.GetLoadedModules()).Contains(myModule));
        if (modContentPack == null)
        {
            throw new InvalidDataException($"Cannot find mod for module {myModule}");
        }

        var directorySeparatorChar = Path.DirectorySeparatorChar;
        return $"{modContentPack.RootDir}{directorySeparatorChar}About{directorySeparatorChar}Harmony.txt";
    }
}