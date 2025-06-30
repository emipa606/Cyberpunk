using System.Reflection;
using HarmonyLib;
using Verse;

namespace Cyberpunk;

[StaticConstructorOnStartup]
internal class Main
{
    static Main()
    {
        new Harmony("net.MDarque.cyberpunk").PatchAll(Assembly.GetExecutingAssembly());
    }
}