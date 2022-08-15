using Verse;

namespace Cyberpunk;

/// <summary>
///     Definition of the settings for the mod
/// </summary>
internal class CyberpunkSettings : ModSettings
{
    public bool Durability = true;

    /// <summary>
    ///     Saving and loading the values
    /// </summary>
    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref Durability, "Durability", true);
    }
}