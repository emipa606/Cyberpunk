using Mlie;
using UnityEngine;
using Verse;

namespace Cyberpunk;

[StaticConstructorOnStartup]
internal class CyberpunkMod : Mod
{
    /// <summary>
    ///     The instance of the settings to be read by the mod
    /// </summary>
    public static CyberpunkMod Instance;

    private static string currentVersion;

    /// <summary>
    ///     The private settings
    /// </summary>
    public readonly CyberpunkSettings Settings;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="content"></param>
    public CyberpunkMod(ModContentPack content) : base(content)
    {
        Instance = this;
        currentVersion =
            VersionFromManifest.GetVersionFromModMetaData(content.ModMetaData);
        Settings = GetSettings<CyberpunkSettings>();
    }


    /// <summary>
    ///     The title for the mod-settings
    /// </summary>
    /// <returns></returns>
    public override string SettingsCategory()
    {
        return "Cyberpunk";
    }

    /// <summary>
    ///     The settings-window
    ///     For more info: https://rimworldwiki.com/wiki/Modding_Tutorials/ModSettings
    /// </summary>
    /// <param name="rect"></param>
    public override void DoSettingsWindowContents(Rect rect)
    {
        var listingStandard = new Listing_Standard();
        listingStandard.Begin(rect);
        listingStandard.Gap();
        listingStandard.CheckboxLabeled("CyP.Durability".Translate(), ref Settings.Durability,
            "CyP.Durability.Tooltip".Translate());
        if (currentVersion != null)
        {
            listingStandard.Gap();
            GUI.contentColor = Color.gray;
            listingStandard.Label("CyP.ModVersion".Translate(currentVersion));
            GUI.contentColor = Color.white;
        }

        listingStandard.End();
    }
}