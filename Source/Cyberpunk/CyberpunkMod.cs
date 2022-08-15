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
    public static CyberpunkMod instance;

    private static string currentVersion;

    /// <summary>
    ///     The private settings
    /// </summary>
    private CyberpunkSettings settings;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="content"></param>
    public CyberpunkMod(ModContentPack content) : base(content)
    {
        instance = this;
        currentVersion =
            VersionFromManifest.GetVersionFromModMetaData(ModLister.GetActiveModWithIdentifier("Mlie.Cyberpunk"));
    }

    /// <summary>
    ///     The instance-settings for the mod
    /// </summary>
    internal CyberpunkSettings Settings
    {
        get
        {
            if (settings == null)
            {
                settings = GetSettings<CyberpunkSettings>();
            }

            return settings;
        }
        set => settings = value;
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
        var listing_Standard = new Listing_Standard();
        listing_Standard.Begin(rect);
        listing_Standard.Gap();
        listing_Standard.CheckboxLabeled("CyP.Durability".Translate(), ref Settings.Durability,
            "CyP.Durability.Tooltip".Translate());
        if (currentVersion != null)
        {
            listing_Standard.Gap();
            GUI.contentColor = Color.gray;
            listing_Standard.Label("CyP.ModVersion".Translate(currentVersion));
            GUI.contentColor = Color.white;
        }

        listing_Standard.End();
    }
}